IF OBJECT_ID('dbo.roc_spGetPharmaFomsByProduct') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetPharmaFomsByProduct
go

/* 
	Author:			Daniel Campos. / Elizabeth Lazo.				 
	Object:			dbo.roc_spGetPharmaFomsByProduct
	
	Description:	Retrieves all pharmaforms by  products.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetPharmaFomsByProduct @productId = 708


 */ 

CREATE PROCEDURE dbo.roc_spGetPharmaFomsByProduct
(
  @productId   int
)

AS
  BEGIN
  --The parameters shouldn't be null:
       IF (@productId  IS NULL) 
  BEGIN

    RAISERROR ('The current operation requires more parameters', 16, 1)
    RETURN -1

  END

  SELECT ProductPharmaForms.PharmaFormId,
         CASE WHEN PharmaceuticalForms.PharmaForm = 'Sin Asignar'
				THEN '' ELSE PharmaceuticalForms.PharmaForm END AS PharmaForm,
         PharmaceuticalForms.Active
  
  FROM ProductPharmaForms
    INNER JOIN PharmaceuticalForms ON  PharmaceuticalForms.PharmaFormId = ProductPharmaForms.PharmaFormId
  
WHERE ProductPharmaForms.ProductId = @productId

 END
  go