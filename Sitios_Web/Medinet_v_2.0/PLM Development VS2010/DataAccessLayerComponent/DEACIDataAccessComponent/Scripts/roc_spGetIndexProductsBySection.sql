IF OBJECT_ID('dbo.roc_spGetIndexProductsBySection') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetIndexProductsBySection
go

/* 
	Author:			Daniel Campos / Ramiro Sánchez				 
	Object:			dbo.roc_spGetIndexProductsBySection
	
	Description:	Retrieves all Products By Section.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetIndexProductsBySection @sectionId = 15

 */ 

CREATE PROCEDURE dbo.roc_spGetIndexProductsBySection
(
	@sectionId			int
)
AS
BEGIN

	-- The parameters shouldn't be null:
	IF (@sectionId IS NULL)
	BEGIN
	
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
		
	END

		SELECT Products.ProductId, Products.ProductName,Products.Description, Companies.CompanyId, Companies.CompanyShortName 
			FROM Products
			INNER JOIN ProductSections ON ProductSections.ProductId = Products.ProductId
			INNER JOIN Companies ON Companies.CompanyId = Products.CompanyId
			WHERE Products.Active = 1 AND Companies.Active = 1 AND  ProductSections.SectionId = @sectionId
			ORDER BY Products.ProductName

END
go