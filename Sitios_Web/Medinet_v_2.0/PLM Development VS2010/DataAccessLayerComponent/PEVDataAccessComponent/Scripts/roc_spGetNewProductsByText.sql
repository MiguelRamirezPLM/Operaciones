IF OBJECT_ID('dbo.roc_spGetNewProductsByText') IS NOT NULL
      DROP PROCEDURE dbo.roc_spGetNewProductsByText
 
  go

  /* 
	Author:			Daniel Campos / Elizabeth Lazo.				 
	Object:			dbo.roc_spGetNewProductsByText
	
	Description:	Retrieves all new products for text
 
	Company:		PLM México.

	EXECUTE dbo.roc_spGetNewProductsByText @editionId=1, @countryId=10, @text='ultra-corn',  @page=0, @numberpage=10
                                                             


 */ 

CREATE PROCEDURE dbo.roc_spGetNewProductsByText
(
  @editionId        int,
  @countryId        tinyint,
  @text             varchar(255),
  @page             int,
  @numberpage       int
)

AS
BEGIN

   --The parametres shouldn't be null:
   IF(@editionId IS NULL OR @countryId IS NULL OR  
      @text IS NULL OR @page IS NULL OR @numberpage IS NULL)
   BEGIN
	
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
		
   END

 SELECT 
   (SELECT count(*) 
    FROM 
     (SELECT Products.ProductId
      FROM NewProducts 
        INNER JOIN Products ON Products.ProductId = NewProducts.ProductId
        INNER JOIN Divisions ON Divisions.DivisionId = NewProducts.DivisionId
      WHERE NewProducts.EditionId = @editionId AND 
            Products.CountryId = @countryId AND 
            Products.Active='1' AND 
           (Products.ProductName LIKE '%' + @text + '%' COLLATE SQL_LATIN1_GENERAL_CP1_CI_AI OR 
            Divisions.Divisionname  LIKE '%' + @text + '%' COLLATE SQL_LATIN1_GENERAL_CP1_CI_AI OR
            Divisions.ShortName  LIKE '%' + @text + '%' COLLATE SQL_LATIN1_GENERAL_CP1_CI_AI)
  
     GROUP BY Products.ProductId,
              Products.ProductName,
              Divisions.Divisionname,
              Divisions.ShortName,
              Divisions.DivisionId) total) AS TOTAL,*   

   FROM 
     (SELECT Products.ProductId,
             Products.ProductName,
             Products.[Description], 
             Divisions.Divisionname,
             Divisions.ShortName,
             Divisions.DivisionId,
             Products.Participant,
             ROW_NUMBER() OVER (ORDER BY Products.ProductName ASC) AS RowNumber
      FROM NewProducts 
        INNER JOIN Products ON Products.ProductId = NewProducts.ProductId
        INNER JOIN Divisions ON Divisions.DivisionId = NewProducts.DivisionId
     WHERE NewProducts.EditionId = @editionId AND 
           Products.CountryId = @countryId AND 
           Products.Active='1' AND 
          (Products.ProductName LIKE '%' + @text + '%' COLLATE SQL_LATIN1_GENERAL_CP1_CI_AI OR 
           Divisions.Divisionname  LIKE '%' + @text + '%' COLLATE SQL_LATIN1_GENERAL_CP1_CI_AI OR 
           Divisions.ShortName  LIKE '%' + @text + '%' COLLATE SQL_LATIN1_GENERAL_CP1_CI_AI)
 
     GROUP BY Products.ProductId,
              Products.ProductName,
              Products.[Description], 
              Divisions.Divisionname,
              Divisions.ShortName,
              Divisions.DivisionId,
              Products.Participant)AS NEW_PRODUCTS_BY_LETTER

  WHERE RowNumber BETWEEN @numberpage * @page + 1 AND @numberpage * (@page + 1)

END
go