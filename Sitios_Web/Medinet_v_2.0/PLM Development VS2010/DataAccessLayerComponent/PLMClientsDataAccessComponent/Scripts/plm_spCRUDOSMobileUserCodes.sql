
IF OBJECT_ID('dbo.plm_spCRUDOSMobileUserCodes') IS NOT NULL
	DROP PROCEDURE dbo.plm_spCRUDOSMobileUserCodes
go

/* 
	Author:			Erick Morales				 
	Object:			dbo.plm_spCRUDOSMobileUserCodes
	
	Description:	Perform the basic operations, or in other words CRUD operations.
					Where CRUD stands for:

						C - Create	{0}
						R - Read	{1}
						U - Update	{2} This operation does not apply
						D - Delete	{3}

	Company:		PLM Latina.
		
 */ 

CREATE PROCEDURE dbo.plm_spCRUDOSMobileUserCodes
(
	@CRUDType			tinyint,
	@osMobileId			tinyint,
	@userId				int,
	@codeId				int,

	@IMEI				varchar(100)
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
	IF (@CRUDType IN (0) AND (@osMobileId IS NULL OR @userId IS NULL OR @codeId IS NULL OR @IMEI IS NULL))
	BEGIN

		RAISERROR ('The current operation [C - Create {0}] requires more parameters (@osMobileId, @userId, @codeId, @IMEI)', 16, 1)
		RETURN -1

	END

	-- If @CRUDType belongs to {3}, then the parameters shouldn't be null:
	IF (@CRUDType IN (3) AND (@osMobileId IS NULL OR @userId IS NULL OR @codeId IS NULL))
	BEGIN

		RAISERROR ('The current operation [D - Delete {3}] requires more parameters (@osMobileId, @userId, @codeId)', 16, 1)
		RETURN -1

	END

	-- C = {(CREATE, 0)}:
	IF (@CRUDType IN (0))
	BEGIN

		INSERT INTO [dbo].[OSMobileUserCodes]
           ([OSMobileId]
           ,[UserId]
           ,[CodeId]
           ,[IMEI])
		VALUES
           (@osMobileId
           ,@userId
           ,@codeId
           ,@IMEI)		

		RETURN 0

	END

	-- R = {(Read, 1)}:
	IF (@CRUDType IN (1))

	BEGIN
		
		SELECT [OSMobileId]
			  ,[UserId]
			  ,[CodeId]
			  ,[IMEI]
		FROM [OSMobileUserCodes]

		WHERE	[OSMobileId]	= @osMobileId AND
				[UserId]		= @userId AND
				[CodeId]		= @codeId
		
		RETURN @@ROWCOUNT

	END

	-- D = {(Delete, 3)}:
	IF (@CRUDType IN (3))
	BEGIN
		
		DELETE FROM [dbo].[OSMobileUserCodes]
		WHERE	[OSMobileId]	= @osMobileId AND
				[UserId]		= @userId AND
				[CodeId]		= @codeId

		RETURN 0

	END

END
go
