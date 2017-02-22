IF OBJECT_ID('dbo.roc_spGetNutritionalProductsByLetter') IS NOT NULL
   DROP PROCEDURE  dbo.roc_spGetNutritionalProductsByLetter

go

  /*

     Author:	   Daniel Campos / Elizabeth Lazo.				 
	 Object:	   dbo.roc_spGetNutritionalProductsByLetter
	
	Description:	Retrieves all  products nutritional for letter
 

	Company:		PLM México.

	EXECUTE dbo.roc_spGetNutritionalProductsByLetter @editionId=1, @countryId=10, @letter ='a',  @page=0, @numberpage=10
                                                             
   
*/

CREATE PROCEDURE dbo.roc_spGetNutritionalProductsByLetter
(
  @editionid         int,
  @countryid         int,
  @letter            varchar(1),
  @page              int,
  @numberpage        int 
)
 
 AS
  BEGIN

 --The parametres shouldn't be null:
   IF(@editionId IS NULL OR @countryId IS NULL OR  
      @letter  IS NULL OR @page IS NULL OR @numberpage IS NULL)
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
        WHERE EditionDivisionProducts.EditionId = @editionid AND 
              Products.CountryId=@countryid AND 
              Products.Active='1'  AND 
              Products.ProductName LIKE @letter  + '%' 

        GROUP BY Products.ProductId) total) AS TOTAL,*   

  FROM 
   (SELECT Products.ProductName,
           Products.ProductId,
           Products.Description, 
           Divisions.DivisionId, 
           Divisions.DivisionName, 
           Products.Participant,
           ROW_NUMBER() OVER (ORDER BY Products.ProductName ASC) AS RowNumber
    FROM Products 
       INNER JOIN EditionDivisionProducts ON EditionDivisionProducts.ProductId = Products.ProductId
       INNER JOIN Divisions ON Divisions.DivisionId = EditionDivisionProducts.DivisionId
       INNER JOIN ProductTypes ON ProductTypes.ProductId = Products.ProductId
    WHERE EditionDivisionProducts.EditionId = @editionid AND 
          Products.CountryId=@countryid AND 
          Products.Active='1' AND 
          ProductTypes.typeId='1' AND 
          Products.ProductName LIKE @letter  + '%' 

   GROUP BY Products.ProductName,
            Products.ProductId,
            Products.Description,
            Divisions.DivisionId,
            Divisions.DivisionName,
            Products.Participant)AS NutritionalProducts

   WHERE RowNumber BETWEEN @numberpage  * @page + 1 AND @numberpage  * (@page + 1)

END
go