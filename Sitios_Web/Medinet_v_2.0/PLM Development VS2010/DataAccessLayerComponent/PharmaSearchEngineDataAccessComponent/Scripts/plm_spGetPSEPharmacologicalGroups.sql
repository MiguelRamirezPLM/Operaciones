IF OBJECT_ID('dbo.plm_spGetPSEPharmacologicalGroups') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetPSEPharmacologicalGroups
go

/* 
	Author:			Ramiro Sánchez.				 
	Object:			dbo.plm_spGetPSEPharmacologicalGroups
	
	Description:	Gets a product from the database verifing if it has content.

	Company:		PLM Latina.

	DECLARE @retValue int

	EXECUTE @retValue = dbo.[plm_spGetPSEPharmacologicalGroups] 
			@editionId = 32, 
			@divisionId = 86, 
			@categoryId = 101, 
			@productId = 9553 , 
			@pharmaFormId = 210

	SELECT @retValue

 */ 

CREATE PROCEDURE [dbo].[plm_spGetPSEPharmacologicalGroups]

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
		pg.PharmaGroupId,
		pg.GroupName,
		pg.Active
	FROM dbo.ProductContents p
	Inner Join ProductContentSubstances pc On(p.ProductContentId = pc.ProductContentId)
	Inner Join PharmaGroupInteractions pgi ON (pc.ProductContentId = pgi.ProductContentId And 
												pc.ProductId = pgi.ProductId And 
													pc.ActiveSubstanceId = pgi.ActiveSubstanceId)
	Inner Join PharmacologicalGroups pg On (pgi.PharmaGroupId = pg.PharmaGroupId)

	WHERE p.EditionId		= @editionId And
		  p.Divisionid		= @divisionId And
		  p.CategoryId		= @categoryId And
		  p.ProductId		= @productId And
		  p.PharmaFormId	= @pharmaFormId

	ORDER BY 2

	RETURN @@ROWCOUNT

END
