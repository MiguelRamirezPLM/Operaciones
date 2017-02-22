IF OBJECT_ID('dbo.roc_spGetSectionsAndProductsByText') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetSectionsAndProductsByText
go

/* 
	Author:			Daniel Campos. / Elizabeth Lazo.				 
	Object:			dbo.roc_spGetSectionsAndProductsByText
	
	Description:	Retrieves sections and products by text.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetSectionsAndProductsByText @editionId = 3, @parentId = 3, @text = 'acido',  @numberByPage = 10 , @page = 0 


 */ 

CREATE PROCEDURE dbo.roc_spGetSectionsAndProductsByText
(
	@editionId		int,
	@parentId		int,
	@text			varchar(30),
	@numberByPage	int,
	@page			int
)

AS
 BEGIN
     --The parameters shouldn't be null:
       IF (@editionId IS NULL OR @parentId IS NULL OR @page IS NULL OR @numberByPage IS NULL OR @text IS NULL)
  BEGIN

    RAISERROR ('The current operation requires more parameters', 16, 1)
    RETURN -1

  END 

  SELECT(
	 SELECT count(*) FROM
	 (
	SELECT Sections.*
	 FROM Products 
	INNER JOIN EditionProductSections ON EditionProductSections.ProductId = Products.ProductId
	INNER JOIN Companies ON Companies.CompanyId = Products.CompanyId
	INNER JOIN CompanyEditions ON CompanyEditions.CompanyId = Companies.CompanyId
	INNER JOIN Editions ON Editions.EditionId = CompanyEditions.EditionId AND EditionProductSections.EditionId=Editions.EditionId  
	INNER JOIN Sections ON EditionProductSections.SectionId=Sections.SectionId
	WHERE  Products.Active = '1' AND Editions.EditionId = @editionId AND
	EditionProductSections.HtmlFile IS NOT NULL and EditionProductSections.SectionId in (SELECT Sections.SectionId FROM Sections  
	  WHERE Sections.ParentId = @parentId AND Sections.Active =  '1') and 
	(SectionName like '%'+@text+'%' COLLATE SQL_LATIN1_GENERAL_CP1_CI_AI or ProductName like '%'+@text+'%' COLLATE SQL_LATIN1_GENERAL_CP1_CI_AI)
	)as contador
	)as total,*
	from(
	SELECT Sections.SectionId,Sections.SectionName,Products.ProductId, Products.ParentId,Products.ProductTypeId,Products.CompanyId,Products.ProductName,
	products.Description,EditionProductSections.HtmlFile,Companies.CompanyName,Companies.CompanyTypeId,
	 ROW_NUMBER() OVER (ORDER BY SectionName,Products.ProductName ASC) As RowNumber
	 FROM Products 
	INNER JOIN EditionProductSections ON EditionProductSections.ProductId = Products.ProductId
	INNER JOIN Companies ON Companies.CompanyId = Products.CompanyId
	INNER JOIN CompanyEditions ON CompanyEditions.CompanyId = Companies.CompanyId
	INNER JOIN Editions ON Editions.EditionId = CompanyEditions.EditionId AND EditionProductSections.EditionId=Editions.EditionId 
	INNER JOIN Sections ON EditionProductSections.SectionId=Sections.SectionId
	WHERE  Products.Active = '1' AND Editions.EditionId = @editionId AND
	EditionProductSections.HtmlFile IS NOT NULL and EditionProductSections.SectionId in (SELECT Sections.SectionId FROM Sections  
	  WHERE Sections.ParentId = @parentId AND Sections.Active =  '1') and 
	(SectionName like '%'+@text+'%' COLLATE SQL_LATIN1_GENERAL_CP1_CI_AI or ProductName like '%'+@text+'%' COLLATE SQL_LATIN1_GENERAL_CP1_CI_AI)
	)as x
	WHERE RowNumber BETWEEN @numberByPage * @page + 1 AND @numberByPage * (@page+1)


  
  END

 go



