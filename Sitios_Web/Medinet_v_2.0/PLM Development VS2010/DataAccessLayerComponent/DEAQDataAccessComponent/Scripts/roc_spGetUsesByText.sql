IF OBJECT_ID('dbo.roc_spGetUsesByText') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetUsesByText
go

/* 
	Author:			Daniel Campos / Ramiro Sánchez				 
	Object:			dbo.roc_spGetUsesByText
	
	Description:	Retrieves all AgrochemicalUses by Edition and text.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetUsesByText @editionId = 4, @numberByPage = 10, @page = 0, @text = 'fertilizante'

 */ 

CREATE PROCEDURE dbo.roc_spGetUsesByText
(
	@editionId			int,
	@numberByPage		int,
	@page				int,
	@text				varchar(30)
)
AS
BEGIN

	-- The parameters shouldn't be null:
	IF (@editionId IS NULL OR @numberByPage IS NULL	OR @page IS NULL OR @text IS NULL)
	
	BEGIN
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
	END

	SELECT (
		SELECT COUNT(*) FROM 
		(
			SELECT DISTINCT AU.AgrochemicalUseId, AU.AgrochemicalUseName 
				FROM AgrochemicalUses AS AU
				INNER JOIN ProductAgrochemicalUses AS PAU ON PAU.AgrochemicalUseId=AU.AgrochemicalUseId
				INNER JOIN EditionDivisionProducts AS EDP ON EDP.ProductId=PAU.ProductId
				WHERE AU.Active = 1 
					AND AU.ParentId IS NULL 
					AND EDP.EditionId = @editionId 
					AND AU.AgrochemicalUseName LIKE '%'+@text+'%' COLLATE SQL_LATIN1_GENERAL_CP1_CI_AI
		) AS contador
	) AS TOTAL,*
	FROM (
		SELECT DISTINCT AU.AgrochemicalUseId, AU.AgrochemicalUseName,
			ROW_NUMBER() OVER (ORDER BY AU.AgrochemicalUseName ASC) AS RowNumber
			FROM AgrochemicalUses AS AU
			INNER JOIN ProductAgrochemicalUses AS PAU ON PAU.AgrochemicalUseId=AU.AgrochemicalUseId
			INNER JOIN EditionDivisionProducts AS EDP ON EDP.ProductId=PAU.ProductId
			WHERE AU.Active = 1 
				AND AU.ParentId IS NULL 
				AND EDP.EditionId = @editionId 
				AND AU.AgrochemicalUseName LIKE '%'+@text+'%' COLLATE SQL_LATIN1_GENERAL_CP1_CI_AI
			GROUP BY AU.AgrochemicalUseId, AU.AgrochemicalUseName
	)AS INDICE
	WHERE RowNumber BETWEEN @numberByPage * @page + 1 AND @numberByPage * (@page + 1)

END
go