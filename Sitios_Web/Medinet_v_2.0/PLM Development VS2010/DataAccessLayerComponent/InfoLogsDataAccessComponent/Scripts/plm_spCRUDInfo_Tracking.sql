
IF OBJECT_ID('dbo.plm_spCRUDInfo_Tracking') IS NOT NULL
	DROP PROCEDURE dbo.plm_spCRUDInfo_Tracking
go

/* 
	Author:			Erick Morales.				 
	Object:			dbo.plm_spCRUDInfo_Tracking
	
	Description:	Perform the basic operations, or in other words CRUD operations.
					Where CRUD stands for:

						C - Create	{0}
						R - Read	{1}
						U - Update	{2}
						D - Delete	{3}

					The current SP only performs insert operation - Create {0} - in [PLMClients].dbo.Info_Tracking entity.

	Company:		PLM Latina.

 */ 

CREATE PROCEDURE dbo.plm_spCRUDInfo_Tracking
(
	@CRUDType				tinyint,
	@trackId				int = NULL,
	@codeString				varchar(50) = NULL,
	@searchDate				datetime = NULL,
	@sourceId				tinyint = NULL,
	@searchTypeId			tinyint = NULL,
	@entityId				int = NULL,
	@searchParameters		varchar(255) = NULL,
	@JSONFormat				varchar(255) = NULL,
	@parentId				int = NULL
)
AS
BEGIN

	-- If @CRUDType belongs to {1, 2, 3}:
	IF (@CRUDType IN (1, 2, 3))
	BEGIN
	
		RAISERROR ('The current operation cannot be performed', 16, 1)
		RETURN -1
		
	END

	-- If @CRUDType belongs to {0}, then the parameters shouldn't be null:
	IF (@CRUDType IN (0) AND ( (@codeString IS NULL OR
								@sourceId IS NULL OR 
								@searchTypeId IS NULL OR 
								@entityId	IS NULL OR
								@searchParameters IS NULL)))
	BEGIN
	
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
		
	END

	-- R = {(Create, 0)}:
	IF (@CRUDType IN (0))
	BEGIN

		IF(@searchDate IS NULL)
		BEGIN
			SET @searchDate = GETDATE()
		END

		DECLARE 
			@currentTrackId		int,
			@error				int

		BEGIN TRAN

			-- Insert current passed values:
			INSERT INTO [dbo].[Info_Tracking]
				   ([ParentId]
				   ,[CodeString]
				   ,[SearchDate]
				   ,[SearchParameters]
				   ,[JSONFormat]
				   ,[SourceId]
				   ,[SearchTypeId]
				   ,[EntityId])
			 VALUES
				   (@parentId
				   ,@codeString
				   ,@searchDate
				   ,@searchParameters
				   ,@JSONFormat
				   ,@sourceId
				   ,@searchTypeId
				   ,@entityId)

			-- Get current identity inserted value:
			SET @currentTrackId = SCOPE_IDENTITY()

			-- Get the value of the @@ERROR global variable:
			SET @error = @@ERROR

			IF (@error != 0)
			BEGIN
				
				ROLLBACK TRAN 
				RAISERROR ('An error ocurred during inserting tracking.', 16, 1)
				RETURN -1

			END

		-- There are no errors, so commit the transaction:
		COMMIT TRAN
		
		-- Return current IDENTITY value:
		RETURN @currentTrackId
		
	END

END
go