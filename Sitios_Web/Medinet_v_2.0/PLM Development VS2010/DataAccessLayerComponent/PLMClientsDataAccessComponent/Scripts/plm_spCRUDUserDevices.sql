
IF OBJECT_ID('dbo.plm_spCRUDUserDevices') IS NOT NULL
	DROP PROCEDURE dbo.plm_spCRUDUserDevices
go

/* 
	Author:			Juan Ramírez / Erick Morales Silva.				 
	Object:			dbo.plm_spCRUDUserDevices
	
	Description:	Perform the basic operations, or in other words CRUD operations.
					Where CRUD stands for:

						C - Create	{0}
						R - Read	{1}
						U - Update	{2}
						D - Delete	{3}

	Company:		PLM Latina.

	DECLARE
		@CRUDType		tinyint,
		@addressId		int



	EXECUTE dbo.plm_spCRUDUserDevices
		@CRUDType,			
		@addressId
		
 */ 

CREATE PROCEDURE [dbo].[plm_spCRUDUserDevices]
(
	@CRUDType			TINYINT,
	@userId				INT,
	@codeId				INT,
	@deviceId			TINYINT,
	@macAddress			VARCHAR(MAX) = NULL,
	@active				BIT = NULL
)
AS
BEGIN

	-- If @CRUDType belongs to {1, 2, 3}:
	IF (@CRUDType IN (1, 2))
	BEGIN
	
		RAISERROR ('The current operation cannot be performed', 16, 1)
		RETURN -1
		
	END


	-- If @CRUDType belongs to {0,2}, then the parameters shouldn't be null:
	IF (@CRUDType IN (0) AND (@userId IS NULL OR @codeId IS NULL OR @deviceId IS NULL OR @macAddress IS NULL OR @active IS NULL))
	BEGIN
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
	END
	
	-- If @CRUDType belongs to {3}, then the parameters shouldn't be null:
	IF (@CRUDType IN (3) AND (@userId IS NULL OR @codeId IS NULL OR @deviceId IS NULL))
	BEGIN
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
	END

	-- C = {(CREATE, 0)}:
	IF (@CRUDType IN (0))
	BEGIN

		INSERT INTO [dbo].[UserDevices]
           ([UserId]
           ,[CodeId]
           ,[DeviceId]
           ,[MacAddress]
           ,[AddedDate]
           ,[Active])
		 VALUES
			   (@userId
			   ,@codeId
			   ,@deviceId
			   ,@macAddress
			   ,GETDATE()
			   ,@active)
			
	 
		RETURN 0

	END
	
	IF (@CRUDType IN (3))
	BEGIN
	
		DELETE FROM [dbo].[UserDevices]
		WHERE	UserId		= @userId AND
				CodeId		= @codeId AND
				DeviceId	= @deviceId
				
		RETURN 0
	
	END

	

END

GO


