IF OBJECT_ID('dbo.roc_spGetProductsBySection') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetProductsBySection
go

/* 
	Author:			Daniel Campos / Ramiro S�nchez				 
	Object:			dbo.roc_spGetProductsBySection
	
	Description:	Retrieves information from all products by SectionId and EditionId.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetProductsBySection @sectionId = 263, @editionId = 3
 */ 

CREATE PROCEDURE dbo.roc_spGetProductsBySection
(
	@sectionId					int,
	@editionId					int
)
AS
BEGIN

	-- The parameters shouldn't be null:
	IF (@sectionId IS NULL OR @editionId IS NULL)
	BEGIN
	
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
		
	END

		SELECT p.ProductId,
			p.ParentId,
			p.ProductTypeId,
			p.ProductName,
			eps.Description,
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
		ORDER BY p.ProductName ASC
	
END
go