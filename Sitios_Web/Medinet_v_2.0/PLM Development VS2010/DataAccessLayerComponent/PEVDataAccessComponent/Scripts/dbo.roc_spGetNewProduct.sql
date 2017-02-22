IF OBJECT_ID('dbo.roc_spGetNewProduct') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetNewProduct
go

/* 
	Author:			Daniel Campos. / Elizabeth Lazo.				 
	Object:			dbo.roc_spGetNewProduct
	
	Description:	Retrieves all products news by edition.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetNewProduct @productId = 957, @editionId = 1, @countryId=10


 */ 

CREATE PROCEDURE dbo.roc_spGetNewProduct
(
  @productId           int,   
  @editionId           int,
  @countryId           tinyint 
)
AS
  BEGIN
  --The parameters shouldn't be null:
       IF (@productId IS NULL OR @editionId IS NULL OR @countryId IS NULL) 
  BEGIN

    RAISERROR ('The current operation requires more parameters', 16, 1)
    RETURN -1

  END

  SELECT 
      (SELECT COUNT(*) 
       FROM 
         (SELECT Products.ProductId
      FROM NewProducts 
         INNER JOIN Products ON Products.ProductId = NewProducts.ProductId
         INNER JOIN Divisions ON Divisions.DivisionId = NewProducts.DivisionId
     WHERE NewProducts.EditionId = @editionId AND 
           Products.CountryId = @countryId AND 
           Products.Active='1'
     GROUP BY Products.ProductId,
              Products.ProductName, 
              Divisions.Divisionname,
              Divisions.ShortName,
              Divisions.DivisionId) total) AS TOTAL,*   

  FROM 
     (SELECT Products.ProductId,
             Products.ProductName, 
             Divisions.Divisionname,
             Divisions.ShortName,
             Divisions.DivisionId,
             ROW_NUMBER() OVER (ORDER BY Products.ProductName ASC) AS RowNumber
      FROM NewProducts 
         INNER JOIN Products ON Products.ProductId = NewProducts.ProductId
         INNER JOIN Divisions ON Divisions.DivisionId = NewProducts.DivisionId
      WHERE NewProducts.EditionId = @editionId AND 
            Products.CountryId = @countryId AND 
            Products.Active='1'AND 
            Products.ProductId = @productId
     GROUP BY Products.ProductId,
              Products.ProductName, 
              Divisions.Divisionname,
              Divisions.ShortName,
              Divisions.DivisionId )AS NEW_PRODUCTS 

END
 go