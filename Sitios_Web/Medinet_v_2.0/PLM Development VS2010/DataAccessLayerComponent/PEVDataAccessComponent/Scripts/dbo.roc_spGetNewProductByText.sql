 IF OBJECT_ID('dbo.roc_spGetNewProductByText') IS NOT NULL
      DROP PROCEDURE dbo.roc_spGetNewProductByText
 
  go

  /* 
	Author:			Daniel Campos / Elizabeth Lazo.				 
	Object:			dbo.roc_spGetNewProductByText
	
	Description:	Retrieves all new products for:
 
                       L -  letter    {1} 
                       T -  text      {2}  
                       F -  fulltext  {3} 

				

	Company:		PLM México.

	EXECUTE dbo.roc_spGetNewProductByText @searchType=1, @editionId=1, @countryId=10,
                                                             @text='a',  @page=0, @numberpage=10
   EXECUTE dbo.roc_spGetNewProductByText @searchType=2, @editionId=1, @countryId=10,
                                                             @text='ultra-corn',  @page=0, @numberpage=10
   EXECUTE dbo.roc_spGetNewProductByText @searchType=3, @editionId=1, @countryId=10,
                                                             @text='pfizer',  @page=0, @numberpage=10

 */ 

CREATE PROCEDURE dbo.roc_spGetNewProductByText
(
  @searchType       int,
  @editionId        int,
  @countryId        tinyint,
  @text             varchar(255),
  @page             int,
  @numberpage       int
)

AS
BEGIN

   --The parametres shouldn't be null:
   IF(@searchType IS NULL OR  @editionId IS NULL OR @countryId IS NULL OR  
      @text IS NULL OR @page IS NULL OR @numberpage IS NULL)
   BEGIN
	
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
		
   END

--Retrieves all new products for letter

   IF (@searchType = 1)

 BEGIN

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
              Products.ProductName LIKE @text + '%' 
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
          Products.Active='1' AND 
          Products.ProductName LIKE @text + '%'

    GROUP BY Products.ProductId, 
             Products.ProductName,
             Divisions.Divisionname, 
             Products.Description, 
             Divisions.ShortName,
             Divisions.DivisionId)AS NEW_PRODUCTS

  WHERE RowNumber BETWEEN @numberpage * @page + 1 AND @numberpage * (@page + 1)

 END

--Retrieves all new products for text

  IF  (@searchType = 2)
  
BEGIN  

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
           Products.Active='1' AND 
          (Products.ProductName LIKE '%' + @text + '%' COLLATE SQL_LATIN1_GENERAL_CP1_CI_AI OR 
           Divisions.Divisionname  LIKE '%' + @text + '%' COLLATE SQL_LATIN1_GENERAL_CP1_CI_AI OR 
           Divisions.ShortName  LIKE '%' + @text + '%' COLLATE SQL_LATIN1_GENERAL_CP1_CI_AI)
 
     GROUP BY Products.ProductId,
              Products.ProductName, 
              Products.Description,
              Divisions.Divisionname,
              Divisions.ShortName,Divisions.DivisionId)AS NEW_PRODUCTS_BY_LETTER

  WHERE RowNumber BETWEEN @numberpage * @page + 1 AND @numberpage * (@page + 1)

END

  --Retrieves all new products for full text

 IF(@searchType = 3)

BEGIN

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
              (FREETEXT (Products.ProductName , @text) OR
               FREETEXT(Divisions.Divisionname, @text) OR 
               FREETEXT(Divisions.ShortName, @text))

         GROUP BY Products.ProductId,
                  Products.ProductName, 
                  Divisions.Divisionname,
                  Divisions.ShortName,
                  Divisions.DivisionId) total) AS TOTAL,*   

   FROM 
    (SELECT Products.ProductId,
            Products.ProductName, 
            Divisions.Divisionname,
            Products.Description,
            Divisions.ShortName,
            Divisions.DivisionId,
            ROW_NUMBER() OVER (ORDER BY Products.ProductName ASC) AS RowNumber
    FROM NewProducts 
      INNER JOIN Products ON Products.ProductId = NewProducts.ProductId
      INNER JOIN Divisions ON Divisions.DivisionId = NewProducts.DivisionId
    WHERE NewProducts.EditionId = @editionId AND 
          Products.CountryId = @countryId AND 
          Products.Active='1'  AND  
         (FREETEXT (Products.ProductName , @text) OR 
          FREETEXT(Divisions.Divisionname, @text) OR 
          FREETEXT(Divisions.ShortName, @text)  )
 
   GROUP BY Products.ProductId,
            Products.ProductName, 
            Divisions.Divisionname,
            Products.Description,
            Divisions.ShortName,
            Divisions.DivisionId)AS NEW_PRODUCTS

  WHERE RowNumber BETWEEN @numberpage * @page + 1 AND @numberpage * (@page + 1)

 END



END
 go