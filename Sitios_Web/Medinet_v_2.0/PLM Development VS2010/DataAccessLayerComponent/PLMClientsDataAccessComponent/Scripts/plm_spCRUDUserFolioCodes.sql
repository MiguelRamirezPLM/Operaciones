IF OBJECT_ID('dbo.plm_spCRUDUserFolioCodes') IS NOT NULL
	DROP PROCEDURE dbo.plm_spCRUDUserFolioCodes
go

/* 
	Author:			Juan Ramírez				 
	Object:			dbo.plm_spCRUDUserFolioCodes
	
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
		@codeId			int,
		@folioId		int



	EXECUTE dbo.plm_spCRUDUserFolioCodes
		@CRUDType,			
		@userId,
		@codeId,
		@folioId

 */ 

CREATE PROCEDURE dbo.plm_spCRUDUserFolioCodes
(
	@CRUDType			tinyint,
	@userId				int,
	@codeId				int,
	@folioId			int
)
AS
BEGIN

	-- If @CRUDType belongs to {1,2}:
	IF (@CRUDType IN (1,2))
	BEGIN
	
		RAISERROR ('The current operation cannot be performed', 16, 1)
		RETURN -1
		
	END

	-- C = {(CREATE, 0)}:
	IF (@CRUDType IN (0))
	BEGIN

		INSERT INTO [dbo].[UserFolioCodes]
           ([UserId]
           ,[CodeId]
           ,[FolioId])
     	VALUES
           (@userId
           ,@codeId
		   ,@folioId)
	 
		RETURN 0

	END

	-- D = {(Delete, 3)}:
	IF (@CRUDType IN (3))
	BEGIN
		
		--Delete all entries in the [dbo].[UserFolioCodes] table:
		DELETE FROM [dbo].[UserFolioCodes]
		WHERE [UserId]	= @userId AND 
			  [CodeId]	= @codeId AND
			  [FolioId] = @folioId

		RETURN 0

	END


END
go
