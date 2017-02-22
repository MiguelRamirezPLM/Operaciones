IF OBJECT_ID('dbo.roc_spGetProductsBySpecie') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetProductsBySpecie
go

/* 
	Author:			Daniel Campos. / Elizabeth Lazo.				 
	Object:			dbo.roc_spGetProductsBySpecie
	
	Description:	Retrieves all products new by specie.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetProductsBySpecie @countryId=10, @editionId = 1, @SpecieId=10, @page=1, @numberByPage = 10


 */ 

CREATE PROCEDURE dbo.roc_spGetProductsBySpecie
(
   @countryId            tinyint,
   @editionId            int,
   @specieId             int,
   @page                 int,
   @numberByPage         int
)

AS
  BEGIN
  --The parameters shouldn't be null:
       IF (@countryId IS NULL OR @editionId IS NULL OR @specieId IS NULL OR @page IS NULL OR @numberByPage IS NULL) 
  BEGIN

    RAISERROR ('The current operation requires more parameters', 16, 1)
    RETURN -1

  END

   SELECT 
      (SELECT count(*) 
       FROM 
          (SELECT Products.ProductId
           FROM Products
                INNER JOIN ProductSpecies ON ProductSpecies.ProductId = Products.ProductId
                INNER JOIN EditionDivisionProducts ON EditionDivisionProducts.ProductId = Products.ProductId
                INNER JOIN Divisions ON Divisions.DivisionId = EditionDivisionProducts.DivisionId
          WHERE 
                EditionDivisionProducts.EditionId = @editionId AND 
                ProductSpecies.SpecieId = @specieId  AND 
                Products.CountryId = @countryId AND
                Products.Active = '1'

          GROUP BY Products.ProductId, 
                   Products.ProductName,
                   Products.Description, 
                   Divisions.Divisionname,
                   Divisions.ShortName,
                   Divisions.DivisionId) as contador) AS TOTAL,*   
        FROM 
          (SELECT Products.ProductId, 
                  Products.ProductName, 
                  Divisions.Divisionname, 
                  Divisions.DivisionId, 
                  Divisions.ShortName, 
                  ROW_NUMBER() OVER (ORDER BY Products.ProductName ASC) AS RowNumber
       FROM Products
          INNER JOIN ProductSpecies ON ProductSpecies.ProductId = Products.ProductId
          INNER JOIN EditionDivisionProducts ON EditionDivisionProducts.ProductId = Products.ProductId
          INNER JOIN Divisions ON Divisions.DivisionId = EditionDivisionProducts.DivisionId
       WHERE 
          EditionDivisionProducts.EditionId = @editionId AND 
          ProductSpecies.SpecieId = @specieId  AND 
          Products.CountryId = @countryId AND
          Products.Active = '1'
  
      GROUP BY Products.ProductId, 
               Products.ProductName,
               Products.Description,
               Divisions.Divisionname,
               Divisions.ShortName,
               Divisions.DivisionId )AS INDICE
 
      WHERE RowNumber BETWEEN @numberByPage * @page + 1 AND @numberByPage * (@page + 1)

END
   go