
IF OBJECT_ID('dbo.plm_spCRUDStateOsMobileUserCodes') IS NOT NULL
	DROP PROCEDURE dbo.plm_spCRUDStateOsMobileUserCodes
go

/* 
	Author:			Juan Manuel Ramírez				 
	Object:			dbo.plm_spCRUDStateOsMobileUserCodes
	
	Description:	Perform the basic operations, or in other words CRUD operations.
					Where CRUD stands for:

						C - Create	{0}
						R - Read	{1}
						U - Update	{2} This operation does not apply
						D - Delete	{3}

	Company:		PLM Latina.
		
 */ 

CREATE PROCEDURE dbo.plm_spCRUDStateOsMobileUserCodes
(
	@CRUDType			tinyint,
	@stateId			int,
	@osMobileId			tinyint,
	@userId				int,
	@codeId				int
)
AS
BEGIN

	-- If @CRUDType belongs to {2}:
	IF (@CRUDType IN (2))
	BEGIN
	
		RAISERROR ('The current operation [U - Update {2}] cannot be performed', 16, 1)
		RETURN -1
		
	END

	-- If @CRUDType belongs to {0}, then the parameters shouldn't be null:
	IF (@CRUDType IN (0) AND (@stateId IS NULL OR @osMobileId IS NULL OR @userId IS NULL OR @codeId IS NULL))
	BEGIN

		RAISERROR ('The current operation [C - Create {0}] requires more parameters (@stateId, @osMobileId, @userId, @codeId)', 16, 1)
		RETURN -1

	END

	-- If @CRUDType belongs to {3}, then the parameters shouldn't be null:
	IF (@CRUDType IN (3) AND (@stateId IS NULL OR @osMobileId IS NULL OR @userId IS NULL OR @codeId IS NULL))
	BEGIN

		RAISERROR ('The current operation [D - Delete {3}] requires more parameters (@stateId, @osMobileId, @userId, @codeId)', 16, 1)
		RETURN -1

	END

	-- C = {(CREATE, 0)}:
	IF (@CRUDType IN (0))
	BEGIN

		INSERT INTO [dbo].[StateOSMobileUserCodes]
           ([StateId]
           ,[OSMobileId]
           ,[UserId]
           ,[CodeId])
		VALUES
           (@stateId
           ,@osMobileId
           ,@userId
           ,@codeId)		

		RETURN 0

	END

	-- R = {(Read, 1)}:
	IF (@CRUDType IN (1))

	BEGIN
		
		SELECT [StateId]
			  ,[OSMobileId]
			  ,[UserId]
			  ,[CodeId]
		  FROM [dbo].[StateOSMobileUserCodes]
		WHERE	[StateId]		= @stateId AND
				[OSMobileId]	= @osMobileId AND
				[UserId]		= @userId AND
				[CodeId]		= @codeId
		
		RETURN @@ROWCOUNT

	END

	-- D = {(Delete, 3)}:
	IF (@CRUDType IN (3))
	BEGIN
		
		DELETE FROM [dbo].[StateOSMobileUserCodes]
		WHERE	[StateId]		= @stateId AND
				[OSMobileId]	= @osMobileId AND
				[UserId]		= @userId AND
				[CodeId]		= @codeId

		RETURN 0

	END

END
go
