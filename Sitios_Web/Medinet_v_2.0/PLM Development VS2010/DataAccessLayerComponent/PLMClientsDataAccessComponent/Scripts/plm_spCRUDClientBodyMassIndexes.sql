IF OBJECT_ID('dbo.plm_spCRUDClientBodyMassIndexes') IS NOT NULL
	DROP PROCEDURE dbo.plm_spCRUDClientBodyMassIndexes
go

/* 
	Author:			Ramiro Sánchez				 
	Object:			dbo.plm_spCRUDClientBodyMassIndexes
	
	Description:	Perform the basic operations, or in other words CRUD operations.
					Where CRUD stands for:

						C - Create	{0}
						R - Read	{1}
						U - Update	{2}
						D - Delete	{3}

	Company:		PLM Latina.
		
 */ 

CREATE PROCEDURE dbo.plm_spCRUDClientBodyMassIndexes
(
	@CRUDType		tinyint,
	@clientId		int,
	@codeId			int,
	@targetId		tinyint,
	@deviceId		tinyint,
	@indexId		tinyint
)
AS
BEGIN

	-- The parameters shouldn't be null:
	IF (@clientId IS NULL OR @codeId IS NULL OR @targetId IS NULL OR @deviceId IS NULL OR @indexId IS NULL)
	BEGIN
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
	END

	-- C = {(CREATE, 0)}:
	IF (@CRUDType IN (0))
	BEGIN
		
		--Delete all entries in the [dbo].[ClientBodyMassIndexes] table:
		DELETE FROM [dbo].[ClientBodyMassIndexes]
		WHERE [ClientId] = @clientId
			And [CodeId] = @codeId
			And [DeviceId] = @deviceId
			And [TargetId] = @targetId
		
		INSERT INTO [dbo].[ClientBodyMassIndexes]
           ([ClientId]
           ,[CodeId]
           ,[DeviceId]
           ,[TargetId]
           ,[IndexId])
		VALUES
           (@clientId
           ,@codeId
           ,@deviceId
           ,@targetId
           ,@indexId)
           
		RETURN 1

	END
		
	-- D = {(Delete, 3)}:
	IF (@CRUDType IN (3))
	BEGIN
		
		--Delete all entries in the [dbo].[ClientBodyMassIndexes] table:
		DELETE FROM [dbo].[ClientBodyMassIndexes]
		WHERE [ClientId] = @clientId
			And [CodeId] = @codeId
			And [DeviceId] = @deviceId
			And [TargetId] = @targetId
			And [IndexId] = @indexId
		
	END
END
go
