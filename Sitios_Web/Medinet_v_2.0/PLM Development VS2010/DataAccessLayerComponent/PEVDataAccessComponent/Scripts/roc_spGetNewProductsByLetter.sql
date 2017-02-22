 IF OBJECT_ID('dbo.roc_spGetNewProductsByLetter') IS NOT NULL
      DROP PROCEDURE dbo.roc_spGetNewProductsByLetter
 
  go

  /* 
	Author:			Daniel Campos / Elizabeth Lazo.				 
	Object:			dbo.roc_spGetNewProductsByLetter
	
	Description:	Retrieves all new products for letter
 
       
    Company:		PLM México.

	EXECUTE dbo.roc_spGetNewProductsByLetter  @editionId=1, @countryId=10, @letter='b',  @page=0, @numberpage=10
                                                             
 

 */ 

CREATE PROCEDURE dbo.roc_spGetNewProductsByLetter
(
  
  @editionId        int,
  @countryId        tinyint,
  @letter           varchar(1),
  @page             int,
  @numberpage       int
)

AS
BEGIN

   --The parametres shouldn't be null:
   IF(@editionId IS NULL OR @countryId IS NULL OR  
      @letter IS NULL OR @page IS NULL OR @numberpage IS NULL)
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
              Products.ProductName LIKE @letter + '%' 
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
          Products.ProductName LIKE @letter + '%'

    GROUP BY Products.ProductId, 
             Products.ProductName,
             Products.[Description], 
             Divisions.Divisionname, 
             Divisions.ShortName,
             Divisions.DivisionId,
             Products.Participant)AS NEW_PRODUCTS

  WHERE RowNumber BETWEEN @numberpage * @page + 1 AND @numberpage * (@page + 1)

 END
go