IF OBJECT_ID('dbo.roc_spGetProductsByFullText') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetProductsByFullText
go

/* 
	Author:			Daniel Campos / Ramiro Sánchez				 
	Object:			dbo.roc_spGetProductsByFullText
	
	Description:	Retrieves all Products by Country, Edition, and FullText.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetProductsByFullText @countryId = 11, @editionId = 2, @numberByPage = 10, @page = 0, @fullText = 'bio'

 */ 

CREATE PROCEDURE dbo.roc_spGetProductsByFullText
(
	@countryId			int,
	@editionId			int,
	@numberByPage		int,
	@page				int,
	@fullText			varchar(30)
)
AS
BEGIN

	-- The parameters shouldn't be null:
	IF (@countryId IS NULL OR  @editionId IS NULL OR @numberByPage IS NULL OR @page IS NULL OR @fullText IS NULL)
	
	BEGIN
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
	END

	SELECT (
		SELECT COUNT(*) FROM 
		(
			SELECT P.ProductId, P.ProductName, P.Description, P.Register, ED.PharmaFormId, 
				P.LaboratoryId, L.LaboratoryName,ED.DivisionId, D.DivisionName,ED.CategoryId
				FROM Products AS P 
				INNER JOIN EditionDivisionProducts AS ED ON ED.ProductId=P.ProductId
				INNER JOIN Divisions AS D ON D.DivisionId=ED.DivisionId
				INNER JOIN Laboratories AS L ON L.LaboratoryId=P.LaboratoryId
				WHERE P.Active = 1 
					AND ED.EditionId = @editionId 
					AND D.CountryId = @countryId 
					AND (FREETEXT(P.ProductName,@fullText) OR FREETEXT(P.Description,@fullText)) 
		) AS contador
	) AS TOTAL,*
	FROM (
			SELECT P.ProductId, P.ProductName, P.Description, P.Register, ED.PharmaFormId, 
				P.LaboratoryId, L.LaboratoryName,ED.DivisionId, D.DivisionName,ED.CategoryId,
			ROW_NUMBER() OVER (ORDER BY P.ProductName ASC) AS RowNumber
			FROM Products AS P 
			INNER JOIN EditionDivisionProducts AS ED ON ED.ProductId=P.ProductId
			INNER JOIN Divisions AS D ON D.DivisionId=ED.DivisionId
			INNER JOIN Laboratories AS L ON L.LaboratoryId=P.LaboratoryId
			WHERE P.Active = 1  
				AND ED.EditionId = @editionId 
				AND D.CountryId = @countryId 
				AND (FREETEXT(P.ProductName,@fullText) OR FREETEXT(P.Description,@fullText))  
	)AS INDICE
	WHERE RowNumber BETWEEN @numberByPage * @page + 1 AND @numberByPage * (@page + 1)
END
go