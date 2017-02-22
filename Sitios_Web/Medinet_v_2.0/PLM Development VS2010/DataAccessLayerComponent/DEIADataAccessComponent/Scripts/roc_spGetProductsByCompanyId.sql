IF OBJECT_ID('dbo.roc_spGetProductsByCompanyId') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetProductsByCompanyId
go

/* 
	Author:			Daniel Campos. / Elizabeth Lazo. / Ramiro Sánchez
	Object:			dbo.roc_spGetProductsByCompanyId
	
	Description:	Retrieves all products by companyId.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetProductsByCompanyId @companyId = 2, @editionId = 3
 */ 

CREATE PROCEDURE dbo.roc_spGetProductsByCompanyId 
(
	@companyId		int,
	@editionId		int
)

AS
	BEGIN
    --The parameters shouldn't be null:
    IF (@companyId IS NULL) 
	BEGIN

		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1

	END 

	SELECT p.ProductId, 
		p.ParentId,
		p.ProductTypeId,
		p.CompanyId,
		p.ProductName,
		p.Description As ProductDescription, 
		eps.EditionId, 
		eps.SectionId,
		eps.HtmlFile,
		eps.HtmlContent,
		eps.Description As EPSDescription
	FROM Products AS p
		INNER JOIN EditionProductSections AS eps ON eps.ProductId=p.ProductId
	WHERE p.CompanyId = @companyId 
		AND p.Active = 1 
		AND eps.EditionId = @editionId 
	ORDER BY p.ProductName

  END

 go



