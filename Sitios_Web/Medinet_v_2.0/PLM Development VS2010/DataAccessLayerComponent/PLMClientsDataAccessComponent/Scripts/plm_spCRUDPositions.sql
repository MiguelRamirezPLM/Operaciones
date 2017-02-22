IF OBJECT_ID('dbo.plm_spCRUDPositions') IS NOT NULL
	DROP PROCEDURE dbo.plm_spCRUDPositions
go

/* 
	Author:			Juan Ramírez				 
	Object:			dbo.plm_spCRUDPositions
	
	Description:	Perform the basic operations, or in other words CRUD operations.
					Where CRUD stands for:

						C - Create	{0}
						R - Read	{1}
						U - Update	{2}
						D - Delete	{3}

	Company:		PLM Latina.

	DECLARE
		@CRUDType		tinyint,
		@positionId		int



	EXECUTE dbo.plm_spCRUDPositions
		@CRUDType,			
		@positionId
		
 */ 

CREATE PROCEDURE dbo.plm_spCRUDPositions
(
	@CRUDType			TINYINT,
	@positionId			INT,
	@clientId			INT = NULL,
	@practiceId			TINYINT = NULL,
	@jobPlaceId			TINYINT = NULL,
	@positionName		VARCHAR = NULL,
	@active				BIT = NULL
)
AS
BEGIN

	-- If @CRUDType belongs to {0,2}, then the parameters shouldn't be null:
	IF (@CRUDType IN (0,2) AND (@clientId IS NULL OR @practiceId IS NULL OR @jobPlaceId IS NULL OR
							  @positionName IS NULL OR @active IS NULL))
	BEGIN
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
	END

	-- C = {(CREATE, 0)}:
	IF (@CRUDType IN (0))
	BEGIN

	INSERT INTO [dbo].[Positions]
			   ([ClientId]
			   ,[PracticeId]
			   ,[JobPlaceId]
			   ,[PositionName]
			   ,[Active])
     VALUES
           (@clientId
           ,@practiceId
           ,@jobPlaceId
           ,@positionName
           ,@active)
	 
		RETURN SCOPE_IDENTITY()

	END

	-- R = {(Read, 1)}
	IF (@CRUDType IN (1))
	BEGIN
		
		SELECT [PositionId]
			  ,[ClientId]
			  ,[PracticeId]
			  ,[JobPlaceId]
			  ,[PositionName]
			  ,[Active]
		  FROM [dbo].[Positions]
		 WHERE [PositionId] = @positionId			

		RETURN @@ROWCOUNT
	END
	
	-- U = {(Update, 2)}
	IF (@CRUDType IN (2))
	BEGIN

		UPDATE [dbo].[Positions]
		   SET [ClientId] = @clientId
			  ,[PracticeId] = @practiceId
			  ,[JobPlaceId] = @jobPlaceId
			  ,[PositionName] = @positionName
			  ,[Active] = @active
		 WHERE [PositionId] = @positionId

		RETURN 0
	END	

	-- D = {(Delete, 3)}:
	IF (@CRUDType IN (3))
	BEGIN
		
		--Delete all entries in the [dbo].[Clients] table:
		DELETE FROM [dbo].[Positions]
		 WHERE 	[PositionId] = @positionId 
		
			
		RETURN 0

	END


END
go
