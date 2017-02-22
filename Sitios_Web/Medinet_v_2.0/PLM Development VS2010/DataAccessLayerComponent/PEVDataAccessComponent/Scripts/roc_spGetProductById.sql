IF OBJECT_ID('dbo.roc_spGetProductById') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetProductById
go

/* 
	Author:			Daniel Campos. / Elizabeth Lazo.				 
	Object:			dbo.roc_spGetProductById
	
	Description:	Retrieves all products.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetProductById @productId= 1236


 */ 

CREATE PROCEDURE dbo.roc_spGetProductById
(
   @productId            int
 
)

AS
  BEGIN
  --The parameters shouldn't be null:
       IF (@productId IS NULL)
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
         Divisions.DivisionId,
         Divisions.DivisionName,
         Divisions.ShortName
         
FROM Products 
    INNER JOIN Divisions ON Divisions.LaboratoryId=Products.LaboratoryId
  WHERE Products.Active='1' AND Divisions.Active='1' AND ProductId=@productId 


 


  END
   go