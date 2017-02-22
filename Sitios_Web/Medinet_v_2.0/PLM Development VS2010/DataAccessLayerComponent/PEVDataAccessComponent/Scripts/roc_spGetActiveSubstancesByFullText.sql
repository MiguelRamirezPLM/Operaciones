IF OBJECT_ID('dbo.roc_spGetActiveSubstancesByFullText') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetActiveSubstancesByFullText
go

/* 
	Author:			Daniel Campos / Ramiro Sánchez				 
	Object:			dbo.roc_spGetActiveSubstancesByFullText
	
	Description:	Retrieves all ActiveSubstances by FullText
					
	Company:		PLM México.

	EXECUTE dbo.roc_spGetActiveSubstancesByFullText @editionId = 1, @fullText = 'abamectina', @page = 0, @numberByPage = 10
	
 */ 

CREATE PROCEDURE dbo.roc_spGetActiveSubstancesByFullText
(
	@editionId			int,
	@fullText			varchar(50),
	@page				int,
	@numberByPage		int
)AS
BEGIN

	IF (@editionId < 1 OR @fullText IS NULL OR @page < 0 OR @numberByPage < 0)
	BEGIN
		
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
	
	END

	BEGIN
	
		SELECT (
			SELECT count(*) from (
				SELECT DISTINCT (ActiveSubstances.ActiveSubstanceName),ActiveSubstances.ActiveSubstanceId 
					FROM Products 
					INNER JOIN ProductSubstances ON ProductSubstances.ProductId = Products.ProductId
					INNER JOIN ActiveSubstances ON ActiveSubstances.ActiveSubstanceId = ProductSubstances.ActiveSubstanceId
					INNER JOIN EditionDivisionProducts ON EditionDivisionProducts.ProductId = Products.ProductId
					WHERE  EditionDivisionProducts.EditionId = @editionId AND ActiveSubstances.Active = 1 
					AND FREETEXT( ActiveSubstances.ActiveSubstanceName ,@fullText)
			) total
		) AS TOTAL,*

		FROM (
			SELECT DISTINCT (ActiveSubstances.ActiveSubstanceName),ActiveSubstances.ActiveSubstanceId, 
				ROW_NUMBER() OVER (ORDER BY ActiveSubstances.ActiveSubstanceName ASC) AS RowNumber  FROM Products 
				INNER JOIN ProductSubstances ON ProductSubstances.ProductId = Products.ProductId
				INNER JOIN ActiveSubstances ON ActiveSubstances.ActiveSubstanceId = ProductSubstances.ActiveSubstanceId
				INNER JOIN EditionDivisionProducts ON EditionDivisionProducts.ProductId = Products.ProductId
				WHERE  EditionDivisionProducts.EditionId = @editionId AND ActiveSubstances.Active = 1 
				AND FREETEXT( ActiveSubstances.ActiveSubstanceName ,@fullText)
				GROUP BY ActiveSubstances.ActiveSubstanceName, ActiveSubstances.ActiveSubstanceId
		)AS ACTIVE_SUBSTANCES
		WHERE RowNumber BETWEEN @numberByPage * @page + 1 AND @numberByPage * (@page + 1)

	END
END
go