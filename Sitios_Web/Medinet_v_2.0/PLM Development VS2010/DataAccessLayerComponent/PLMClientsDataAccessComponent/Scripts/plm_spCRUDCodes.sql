IF OBJECT_ID('dbo.plm_spCRUDCodes') IS NOT NULL
	DROP PROCEDURE dbo.plm_spCRUDCodes
go

/* 
	Author:			Juan Ramírez				 
	Object:			dbo.plm_spCRUDCodes
	
	Description:	Perform the basic operations, or in other words CRUD operations.
					Where CRUD stands for:

						C - Create	{0}
						R - Read	{1}
						U - Update	{2}
						D - Delete	{3}

	Company:		PLM Latina.

	DECLARE
		@CRUDType		tinyint,
		@codeId			int



	EXECUTE dbo.plm_spCRUDCodes
		@CRUDType,			
		@codeId
		
 */ 

CREATE PROCEDURE dbo.plm_spCRUDCodes
(
	@CRUDType			TINYINT,
	@codeId				INT,
	@codeString			VARCHAR(50) = NULL,
	@codeStatusId		TINYINT = NULL,
	@prefixId			INT = NULL,
	@assign				BIT = NULL
)
AS
BEGIN

	-- If @CRUDType belongs to {0,2}, then the parameters shouldn't be null:
	IF (@CRUDType IN (0,2) AND (@codeString IS NULL OR @prefixId IS NULL OR @codeStatusId IS NULL 
								OR @assign IS NULL))
	BEGIN
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
	END

	-- C = {(CREATE, 0)}:
	IF (@CRUDType IN (0))
	BEGIN


		INSERT INTO [dbo].[Codes]
           ([CodeStatusId]
           ,[CodeString]
           ,[PrefixId]
           ,[Assign])
		 VALUES
			   (@codeStatusId
			   ,@codeString
			   ,@prefixId
			   ,@assign)
	 
		RETURN SCOPE_IDENTITY()

	END

	-- R = {(Read, 1)}
	IF (@CRUDType IN (1))
	BEGIN
		
		SELECT [CodeId]
			  ,[CodeStatusId]
			  ,[CodeString]
			  ,[PrefixId]
			  ,[Assign]
		  FROM [dbo].[Codes]
		 WHERE [CodeId] = @codeId			

		RETURN @@ROWCOUNT
	END
	
	-- U = {(Update, 2)}
	IF (@CRUDType IN (2))
	BEGIN

		UPDATE [dbo].[Codes]
		   SET [CodeStatusId] = @codeStatusId
			  ,[CodeString] = @codeString
			  ,[PrefixId] = @prefixId
			  ,[Assign] = @assign
		 WHERE [CodeId] = @codeId	

		RETURN 0

	END	

	-- D = {(Delete, 3)}:
	IF (@CRUDType IN (3))
	BEGIN
		DECLARE @error int

		BEGIN TRAN
		
		--Delete all entries in the [dbo].[CodeTransactions] table:
		DELETE FROM [dbo].[CodeTransactions]
		WHERE [CodeId] = @codeId	

		-- Get the value of the @@ERROR global variable:
		SET @error = @@ERROR

		IF (@error != 0)
		BEGIN
			
			ROLLBACK TRAN 
			RAISERROR ('An error ocurred during deleting the Transaction associated with the code', 16, 1)
			RETURN -1

		END	

		--Delete all entries in the [dbo].[UserFolioCodes] table:
		DELETE FROM [dbo].[UserFolioCodes]
		WHERE [CodeId] = @codeId	

		-- Get the value of the @@ERROR global variable:
		SET @error = @@ERROR

		IF (@error != 0)
		BEGIN
			
			ROLLBACK TRAN 
			RAISERROR ('An error ocurred during deleting the folio associated with the code', 16, 1)
			RETURN -1

		END	
		
		--Delete all entries in the [dbo].[UserCodes] table:
		DELETE FROM [dbo].[UserCodes]
		WHERE [CodeId] = @codeId	

		-- Get the value of the @@ERROR global variable:
		SET @error = @@ERROR

		IF (@error != 0)
		BEGIN
			
			ROLLBACK TRAN 
			RAISERROR ('An error ocurred during deleting the code associated with the user', 16, 1)
			RETURN -1

		END
		
		--Delete all entries in the [dbo].[Users] table:
		DELETE FROM [dbo].[Codes]
		 WHERE [CodeId] = @codeId	
		
		-- Get the value of the @@ERROR global variable:
		SET @error = @@ERROR

		IF (@error != 0)
		BEGIN
			
			ROLLBACK TRAN 
			RAISERROR ('An error ocurred during deleting the code', 16, 1)
			RETURN -1

		END

		-- There are no errors, so commit the transaction:
		COMMIT TRAN
		
		RETURN 0

	END


END
go
