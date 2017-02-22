IF OBJECT_ID('dbo.plm_spCRUDCodePrefixEditions') IS NOT NULL
	DROP PROCEDURE dbo.plm_spCRUDCodePrefixEditions
go

/* 
	Author:			Juan Ramírez				 
	Object:			dbo.plm_spCRUDCodePrefixEditions
	
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



	EXECUTE dbo.plm_spCRUDCodePrefixEditions
		@CRUDType,			
		@editionId
		@prefixId

 */ 

CREATE PROCEDURE dbo.plm_spCRUDEditionCodes
(
	@CRUDType			tinyint,
	@editionId			int,
	@prefixId			int
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
		
		INSERT INTO [dbo].[CodePrefixEditions]
			   ([EditionId]
			   ,[PrefixId])
		 VALUES
			   (@editionId
			   ,@prefixId)
	 
		RETURN 0

	END

	-- R = {(Read, 1)}:
	IF (@CRUDType IN (1))

	BEGIN
		SELECT [EditionId]
			  ,[PrefixId]
		  FROM [dbo].[CodePrefixEditions]
		 WHERE [EditionId]	= @editionId AND 
			   [PrefixId]	= @prefixId
	 
		RETURN @@ROWCOUNT

	END		

	-- D = {(Delete, 3)}:
	IF (@CRUDType IN (3))
	BEGIN
		
		--Delete all entries in the [dbo].[EditionCodes] table:
		DELETE FROM [dbo].[CodePrefixEditions]
		 WHERE [EditionId]	= @editionId AND 
			   [PrefixId]	= @prefixId

		
		RETURN 0

	END


END
go
