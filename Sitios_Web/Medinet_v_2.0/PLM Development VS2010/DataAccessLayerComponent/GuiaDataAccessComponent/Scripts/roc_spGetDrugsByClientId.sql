IF OBJECT_ID('dbo.roc_spGetDrugsByClientId') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetDrugsByClientId
go

/* 
	Author:			Benjamín Castillo Arriaga / Elizabeth Lazo.				 
	Object:			dbo.roc_spGetDrugsByClientId
	
	Description:	Retrieves all drugs by clientId.

	Company:		PLM Latina.

	


    EXECUTE  dbo.roc_spGetDrugsByClientId @clientId = 36597


 */ 

CREATE PROCEDURE [dbo].[roc_spGetDrugsByClientId]
(
    @clientId               int
)
AS
BEGIN

	-- The parameters shouldn't be null:
	IF (@clientId  IS NULL)
	BEGIN
	
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
		
	END

	
      SELECT * FROM Drugs 
        WHERE ClientId=@clientId AND Active='1' ORDER BY DrugName


END

