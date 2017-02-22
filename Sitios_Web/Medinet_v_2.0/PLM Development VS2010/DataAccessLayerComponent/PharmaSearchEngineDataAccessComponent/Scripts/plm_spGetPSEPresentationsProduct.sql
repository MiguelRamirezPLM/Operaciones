IF OBJECT_ID('dbo.plm_spGetPSEPresentationsProduct') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetPSEPresentationsProduct
go

/* 
	Author:			Angel Eduardo Hernández Aguilar			 
	Object:			dbo.plm_spGetPSEPresentationsProduct
	
	Description:	Gets a product from the database verifing if it has content.

	Company:		PLM Latina.

	DECLARE @retValue int

	EXECUTE @retValue = dbo.[plm_spGetPSEPresentationsProduct] 
			@editionId = 55, 
			@divisionId = 186, 
			@categoryId = 101, 
			@productId = 6083, 
			@pharmaFormId = 146

	SELECT @retValue

 */ 
 --DROP PROCEDURE [plm_spGetPSEPresentationsProduct]
 CREATE PROCEDURE [dbo].[plm_spGetPSEPresentationsProduct]

	@editionId		int,
	@divisionId		int,
	@categoryId		int,
	@productId		int,
	@pharmaFormId	int
AS
BEGIN		
	
	SELECT DISTINCT	P.PresentationId,
					P.QtyExternalPack,
					P.ExternalPackId, 
					EP.ExternalPackName,
					P.QtyInternalPack,
					P.InternalPackId,
					IP.InternalPackName,
					P.QtyContentUnit,
					P.ContentUnitId,
					CU.UnitName,
					P.QtyWeightUnit,
					P.WeightUnitId,
					WU.ShortName
					
	FROM plm_vwProductsByEdition V
	INNER JOIN Presentations P ON V.CategoryId = P.CategoryId AND
								  V.DivisionId = P.DivisionId AND
								  V.PharmaFormId = P.PharmaFormId AND
								  V.ProductId = P.ProductId
	LEFT JOIN InternalPacks IP ON P.InternalPackId = IP.InternalPackId
	LEFT JOIN ExternalPacks EP ON P.ExternalPackId = EP.ExternalPackId
	LEFT JOIN ContentUnits CU ON P.ContentUnitId = CU.ContentUnitId
	LEFT JOIN WeightUnits WU ON P.WeightUnitId = WU.WeightUnitId
	WHERE	V.EditionId	= @editionId AND
			V.Divisionid = @divisionId AND
			V.CategoryId = @categoryId AND
			V.ProductId = @productId AND
			V.PharmaFormId = @pharmaFormId
			
	ORDER BY 1
			
	RETURN @@ROWCOUNT

END							 