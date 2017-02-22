IF OBJECT_ID('dbo.roc_spGetProductByTherapeuticUse') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetProductByTherapeuticUse
go

/* 
	Author:			Daniel Campos. / Elizabeth Lazo.				 
	Object:			dbo.roc_spGetPharmaFomsByProduct
	
	Description:	Retrieves all products by therapeutic use.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetProductByTherapeuticUse @therapeuticUseId=276, @editionId=1


 */ 

CREATE PROCEDURE dbo.roc_spGetProductByTherapeuticUse
(
  @therapeuticUseId   int,
  @editionId          int 
)

AS
  BEGIN
  --The parameters shouldn't be null:
       IF (@therapeuticUseId IS NULL OR @editionId IS NULL ) 
  BEGIN

    RAISERROR ('The current operation requires more parameters', 16, 1)
    RETURN -1

  END


   SELECT Products.ProductId,
          Products.ProductName,
          Divisions.DivisionId, 
          Divisions.ShortName,
          Products.Participant 
   FROM Products
     INNER JOIN ParticipantProducts ON ParticipantProducts.ProductId = Products.ProductId
     INNER JOIN Divisions ON Divisions.DivisionId = ParticipantProducts.DivisionId
     INNER JOIN  ProductTherapeuticUses ON ProductTherapeuticUses.ProductId = Products.ProductId
      INNER JOIN TherapeuticUses ON TherapeuticUses.TherapeuticId = ProductTherapeuticUses.TherapeuticId

 WHERE ParticipantProducts.EditionId = @editionId AND 
       TherapeuticUses.TherapeuticId = @therapeuticUseId

ORDER BY Products.ProductName ASC 

END
go