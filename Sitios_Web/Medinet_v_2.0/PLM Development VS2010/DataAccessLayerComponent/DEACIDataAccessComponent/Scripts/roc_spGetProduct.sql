IF OBJECT_ID('dbo.roc_spGetProduct') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetProduct
go

/* 
	Author:			Daniel Campos / Ramiro Sánchez				 
	Object:			dbo.roc_spGetProduct
	
	Description:	Retrieves information about an Product.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetProduct @editionId = 2, @productId = 3898

 */ 

CREATE PROCEDURE dbo.roc_spGetProduct
(
	@editionId			int,
	@productId			int
)
AS
BEGIN

	-- The parameters shouldn't be null:
	IF (@editionId IS NULL OR @productId IS NULL)
	BEGIN
	
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
		
	END

		SELECT Products.ProductId, Products.ProductIdParent, Products.ProductTypeId, Products.ProductName, Products.Description, 
			Products.CompanyId, Products.ProdId, Products.Active, ProductEditions.HTMLContent as HTMLFile, Sections.SectionIdParent 
			FROM ProductEditions
			INNER JOIN Products ON Products.ProductId=ProductEditions.ProductId
			INNER JOIN ProductSections ON Products.ProductId = ProductSections.ProductID
			INNER JOIN Sections ON ProductSections.SectionId = Sections.SectionId
			WHERE ProductEditions.EditionId = @editionId AND ProductEditions.ProductId = @productId

END
go