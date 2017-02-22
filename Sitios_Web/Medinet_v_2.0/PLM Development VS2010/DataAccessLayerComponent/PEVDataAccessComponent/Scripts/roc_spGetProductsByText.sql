IF OBJECT_ID('dbo.roc_spGetProductsByText') IS NOT NULL
   DROP PROCEDURE  dbo.roc_spGetProductsByText

go

  /*

     Author:	   Daniel Campos / Elizabeth Lazo.				 
	 Object:	   dbo.roc_spGetProductsByText
	
	Description:	Retrieves all  products by text.				

	Company:		PLM México.


    EXECUTE dbo.roc_spGetProductsByText  @editionId=1, @countryId=10, @text='%ABA%', @page=0, @numberpage=15
                                                            
   

*/

CREATE PROCEDURE dbo.roc_spGetProductsByText
(
  @editionid         int,
  @countryid         int,
  @text              varchar(250),
  @page              int,
  @numberpage        int 
)
 
 AS

 BEGIN

 --The parametres shouldn't be null:
   IF( @editionId IS NULL OR @countryId IS NULL OR  
      @text IS NULL OR @page IS NULL OR @numberpage IS NULL)
   BEGIN
	
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
		
   END

 SELECT 
    (SELECT count(*) 
     FROM 
     (SELECT DISTINCT(Products.ProductId) 
      FROM Products 
         INNER JOIN EditionDivisionProducts ON EditionDivisionProducts.ProductId = Products.ProductId
         INNER JOIN Divisions ON Divisions.DivisionId= EditionDivisionProducts.DivisionId
  
      WHERE EditionDivisionProducts.EditionId = @editionid AND 
            Products.Active = '1' AND 
            Products.CountryId = @countryid AND
            Products.ProductName LIKE '%' + @text + '%'  COLLATE SQL_LATIN1_GENERAL_CP1_CI_AI) as contador) AS TOTAL,*   

  FROM 
  (SELECT Products.ProductId,
          Products.ProductName,
          Products.Description,
          Divisions.DivisionId,
          Divisions.ShortName,
          Products.Participant,
          ROW_NUMBER() OVER (ORDER BY Products.ProductName ASC) AS RowNumber
   FROM Products 
     INNER JOIN EditionDivisionProducts ON EditionDivisionProducts.ProductId = Products.ProductId
     INNER JOIN Divisions ON Divisions.DivisionId= EditionDivisionProducts.DivisionId
  
  WHERE EditionDivisionProducts.EditionId = @editionid AND 
        Products.Active = '1' AND 
        Products.CountryId = @countryid  AND
        Products.ProductName LIKE '%' + @text + '%'  COLLATE SQL_LATIN1_GENERAL_CP1_CI_AI  
  
  GROUP BY Products.ProductId,
           Products.ProductName, 
           Products.Description,
           Divisions.DivisionId, 
           Divisions.ShortName,
           Products.Participant)AS INDICE_GENERAL

WHERE RowNumber BETWEEN @numberpage * @page + 1 AND @numberpage * (@page + 1)
 
END 
go