IF OBJECT_ID('dbo.plm_spCheckClientCodes') IS NOT NULL
	DROP PROCEDURE dbo.plm_spCheckClientCodes
go

/* 
	Author:			Ramiro Sánchez.				 
	Object:			dbo.plm_spCheckClientCodes
	
	Description:	Check if a code was assigned to a client.

	Company:		PLM Latina.
	
	EXECUTE dbo.plm_spCheckClientCodes @codeString = 'a'

 */ 

CREATE PROCEDURE dbo.plm_spCheckClientCodes
(
	@codeString		varchar(60)
)
AS
BEGIN
	
		DECLARE @clientCode int
		SET @clientCode = 0
		
		SELECT @clientCode = COUNT(*)
		  FROM [dbo].[ClientCodes] cc
			Inner Join Codes c On cc.CodeId = c.CodeId
			Inner Join Clients cl On cc.ClientId = cl.ClientId
		 WHERE [CodeString] = @codeString
			And cl.Active = 1
			And c.CodeStatusId = 2

		RETURN @clientCode
	
END
go