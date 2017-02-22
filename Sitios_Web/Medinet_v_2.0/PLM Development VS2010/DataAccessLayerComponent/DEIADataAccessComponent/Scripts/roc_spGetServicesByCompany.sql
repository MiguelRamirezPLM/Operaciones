IF OBJECT_ID('dbo.roc_spGetServicesByCompany') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetServicesByCompany
go

/* 
	Author:			Daniel Campos. / Elizabeth Lazo.				 
	Object:			dbo.roc_spGetServicesByCompany
	
	Description:	Retrieves all service by company

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetServicesByCompany @sectionId = 25, @editionId = 3


 */ 

CREATE PROCEDURE dbo.roc_spGetServicesByCompany
(
    @sectionId     int,
    @editionId     int  
)


AS
 BEGIN
     --The parameters shouldn't be null:
       IF (@sectionId IS NULL OR  @editionId IS NULL) 
  BEGIN

    RAISERROR ('The current operation requires more parameters', 16, 1)
    RETURN -1

  END 

	SELECT p.ProductId,p.ProductName,p.ProductTypeId,p.CompanyId,p.Description, eps.EdProdId,
	eps.HtmlFile,eps.HtmlContent,c.CompanyName,c.CompanyTypeId FROM Products AS p
	INNER JOIN EditionProductSections AS eps ON eps.ProductId = p.ProductId
	INNER JOIN Companies AS c ON c.CompanyId = p.CompanyId
	WHERE eps.SectionId = @sectionId  AND eps.EditionId= @editionId
	ORDER BY p.ProductName ASC

  
  END

 go
