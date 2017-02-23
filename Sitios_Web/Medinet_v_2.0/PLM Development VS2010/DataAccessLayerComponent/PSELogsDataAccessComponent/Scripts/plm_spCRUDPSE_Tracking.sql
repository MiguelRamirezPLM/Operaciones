USE [Medinet 20130313]
GO
/****** Object:  StoredProcedure [dbo].[plm_spCRUDPSE_Tracking]    Script Date: 07/05/2013 14:56:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/* 
	Author:			Juan Ramírez / Erick Morales / Ramiro Sánchez
	Object:			dbo.plm_spCRUDPSE_Tracking
	
	Description:	Perform the basic operations, or in other words CRUD operations.
					Where CRUD stands for:

						C - Create	{0}
						R - Read	{1}
						U - Update	{2}
						D - Delete	{3}

					The current SP only performs insert operation - Create {0} - in PSE_Tracking entity and
					if PSE_Tracking record is a text type, it means that the current tracking has attributes, 
					the current script has to insert the search attributes into PSE_TrackingAttributeGroups entity as well.

	Company:		PLM Latina.

	DECLARE 
		@CRUDType				tinyint,
		@trackId				int,
		@codeString				varchar(50),
		@editionId				int,
		@searchDate				datetime,
		@sourceId				tinyint,
		@searchTypeId			tinyint,
		@entityId				int,
		@searchParameters		varchar(255),
		@ClientTrackId			int,
		@AttributeIds			varchar(255)

	SELECT @searchDate = NULL

	EXECUTE dbo.plm_spCRUDPSE_Tracking	@CRUDType			= 0,
										@trackId			= NULL,
										@codeString			= 'EXAMPLE 5',
										@editionId			= 60,
										@searchDate			= 12,
										@sourceId			= 2,
										@searchTypeId		= 2,
										@entityId			= 27,
										@searchParameters	= 'Text=Dolor estomacal',
										@ClientTrackId		= NULL,
										@AttributeIds		= '51'
										@JsonFormat productId = papapa

 */ 

ALTER PROCEDURE [dbo].[plm_spCRUDPSE_Tracking]
(
	@CRUDType				tinyint,
	@trackId				int = NULL,
	@codeString				varchar(50) = NULL,
	@editionId				int = NULL,
	@searchDate				datetime = NULL,
	@sourceId				tinyint = NULL,
	@searchTypeId			tinyint = NULL,
	@entityId				int = NULL,
	@searchParameters		varchar(255) = NULL,
	@ClientTrackId			int = NULL,
	@AttributeIds			varchar(255) = NULL,
	@ParentId				int = NULL,
	@JsonFormat varchar(255)
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
								@editionId IS NULL OR
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
			INSERT INTO [dbo].[PSE_Tracking]
				   ([CodeString]
				   ,[SearchDate]
				   ,[EditionId]
				   ,[SourceId]
				   ,[SearchTypeId]
				   ,[EntityId]
				   ,[SearchParameters]
				   ,[ClientTrackId]
				   ,[ParentId]
				  ,[JSONFormat] )
			 VALUES
				   (@codeString
				   ,@searchDate
				   ,@editionId
				   ,@sourceId
				   ,@searchTypeId
				   ,@entityId
				   ,@searchParameters
				   ,@ClientTrackId
				   ,@ParentId
				   , @JsonFormat )

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

			-- If @AttributeIds variable is not null means that the search, is text type and 
			-- we have to insert all parameters involved in the search:
			IF (@AttributeIds IS NOT NULL)
			BEGIN

				DECLARE @sql varchar(8000)

				-- Create a temp table to establish if the passed attributes are associated to the given EditionId:
				CREATE TABLE #tmpAttributeGroupsExists ( AttributeGroupsExists int )

				-- Verify if the Attributes exists for the given EditionId:
				SET @sql =	'IF (NOT EXISTS(SELECT Distinct EAG.* FROM EditionAttributeGroup EAG ' + char(13) +
							'INNER JOIN AttributeGroup AG ON EAG.AttributeGroupId = AG.AttributeGroupId ' + char(13) +
							'WHERE EAG.EditionId = ' + CONVERT(varchar(10), @editionId) + ' AND AG.AttributeGroupId IN (' + @AttributeIds + ') ) )' + char(13) +
							'BEGIN INSERT INTO #tmpAttributeGroupsExists (AttributeGroupsExists) VALUES (-1) END'

				PRINT 'Verifying attributes: ' + @sql + char(13)

				EXEC (@sql)
				
				-- Take the result:
				SELECT @error = AttributeGroupsExists FROM #tmpAttributeGroupsExists

				-- Destroy the table:
				DROP TABLE #tmpAttributeGroupsExists

				IF (@error != 0)
				BEGIN
					ROLLBACK TRAN 
					RAISERROR ('The passed Attributes don´t be associated to the given EditionId.', 16, 1)
					RETURN -1
				END

				-- Prepare SQL statement to insert PSE_TrackingAttributeGroups:
				SET @sql =	'INSERT INTO [dbo].[PSE_TrackingAttributeGroups] ([TrackId],[AttributeGroupId]) ' + char(13) +
							'SELECT Distinct ' + CONVERT(varchar(10), @currentTrackId) + ', AG.AttributeGroupId ' + char(13) +
							'FROM EditionAttributeGroup EAG' + char(13) +
							'INNER JOIN AttributeGroup AG ON EAG.AttributeGroupId = AG.AttributeGroupId ' + char(13) +
							'WHERE EAG.EditionId = ' + CONVERT(varchar(10), @editionId) + ' AND AG.AttributeGroupId IN (' + @AttributeIds + ') '
		
				PRINT 'Inserting into [dbo].[PSE_TrackingAttributeGroups]: ' + @sql
				
				EXEC (@sql)

				-- Get the value of the @@ERROR global variable:
				SET @error = @@ERROR

				IF (@error != 0)
				BEGIN
					ROLLBACK TRAN 
					RAISERROR ('An error ocurred during inserting attibute tracking.', 16, 1)
					RETURN -1
				END

			END

		-- There are no errors, so commit the transaction:
		COMMIT TRAN
		
		-- Return current IDENTITY value:
		RETURN @currentTrackId
		
	END
END
