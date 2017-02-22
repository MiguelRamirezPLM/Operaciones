IF OBJECT_ID('dbo.roc_spGetIndexProductsByText') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetIndexProductsByText
go

/* 
	Author:			Daniel Campos / Ramiro Sánchez				 
	Object:			dbo.roc_spGetIndexProductsByText
	
	Description:	Retrieves all Sections By Index and By Text.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetIndexProductsByText @page = 0, @numberByPage = 10, @editionId = 2, @indexId = 3, @text = 'deteccion'

 */ 

CREATE PROCEDURE dbo.roc_spGetIndexProductsByText
(
	@page				int,
	@numberByPage		int,
	@editionId			int,
	@indexId			int,
	@text				varchar(30)
)
AS
BEGIN

	-- The parameters shouldn't be null:
	IF (@page IS NULL OR @numberByPage IS NULL OR @editionId IS NULL OR @indexId IS NULL OR @text IS NULL)
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
						AND Sections.Description LIKE '%'+ @text +'%' COLLATE SQL_LATIN1_GENERAL_CP1_CI_AI
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
					AND Sections.Description LIKE '%'+ @text +'%' COLLATE SQL_LATIN1_GENERAL_CP1_CI_AI
				GROUP BY Sections.SectionId, Sections.Description
		)AS INDICE
		WHERE RowNumber BETWEEN @numberByPage * @page + 1 AND @numberByPage * (@page + 1)
	
END
go