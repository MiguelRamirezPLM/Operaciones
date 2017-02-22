IF OBJECT_ID('dbo.plm_spGetProductsByEditionByATC') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetProductsByEditionByATC
go

/* 
	Author:			Ramiro Sánchez
	Object:			dbo.plm_spGetProductsByEditionByATC
	
	Description:	
	Company:		PLM Latina.

	EXECUTE dbo.plm_spGetProductsByEditionByATC @parentEditionId = 55, @editionId = 79, @therapeuticId = 1255
	EXECUTE dbo.plm_spGetProductsByEditionByATC @parentEditionId = 80, @editionId = 81, @therapeuticId = 1255

 */ 

CREATE PROCEDURE dbo.plm_spGetProductsByEditionByATC
(
	@parentEditionId	int,
	@editionId			int,
	@therapeuticId		int
)
AS
BEGIN

	Select Distinct v.DivisionId, v.DivisionName, v.DivisionShortName, v.CategoryId,v.CategoryName,
		v.ProductId, v.Brand, v.PharmaFormId, v.PharmaForm
	From plm_vwProductsByEdition v
	Left Join (Select DivisionId, CategoryId, ProductId, PharmaFormId
				From plm_vwProductsByEdition
				Where EditionId = @editionId) t On v.DivisionId = t.DivisionId
												And v.CategoryId = t.CategoryId
												And v.ProductId = t.ProductId
												And v.PharmaFormId = t.PharmaFormId
	Inner Join ProductTherapeutics pt On v.ProductId = pt.ProductId
		And v.PharmaFormId = pt.PharmaFormId
	Where v.EditionId = @parentEditionId 
		And t.DivisionId Is Null
		And pt.TherapeuticId = @therapeuticId
		And v.TypeInEdition='P'
		--And t.EditionId Is Null
	Order by v.brand
	
	RETURN @@ROWCOUNT

END
go