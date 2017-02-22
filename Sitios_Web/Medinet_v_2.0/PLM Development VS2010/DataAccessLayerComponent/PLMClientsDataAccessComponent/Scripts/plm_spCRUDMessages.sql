IF OBJECT_ID('dbo.plm_spCRUDMessages') IS NOT NULL
	DROP PROCEDURE dbo.plm_spCRUDMessages
go

/* 
	Author:			Juan Ramírez.				 
	Object:			dbo.plm_spCRUDMessages
	
	Description:	Perform the basic operations, or in other words CRUD operations.
					Where the acronym CRUD means:
						C - Create	{0}
						R - Read	{1}
						U - Update	{2}
						D - Delete	{3}

	Company:		PLM México.

	DECLARE
		@CRUDType		tinyint,
		@messageId		int

	SELECT 
		@CRUDType			= 1,
		@messageId			= 1

	EXECUTE dbo.plm_spCRUDMessages
		@CRUDType,			
		@messageId

 */ 

CREATE PROCEDURE dbo.plm_spCRUDMessages
(
	@CRUDType				tinyint,
	@messageId				int
)
AS
BEGIN
	-- If @CRUDType belongs to {0, 2, 3}:
	IF (@CRUDType IN (0, 2, 3))
	BEGIN
	
		RAISERROR ('The current operation cannot be performed', 16, 1)
		RETURN -1
		
	END


	-- R = {(Read, 1)}:
	IF (@CRUDType IN (1))
	BEGIN
	
		SELECT [MessageId]
			  ,[MessageText]
			  ,[Active]
		  FROM [dbo].[Messages]
		 WHERE [MessageId] = @messageId

		RETURN @@ROWCOUNT
		
	END

END
go