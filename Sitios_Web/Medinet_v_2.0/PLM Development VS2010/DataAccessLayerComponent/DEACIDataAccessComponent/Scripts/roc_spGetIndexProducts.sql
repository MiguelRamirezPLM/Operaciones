IF OBJECT_ID('dbo.roc_spGetIndexProducts') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetIndexProducts
go

/* 
	Author:			Daniel Campos. / Elizabeth Lazo.				 
	Object:			dbo.roc_spGetIndexProducts
	
	Description:	Retrieves all index by products.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetIndexProducts @page = 0, @numberByPage = 10,  @editionId = 2, @indexId = 3  


 */ 

CREATE PROCEDURE [dbo].[roc_spGetIndexProducts]
(
	@page				int,
	@numberByPage		int,
	@editionId			int,
	@indexId			int
)
AS
BEGIN

	-- The parameters shouldn't be null:
	IF (@page IS NULL OR @numberByPage IS NULL OR @editionId IS NULL OR @indexId IS NULL)
	BEGIN
	
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
		
	END

		SELECT (
			SELECT count(*) from 
			(
				SELECT Sections.SectionId, Sections.Description FROM Products
					INNER JOIN ProductIndexes ON ProductIndexes.ProductId = Products.ProductId
					INNER JOIN Indexes ON Indexes.IndexId = ProductIndexes.IndexId
					INNER JOIN ProductSections ON ProductSections.ProductId = Products.ProductId
					INNER JOIN Sections ON Sections.SectionId = ProductSections.SectionId
					INNER JOIN ProductEditions ON ProductEditions.ProductId = Products.ProductId
					WHERE ProductEditions.EditionId = @editionId AND Indexes.IndexId = @indexId AND Sections.Active = 1 AND Indexes.Active = 1
					GROUP BY Sections.SectionId, Sections.Description
			) as contador
		) AS TOTAL,*
		FROM (   
			SELECT Sections.SectionId, Sections.Description, 
				ROW_NUMBER() OVER (ORDER BY Sections.Description ASC) AS RowNumber FROM Products
				INNER JOIN ProductIndexes ON ProductIndexes.ProductId = Products.ProductId
				INNER JOIN Indexes ON Indexes.IndexId = ProductIndexes.IndexId
				INNER JOIN ProductSections ON ProductSections.ProductId = Products.ProductId
				INNER JOIN Sections ON Sections.SectionId = ProductSections.SectionId
				INNER JOIN ProductEditions ON ProductEditions.ProductId = Products.ProductId
				WHERE ProductEditions.EditionId = @editionId AND Indexes.IndexId = @indexId AND Sections.Active = 1 AND Indexes.Active = 1
				GROUP BY Sections.SectionId, Sections.Description
		)AS INDICE
		WHERE RowNumber BETWEEN @numberByPage * @page + 1 AND @numberByPage * (@page + 1)
	
END
