IF OBJECT_ID('dbo.plm_spGetClientByEmailByPassword') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetClientByEmailByPassword
go

/* 
	Author:			Ramiro Sánchez
	Object:			dbo.plm_spGetClientByEmailByPassword
	
	Description:	Get only one client from the database given its email.

	Company:		PLM Latina.

	
	EXECUTE dbo.plm_spGetClientByEmailByPassword @email = 'lala', @password = 'lla'

 */ 

CREATE PROCEDURE dbo.plm_spGetClientByEmailByPassword
(
	@email			varchar(60),
	@password		varchar(100)
)
AS
BEGIN
		
	SELECT c.[ClientId]
		  ,c.[Email]
		  ,cd.[CodeId]
		  ,cd.[CodeString]
	FROM [dbo].[Clients] c
		Inner Join ClientCodes cc On c.ClientId = cc.ClientId
		Inner Join Codes cd On cc.CodeId = cd.CodeId
	WHERE	c.[Email] = @email
		And c.[Password] = @password
		And c.Active = 1

		RETURN @@ROWCOUNT
	
END
go