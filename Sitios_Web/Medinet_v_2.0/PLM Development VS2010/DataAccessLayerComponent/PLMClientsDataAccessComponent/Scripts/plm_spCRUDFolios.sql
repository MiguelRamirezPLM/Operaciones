IF OBJECT_ID('dbo.plm_spCRUDFolios') IS NOT NULL
	DROP PROCEDURE dbo.plm_spCRUDFolios
go

/* 
	Author:			Juan Ramírez				 
	Object:			dbo.plm_spCRUDFolios
	
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



	EXECUTE dbo.plm_spCRUDFolios
		@CRUDType,			
		@codeId
		
 */ 

CREATE PROCEDURE dbo.plm_spCRUDFolios
(
	@CRUDType			TINYINT,
	@folioId			INT,
	@folioString		VARCHAR(50) = NULL,
	@prefixId			INT = NULL,
	@active				BIT = NULL
)
AS
BEGIN

	-- If @CRUDType belongs to {0,2}, then the parameters shouldn't be null:
	IF (@CRUDType IN (0,2) AND (@folioString IS NULL OR @prefixId IS NULL OR  @active IS NULL))
	BEGIN
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
	END

	-- C = {(CREATE, 0)}:
	IF (@CRUDType IN (0))
	BEGIN


		INSERT INTO [dbo].[Folios]
           ([FolioString]
           ,[PrefixId]
           ,[Active])
		 VALUES
			   (@folioString
			   ,@prefixId
			   ,@active)
	 
		RETURN SCOPE_IDENTITY()

	END

	-- R = {(Read, 1)}
	IF (@CRUDType IN (1))
	BEGIN
		
		SELECT [FolioId]
			  ,[FolioString]
			  ,[PrefixId]
			  ,[Active]
		  FROM [dbo].[Folios]
		 WHERE [FolioId] = @folioId			

		RETURN @@ROWCOUNT
	END
	
	-- U = {(Update, 2)}
	IF (@CRUDType IN (2))
	BEGIN

		UPDATE [dbo].[Folios]
		   SET [FolioString] = @folioString
			  ,[PrefixId] = @prefixId
			  ,[Active] = @active
		 WHERE [FolioId] = @folioId			

		RETURN 0

	END	

	-- D = {(Delete, 3)}:
	IF (@CRUDType IN (3))
	BEGIN
		DECLARE @error int

		BEGIN TRAN
		

		--Delete all entries in the [dbo].[UserFolioCodes] table:
		DELETE FROM [dbo].[UserFolioCodes]
		WHERE [FolioId] = @folioId	

		-- Get the value of the @@ERROR global variable:
		SET @error = @@ERROR

		IF (@error != 0)
		BEGIN
			
			ROLLBACK TRAN 
			RAISERROR ('An error ocurred during deleting the user and code associated with the folio', 16, 1)
			RETURN -1

		END	
		
		--Delete all entries in the [dbo].[Users] table:
		DELETE FROM [dbo].[Folios]
		 WHERE [FolioId] = @folioId	
		
		-- Get the value of the @@ERROR global variable:
		SET @error = @@ERROR

		IF (@error != 0)
		BEGIN
			
			ROLLBACK TRAN 
			RAISERROR ('An error ocurred during deleting the folio', 16, 1)
			RETURN -1

		END

		-- There are no errors, so commit the transaction:
		COMMIT TRAN
		
		RETURN 0

	END


END
go
