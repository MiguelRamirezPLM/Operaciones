IF OBJECT_ID('dbo.roc_spGetProductsByText') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetProductsByText
go

/* 
	Author:			Daniel Campos / Ramiro Sánchez				 
	Object:			dbo.roc_spGetProductsByText
	
	Description:	Retrieves all Products by Country, Edition and Text.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetProductsByText @countryId = 11, @editionId = 2, @numberByPage = 10, @page = 0, @text = 'ferti'

 */ 

CREATE PROCEDURE dbo.roc_spGetProductsByText
(
	@countryId			int,
	@editionId			int,
	@numberByPage		int,
	@page				int,
	@text				varchar(30)
)
AS
BEGIN

	-- The parameters shouldn't be null:
	IF (@countryId IS NULL OR @editionId IS NULL OR  @numberByPage IS NULL OR @page IS NULL OR @text IS NULL)
	
	BEGIN
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
	END

	SELECT (
		SELECT COUNT(*) FROM 
		(
			SELECT P.ProductId, P.ProductName, P.Description, P.Register, ED.PharmaFormId,
				P.LaboratoryId, L.LaboratoryName, ED.DivisionId, D.DivisionName,ED.CategoryId
				FROM Products AS P 
				INNER JOIN EditionDivisionProducts AS ED ON ED.ProductId=P.ProductId
				INNER JOIN Divisions AS D ON D.DivisionId=ED.DivisionId
				INNER JOIN Laboratories AS L ON L.LaboratoryId=P.LaboratoryId
				WHERE P.Active = 1
					AND ED.EditionId = @editionId 
					AND D.CountryId = @countryId 
					AND (P.ProductName LIKE '%'+@text+'%' COLLATE SQL_LATIN1_GENERAL_CP1_CI_AI 
						OR P.Description LIKE '%'+@text+'%' COLLATE SQL_LATIN1_GENERAL_CP1_CI_AI)
		) AS contador
	) AS TOTAL,*
	FROM (
		SELECT P.ProductId, P.ProductName, P.Description, P.Register, ED.PharmaFormId,
			P.LaboratoryId, L.LaboratoryName, ED.DivisionId, D.DivisionName, ED.CategoryId,
			ROW_NUMBER() OVER (ORDER BY P.ProductName ASC) AS RowNumber
			FROM Products AS P 
			INNER JOIN EditionDivisionProducts AS ED ON ED.ProductId=P.ProductId
			INNER JOIN Divisions AS D ON D.DivisionId=ED.DivisionId
			INNER JOIN Laboratories AS L ON L.LaboratoryId=P.LaboratoryId
			WHERE P.Active = 1
				AND ED.EditionId = @editionId 
				AND D.CountryId = @countryId 
				AND  (P.ProductName LIKE '%'+@text+'%' COLLATE SQL_LATIN1_GENERAL_CP1_CI_AI 
					OR P.Description LIKE '%'+@text+'%' COLLATE SQL_LATIN1_GENERAL_CP1_CI_AI)
	)AS INDICE
	WHERE RowNumber BETWEEN @numberByPage * @page + 1 AND @numberByPage * (@page + 1)

END
go