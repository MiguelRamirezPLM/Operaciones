IF OBJECT_ID('dbo.plm_spCRUDAddresses') IS NOT NULL
	DROP PROCEDURE dbo.plm_spCRUDAddresses
go

/* 
	Author:			Juan Ramírez				 
	Object:			dbo.plm_spCRUDAddresses
	
	Description:	Perform the basic operations, or in other words CRUD operations.
					Where CRUD stands for:

						C - Create	{0}
						R - Read	{1}
						U - Update	{2}
						D - Delete	{3}

	Company:		PLM Latina.

	DECLARE
		@CRUDType		tinyint,
		@addressId		int



	EXECUTE dbo.plm_spCRUDAddresses
		@CRUDType,			
		@addressId
		
 */ 

CREATE PROCEDURE dbo.plm_spCRUDAddresses
(
	@CRUDType			TINYINT,
	@addressId			INT,
	@street				VARCHAR(100) = NULL,
	@internalNumber		VARCHAR(10) = NULL,
	@suburb				VARCHAR(40) = NULL,
	@zipCode			VARCHAR(10) = NULL,
	@location			VARCHAR(60) = NULL,
	@countryId			TINYINT = NULL,
	@stateId			INT = NULL,
	@stateName			VARCHAR(100) = NULL,
	@active				BIT = NULL
)
AS
BEGIN

	-- If @CRUDType belongs to {0,2}, then the parameters shouldn't be null:
	IF (@CRUDType IN (0,2) AND (@street IS NULL OR @suburb IS NULL OR @countryId IS NULL OR @active IS NULL))
	BEGIN
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
	END

	-- C = {(CREATE, 0)}:
	IF (@CRUDType IN (0))
	BEGIN


		INSERT INTO [dbo].[Addresses]
           ([Street]
           ,[InternalNumber]
           ,[Suburb]
           ,[ZipCode]
           ,[Location]
           ,[CountryId]
           ,[StateId]
           ,[StateName]
           ,[Active])
		 VALUES
			   (@street
			   ,@internalNumber
			   ,@suburb
			   ,@zipCode
			   ,@location
			   ,@countryId
			   ,@stateId
			   ,@stateName
			   ,@active)
	 
		RETURN SCOPE_IDENTITY()

	END

	-- R = {(Read, 1)}
	IF (@CRUDType IN (1))
	BEGIN
		
		SELECT [AddressId]
			  ,[Street]
			  ,[InternalNumber]
			  ,[Suburb]
			  ,[ZipCode]
			  ,[Location]
			  ,[CountryId]
			  ,[StateId]
			  ,[StateName]
			  ,[Active]
		  FROM [dbo].[Addresses]
		 WHERE [AddressId] = @addressId			

		RETURN @@ROWCOUNT
	END
	
	-- U = {(Update, 2)}
	IF (@CRUDType IN (2))
	BEGIN

		UPDATE [dbo].[Addresses]
		   SET [Street] = @street
			  ,[InternalNumber] = @internalNumber
			  ,[Suburb] = @suburb
			  ,[ZipCode] = @zipCode
			  ,[Location] = @location
			  ,[CountryId] = @countryId
			  ,[StateId] = @stateId
			  ,[StateName] = @stateName
			  ,[Active] = @active
		 WHERE [AddressId] = @addressId

	END	

	-- D = {(Delete, 3)}:
	IF (@CRUDType IN (3))
	BEGIN
		DECLARE @error int

		--Delete all entries in the [dbo].[JobPracticeAddresses] table:
		DELETE FROM [dbo].[JobPracticeAddresses]
		WHERE [AddressId] = @addressId

		-- Get the value of the @@ERROR global variable:
		SET @error = @@ERROR

		IF (@error != 0)
		BEGIN
			
			ROLLBACK TRAN 
			RAISERROR ('An error ocurred during deleting the address associated with the user', 16, 1)
			RETURN -1

		END	
		
		--Delete all entries in the [dbo].[Address] table:
		DELETE FROM [dbo].[Addresses]
		 WHERE 	[AddressId]	= @addressId 
		
		-- Get the value of the @@ERROR global variable:
		SET @error = @@ERROR

		IF (@error != 0)
		BEGIN
			
			ROLLBACK TRAN 
			RAISERROR ('An error ocurred during deleting the address', 16, 1)
			RETURN -1

		END

		-- There are no errors, so commit the transaction:
		COMMIT TRAN
		
		RETURN 0

	END


END
go
