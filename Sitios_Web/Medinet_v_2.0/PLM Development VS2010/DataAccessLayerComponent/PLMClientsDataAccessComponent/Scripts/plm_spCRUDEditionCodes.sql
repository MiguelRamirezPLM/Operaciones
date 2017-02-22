IF OBJECT_ID('dbo.plm_spCRUDEditionCodes') IS NOT NULL
	DROP PROCEDURE dbo.plm_spCRUDEditionCodes
go

/* 
	Author:			Juan Ramírez				 
	Object:			dbo.plm_spCRUDEditionCodes
	
	Description:	Perform the basic operations, or in other words CRUD operations.
					Where CRUD stands for:

						C - Create	{0}
						R - Read	{1}
						U - Update	{2}
						D - Delete	{3}

	Company:		PLM Latina.

	DECLARE
		@CRUDType		tinyint,
		@editionId		int,
		@codeId			int



	EXECUTE dbo.plm_spCRUDEditionCodes
		@CRUDType,			
		@editionId
		@codeId

 */ 

CREATE PROCEDURE dbo.plm_spCRUDEditionCodes
(
	@CRUDType			tinyint,
	@editionId			int,
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

		INSERT INTO [dbo].[EditionCodes]
           ([EditionId]
           ,[CodeId]
           ,[InitialDate]
           ,[FinalDate])
     VALUES
           (@editionId
           ,@codeId
           ,@initialDate
           ,@finalDate)
	 
		RETURN 0

	END

	-- R = {(Read, 1)}:
	IF (@CRUDType IN (1))

	BEGIN
		SELECT [EditionId]
			  ,[CodeId]
			  ,[InitialDate]
			  ,[FinalDate]
		  FROM [dbo].[EditionCodes]
		 WHERE [EditionId]	= @editionId AND 
			   [CodeId]	= @codeId
	 
		RETURN @@ROWCOUNT

	END		

	-- D = {(Delete, 3)}:
	IF (@CRUDType IN (3))
	BEGIN
		
		--Delete all entries in the [dbo].[EditionCodes] table:
		DELETE FROM [dbo].[EditionCodes]
		 WHERE [EditionId]	= @editionId AND 
			   [CodeId]	= @codeId

		
		RETURN 0

	END


END
go
