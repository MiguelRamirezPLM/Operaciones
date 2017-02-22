IF OBJECT_ID('dbo.roc_spGetAddressByInternationalId') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetAddressByInternationalId
go

/* 
	Author:			Daniel Campos. / Elizabeth Lazo.
				 
	Object:			dbo.roc_spGetAddressByInternationalId
	
	Description:	Retrieves the internacional address by clients.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetAddressByInternationalId  @clientId = 1


 */ 

CREATE PROCEDURE dbo.roc_spGetAddressByInternationalId 
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


		SELECT IA.*,C.CountryName
			FROM IntAddresses AS IA
			INNER JOIN IntClientAddresses AS ICA ON ICA.IntAddressesId=IA.IntAddressesId
			INNER JOIN IntClients AS IC ON IC.IntClientId=ICA.IntClientId
			INNER JOIN Countries AS C ON C.CountryId=IA.CountryId
	   WHERE IC.IntClientId=@clientId AND IA.Active='1'

END
go