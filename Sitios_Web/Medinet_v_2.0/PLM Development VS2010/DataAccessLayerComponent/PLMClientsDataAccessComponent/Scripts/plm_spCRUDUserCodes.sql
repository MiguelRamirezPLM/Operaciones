IF OBJECT_ID('dbo.plm_spCRUDUserCodes') IS NOT NULL
	DROP PROCEDURE dbo.plm_spCRUDUserCodes
go

/* 
	Author:			Juan Ramírez				 
	Object:			dbo.plm_spCRUDUserCodes
	
	Description:	Perform the basic operations, or in other words CRUD operations.
					Where CRUD stands for:

						C - Create	{0}
						R - Read	{1}
						U - Update	{2}
						D - Delete	{3}

	Company:		PLM Latina.

	DECLARE
		@CRUDType		tinyint,
		@userId			int,
		@codeId			int



	EXECUTE dbo.plm_spCRUDUserCodes
		@CRUDType,			
		@userId
		@codeId

 */ 

CREATE PROCEDURE dbo.plm_spCRUDUserCodes
(
	@CRUDType			tinyint,
	@userId				int,
	@codeId				int,
	@initialDate		datetime = null,
	@finalDate			datetime = null
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

		INSERT INTO [dbo].[UserCodes]
           ([UserId]
           ,[CodeId]
           ,[InitialDate]
           ,[FinalDate])
		VALUES
           (@userId
           ,@codeId
		   ,@initialDate
		   ,@finalDate)
	 
		RETURN 0

	END

	-- R = {(Read, 1)}:
	IF (@CRUDType IN (1))

	BEGIN
		SELECT [UserId]
			  ,[CodeId]
			  ,[InitialDate]
			  ,[FinalDate]
		  FROM [dbo].[UserCodes]
		 WHERE [UserId]	= @userId AND 
			   [CodeId]	= @codeId
	 
		RETURN @@ROWCOUNT

	END		

	-- D = {(Delete, 3)}:
	IF (@CRUDType IN (3))
	BEGIN
		DECLARE @error int

		BEGIN TRAN
		

		--Delete all entries in the [dbo].[UserFolioCodes] table:
		DELETE FROM [dbo].[UserFolioCodes]
		WHERE [UserId]	= @userId AND 
			   [CodeId]	= @codeId

		-- Get the value of the @@ERROR global variable:
		SET @error = @@ERROR

		IF (@error != 0)
		BEGIN
			
			ROLLBACK TRAN 
			RAISERROR ('An error ocurred during deleting the folio associated with the user', 16, 1)
			RETURN -1

		END	
		
		--Delete all entries in the [dbo].[UserCodes] table:
		DELETE FROM [dbo].[UserCodes]
		WHERE [UserId]	= @userId AND 
			   [CodeId]	= @codeId	

		-- Get the value of the @@ERROR global variable:
		SET @error = @@ERROR

		IF (@error != 0)
		BEGIN
			
			ROLLBACK TRAN 
			RAISERROR ('An error ocurred during deleting the code associated with the user', 16, 1)
			RETURN -1

		END

		-- There are no errors, so commit the transaction:
		COMMIT TRAN

		RETURN 0

	END


END
go
