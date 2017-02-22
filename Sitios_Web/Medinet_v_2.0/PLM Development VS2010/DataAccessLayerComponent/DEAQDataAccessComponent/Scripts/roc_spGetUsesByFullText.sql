IF OBJECT_ID('dbo.roc_spGetUsesByFullText') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetUsesByFullText
go

/* 
	Author:			Daniel Campos / Ramiro Sánchez				 
	Object:			dbo.roc_spGetUsesByFullText
	
	Description:	Retrieves all AgrochemicalUses by Edition and FullText.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetUsesByFullText @editionId = 4, @numberByPage = 10, @page = 0, @fullText = 'insecticida'

 */ 

CREATE PROCEDURE dbo.roc_spGetUsesByFullText
(
	@editionId			int,
	@numberByPage		int,
	@page				int,
	@fullText			varchar(30)
)
AS
BEGIN

	-- The parameters shouldn't be null:
	IF (@editionId IS NULL OR @numberByPage IS NULL	OR @page IS NULL OR @fullText IS NULL)
	
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
					AND FREETEXT(AU.AgrochemicalUseName,@fullText)
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
				AND FREETEXT(AU.AgrochemicalUseName,@fullText)
			GROUP BY AU.AgrochemicalUseId, AU.AgrochemicalUseName
	)AS INDICE
	WHERE RowNumber BETWEEN @numberByPage * @page + 1 AND @numberByPage * (@page + 1)

END
go