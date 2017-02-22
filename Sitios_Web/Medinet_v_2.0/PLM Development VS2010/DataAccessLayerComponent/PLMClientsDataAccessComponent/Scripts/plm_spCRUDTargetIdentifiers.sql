IF OBJECT_ID('dbo.plm_spCRUDTargetIdentifiers') IS NOT NULL
	DROP PROCEDURE dbo.plm_spCRUDTargetIdentifiers
go

/* 
	Author:			Ramiro Sánchez				 
	Object:			dbo.plm_spCRUDTargetIdentifiers
	
	Description:	Perform the basic operations, or in other words CRUD operations.
					Where CRUD stands for:

						C - Create	{0}
						R - Read	{1}
						U - Update	{2} This operation does not apply
						D - Delete	{3}

	Company:		PLM Latina.
		
 */ 

CREATE PROCEDURE dbo.plm_spCRUDTargetIdentifiers
(
	@CRUDType			tinyint,
	@targetId			tinyint = Null,
	@deviceId			tinyint = Null
)
AS
BEGIN

	-- If @CRUDType belongs to {2}:
	IF (@CRUDType IN (2))
	BEGIN
	
		RAISERROR ('The current operation cannot be performed', 16, 1)
		RETURN -1
		
	END

	-- If @CRUDType belongs to {0, 3}, then the parameters shouldn't be null:
	IF (@CRUDType IN (0,3) AND (@targetId IS NULL OR @deviceId IS NULL))
	BEGIN
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
	END

	-- C = {(CREATE, 0)}:
	IF (@CRUDType IN (0))
	BEGIN

		INSERT INTO [dbo].[TargetIdentifiers]
           ([TargetId]
           ,[DeviceId])
		VALUES
           (@targetId
           ,@deviceId)	
	 
		RETURN 0

	END

	-- R = {(Read, 1)}:
	IF (@CRUDType IN (1))

	BEGIN

		IF(@targetId IS NOT NULL AND @deviceId IS NULL)
		BEGIN
		
			SELECT	[TargetId]
					,[DeviceId]
			FROM [dbo].[TargetIdentifiers]
			WHERE	[TargetId] = @targetId
	 
		RETURN @@ROWCOUNT
		END

		IF(@targetId IS NOT NULL AND @deviceId IS NOT NULL)
		BEGIN
		
			SELECT	[TargetId]
					,[DeviceId]
			FROM [dbo].[TargetIdentifiers]
			WHERE	[TargetId] = @targetId 
				AND [DeviceId]	= @deviceId
	 
		RETURN @@ROWCOUNT
		END

	END

	-- D = {(Delete, 3)}:
	IF (@CRUDType IN (3))
	BEGIN
		
		DELETE FROM [dbo].[TargetIdentifiers]
		WHERE	[TargetId]		= @targetId AND
				[DeviceId]	= @deviceId	

		RETURN 0

	END

END
go
