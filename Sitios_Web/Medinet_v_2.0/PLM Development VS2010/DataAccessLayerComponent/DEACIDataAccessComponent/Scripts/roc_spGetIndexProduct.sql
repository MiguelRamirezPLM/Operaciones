IF OBJECT_ID('dbo.roc_spGetIndexProduct') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetIndexProduct
go

/* 
	Author:			Daniel Campos / Ramiro Sánchez				 
	Object:			dbo.roc_spGetIndexProduct
	
	Description:	Retrieves all Sections By SectionId.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetIndexProduct @editionId = 2, @indexId = 3, @sectionId = 15

 */ 

CREATE PROCEDURE dbo.roc_spGetIndexProduct
(
	@editionId			int,
	@indexId			int,
	@sectionId			int
)
AS
BEGIN

	-- The parameters shouldn't be null:
	IF (@editionId IS NULL OR @indexId IS NULL OR @sectionId IS NULL)
	BEGIN
	
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
		
	END

		SELECT Sections.SectionId, Sections.Description FROM Products
			INNER JOIN ProductIndexes ON ProductIndexes.ProductId = Products.ProductId
			INNER JOIN Indexes ON Indexes.IndexId = ProductIndexes.IndexId
			INNER JOIN ProductSections ON ProductSections.ProductId = Products.ProductId
			INNER JOIN Sections ON Sections.SectionId = ProductSections.SectionId
			INNER JOIN ProductEditions ON ProductEditions.ProductId = Products.ProductId
			WHERE ProductEditions.EditionId = @editionId AND Indexes.IndexId = @indexId AND Sections.Active = 1 
				AND Indexes.Active = 1 AND Sections.SectionId = @sectionId
			GROUP BY Sections.SectionId, Sections.Description
			ORDER BY Sections.Description ASC
	
END
go