IF OBJECT_ID('dbo.roc_spGetPharmaFormsByProductId') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetPharmaFormsByProductId
go

/* 
	Author:			Daniel Campos / Elizabeth Lazo.				 
	Object:			dbo.roc_spGetPharmaFormsByProductId
	
	Description:	Retrieves all information the pharmaform by productId.
	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetPharmaFormsByProductId @editionId=6, @productId=4145


 */ 

CREATE PROCEDURE dbo.roc_spGetPharmaFormsByProductId
(   
  @editionId         int,
  @productId         int
  
) 
AS
  BEGIN
  --The parameters shouldn't be null:
 
       IF (@editionId IS NULL OR @productId IS NULL ) 
             
  BEGIN

    RAISERROR ('The current operation requires more parameters', 16, 1)
    RETURN -1

  END
			SELECT PF.PharmaFormId,
				   CASE WHEN PF.PharmaForm = 'Sin Asignar' THEN '' ELSE PF.PharmaForm END AS PharmaForm,
				   PF.Active
			  FROM PharmaForms AS PF
				INNER JOIN ProductPharmaForms AS PPF ON PF.PharmaFormId=PPF.PharmaFormId 
				INNER JOIN EditionDivisionProducts AS EDP ON EDP.ProductId=PPF.ProductId
			  WHERE PPF.ProductId=@productId  AND PPF.Active='1' AND EDP.EditionId=@editionId AND PF.PharmaFormId=EDP.PharmaFormId

	END

go


