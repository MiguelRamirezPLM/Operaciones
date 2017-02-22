
IF OBJECT_ID('dbo.plm_spCRUDPrescriptions') IS NOT NULL
	DROP PROCEDURE dbo.plm_spCRUDPrescriptions
go

/* 
	Author:			Ramiro Sánchez				 
	Object:			dbo.plm_spCRUDPrescriptions
	
	Description:	Perform the basic operations, or in other words CRUD operations.
					Where CRUD stands for:

						C - Create	{0}
						R - Read	{1}
						U - Update	{2}
						D - Delete	{3}

	Company:		PLM Latina.
		
 */ 

CREATE PROCEDURE dbo.plm_spCRUDPrescriptions
(
	@CRUDType			TINYINT,
	@prescriptionId		INT,
	@clientId			INT,
	@codeId				INT,
	@patient			VARCHAR(100),
	@folio				VARCHAR(18),
	@comments			VARCHAR(MAX)
)
AS
BEGIN

	-- If @CRUDType belongs to {0,2}, then the parameters shouldn't be null:
	IF (@CRUDType IN (0,2) AND (@clientId IS NULL OR 
			@codeId IS NULL OR @patient IS NULL OR @folio IS NULL))
	BEGIN
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
	END

	-- C = {(CREATE, 0)}:
	IF (@CRUDType IN (0))
	BEGIN

		INSERT INTO [dbo].[Prescriptions]
           ([ClientId]
           ,[CodeId]
           ,[Patient]
		   ,[Folio]
		   ,[Comments])
		VALUES
           (@clientId
           ,@codeId
           ,@patient
		   ,@folio
		   ,@comments)

		RETURN SCOPE_IDENTITY()

	END

	-- R = {(Read, 1)}
	IF (@CRUDType IN (1))
	BEGIN
		
		SELECT [PrescriptionId]
				,[ClientId]
				,[CodeId]
				,[Patient]
				,[Folio]
				,[Comments]
		  FROM [dbo].[Prescriptions]
		 WHERE [PrescriptionId] = @prescriptionId

		RETURN @@ROWCOUNT
	END
	
	-- U = {(Update, 2)}
	IF (@CRUDType IN (2))
	BEGIN

		UPDATE [dbo].[Prescriptions]
		   SET [ClientId] = @clientId
			   ,[CodeId] = @codeId
			   ,[Patient] = @patient
			   ,[Folio] = @Folio
			   ,[Comments] = @comments
		 WHERE [PrescriptionId] = @prescriptionId

		RETURN 0

	END	

	-- D = {(Delete, 3)}:
	IF (@CRUDType IN (3))
	BEGIN
		DECLARE @error int

		BEGIN TRAN
		
		--Delete all entries in the [dbo].[ClientCodes] table:
		DELETE FROM [dbo].[Prescriptions]
		WHERE [PrescriptionId] = @prescriptionId

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
