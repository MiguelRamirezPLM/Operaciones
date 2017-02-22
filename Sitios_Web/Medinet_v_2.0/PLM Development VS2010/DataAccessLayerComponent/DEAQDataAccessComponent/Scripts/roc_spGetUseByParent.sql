IF OBJECT_ID('dbo.roc_spGetUseByParent') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetUseByParent
go

/* 
	Author:			Daniel Campos / Ramiro Sánchez				 
	Object:			dbo.roc_spGetUseByParent
	
	Description:	Retrieves all AgrochemicalUses by Edition and ParentId.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetUseByParent @editionId = 4, @parentId = 140

 */ 

CREATE PROCEDURE dbo.roc_spGetUseByParent
(
	@editionId			int,
	@parentId			int
)
AS
BEGIN

	-- The parameters shouldn't be null:
	IF (@editionId IS NULL OR @parentId IS NULL)
	
	BEGIN
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
	END

	SELECT DISTINCT AU.*
		FROM AgrochemicalUses AS AU
		INNER JOIN ProductAgrochemicalUses AS PAU ON PAU.AgrochemicalUseId=AU.AgrochemicalUseId
		INNER JOIN EditionDivisionProducts AS EDP ON EDP.ProductId=PAU.ProductId
		WHERE AU.Active = 1 
			AND AU.ParentId = @parentId 
			AND EDP.EditionId = @editionId

END
go