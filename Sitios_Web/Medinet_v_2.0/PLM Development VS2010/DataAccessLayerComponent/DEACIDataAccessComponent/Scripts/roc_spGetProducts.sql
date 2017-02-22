IF OBJECT_ID('dbo.roc_spGetProducts') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetProducts
go

/* 
	Author:			Daniel Campos / Ramiro Sánchez				 
	Object:			dbo.roc_spGetProducts
	
	Description:	Retrieves all Products By Section and ProductType.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetProducts @editionId = 2, @sectionId = 64, @productTypeId = 5

 */ 

CREATE PROCEDURE dbo.roc_spGetProducts
(
	@editionId			int,
	@sectionId			int,
	@productTypeId		int
)
AS
BEGIN

	-- The parameters shouldn't be null:
	IF (@editionId IS NULL OR @sectionId IS NULL OR @productTypeId IS NULL)
	BEGIN
	
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
		
	END

		SELECT Products.ProductId, Products.ProductIdParent, Products.ProductTypeId, Products.ProductName, Products.Description,
			Products.CompanyId, Products.ProdId, Products.Active, ProductEditions.HTMLContent as HtmlFile, Sections.SectionIdParent 
			FROM Products
			INNER JOIN ProductSections ON ProductSections.ProductId = Products.ProductID
			INNER JOIN ProductEditions ON ProductEditions.ProductId = Products.ProductId
			INNER JOIN Editions ON Editions.EditionId = ProductEditions.EditionId
			INNER JOIN Sections ON Sections.SectionId = ProductSections.SectionId
			WHERE Editions.EditionId = @editionId AND Products.Active = 1 AND Sections.Active = 1
			AND Sections.SectionId = @sectionId AND Products.ProductTypeId = @productTypeId
			ORDER BY Products.ProductName ASC

END
go