IF OBJECT_ID('dbo.plm_spCRUDTargetClientComments') IS NOT NULL
	DROP PROCEDURE dbo.plm_spCRUDTargetClientComments
go

/* 
	Author:			Ramiro Sánchez				 
	Object:			dbo.plm_spCRUDTargetClientComments
	
	Description:	Perform the basic operations, or in other words CRUD operations.
					Where CRUD stands for:

						C - Create	{0}
						R - Read	{1}
						U - Update	{2}
						D - Delete	{3}

	Company:		PLM Latina.
		
 */ 

CREATE PROCEDURE dbo.plm_spCRUDTargetClientComments
(
	@CRUDType		tinyint,
	@clientId		int,
	@codeId			int,
	@targetId		tinyint,
	@deviceId		tinyint,
	@commentId		int
)
AS
BEGIN

	-- If @CRUDType belongs to {1, 2, 3}:
	IF (@CRUDType IN (1, 2, 3))
	BEGIN
	
		RAISERROR ('The current operation cannot be performed', 16, 1)
		RETURN -1
		
	END

	-- The parameters shouldn't be null:
	IF (@clientId IS NULL OR @codeId IS NULL OR @targetId IS NULL OR @deviceId IS NULL OR @commentId IS NULL)
	BEGIN
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
	END

	-- C = {(CREATE, 0)}:
	IF (@CRUDType IN (0))
	BEGIN
		
		INSERT INTO [dbo].[TargetClientComments]
           ([ClientId]
           ,[CodeId]
           ,[DeviceId]
           ,[TargetId]
           ,[CommentId])
		VALUES
           (@clientId
           ,@codeId
           ,@deviceId
           ,@targetId
           ,@commentId)
           
		RETURN 1

	END

END
go
