IF OBJECT_ID('dbo.plm_spCRUDUserSessions') IS NOT NULL
	DROP PROCEDURE dbo.plm_spCRUDUserSessions
go

/* 
	Author:			Juan Ramírez.				 
	Object:			dbo.plm_spCRUDUserSessions
	
	Description:	Perform the basic operations, or in other words CRUD operations.
					Where the acronym CRUD means:
						C - Create	{0}
						R - Read	{1}
						U - Update	{2}
						D - Delete	{3}

	Company:		PLM México.

	DECLARE
		@CRUDType		tinyint,
		@UserSessionId	int

	SELECT 
		@CRUDType			= 1,
		@UserSessionId		= 1

	EXECUTE dbo.plm_spCRUDUserSessions
		@CRUDType,			
		@UserSessionId

 */ 

CREATE PROCEDURE dbo.plm_spCRUDUserSessions
(
	@CRUDType				tinyint,
	@userSessionId			int,
	@userId					int = null,
	@sessionId				tinyint = null
)
AS
BEGIN
	-- If @CRUDType belongs to {0, 2, 3}:
	IF (@CRUDType IN (2, 3))
	BEGIN
	
		RAISERROR ('The current operation cannot be performed', 16, 1)
		RETURN -1
		
	END


	-- If @CRUDType belongs to {0}, then the parameters shouldn't be null:
	IF (@CRUDType IN (0) AND ( (		@userId IS NULL OR
										@sessionId IS NULL ) ) )
	BEGIN
	
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
		
	END
	
	-- C = {(Create, 0)}:
	IF (@CRUDType IN (0))
	BEGIN

		INSERT INTO [dbo].[UserSessions]
           ([UserId]
           ,[SessionId]
           ,[SessionDate])
     VALUES
           (@userId
           ,@sessionId
           ,GETDATE())

		RETURN SCOPE_IDENTITY()

		
	END	

	-- R = {(Read, 1)}:
	IF (@CRUDType IN (1))
	BEGIN
	
		SELECT [UserSessionId]
			  ,[UserId]
			  ,[SessionId]
			  ,[SessionDate]
		  FROM [dbo].[UserSessions]
		 WHERE [UserSessionId] = @userSessionId

		RETURN @@ROWCOUNT
		
	END

	-- D = {(Delete, 3)}:
	IF (@CRUDType IN (3))
	BEGIN

		DELETE FROM [dbo].[UserSessions]
		WHERE [UserSessionId] = @userSessionId
		
		RETURN 0

	END

END
go