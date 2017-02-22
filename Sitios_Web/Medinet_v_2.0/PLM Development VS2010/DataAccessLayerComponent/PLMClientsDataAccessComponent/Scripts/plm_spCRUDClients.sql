
IF OBJECT_ID('dbo.plm_spCRUDClients') IS NOT NULL
	DROP PROCEDURE dbo.plm_spCRUDClients
go

/* 
	Author:			Juan Ramírez / Ramiro Sánchez				 
	Object:			dbo.plm_spCRUDClients
	
	Description:	Perform the basic operations, or in other words CRUD operations.
					Where CRUD stands for:

						C - Create	{0}
						R - Read	{1}
						U - Update	{2}
						D - Delete	{3}

	Company:		PLM Latina.

	DECLARE
		@CRUDType		tinyint,
		@clientId		int



	EXECUTE dbo.plm_spCRUDClients
		@CRUDType,			
		@clientId
		
 */ 

CREATE PROCEDURE dbo.plm_spCRUDClients
(
	@CRUDType			TINYINT,
	@clientId			INT,
	@firstName			VARCHAR(60) = NULL,
	@lastName			VARCHAR(60) = NULL,
	@secondLastName		VARCHAR(60) = NULL,
	@completeName		VARCHAR(100) = NULL,
	@gender				CHAR(1) = NULL,
	@birthday			SMALLDATETIME = NULL,
	@email				VARCHAR(60) = NULL,
	@password			VARCHAR(60) = NULL,
	@countryId			INT = NULL,
	@stateId			INT = NULL,
	@active				BIT = NULL,
	@entrySourceId		INT = NULL,
	@age				CHAR(2) = NULL,
	@zipCode			VARCHAR(10) = NULL
)
AS
BEGIN

	-- If @CRUDType belongs to {0,2}, then the parameters shouldn't be null:
	IF (@CRUDType IN (0,2) AND (@firstname IS NULL OR @lastName IS NULL OR @secondLastName IS NULL OR
							  @gender IS NULL OR @email IS NULL OR @active IS NULL OR @completeName IS NULL))
	BEGIN
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
	END

	-- C = {(CREATE, 0)}:
	IF (@CRUDType IN (0))
	BEGIN
		
		DECLARE @cliId INT

		INSERT INTO [dbo].[Clients]
           ([FirstName]
           ,[LastName]
           ,[SecondLastName]
		   ,[Gender]
           ,[Birthday]
           ,[Email]
           ,[Password]
		   ,[AddedDate]
           ,[LastUpdate]
		   ,[CompleteName]
		   ,[CountryId]
		   ,[StateId]
		   ,[Age]
		   ,[ZipCode]
		   ,[Active])
		 VALUES
			   (@firstName
			   ,@lastName
			   ,@secondLastName
			   ,@gender
			   ,@birthday
			   ,@email
			   ,@password
			   ,GETDATE()
			   ,GETDATE()
			   ,@completeName
			   ,@countryId
			   ,@stateId
			   ,@age
			   ,@zipCode
			   ,@active)
	 
		
		SELECT @cliId = SCOPE_IDENTITY()

		--Add client source:

		/*
		 * This code has to be changed, 
		 * it compulsory create CRUD operations for ClientSources.
		 * 
		 * To avoid any error in current applications which uses this SP,
		 * if the @entrySourceId variable is null, then the default value 
		 * is going to be 1 which stands for "Portal PLM 2010"
		 *
		 */

		IF (@entrySourceId IS NULL)
			SET @entrySourceId = 1

		INSERT INTO [dbo].[ClientSources]
           ([EntrySourceId]
           ,[ClientId]
           ,[AddedDate])
		VALUES
           (@entrySourceId
           ,@cliId
           ,GETDATE())

		RETURN @cliId

	END

	-- R = {(Read, 1)}
	IF (@CRUDType IN (1))
	BEGIN
		
		SELECT [ClientId]
			  ,[FirstName]
			  ,[LastName]
			  ,[SecondLastName]
			  ,[Gender]
			  ,[Birthday]
			  ,[Email]
			  ,[Password]
			  ,[AddedDate]
			  ,[LastUpdate]
			  ,[CompleteName]
			  ,[CountryId]
			  ,[StateId]
			  ,[Age]
			  ,[ZipCode]
			  ,[Active]
		  FROM [dbo].[Clients]
		 WHERE [ClientId] = @clientId			

		RETURN @@ROWCOUNT
	END
	
	-- U = {(Update, 2)}
	IF (@CRUDType IN (2))
	BEGIN
		IF(@password IS NOT NULL)
		BEGIN
			UPDATE [dbo].[Clients]
				SET [FirstName] = @firstName
				,[LastName] = @lastName
				,[SecondLastName] = @secondLastName
				,[Gender] = @gender
				,[Birthday] = @birthday
				,[Email] = @email
				,[Password] = @password
				,[LastUpdate] = GETDATE()
				,[CompleteName] = @completeName
				,[CountryId] = @countryId
				,[StateId] = @stateId
				,[Age] = @age
				,[ZipCode] = @zipCode
				,[Active] = @active
			WHERE [ClientId] = @clientId

			RETURN 0
		END
		ELSE
		BEGIN
			UPDATE [dbo].[Clients]
				SET [FirstName] = @firstName
				,[LastName] = @lastName
				,[SecondLastName] = @secondLastName
				,[Gender] = @gender
				,[Birthday] = @birthday
				,[Email] = @email
				,[LastUpdate] = GETDATE()
				,[CompleteName] = @completeName
				,[CountryId] = @countryId
				,[StateId] = @stateId
				,[Age] = @age
				,[ZipCode] = @zipCode
				,[Active] = @active
			WHERE [ClientId] = @clientId
		END
	END	

	-- D = {(Delete, 3)}:
	IF (@CRUDType IN (3))
	BEGIN
		DECLARE @error int

		BEGIN TRAN
		
		--Delete all entries in the [dbo].[ClientSources] table:
		DELETE FROM [dbo].[ClientSources]
		WHERE [ClientId] = @clientId	

		-- Get the value of the @@ERROR global variable:
		SET @error = @@ERROR

		IF (@error != 0)
		BEGIN
			
			ROLLBACK TRAN 
			RAISERROR ('An error ocurred during deleting the entry source associated with the client', 16, 1)
			RETURN -1

		END	


		--Delete all entries in the [dbo].[ClientAddresses] table:
		DELETE FROM [dbo].[ClientAddresses]
		WHERE [ClientId] = @clientId

		-- Get the value of the @@ERROR global variable:
		SET @error = @@ERROR

		IF (@error != 0)
		BEGIN
			
			ROLLBACK TRAN 
			RAISERROR ('An error ocurred during deleting the address associated with the user', 16, 1)
			RETURN -1

		END	
		
		--Delete all entries in the [dbo].[JobPractice] table:
		DELETE FROM [dbo].[JobPractice]
		WHERE [ClientId] = @clientId

		-- Get the value of the @@ERROR global variable:
		SET @error = @@ERROR

		IF (@error != 0)
		BEGIN
			
			ROLLBACK TRAN 
			RAISERROR ('An error ocurred during deleting the place associated with the user', 16, 1)
			RETURN -1

		END

		--Delete all entries in the [dbo].[ClientPractices] table:
		DELETE FROM [dbo].[ClientPractices]
		WHERE [ClientId] = @clientId

		-- Get the value of the @@ERROR global variable:
		SET @error = @@ERROR

		IF (@error != 0)
		BEGIN
			
			ROLLBACK TRAN 
			RAISERROR ('An error ocurred during deleting the professional practice associated with the user', 16, 1)
			RETURN -1

		END
		
		--Delete all entries in the [dbo].[SpecialityClients] table:
		DELETE FROM [dbo].[SpecialityClients]
		WHERE [ClientId] = @clientId

		-- Get the value of the @@ERROR global variable:
		SET @error = @@ERROR

		IF (@error != 0)
		BEGIN
			
			ROLLBACK TRAN 
			RAISERROR ('An error ocurred during deleting the speciality associated with the user', 16, 1)
			RETURN -1

		END

		--Delete all entries in the [dbo].[ProfessionClients] table:
		DELETE FROM [dbo].[ProfessionClients]
		WHERE [ClientId] = @clientId

		-- Get the value of the @@ERROR global variable:
		SET @error = @@ERROR

		IF (@error != 0)
		BEGIN
			
			ROLLBACK TRAN 
			RAISERROR ('An error ocurred during deleting the profession associated with the user', 16, 1)
			RETURN -1

		END		

		--Delete all entries in the [dbo].[Clients] table:
		DELETE FROM [dbo].[Clients]
		 WHERE 	[ClientId]	= @clientId 
		
		-- Get the value of the @@ERROR global variable:
		SET @error = @@ERROR

		IF (@error != 0)
		BEGIN
			
			ROLLBACK TRAN 
			RAISERROR ('An error ocurred during deleting the user', 16, 1)
			RETURN -1

		END

		-- There are no errors, so commit the transaction:
		COMMIT TRAN
		
		RETURN 0

	END


END
go
