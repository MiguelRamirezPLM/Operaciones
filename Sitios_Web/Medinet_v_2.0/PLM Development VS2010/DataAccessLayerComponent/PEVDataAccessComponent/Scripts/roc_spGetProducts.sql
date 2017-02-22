IF OBJECT_ID('dbo.roc_spGetProducts') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetProducts
go

/* 
	Author:			Daniel Campos. / Elizabeth Lazo.				 
	Object:			dbo.roc_spGetProducts
	
	Description:	Retrieves all products.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetProducts @countryId=10, @editionId = 1, @page=0, @numberByPage = 10



 */ 


CREATE PROCEDURE dbo.roc_spGetProducts
(
  @countryId       tinyint,
  @editionId       int,
  @page            int,
  @numberByPage    int
)

AS
  BEGIN
  --The parameters shouldn't be null:
       IF (@countryId IS NULL OR @editionId IS NULL OR @page  IS NULL OR @numberByPage IS NULL ) 
  BEGIN

    RAISERROR ('The current operation requires more parameters', 16, 1)
    RETURN -1

  END


    SELECT 
       (SELECT COUNT(*) 
        FROM 
           (SELECT DISTINCT(Products.ProductId) 
            FROM Products 
                INNER JOIN EditionDivisionProducts ON EditionDivisionProducts.ProductId = Products.ProductId
                INNER JOIN Divisions ON Divisions.DivisionId= EditionDivisionProducts.DivisionId
            WHERE EditionDivisionProducts.EditionId = @editionId AND 
                  Products.Active = '1' AND 
                  Products.CountryId = @countryId ) as contador) AS TOTAL,*   

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
       WHERE EditionDivisionProducts.EditionId = @editionId AND 
             Products.Active = '1' AND 
             Products.CountryId = @countryId 
      GROUP BY Products.ProductId,
               Products.ProductName, 
               Products.Description,  
               Divisions.DivisionId,
               Divisions.ShortName,
               Products.Participant)AS INDICE_GENERAL

WHERE RowNumber BETWEEN @numberByPage * @page + 1 AND @numberByPage * (@page + 1)

 END
   go

