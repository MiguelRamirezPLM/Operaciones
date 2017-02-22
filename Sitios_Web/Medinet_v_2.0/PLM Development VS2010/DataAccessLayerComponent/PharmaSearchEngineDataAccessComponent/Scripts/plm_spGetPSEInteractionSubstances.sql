IF OBJECT_ID('dbo.plm_spGetPSEInteractionSubstances') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetPSEInteractionSubstances
go

/* 
	Author:			Ramiro Sánchez.				 
	Object:			dbo.plm_spGetPSEInteractionSubstances
	
	Description:	Gets a product from the database verifing if it has content.

	Company:		PLM Latina.

	DECLARE @retValue int

	EXECUTE @retValue = dbo.[plm_spGetPSEInteractionSubstances] 
			@editionId = 32, 
			@divisionId = 50, 
			@categoryId = 101, 
			@productId = 7093 , 
			@pharmaFormId = 14

	SELECT @retValue

 */ 

CREATE PROCEDURE [dbo].[plm_spGetPSEInteractionSubstances]

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
	 
		acs.ActiveSubstanceId,
		acs.[Description],
		acs.Enunciative,
		acs.Active

	FROM dbo.ProductContents p
	Inner Join ProductContentSubstances pc On(p.ProductContentId = pc.ProductContentId)
	Inner Join ProductInteractions pin ON (pc.ProductContentId = pin.ProductContentId And 
												pc.ProductId = pin.ProductId And 
													pc.ActiveSubstanceId = pin.ActiveSubstanceId)
	Inner Join ActiveSubstances acs On (pin.SubstanceInteractId = acs.ActiveSubstanceId)

	WHERE p.EditionId		= @editionId And
		  p.Divisionid		= @divisionId And
		  p.CategoryId		= @categoryId And
		  p.ProductId		= @productId And
		  p.PharmaFormId	= @pharmaFormId

	ORDER BY 2

	RETURN @@ROWCOUNT

END
