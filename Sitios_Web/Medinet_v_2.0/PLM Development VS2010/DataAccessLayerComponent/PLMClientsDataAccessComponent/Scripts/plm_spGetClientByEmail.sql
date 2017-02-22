
IF OBJECT_ID('dbo.plm_spGetClientByEmail') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetClientByEmail
go

/* 
	Author:			Erick Morales / Ramiro Sánchez
	Object:			dbo.plm_spGetClientByEmail
	
	Description:	Get only one client from the database given its email.

	Company:		PLM Latina.

	
	EXECUTE dbo.plm_spGetClientByEmail 'ems_29_mo@hotmail.com'

 */ 

CREATE PROCEDURE dbo.plm_spGetClientByEmail
(
	@email			varchar(60)
)
AS
BEGIN
	
	DECLARE
		@qtyEmails	int,
		@clientId	int

	-- Clean any empty character from the given string:
	SET @email = RTRIM(LTRIM(@email))

	-- Count how many emails has been registered in the database:
	SELECT @qtyEmails = COUNT(*) 
	FROM Clients 
	WHERE RTRIM(LTRIM(Email)) = @email

	-- If there are more than one entry in the database:
	IF (@qtyEmails > 1)
	BEGIN

		-- Take the younger entry in the database:
		SELECT @clientId = MAX(ClientId)
		FROM Clients 
		WHERE RTRIM(LTRIM(Email)) = @email

		-- Inactive all the clients given by @email except the clientId found in the last step:
		UPDATE Clients SET Active = 0
		WHERE	RTRIM(LTRIM(Email)) = @email AND
				ClientId NOT IN (@clientId)

		
		SELECT [ClientId]
			  ,[FirstName]
			  ,[LastName]
			  ,[SecondLastName]
			  ,[CompleteName]
			  ,[Gender]
			  ,[Birthday]
			  ,[Email]
			  ,[Password]
			  ,[AddedDate]
			  ,[LastUpdate]
			  ,[CountryId]
			  ,[StateId]
			  ,[Age]
			  ,[ZipCode]
			  ,[Active]
   
		FROM [dbo].[Clients]
		WHERE	ClientId = @clientId

		RETURN @@ROWCOUNT

	END
	ELSE
	BEGIN

		SELECT [ClientId]
			  ,[FirstName]
			  ,[LastName]
			  ,[SecondLastName]
			  ,[CompleteName]
			  ,[Gender]
			  ,[Birthday]
			  ,[Email]
			  ,[Password]
			  ,[AddedDate]
			  ,[LastUpdate]
			  ,[CountryId]
			  ,[StateId]
			  ,[Age]
			  ,[ZipCode]
			  ,[Active]
   
		FROM [dbo].[Clients]
		WHERE RTRIM(LTRIM(Email)) = @email

		RETURN @@ROWCOUNT

	END
	
END
go