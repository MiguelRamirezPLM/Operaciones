IF OBJECT_ID('dbo.plm_spGetPSEOtherInteractions') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetPSEOtherInteractions
go

/* 
	Author:			Ramiro Sánchez.				 
	Object:			dbo.plm_spGetPSEOtherInteractions
	
	Description:	Gets a product from the database verifing if it has content.

	Company:		PLM Latina.

	DECLARE @retValue int

	EXECUTE @retValue = dbo.[plm_spGetPSEOtherInteractions] 
			@editionId = 32, 
			@divisionId = 12, 
			@categoryId = 101, 
			@productId = 11014 , 
			@pharmaFormId = 162

	SELECT @retValue

 */ 

CREATE PROCEDURE [dbo].[plm_spGetPSEOtherInteractions]
	@editionId		int,
	@divisionId		int,
	@categoryId		int,
	@productId		int,
	@pharmaFormId	int
AS
BEGIN

	-- The parameters shouldn't be null:
	IF (@editionId IS NULL OR @divisionId IS NULL OR @categoryId IS NULL OR @productId IS NULL OR @pharmaFormId IS NULL)
	BEGIN
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
	END

	SELECT DISTINCT
		ote.ElementId,
		ote.ElementName,
		ote.Active
	FROM dbo.ProductContents p
	Inner Join ProductContentSubstances pc On(p.ProductContentId = pc.ProductContentId)
	Inner Join OtherInteractions oi ON (pc.ProductContentId = oi.ProductContentId And 
												pc.ProductId = oi.ProductId And 
													pc.ActiveSubstanceId = oi.ActiveSubstanceId)
	Inner Join OtherElements ote On (oi.ElementId = ote.ElementId)

	WHERE p.EditionId		= @editionId And
		  p.Divisionid		= @divisionId And
		  p.CategoryId		= @categoryId And
		  p.ProductId		= @productId And
		  p.PharmaFormId	= @pharmaFormId

	ORDER BY 2

	RETURN @@ROWCOUNT

END
