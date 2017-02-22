IF OBJECT_ID('dbo.roc_spGetProductBySectionId') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetProductBySectionId
go

/* 
	Author:			Daniel Campos / Ramiro Sánchez				 
	Object:			dbo.roc_spGetProductBySectionId
	
	Description:	Retrieves all Products By Section.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetProductBySectionId @editionId = 2, @sectionId = 109

 */ 

CREATE PROCEDURE dbo.roc_spGetProductBySectionId
(
	@editionId			int,
	@sectionId			int
)
AS
BEGIN

	-- The parameters shouldn't be null:
	IF (@editionId IS NULL OR @sectionId IS NULL)
	BEGIN
	
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
		
	END

		SELECT Products.ProductId, Products.ProductIdParent, Products.ProductTypeId, Products.ProductName, Products.Description, 
			Products.CompanyId, Products.ProdId, Products.Active, ProductEditions.HTMLContent as HTMLFile, Sections.SectionIdParent
			FROM ProductEditions
			INNER JOIN Products ON Products.ProductId=ProductEditions.ProductId
			INNER JOIN ProductSections ON ProductSections.ProductId =Products.ProductId
			INNER JOIN Sections ON ProductSections.SectionId = Sections.SectionId
			WHERE ProductEditions.EditionId = @editionId AND ProductSections.SectionId = @sectionId  
			ORDER BY ProductName

END
go