IF OBJECT_ID('dbo.roc_spGetComercialNameProductsByLetter') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetComercialNameProductsByLetter
go

/* 
	Author:			Daniel Campos. / Elizabeth Lazo.				 
	Object:			dbo.roc_spGetComercialNameProducts
	
	Description:	Retrieves all comercial name the products by text.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetComercialNameProductsByLetter @page=0, @numberByPage= 10,  @editionId = 2, @countryId = 4, @letter='u'


 */ 

CREATE PROCEDURE dbo.roc_spGetComercialNameProductsByLetter
(   
  @page               int,   
  @numberByPage       int,
  @editionId          int,
  @countryId          int,
  @letter             varchar(1)
)
AS
  BEGIN
  --The parameters shouldn't be null:
       IF (@page IS NULL OR @numberByPage IS NULL OR  @editionId IS NULL OR 
           @countryId IS NULL OR @letter IS NULL) 
  BEGIN

    RAISERROR ('The current operation requires more parameters', 16, 1)
    RETURN -1

  END

   SELECT (
  SELECT count(*) from 
  (
   SELECT Products.ProductId, Products.ProductName, Products.ProductTypeId, Products.Description,Companies.CompanyId, Companies.CompanyShortName 
    FROM Products
	   INNER JOIN ProductEditions ON ProductEditions.ProductId = Products.ProductId
	   INNER JOIN Editions ON Editions.EditionId = ProductEditions.EditionId
	   INNER JOIN Companies ON Companies.CompanyId = Products.CompanyId
	   WHERE Editions.EditionId = @editionId  AND 
             Products.Active = '1' AND 
             Editions.CountryId = @countryId  AND 
            (Products.ProductTypeId = '5' OR 
             Products.ProductTypeId = '1') AND 
             Products.ProductName LIKE @letter + '%') as contador) AS TOTAL,*
	  FROM 
          (SELECT Products.ProductId, Products.ProductName, Products.ProductTypeId, Products.Description,Companies.CompanyId, Companies.CompanyShortName, 
		   ROW_NUMBER() OVER (ORDER BY Products.ProductName,Products.ProductTypeId ASC) AS RowNumber FROM Products
		   INNER JOIN ProductEditions ON ProductEditions.ProductId = Products.ProductId
		   INNER JOIN Editions ON Editions.EditionId = ProductEditions.EditionId
		   INNER JOIN Companies ON Companies.CompanyId = Products.CompanyId
		   WHERE Editions.EditionId = @editionId  AND 
                 Products.Active = '1' AND 
                 Editions.CountryId = @countryId  AND 
                (Products.ProductTypeId = '5' OR 
                 Products.ProductTypeId = '1') AND 
                 Products.ProductName LIKE @letter + '%')AS INDICE

		WHERE RowNumber BETWEEN @numberByPage * @page  + 1 AND @numberByPage * (@page  + 1)


END
 go




