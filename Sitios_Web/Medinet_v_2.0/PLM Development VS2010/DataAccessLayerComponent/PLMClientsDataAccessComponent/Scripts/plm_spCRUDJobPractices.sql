IF OBJECT_ID('dbo.plm_spCRUDJobPractices') IS NOT NULL
	DROP PROCEDURE dbo.plm_spCRUDJobPractices
go

/* 
	Author:			Juan Ramírez				 
	Object:			dbo.plm_spCRUDJobPractices
	
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
		@practiceId		tinyint,
		@jobPlaceId		tinyint,
		@addressId		tinyint


	EXECUTE dbo.plm_spCRUDJobPractices
		@CRUDType,			
		@clientId,
		@practiceId,
		@jobPlaceId,
		@addressId


 */ 

CREATE PROCEDURE dbo.plm_spCRUDJobPractices
(
	@CRUDType				tinyint,
	@clientId				int,
	@practiceId			    tinyint,
	@jobPlaceId				tinyint = null,
	@institutionName		varchar(100) = null,
	@positionName			varchar(50) = null

)
AS
BEGIN

	-- If @CRUDType belongs to {0,2}, then the parameters shouldn't be null:
	IF (@CRUDType IN (0,2) AND (@clientId IS NULL OR @practiceId IS NULL OR @jobPlaceId IS NULL))
	BEGIN
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
	END

	-- C = {(CREATE, 0)}:
	IF (@CRUDType IN (0))
	BEGIN


		INSERT INTO [dbo].[JobPractices]
           ([ClientId]
           ,[PracticeId]
           ,[JobPlaceId]
           ,[InstitutionName]
		   ,[PositionName])
        VALUES
           (@clientId
           ,@practiceId
           ,@jobPlaceId
           ,@institutionName
		   ,@positionName)
	 
		RETURN 0

	END

	-- R = {(Read, 1)}
	IF(@CRUDType IN (1))	
	BEGIN
		SELECT [ClientId]
			  ,[PracticeId]
			  ,[JobPlaceId]
			  ,[InstitutionName]
			  ,[PositionName]
		  FROM [dbo].[JobPractices]
		 WHERE [ClientId]	= @clientId AND 
			[PracticeId]	= @practiceId AND
			[JobPlaceId]	= @jobPlaceId

		RETURN @@ROWCOUNT
	
	END

	-- U = {(Update, 2)}
	IF(@CRUDType IN (2))	
	BEGIN
		UPDATE [dbo].[JobPractices]
		   SET [InstitutionName] = @institutionName,
			   [PositionName] = @positionName
		 WHERE [ClientId]	= @clientId AND 
				[PracticeId]	= @practiceId AND
				[JobPlaceId]	= @jobPlaceId
		
		RETURN 0
	END

	-- D = {(Delete, 3)}:
	IF (@CRUDType IN (3))
	BEGIN
		DECLARE @error int
		
		IF(@clientId IS NOT NULL AND
		   @practiceId IS NOT NULL AND
		   @jobPlaceId IS NOT NULL)
		BEGIN
			
	
			BEGIN TRAN

				--Delete all entries in the [dbo].[JobPractices] table:			
				DELETE FROM [dbo].[JobPractices]
				 WHERE 	[ClientId]	= @clientId AND 
					[PracticeId]	= @practiceId AND
					[JobPlaceId]	= @jobPlaceId 

				-- Get the value of the @@ERROR global variable:
				SET @error = @@ERROR

				IF (@error != 0)
				BEGIN
					
					ROLLBACK TRAN 
					RAISERROR ('An error ocurred during deleting the Practice', 16, 1)
					RETURN -1

				END

				-- There are no errors, so commit the transaction:
				COMMIT TRAN
		
				RETURN 0
		END
		
		IF(@clientId IS NOT NULL AND
		   @practiceId IS NOT NULL AND
		   @jobPlaceId IS NULL)
		BEGIN
				
			BEGIN TRAN

				--Delete all entries in the [dbo].[JobPractices] table:			
				DELETE FROM [dbo].[JobPractices]
				 WHERE 	[ClientId]	= @clientId AND 
					[PracticeId]	= @practiceId 

				-- Get the value of the @@ERROR global variable:
				SET @error = @@ERROR

				IF (@error != 0)
				BEGIN
					
					ROLLBACK TRAN 
					RAISERROR ('An error ocurred during deleting the Practice', 16, 1)
					RETURN -1

				END

				-- There are no errors, so commit the transaction:
				COMMIT TRAN
		
				RETURN 0
		END
		

	END


END
go
