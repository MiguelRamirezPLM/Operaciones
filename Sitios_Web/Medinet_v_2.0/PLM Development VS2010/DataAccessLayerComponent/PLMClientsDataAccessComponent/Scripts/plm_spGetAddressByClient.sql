IF OBJECT_ID('dbo.plm_spGetAddressByClient') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetAddressByClient
go

/* 
	Author:			Juan Ramírez.				 
	Object:			dbo.plm_spGetAddressByClient
	
	Description:	Retrieves all Address as a Client.

	Company:		PLM Latina.

	
	EXECUTE dbo.plm_spGetAddressByClient @clientId

	

 */ 

CREATE PROCEDURE dbo.plm_spGetAddressByClient
(
	@clientId		int
)
AS
BEGIN
	
		SELECT [ClientAddresses].[ClientId]
			  ,[Addresses].[AddressId]
			  ,[Addresses].[Street]
			  ,[Addresses].[InternalNumber]
			  ,[Addresses].[Suburb]
			  ,[Addresses].[ZipCode]
			  ,[Addresses].[Location]
			  ,[Addresses].[CountryId]
			  ,[Addresses].[StateId]
			  ,[Addresses].[StateName]
		  FROM [dbo].[Addresses]
			   INNER JOIN [ClientAddresses] ON([Addresses].[AddressId] = [ClientAddresses].[AddressId])
			   
		 WHERE [Addresses].[Active] = 1 AND
			   [ClientAddresses].[ClientId] = @clientId

		RETURN @@ROWCOUNT
END
go