IF OBJECT_ID('dbo.plm_spCRUDClientPractices') IS NOT NULL
	DROP PROCEDURE dbo.plm_spCRUDClientPractices
go

/* 
	Author:			Juan Ramírez				 
	Object:			dbo.plm_spCRUDClientPractices
	
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
		@practiceId		tinyint



	EXECUTE dbo.plm_spCRUDClientPractices
		@CRUDType,			
		@clientId,
		@practiceId

 */ 

CREATE PROCEDURE dbo.plm_spCRUDClientPractices
(
	@CRUDType				tinyint,
	@clientId				int,
	@practiceId			    tinyint = null
)
AS
BEGIN

	DECLARE 
			@error  INT

	-- If @CRUDType belongs to {1,2}:
	IF (@CRUDType IN (1,2))
	BEGIN
	
		RAISERROR ('The current operation cannot be performed', 16, 1)
		RETURN -1
		
	END

	-- C = {(CREATE, 0)}:
	IF (@CRUDType IN (0))
	BEGIN


		INSERT INTO [dbo].[ClientPractices]
           ([ClientId]
           ,[PracticeId])
     
     	VALUES
           (@clientId
			,@practiceId)
	 
		RETURN 0

	END

	-- D = {(Delete, 3)}:
	IF (@CRUDType IN (3) AND @practiceId IS NOT NULL)
	BEGIN

		BEGIN TRAN
		
		
		--Delete [dbo].[JobPractices]
		DELETE FROM [dbo].[JobPractices]
		 WHERE 	[ClientId]	= @clientId AND 
			[PracticeId]	= @practiceId
		
		--Set error

		SELECT @error = @@error

		IF(@error != 0)
		BEGIN
			
			ROLLBACK TRAN
			
			RAISERROR ('An exception occured during deleted JobPractice', 16, 1)
			RETURN -1
			
		END

		--Delete [dbo].[ClientPractices]
		DELETE FROM [dbo].[ClientPractices]
		 WHERE 	[ClientId]	= @clientId AND 
			[PracticeId]	= @practiceId

		--Set error

		SELECT @error = @@error

		IF(@error != 0)
		BEGIN
			
			ROLLBACK TRAN
			
			RAISERROR ('An exception occured during deleted ClientPractice', 16, 1)
			RETURN -1
			
		END
		
		COMMIT TRAN
		
		RETURN 0
	
	END

	IF (@CRUDType IN (3) AND @practiceId IS NULL)
	BEGIN

		BEGIN TRAN
		

		--Delete [dbo].[JobPractices]
		DELETE FROM [dbo].[JobPractices]
		 WHERE 	[ClientId]	= @clientId 
		
		--Set error

		SELECT @error = @@error

		IF(@error != 0)
		BEGIN
			
			ROLLBACK TRAN
			
			RAISERROR ('An exception occured during deleted JobPractice', 16, 1)
			RETURN -1
			
		END

		--Delete [dbo].[ClientPractices]
		DELETE FROM [dbo].[ClientPractices]
		 WHERE 	[ClientId]	= @clientId 

		--Set error

		SELECT @error = @@error

		IF(@error != 0)
		BEGIN
			
			ROLLBACK TRAN
			
			RAISERROR ('An exception occured during deleted ClientPractice', 16, 1)
			RETURN -1
			
		END
		
		COMMIT TRAN
		
		RETURN 0
	
	END

END
go
