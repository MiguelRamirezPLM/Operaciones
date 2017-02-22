IF OBJECT_ID('dbo.roc_spGetActiveSubstancesByText') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetActiveSubstancesByText
go

/* 
	Author:			Daniel Campos / Ramiro Sánchez				 
	Object:			dbo.roc_spGetActiveSubstancesByText
	
	Description:	Retrieves all ActiveSubstances By Edition and text.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetActiveSubstancesByText @numberByPage = 10, @page = 0, @editionId = 2, @text = 'acidos'

 */ 

CREATE PROCEDURE dbo.roc_spGetActiveSubstancesByText
(
	@numberByPage				int,
	@page						int,
	@editionId					int,
	@text						varchar(30)
)
AS
BEGIN

	-- The parameters shouldn't be null:
	IF (@numberByPage IS NULL OR @page IS NULL OR @editionId IS NULL OR @text IS NULL)
	
	BEGIN
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
	END

	SELECT (
		SELECT COUNT(*) FROM 
		(
			SELECT DISTINCT ASU.ActiveSubstanceId, ASU.ActiveSubstanceName 
				FROM ActiveSubstances AS ASU
				INNER JOIN ProductSubstances AS PS ON PS.ActiveSubstanceId=ASU.ActiveSubstanceId
				INNER JOIN EditionDivisionProducts AS EDP ON EDP.ProductId=PS.ProductId
				WHERE ASU.Active = 1 
					AND EDP.EditionId = @editionId 
					AND ASU.ActiveSubstanceName LIKE '%'+@text+'%' COLLATE SQL_LATIN1_GENERAL_CP1_CI_AI
		) AS contador
	) AS TOTAL,*
	FROM (
		SELECT DISTINCT ASU.ActiveSubstanceId, ASU.ActiveSubstanceName,
			ROW_NUMBER() OVER (ORDER BY ASU.ActiveSubstanceName ASC) AS RowNumber
			FROM ActiveSubstances AS ASU
			INNER JOIN ProductSubstances AS PS ON PS.ActiveSubstanceId=ASU.ActiveSubstanceId
			INNER JOIN EditionDivisionProducts AS EDP ON EDP.ProductId=PS.ProductId
			WHERE ASU.Active = 1 
				AND EDP.EditionId = @editionId 
				AND ASU.ActiveSubstanceName LIKE '%'+@text+'%' COLLATE SQL_LATIN1_GENERAL_CP1_CI_AI 
			GROUP BY ASU.ActiveSubstanceId, ASU.ActiveSubstanceName
	)AS INDICE
	WHERE RowNumber BETWEEN @numberByPage * @page + 1 AND @numberByPage * (@page + 1)
	
END
go