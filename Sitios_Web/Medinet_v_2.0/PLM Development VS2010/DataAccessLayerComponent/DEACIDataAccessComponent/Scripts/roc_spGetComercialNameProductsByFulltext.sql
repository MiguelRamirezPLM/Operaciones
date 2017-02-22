IF OBJECT_ID('dbo.roc_spGetComercialNameProductsByFulltext') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetComercialNameProductsByFulltext
go

/* 
	Author:			Daniel Campos. / Elizabeth Lazo.				 
	Object:			dbo.roc_spGetComercialNameProductsByFulltext
	
	Description:	Retrieves all comercial name the products by full text.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetComercialNameProductsByFulltext @page=0, @numberByPage= 23,  @editionId = 2, @countryId = 4, @text='turbo polvo'


 */ 

CREATE PROCEDURE dbo.roc_spGetComercialNameProductsByFulltext
(   
  @page               int,   
  @numberByPage       int,
  @editionId          int,
  @countryId          int,
  @text             varchar(250)
)
AS
  BEGIN
  --The parameters shouldn't be null:
       IF (@page IS NULL OR @numberByPage IS NULL OR  @editionId IS NULL OR 
            @countryId IS NULL OR @text IS NULL ) 
  BEGIN

    RAISERROR ('The current operation requires more parameters', 16, 1)
    RETURN -1

  END

  SELECT 
  (SELECT count(*) 
    from 
	  (SELECT Products.ProductId, Products.ProductName, Products.ProductTypeId, Products.Description, Companies.CompanyId, Companies.CompanyShortName 
       FROM Products
		   INNER JOIN ProductEditions ON ProductEditions.ProductId = Products.ProductId
		   INNER JOIN Editions ON Editions.EditionId = ProductEditions.EditionId
		   INNER JOIN Companies ON Companies.CompanyId = Products.CompanyId
		   WHERE Editions.EditionId = @editionId AND 
                 Products.Active = '1' AND 
                 Editions.CountryId = @countryId 
			     AND (Products.ProductTypeId = '5' OR 
                Products.ProductTypeId = '1') AND 
               (FREETEXT(Products.ProductName,@text ) OR 
                FREETEXT(Products.Description,@text ))) as contador) AS TOTAL,*
	     FROM 
              (SELECT Products.ProductId, Products.ProductName, Products.ProductTypeId, Products.Description, Companies.CompanyId, Companies.CompanyShortName, 
			   ROW_NUMBER() OVER (ORDER BY Products.ProductName,Products.ProductTypeId ASC) AS RowNumber 
               FROM Products
				   INNER JOIN ProductEditions ON ProductEditions.ProductId = Products.ProductId
				   INNER JOIN Editions ON Editions.EditionId = ProductEditions.EditionId
				   INNER JOIN Companies ON Companies.CompanyId = Products.CompanyId
				   WHERE Editions.EditionId = @editionId AND 
                         Products.Active = '1' AND 
                         Editions.CountryId = @countryId AND 
                        (Products.ProductTypeId = '5' OR 
                         Products.ProductTypeId = '1') AND 
                        (FREETEXT(Products.ProductName,@text ) OR 
                        FREETEXT(Products.Description,@text )))AS INDICE

				WHERE RowNumber BETWEEN @numberByPage  * @page + 1 AND @numberByPage  * (@page + 1)

  END
  go