IF OBJECT_ID('dbo.roc_spGetComercialNameProducts') IS NOT NULL
     DROP PROCEDURE dbo.roc_spGetComercialNameProducts

go

/* 
	Author:			Daniel Campos. / Elizabeth Lazo.				 
	Object:			dbo.roc_spGetComercialNameProducts
	
	Description:	Retrieves all products by comercial name.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetComercialNameProducts  @editionId=2, @page=0 , @numberbypage=10, @countryId=4 


 */

CREATE PROCEDURE dbo.roc_spGetComercialNameProducts 
(
  @editionId      int,
  @countryId      int,
  @page           int,
  @numberbypage   int
)


AS
BEGIN

   --The parametres shouldn't be null:
   IF(@editionId IS NULL OR @countryId IS NULL OR @page IS NULL OR @numberbypage IS NULL)
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
                     Editions.CountryId = @countryId AND 
                    (Products.ProductTypeId = '5' OR 
                     Products.ProductTypeId = '1')) as contador) AS TOTAL,*
		    FROM 
              (SELECT Products.ProductId, Products.ProductName, Products.ProductTypeId, Products.Description, Companies.CompanyId, Companies.CompanyShortName, 
		              ROW_NUMBER() OVER (ORDER BY Products.ProductName,Products.ProductTypeId ASC) AS RowNumber FROM Products
					   INNER JOIN ProductEditions ON ProductEditions.ProductId = Products.ProductId
					   INNER JOIN Editions ON Editions.EditionId = ProductEditions.EditionId
					   INNER JOIN Companies ON Companies.CompanyId = Products.CompanyId
					   WHERE Editions.EditionId = @editionId AND 
                             Products.Active = '1' AND 
                             Editions.CountryId = @countryId AND 
                            (Products.ProductTypeId = '5' OR 
                             Products.ProductTypeId = '1'))AS INDICE

		   WHERE RowNumber BETWEEN @numberbypage * @page + 1 AND @numberbypage* (@page  + 1)

END
  go
