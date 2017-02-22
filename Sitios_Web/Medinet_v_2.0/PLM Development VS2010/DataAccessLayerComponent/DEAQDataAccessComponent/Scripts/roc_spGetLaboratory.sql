IF OBJECT_ID('dbo.roc_spGetLaboratory') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetLaboratory
go

/* 
	Author:			Daniel Campos / Elizabeth Lazo.				 
	Object:			dbo.roc_spGetLaboratory
	
	Description:	Retrieves all information the laboratories.
	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetLaboratory @laboratoryId = 105


 */ 

CREATE PROCEDURE dbo.roc_spGetLaboratory
(   
  @laboratoryId         int
 
  
) 
AS
  BEGIN
  --The parameters shouldn't be null:
 
       IF (@laboratoryId IS NULL) 
             
  BEGIN

    RAISERROR ('The current operation requires more parameters', 16, 1)
    RETURN -1

  END
			
			SELECT * 
			  FROM Laboratories
			  WHERE LaboratoryId=@laboratoryId  AND Active='1'

	END

go


