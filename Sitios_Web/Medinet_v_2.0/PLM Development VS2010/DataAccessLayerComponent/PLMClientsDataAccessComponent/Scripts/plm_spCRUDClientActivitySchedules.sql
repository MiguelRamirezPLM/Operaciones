IF OBJECT_ID('dbo.plm_spCRUDClientActivitySchedules') IS NOT NULL
	DROP PROCEDURE dbo.plm_spCRUDClientActivitySchedules
go

/* 
	Author:			Ramiro Sánchez				 
	Object:			dbo.plm_spCRUDClientActivitySchedules
	
	Description:	Perform the basic operations, or in other words CRUD operations.
					Where CRUD stands for:

						C - Create	{0}
						R - Read	{1}
						U - Update	{2}
						D - Delete	{3}

	Company:		PLM Latina.
		
 */ 

CREATE PROCEDURE dbo.plm_spCRUDClientActivitySchedules
(
	@CRUDType			tinyint,
	@clientId			int,
	@codeId				int,
	@targetId			tinyint,
	@deviceId			tinyint,
	@activityTypeId		tinyint,
	@activitySchedule	varchar(20)
)
AS
BEGIN

	-- The parameters shouldn't be null:
	IF (@clientId IS NULL OR @codeId IS NULL OR @targetId IS NULL OR @deviceId IS NULL OR @activityTypeId IS NULL)
	BEGIN
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
	END

	-- C = {(CREATE, 0)}:
	IF (@CRUDType IN (0))
	BEGIN

		INSERT INTO [dbo].[ClientActivitySchedules]
           ([ClientId]
           ,[CodeId]
           ,[DeviceId]
           ,[TargetId]
           ,[ActivityTypeId]
           ,[ActivitySchedule])
		VALUES
           (@clientId
           ,@codeId
           ,@deviceId
           ,@targetId
           ,@activityTypeId
           ,@activitySchedule)
           
		RETURN 1

	END
	
	-- U = {(UPDATE, 2)}:
	IF (@CRUDType IN (2))
	BEGIN

		UPDATE [dbo].[ClientActivitySchedules]
			SET [ActivitySchedule] = @activitySchedule
		WHERE [ClientId] = @clientId
			And [CodeId] = @codeId
			And [DeviceId] = @deviceId
			And [TargetId] = @targetId
			And [ActivityTypeId] = @activityTypeId

	END
	
	-- D = {(Delete, 3)}:
	IF (@CRUDType IN (3))
	BEGIN
		
		--Delete all entries in the [dbo].[ClientActivitySchedules] table:
		DELETE FROM [dbo].[ClientActivitySchedules]
		WHERE [ClientId] = @clientId
			And [CodeId] = @codeId
			And [DeviceId] = @deviceId
			And [TargetId] = @targetId
			And [ActivityTypeId] = @activityTypeId
		
	END
END
go
