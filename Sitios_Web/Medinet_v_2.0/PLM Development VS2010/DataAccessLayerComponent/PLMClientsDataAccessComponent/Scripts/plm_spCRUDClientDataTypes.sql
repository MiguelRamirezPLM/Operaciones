IF OBJECT_ID('dbo.plm_spCRUDClientDataTypes') IS NOT NULL
	DROP PROCEDURE dbo.plm_spCRUDClientDataTypes
go

/* 
	Author:			Ramiro Sánchez				 
	Object:			dbo.plm_spCRUDClientDataTypes
	
	Description:	Perform the basic operations, or in other words CRUD operations.
					Where CRUD stands for:

						C - Create	{0}
						R - Read	{1}
						U - Update	{2}
						D - Delete	{3}

	Company:		PLM Latina.
		
 */ 

CREATE PROCEDURE dbo.plm_spCRUDClientDataTypes
(
	@CRUDType		tinyint,
	@clientId		int,
	@codeId			int,
	@targetId		tinyint,
	@deviceId		tinyint,
	@dataTypeId		tinyint,
	@dataValue		varchar(10)
)
AS
BEGIN

	-- The parameters shouldn't be null:
	IF (@clientId IS NULL OR @codeId IS NULL OR @targetId IS NULL OR @deviceId IS NULL OR @dataTypeId IS NULL)
	BEGIN
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
	END

	-- C = {(CREATE, 0)}:
	IF (@CRUDType IN (0))
	BEGIN
	
		INSERT INTO [dbo].[ClientDataTypes]
           ([ClientId]
           ,[CodeId]
           ,[DeviceId]
           ,[TargetId]
           ,[DataTypeId]
           ,[DataValue])
		VALUES
           (@clientId
           ,@codeId
           ,@deviceId
           ,@targetId
           ,@dataTypeId
           ,@dataValue)
           
		RETURN 1

	END
	
	-- U = {(UPDATE, 2)}:
	IF (@CRUDType IN (2))
	BEGIN

		UPDATE [dbo].[ClientDataTypes]
			SET [DataValue] = @dataValue
		WHERE [ClientId] = @clientId
			And [CodeId] = @codeId
			And [DeviceId] = @deviceId
			And [TargetId] = @targetId
			And [DataTypeId] = @dataTypeId

	END
	
	-- D = {(Delete, 3)}:
	IF (@CRUDType IN (3))
	BEGIN
		
		--Delete all entries in the [dbo].[ClientDataTypes] table:
		DELETE FROM [dbo].[ClientDataTypes]
		WHERE [ClientId] = @clientId
			And [CodeId] = @codeId
			And [DeviceId] = @deviceId
			And [TargetId] = @targetId
			And [DataTypeId] = @dataTypeId
		
	END
END
go
