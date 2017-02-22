IF OBJECT_ID('dbo.roc_spGetNewProducts') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetNewProducts
go

/* 
	Author:			Daniel Campos. / Elizabeth Lazo.				 
	Object:			dbo.roc_spGetNewProducts
	
	Description:	Retrieves all products new by edition and page.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetNewProducts @editionId = 1, @countryId, @page=1, @numberByPage = 10


 */ 

CREATE PROCEDURE dbo.roc_spGetNewProducts
(
   @editionId            int,
   @countryId            tinyint,
   @page                 int,
   @numberByPage         int
)

AS
  BEGIN
  --The parameters shouldn't be null:
       IF (@editionId IS NULL OR @countryId IS NULL OR @page IS NULL OR @numberByPage IS NULL) 
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
             Products.Description, 
             Divisions.Divisionname,
             Divisions.ShortName,
             Divisions.DivisionId,
             ROW_NUMBER() OVER (ORDER BY Products.ProductName ASC) AS RowNumber
     FROM NewProducts 
        INNER JOIN Products ON Products.ProductId = NewProducts.ProductId
        INNER JOIN Divisions ON Divisions.DivisionId = NewProducts.DivisionId
     WHERE NewProducts.EditionId = @editionId AND 
           Products.CountryId = @countryId AND 
           Products.Active='1'
    GROUP BY Products.ProductId,
             Products.ProductName, 
             Products.Description, 
             Divisions.Divisionname,
             Divisions.ShortName,
             Divisions.DivisionId)AS NEW_PRODUCTS

      WHERE RowNumber BETWEEN @numberByPage * @page + 1 AND @numberByPage * (@page + 1)
   
  END
   go