IF OBJECT_ID('dbo.roc_spGetPhonesByInternationalId') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetPhonesByInternationalId
go

/* 
	Author:			Daniel Campos. / Elizabeth Lazo.
				 
	Object:			dbo.roc_spGetPhonesByInternationalId
	
	Description:	Retrieves the internacional phones by clients.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetPhonesByInternationalId  @clientId = 1


 */ 

CREATE PROCEDURE dbo.roc_spGetPhonesByInternationalId 
(
  @clientId              int  
  
)
AS
  BEGIN
  --The parameters shouldn't be null:
       IF (@clientId  IS NULL) 
  BEGIN

    RAISERROR ('The current operation requires more parameters', 16, 1)
    RETURN -1

  END

		SELECT ICP.*,PT.Description
			FROM IntClientPhones AS ICP
			INNER JOIN IntClients AS IC ON IC.IntClientId=ICP.IntClientId
			INNER JOIN PhoneTypes AS PT ON ICP.PhoneTypeId=PT.PhoneTypeId
			WHERE IC.IntClientId=@clientId AND ICP.Active='1' AND PT.Active='1'

END
go