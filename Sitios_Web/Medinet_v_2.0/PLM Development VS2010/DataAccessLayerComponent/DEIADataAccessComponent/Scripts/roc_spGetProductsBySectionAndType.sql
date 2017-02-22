IF OBJECT_ID('dbo.roc_spGetProductsBySectionAndType') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetProductsBySectionAndType
go

/* 
	Author:			Daniel Campos / Ramiro Sánchez				 
	Object:			dbo.roc_spGetProductsBySectionAndType
	
	Description:	Retrieves information from all products by SectionId and Type.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetProductsBySectionAndType @productTypeId = 2, @sectionId = 263, @editionId = 3

 */ 

CREATE PROCEDURE dbo.roc_spGetProductsBySectionAndType
(
	@productTypeId				int,
	@sectionId					int,
	@editionId					int
)
AS
BEGIN

	-- The parameters shouldn't be null:
	IF (@productTypeId IS NULL OR @sectionId IS NULL OR @editionId IS NULL)
	BEGIN
	
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
		
	END

		SELECT p.ProductId,
			p.ParentId,
			p.ProductTypeId,
			p.ProductName,
			p.Description,
			c.CompanyId,
			c.CompanyName,
			eps.HtmlFile,
			eps.HtmlContent
		FROM Products AS p
			INNER JOIN Companies AS c ON c.CompanyId = p.CompanyId
			INNER JOIN CompanyEditions AS ce ON ce.CompanyId = c.CompanyId
			INNER JOIN Editions AS e ON e.EditionId = ce.EditionId
			INNER JOIN EditionProductSections AS eps ON eps.ProductId = p.ProductId AND eps.EditionId=e.EditionId
		WHERE e.EditionId = @editionId
			AND p.Active = 1
			AND eps.SectionId = @sectionId
			AND p.ProductTypeId = @productTypeId
		ORDER BY p.ProductName ASC
	
END
go