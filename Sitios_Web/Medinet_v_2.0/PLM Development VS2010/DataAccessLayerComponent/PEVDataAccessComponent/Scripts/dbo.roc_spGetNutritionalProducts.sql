IF OBJECT_ID('dbo.roc_spGetNutritionalProducts') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetNutritionalProducts
go

/* 
	Author:			Daniel Campos. / Elizabeth Lazo.				 
	Object:			dbo.roc_spGetNutritionalProducts
	
	Description:	Retrieves all products nutritional.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetNutritionalProducts @editionId = 1, @countryId=10, @page=1, @numberByPage = 10


 */ 

CREATE PROCEDURE dbo.roc_spGetNutritionalProducts
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
    (SELECT count(*) 
     FROM 
       (SELECT Products.ProductId 
            FROM Products 
               INNER JOIN EditionDivisionProducts ON EditionDivisionProducts.ProductId = Products.ProductId
               INNER JOIN Divisions ON Divisions.DivisionId = EditionDivisionProducts.DivisionId
               INNER JOIN ProductTypes ON ProductTypes.ProductId = Products.ProductId
            WHERE EditionDivisionProducts.EditionId = @editionId  AND 
                  Products.CountryId=@countryId  AND 
                  Products.Active='1'

          GROUP BY Products.ProductId) total) AS TOTAL,*   
          
          FROM 
            (SELECT Products.ProductId,
                    Products.ProductName,
                    Products.Description, 
                    Divisions.DivisionName, 
                    Divisions.DivisionId,
                    Divisions.ShortName,
                    ROW_NUMBER() OVER (ORDER BY Products.ProductName ASC) AS RowNumber

            FROM Products 
               INNER JOIN EditionDivisionProducts ON EditionDivisionProducts.ProductId = Products.ProductId
               INNER JOIN Divisions ON Divisions.DivisionId = EditionDivisionProducts.DivisionId
               INNER JOIN ProductTypes ON ProductTypes.ProductId = Products.ProductId

           WHERE EditionDivisionProducts.EditionId = @editionId  AND 
                 Products.CountryId=@countryId  AND 
                 Products.Active='1'

          GROUP BY Products.ProductId,
                   Products.ProductName, 
                   Products.Description, 
                   Divisions.DivisionName, 
                   Divisions.DivisionId, 
                   Divisions.ShortName)AS NutritionalProducts

  WHERE RowNumber BETWEEN @numberByPage * @page + 1 AND @numberByPage * (@page + 1)


END
   go