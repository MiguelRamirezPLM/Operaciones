IF OBJECT_ID('dbo.plm_spGetProductsByDivision') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetProductsByDivision
go

/* 
	Author:			Ramiro Sánchez
	Object:			dbo.plm_spGetProductsByDivision
	
	Description:	Gets all the editable products from database by division.

	Company:		PLM Latina.

	EXECUTE dbo.plm_spGetProductsByDivision @divisionId = 3, @countryId = 11, @bookId = 1

 */ 

CREATE PROCEDURE dbo.plm_spGetProductsByDivision
(
	@divisionId		int,
	@countryId		int,
	@bookId			int
)
AS
BEGIN

	Select Distinct p.ProductId,
					p.CountryId,
					p.LaboratoryId,
					p.AlphabetId,
					p.Brand,
					p.SanitaryRegistry,
					p.[Description],
					p.Active
	
	FROM (Select *
		  From plm_vwProductsByEdition 
		  Where DivisionId = @divisionId) v
			--And BookId = @bookId) v

	Inner Join Products p On v.ProductId = p.ProductId
		And v.LaboratoryId = p.LaboratoryId
		And v.CountryId = p.CountryId
	
	Where v.CountryId = @countryId And
		  v.DivisionId = @divisionId And
		  --v.BookId = @bookId And
		  p.Active = 1
		  
    Order By 5

END
go