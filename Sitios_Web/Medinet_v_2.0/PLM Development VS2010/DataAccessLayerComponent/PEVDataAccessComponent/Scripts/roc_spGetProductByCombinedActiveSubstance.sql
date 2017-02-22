IF OBJECT_ID('dbo.roc_spGetProductByCombinedActiveSubstance') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetProductByCombinedActiveSubstance
go

/* 
	Author:			Daniel Campos / Elizabeth Lazo.				 
	Object:			dbo.roc_spGetProductByCombinedActiveSubstance
	
	Description:	Retrieves all products by combined active substance.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetProductByActiveSubstance @activesubstanceId=1, @countryId= 10, @editionId = 1 


 */ 

CREATE PROCEDURE dbo.roc_spGetProductByCombinedActiveSubstance
(

@activesubstanceId       int,
@countryId               tinyint,
@editionId               int
)

AS
  BEGIN
  --The parameters shouldn't be null:
       IF (@activesubstanceId  IS NULL OR @countryId IS NULL OR @editionId  IS NULL) 
  BEGIN

    RAISERROR ('The current operation requires more parameters', 16, 1)
    RETURN -1

  END

SELECT DISTINCT     (Products.ProductId), 
                     Products.ProductName,
                     Products.Description, 
                     Products.CountryId,
                     Products.Active,
                     Products.Participant,
                     Laboratories.LaboratoryName,
                     Laboratories.LaboratoryId,
                     Laboratories.ImageName,
                     Laboratories.CourtesyImage
                  
FROM Products 
  INNER JOIN ProductSubstances ON ProductSubstances.ProductId = Products.ProductId
  INNER JOIN ProductPharmaForms ON ProductPharmaForms.ProductId = Products.ProductId
  INNER JOIN ActiveSubstances ON ActiveSubstances.ActiveSubstanceId = ProductSubstances.ActiveSubstanceId
  INNER JOIN Laboratories ON Laboratories.LaboratoryId = Products.LaboratoryId
  INNER JOIN EditionDivisionProducts ON EditionDivisionProducts.ProductId = Products.ProductId

WHERE ActiveSubstances.ActiveSubstanceId = @activesubstanceId AND 
      Products.Active = '1'  AND Products.CountryId=@countryId AND 
      EditionDivisionProducts.EditionId= @editionId  AND 
 
(SELECT count(*) 
 FROM ProductSubstances 
  INNER JOIN ActiveSubstances ON ActiveSubstances.ActiveSubstanceId = ProductSubstances.ActiveSubstanceId
 WHERE ProductSubstances.ProductId = Products.ProductId AND 
       ActiveSubstances.Active=@activesubstanceId) > 1

 ORDER BY Products.ProductName ASC

END
go