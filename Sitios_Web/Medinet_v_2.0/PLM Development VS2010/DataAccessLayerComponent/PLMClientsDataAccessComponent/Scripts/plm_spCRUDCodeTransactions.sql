IF OBJECT_ID('dbo.plm_spCRUDCodeTransactions') IS NOT NULL
	DROP PROCEDURE dbo.plm_spCRUDCodeTransactions
go

/* 
	Author:			Juan Ramírez				 
	Object:			dbo.plm_spCRUDCodeTransactions
	
	Description:	Perform the basic operations, or in other words CRUD operations.
					Where CRUD stands for:

						C - Create	{0}
						R - Read	{1}
						U - Update	{2}
						D - Delete	{3}

	Company:		PLM Latina.

	DECLARE
		@CRUDType		tinyint,
		@codeId			int,
		@transactionId	tinyint


	EXECUTE dbo.plm_spCRUDCodeTransactions
		@CRUDType,			
		@codeId,
		@transactionId

 */ 

CREATE PROCEDURE dbo.plm_spCRUDCodeTransactions
(
	@CRUDType			tinyint,
	@codeId				int,
	@transactionId		tinyint
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

		INSERT INTO [dbo].[CodeTransactions]
           ([CodeId]
           ,[TransactionId]
           ,[TranDate])
     	VALUES
           (@codeId
           ,@transactionId
		   ,GETDATE())
	 
		RETURN 0

	END

	-- R = {(READ, 1)}:
	IF (@CRUDType IN (1))
	BEGIN

		
		SELECT CodeId,
			   TransactionId,
			   TranDate
		FROM [dbo].[CodeTransactions]
		WHERE [CodeId] = @codeId AND 
			  [TransactionId] = @transactionId
		
		RETURN @@ROWCOUNT

	END

	-- D = {(Delete, 3)}:
	IF (@CRUDType IN (3))
	BEGIN
		
		DELETE FROM [dbo].[CodeTransactions]
		WHERE [CodeId] = @codeId AND 
			  [TransactionId] = @transactionId	

		RETURN 0

	END


END
go
