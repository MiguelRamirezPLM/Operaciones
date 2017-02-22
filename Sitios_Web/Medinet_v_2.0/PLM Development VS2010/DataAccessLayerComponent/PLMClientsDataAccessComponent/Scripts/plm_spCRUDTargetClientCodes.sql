IF OBJECT_ID('dbo.plm_spCRUDTargetClientCodes') IS NOT NULL
	DROP PROCEDURE dbo.plm_spCRUDTargetClientCodes
go

/* 
	Author:			Ramiro Sánchez				 
	Object:			dbo.plm_spCRUDTargetClientCodes
	
	Description:	Perform the basic operations, or in other words CRUD operations.
					Where CRUD stands for:

						C - Create	{0}
						R - Read	{1}
						U - Update	{2}
						D - Delete	{3}

	Company:		PLM Latina.
		
 */ 

CREATE PROCEDURE [dbo].[plm_spCRUDTargetClientCodes]
(
	@CRUDType			TINYINT,
	@clientId			INT,
	@codeId				INT,
	@targetId			TINYINT,
	@deviceId			TINYINT,
	@hwIdentifier		VARCHAR(MAX) = NULL,
	@installationDate	datetime = null,
	@active				bit = null
)
AS
BEGIN

	-- If @CRUDType belongs to {2}:
	IF (@CRUDType IN (1))
	BEGIN
	
		RAISERROR ('The current operation cannot be performed', 16, 1)
		RETURN -1
		
	END

	-- If @CRUDType belongs to {0}, then the parameters shouldn't be null:
	IF (@CRUDType IN (0) AND (@clientId IS NULL OR @codeId IS NULL OR 
			@targetId IS NULL OR @deviceId IS NULL))
	BEGIN
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
	END

	-- If @CRUDType belongs to {2}, then the parameters shouldn't be null:
	IF (@CRUDType IN (0) AND (@clientId IS NULL OR @codeId IS NULL OR 
			@targetId IS NULL OR @deviceId IS NULL))
	BEGIN
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
	END

	-- If @CRUDType belongs to {3}, then the parameters shouldn't be null:
	IF (@CRUDType IN (3) AND (@clientId IS NULL OR @codeId IS NULL OR 
			@targetId IS NULL OR @deviceId IS NULL))
	BEGIN
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
	END

	-- C = {(CREATE, 0)}:
	IF (@CRUDType IN (0))
	BEGIN

		INSERT INTO [dbo].[TargetClientCodes]
           ([ClientId]
           ,[CodeId]
           ,[TargetId]
           ,[DeviceId]
           ,[HWIdentifier]
		   ,[InstallationDate]
		   ,[Active])
     VALUES
           (@clientId
           ,@codeId
           ,@targetId
           ,@deviceId
           ,@hwIdentifier
		   ,GETDATE()
		   ,1)
	 
		RETURN 0

	END

	-- U = {(UPDATE, 2)}:
	IF (@CRUDType IN (2))
	BEGIN
		UPDATE [dbo].[TargetClientCodes]
			SET [HWIdentifier] = @hwIdentifier
		   ,[InstallationDate] = @installationDate
		   ,[Active] = @active
		 WHERE [ClientId] = @clientId
			AND [CodeId] = @codeId
			AND [TargetId] = @targetId
			AND [DeviceId] = @deviceId
	END
	
	-- D = {(DELETE, 3)}:
	IF (@CRUDType IN (3))
	BEGIN
	
		DELETE FROM [dbo].[TargetClientCodes]
		WHERE [ClientId] = @clientId
			AND [CodeId] = @codeId
			AND [TargetId] = @targetId
			AND [DeviceId] = @deviceId
				
		RETURN 0
	
	END

END

GO


