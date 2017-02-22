IF OBJECT_ID('dbo.roc_spGetPharmaForm') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetPharmaForm
go

/* 
	Author:			Daniel Campos / Elizabeth Lazo.				 
	Object:			dbo.roc_spGetPharmaForm
	
	Description:	Retrieves all information the pharmaform.
	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetPharmaForm @pharmaFormId = 63


 */ 

CREATE PROCEDURE dbo.roc_spGetPharmaForm
(   
  @pharmaFormId         int
  
) 
AS
  BEGIN
  --The parameters shouldn't be null:
 
       IF (@pharmaFormId IS NULL) 
             
  BEGIN

    RAISERROR ('The current operation requires more parameters', 16, 1)
    RETURN -1

  END
			SELECT PF.PharmaFormId,
				   CASE WHEN PF.PharmaForm = 'Sin Asignar' THEN '' ELSE PF.PharmaForm END AS PharmaForm,
				   PF.Active
			FROM PharmaForms AS PF
			WHERE PF.PharmaFormId=@pharmaFormId AND PF.Active='1'

	END

go


