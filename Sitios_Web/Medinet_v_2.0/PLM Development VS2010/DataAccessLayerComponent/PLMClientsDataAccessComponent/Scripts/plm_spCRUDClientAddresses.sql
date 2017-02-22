IF OBJECT_ID('dbo.plm_spCRUDClientAddresses') IS NOT NULL
	DROP PROCEDURE dbo.plm_spCRUDClientAddresses
go

/* 
	Author:			Juan Ramírez				 
	Object:			dbo.plm_spCRUDClientAddresses
	
	Description:	Perform the basic operations, or in other words CRUD operations.
					Where CRUD stands for:

						C - Create	{0}
						R - Read	{1}
						U - Update	{2}
						D - Delete	{3}

	Company:		PLM Latina.

	DECLARE
		@CRUDType		tinyint,
		@clientId		int,
		@addressId		int



	EXECUTE dbo.plm_spCRUDClientAddresses
		@CRUDType,			
		@clientId
		@addressId

 */ 

CREATE PROCEDURE dbo.plm_spCRUDClientAddresses
(
	@CRUDType				tinyint,
	@clientId				int,
	@addressId				int = null
)
AS
BEGIN

	-- If @CRUDType belongs to {2}:
	IF (@CRUDType IN (2))
	BEGIN
	
		RAISERROR ('The current operation cannot be performed', 16, 1)
		RETURN -1
		
	END

	-- C = {(CREATE, 0)}:
	IF (@CRUDType IN (0))
	BEGIN

		IF (EXISTS (SELECT * FROM [ClientAddresses] WHERE [ClientId] = @clientId))
		BEGIN

			RAISERROR ('User cannot have more than one speciality', 16, 1)
			RETURN -1

		END

		INSERT INTO [dbo].[ClientAddresses]
           ([ClientId]
           ,[AddressId])
     	VALUES
           (@clientId
			,@addressId)
	 
		RETURN 0

	END

	-- R = {(Read, 1)}:
	IF (@CRUDType IN (1))

	BEGIN
		SELECT [ClientId]
			  ,[AddressId]
		  FROM [dbo].[ClientAddresses]
		 WHERE [ClientId]	= @clientId AND 
			[AddressId]	= @addressId
	 
		RETURN @@ROWCOUNT

	END		

	-- D = {(Delete, 3)}:
	IF (@CRUDType IN (3) AND @addressId IS NOT NULL)
	BEGIN
		DELETE FROM [dbo].[ClientAddresses]
		 WHERE 	[ClientId]	= @clientId AND 
			[AddressId]	= @addressId

		RETURN 0

	END

	IF (@CRUDType IN (3) AND @addressId IS NULL)
	BEGIN
		DELETE FROM [dbo].[ClientAddresses]
		 WHERE 	[ClientId]	= @clientId 

		RETURN 0

	END


END
go
