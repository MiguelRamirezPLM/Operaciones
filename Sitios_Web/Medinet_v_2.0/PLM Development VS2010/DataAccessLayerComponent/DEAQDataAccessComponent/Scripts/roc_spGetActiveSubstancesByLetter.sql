IF OBJECT_ID('dbo.roc_spGetActiveSubstancesByLetter') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetActiveSubstancesByLetter
go

/* 
	Author:			Daniel Campos / Ramiro Sánchez				 
	Object:			dbo.roc_spGetActiveSubstancesByLetter
	
	Description:	Retrieves all ActiveSubstances By Edition and letter.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetActiveSubstancesByLetter @numberByPage = 10, @page = 0, @editionId = 2, @letter = 'd'

 */ 

CREATE PROCEDURE dbo.roc_spGetActiveSubstancesByLetter
(
	@numberByPage				int,
	@page						int,
	@editionId					int,
	@letter						varchar(10)
)
AS
BEGIN

	-- The parameters shouldn't be null:
	IF (@numberByPage IS NULL OR @page IS NULL OR @editionId IS NULL OR @letter IS NULL)
	
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
					AND EDP.EditionId = @editionId AND ASU.ActiveSubstanceName LIKE @letter+'%' 
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
				AND ASU.ActiveSubstanceName LIKE @letter+'%' 
			GROUP BY ASU.ActiveSubstanceId, ASU.ActiveSubstanceName
	)AS INDICE
	WHERE RowNumber BETWEEN @numberByPage * @page + 1 AND @numberByPage * (@page + 1)
	
END
go