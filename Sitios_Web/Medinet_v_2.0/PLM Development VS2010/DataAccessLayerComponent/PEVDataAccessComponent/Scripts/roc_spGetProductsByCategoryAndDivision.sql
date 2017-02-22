IF OBJECT_ID('dbo.roc_spGetProductsByCategoryAndDivision') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetProductsByCategoryAndDivision
go

/* 
	Author:			Daniel Campos. / Elizabeth Lazo.				 
	Object:			dbo.roc_spGetProductsByCategoryAndDivision
	
	Description:	Retrieves all products by category and division.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetProductsByCategoryAndDivision @category=18, @division=1, @editionId = 1, @countryId=10


 */ 

CREATE PROCEDURE dbo.roc_spGetProductsByCategoryAndDivision
(
   @category     int,
   @division     int,
   @editionId    int,
   @countryId    tinyint 
)

AS
  BEGIN
      --The parameters shouldn't be null:
       IF (@category IS NULL OR @division IS NULL OR @editionId IS NULL OR @countryId IS NULL) 
  BEGIN

    RAISERROR ('The current operation requires more parameters', 16, 1)
    RETURN -1

  END

  SELECT Products.ProductId, 
         Products.ProductName,
         Products.CountryId,
         Products.LaboratoryId,
         Products.Description, 
         Products.Active,
         Products.Participant,
         ProductCategories.CategoryId
 
  FROM ProductCategories
    INNER JOIN Products ON Products.productid = ProductCategories.productId
    INNER JOIN EditionDivisionProducts ON EditionDivisionProducts.ProductId = Products.productid

 WHERE 
    ProductCategories.CategoryId = @category  AND
    EditionDivisionProducts.EditionId = @editionId AND  
    ProductCategories.DivisionId = @division AND 
    Products.Active='1' AND 
    Products.CountryId=@countryId AND 
    EditionDivisionProducts.CategoryId = @category 

ORDER BY Products.ProductName ASC 

END
 go 