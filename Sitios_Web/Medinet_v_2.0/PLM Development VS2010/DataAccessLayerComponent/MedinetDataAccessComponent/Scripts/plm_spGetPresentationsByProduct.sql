IF OBJECT_ID('dbo.plm_spGetPresentationsByProduct') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetPresentationsByProduct
go

/* 
	Author:			Ramiro Sánchez
	Object:			dbo.plm_spGetPresentationsByProduct
	
	Description:	Gets all presentations by product.
	Company:		PLM Latina.

	EXECUTE dbo.plm_spGetPresentationsByProduct @editionId = 32, @divisionId = 3, @categoryId = 103, @productId = 6660, @pharmaFormId = 210
	EXECUTE dbo.plm_spGetPresentationsByProduct @editionId = 55, @divisionId = 3, @categoryId = 103, @productId = 6660, @pharmaFormId = 210

 */ 

CREATE PROCEDURE dbo.plm_spGetPresentationsByProduct
(
	@editionId		int,
	@divisionId		int,
	@categoryId		int,
	@productId		int,
	@pharmaFormId	int
)
AS
BEGIN

	Select Distinct	p.PresentationId
			,p.DivisionId
			,d.[Description] As DivisionName
			,d.ShortName As DivisionShortName
			,p.CategoryId
			,c.[Description] As CategoryName
			,p.ProductId
			,pr.Brand
			,p.PharmaFormId
			,pf.[Description] As PharmaForm
			,p.QtyExternalPack
			,p.ExternalPackId
			,ep.ExternalPackName
			,p.QtyInternalPack
			,p.InternalPackId
			,ip.InternalPackName
			,p.QtyContentUnit
			,p.ContentUnitId
			,cu.UnitName As ContentUnitName
			,p.QtyWeightUnit
			,p.WeightUnitId
			,wu.UnitName As WeightUnitName
			,p.Presentation
			,p.Active As PresentationActive
	From Presentations p
		Inner Join ProductCategories pc On p.DivisionId = pc.DivisionId
			And p.CategoryId = pc.CategoryId
			And p.ProductId = pc.ProductId
			And p.PharmaFormId = pc.PharmaFormId
		Inner Join Products pr On pc.ProductId = pr.ProductId
		Inner Join PharmaceuticalForms pf On pc.PharmaFormId = pf.PharmaFormId
		Inner Join Categories c On pc.CategoryId = c.CategoryId
		Inner Join Divisions d On pc.DivisionId = d.DivisionId
		Left Join ExternalPacks ep On p.ExternalPackId = ep.ExternalPackId
		Left Join InternalPacks ip On p.InternalPackId = ip.InternalPackId
		Left Join ContentUnits cu On p.ContentUnitId = cu.ContentUnitId
		Left Join WeightUnits wu On p.WeightUnitId = wu.WeightUnitId
	Where p.PresentationId Not In (	Select PresentationId
									From EditionPresentations
									Where EditionId = @editionId)
		And p.DivisionId = @divisionId
		And p.CategoryId = @categoryId
		And p.ProductId = @productId
		And p.PharmaFormId = @pharmaFormId
		And p.Active = 1
		And pr.Active = 1
		And pf.Active = 1
		And c.Active = 1
		And d.Active = 1
	Order By pr.Brand

END
go