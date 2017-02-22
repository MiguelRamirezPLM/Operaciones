IF OBJECT_ID('dbo.plm_spGetParticipantProductsByEdition') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetParticipantProductsByEdition
go

/* 
	Author:			Ramiro Sánchez
	Object:			dbo.plm_spGetParticipantProductsByEdition
	
	Description:	Gets all the editable products from database by division.

	Company:		PLM Latina.

	EXECUTE dbo.plm_spGetParticipantProductsByEdition @countryId = 11, @bookId = 1, @editionId = 32
	EXECUTE dbo.plm_spGetParticipantProductsByEdition @countryId = 11, @bookId = 1, @editionId = 55
	EXECUTE dbo.plm_spGetParticipantProductsByEdition @countryId = 11, @bookId = 1, @editionId = 80
	EXECUTE dbo.plm_spGetParticipantProductsByEdition @countryId = 11, @bookId = 1, @editionId = 80, @brand='a%'

 */ 

CREATE PROCEDURE dbo.plm_spGetParticipantProductsByEdition
(
	@countryId		int,
	@bookId			int,
	@editionId		int,
	@brand			varchar(100) = Null
)
AS
BEGIN

	Select Distinct v.ProductId,
					v.Brand,
					v.ProductDescription,
					v.ProductActive,
					
					v.PharmaFormId,
					v.PharmaForm,
					v.PharmaActive,
					
					v.DivisionId,
					v.DivisionName,
					v.DivisionShortName,
										
					v.CategoryId,
					v.CategoryName,
										
					pc.AdDescription,
				
					CASE WHEN pp.EditionId Is Not Null Then 1 Else 0 End as Participant,
					CASE WHEN mp.EditionId Is Not Null Then 1 Else 0 End as Mentionated,
					CASE WHEN np.EditionId Is Not Null Then 1 Else 0 End as NewProduct,
					CASE WHEN pp.ModifiedContent Is Null Then 0 Else pp.ModifiedContent End as ModifiedContent,

					dbo.plm_dfGetEditionsByProduct(v.ProductId,v.PharmaFormId,v.DivisionId,v.CategoryId,@editionId,@bookId) as Editions,

					CASE WHEN eps.EditionId Is Not Null Then 1 Else 0 End as Sidef,
					
					ffp.FamilyId As ContentFamilyId,
					ffp.FamilyString As ContentFamilyString,

					ffps.FamilyId As PSFamilyId,
					ffps.FamilyString As PSFamilyString
					
	
	FROM (Select *
		  From plm_vwProductsByEdition 
		  Where BookId = @bookId
			And EditionId = @editionId
			And TypeInEdition = 'P') v

	Left Join (Select *
		  From plm_vwProductsByCountry) pc
										On v.DivisionId = pc.DivisionId And
											  v.CategoryId = pc.CategoryId And
											  v.ProductId = pc.ProductId And
											  v.PharmaFormId = pc.PharmaFormId

	Left Join (Select EditionId,DivisionId,CategoryId,
					  ProductId,PharmaFormId,ModifiedContent
			   From ParticipantProducts) pp
											   On v.EditionId = pp.EditionId And
												  v.DivisionId = pp.DivisionId And
												  v.CategoryId = pp.CategoryId And
												  v.ProductId = pp.ProductId And
												  v.PharmaFormId = pp.PharmaFormId
												  
	Left Join (Select EditionId,DivisionId,CategoryId,
					  ProductId,PharmaFormId
			   From MentionatedProducts) mp
											   On v.EditionId = mp.EditionId And
												  v.DivisionId = mp.DivisionId And
												  v.CategoryId = mp.CategoryId And
												  v.ProductId = mp.ProductId And
												  v.PharmaFormId = mp.PharmaFormId

	Left Join (Select EditionId,DivisionId,CategoryId,
					  ProductId,PharmaFormId
			   From NewProducts) np
											   On v.EditionId = np.EditionId And
												  v.DivisionId = np.DivisionId And
												  v.CategoryId = np.CategoryId And
												  v.ProductId = np.ProductId And
												  v.PharmaFormId = np.PharmaFormId

	Left Join (Select * From EditionProductShots ) eps
											   On v.EditionId = eps.EditionId And
												  v.DivisionId = eps.DivisionId And
												   v.CategoryId = eps.CategoryId And
												   v.ProductId = eps.ProductId And
												   v.PharmaFormId = eps.PharmaFormId

	Left Join (Select distinct f.FamilyId, f.FamilyString, fp.DivisionId, fp.CategoryId, fp.ProductId, fp.PharmaFormId
				From Families f
				Inner Join FamilyProducts fp On f.FamilyId = fp.FamilyId
				Where fp.EditionId = @editionId) ffp
												On v.DivisionId = ffp.DivisionId And
												   v.CategoryId = ffp.CategoryId And
												   v.ProductId = ffp.ProductId And
												   v.PharmaFormId = ffp.PharmaFormId

	Left Join (Select distinct ff.FamilyId, ff.FamilyString, ep.DivisionId, ep.CategoryId, ep.ProductId, ep.PharmaFormId
				From Families ff
				Inner Join FamilyProductShots fps On ff.FamilyId = fps.FamilyId
				Inner Join EditionProductShots ep On fps.EditionProductShotId = ep.EditionProductShotId
				Where ep.EditionId = @editionId) ffps
												On v.DivisionId = ffps.DivisionId And
												   v.CategoryId = ffps.CategoryId And
												   v.ProductId = ffps.ProductId And
												   v.PharmaFormId = ffps.PharmaFormId

	Where v.CountryId = @countryId And
		  v.BookId = @bookId And
		  v.EditionId = @editionId And
		  v.Brand COLLATE SQL_Latin1_General_CP1_CI_AI LIKE CASE WHEN @brand IS NOT NULL THEN @brand
										ELSE v.Brand END
		  
    Order By 2,6,8

END
go