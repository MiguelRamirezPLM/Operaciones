
IF OBJECT_ID('dbo.plm_spCRUDClientCodes') IS NOT NULL
	DROP PROCEDURE dbo.plm_spCRUDClientCodes
go

/* 
	Author:			Ramiro Sánchez				 
	Object:			dbo.plm_spCRUDClientCodes
	
	Description:	Perform the basic operations, or in other words CRUD operations.
					Where CRUD stands for:

						C - Create	{0}
						R - Read	{1}
						U - Update	{2}
						D - Delete	{3}

	Company:		PLM Latina.
		
 */ 

CREATE PROCEDURE dbo.plm_spCRUDClientCodes
(
	@CRUDType			TINYINT,
	@clientId			INT,
	@codeId				INT,
	@prescriptionFolio	INT = NULL
)
AS
BEGIN

	-- The parameters shouldn't be null:
	IF (@clientId IS NULL OR @codeId IS NULL)
	BEGIN
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
	END

	-- C = {(CREATE, 0)}:
	IF (@CRUDType IN (0))
	BEGIN

		INSERT INTO [dbo].[ClientCodes]
           ([ClientId]
           ,[CodeId]
           ,[PrescriptionFolio])
		VALUES
           (@clientId
           ,@codeId
           ,@prescriptionFolio)

		RETURN 1

	END

	-- R = {(Read, 1)}
	IF (@CRUDType IN (1))
	BEGIN
		
		SELECT [ClientId]
			  ,[CodeId]
			  ,[PrescriptionFolio]
		  FROM [dbo].[ClientCodes]
		 WHERE [ClientId] = @clientId AND [CodeId] = @codeId

		RETURN @@ROWCOUNT
	END
	
	-- U = {(Update, 2)}
	IF (@CRUDType IN (2))
	BEGIN

		UPDATE [dbo].[ClientCodes]
		   SET [PrescriptionFolio] = @prescriptionFolio
		 WHERE [ClientId] = @clientId AND [CodeId] = @codeId

		RETURN 0

	END	

	-- D = {(Delete, 3)}:
	IF (@CRUDType IN (3))
	BEGIN
		DECLARE @error int

		BEGIN TRAN
		
		--Delete all entries in the [dbo].[ClientCodes] table:
		DELETE FROM [dbo].[ClientCodes]
		WHERE [ClientId] = @clientId AND [CodeId] = @codeId	

		-- Get the value of the @@ERROR global variable:
		SET @error = @@ERROR

		IF (@error != 0)
		BEGIN
			
			ROLLBACK TRAN 
			RAISERROR ('An error ocurred during deleting the entry source associated with the client', 16, 1)
			RETURN -1

		END	
		
		-- There are no errors, so commit the transaction:
		COMMIT TRAN
		
		RETURN 0

	END

END
go
