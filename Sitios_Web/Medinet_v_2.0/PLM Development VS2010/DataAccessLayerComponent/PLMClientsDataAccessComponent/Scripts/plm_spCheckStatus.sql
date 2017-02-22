IF OBJECT_ID('dbo.plm_spCheckStatus') IS NOT NULL
	DROP PROCEDURE dbo.plm_spCheckStatus
go

/* 
	Author:			Juan Ramírez.				 
	Object:			dbo.plm_spCheckStatus
	
	Description:	Validates the status of a user session.

	Company:		PLM México.

	DECLARE
		@userId		int

	SELECT 
		@userId	

	EXECUTE dbo.plm_spCheckStatus @userId

 */ 

CREATE PROCEDURE dbo.plm_spCheckStatus
(
	@userId			INT,
	@sessionTime	INT
)
AS
BEGIN
	
	DECLARE 
		
		@sessionDate   Datetime,
		@sessionId	   Tinyint

	  SET @sessionDate = NULL	

	
	  SELECT TOP 1 @sessionDate = [SessionDate],
			       @sessionId = [SessionId]
	  FROM [dbo].[UserSessions]
	  WHERE [UserId] = @userId
	  ORDER BY [UserSessionId] DESC

	  IF(@sessionDate IS NULL)	
	  BEGIN
			INSERT INTO [dbo].[UserSessions]
			([UserId]
			,[SessionId]
			,[SessionDate])
			VALUES
			   (@userId
			   ,1
			   ,GETDATE())
		
			RETURN SCOPE_IDENTITY()
	  END	
	  ELSE
	  BEGIN
		    IF(@sessionId IN (2))
			BEGIN
				INSERT INTO [dbo].[UserSessions]
				([UserId]
				,[SessionId]
				,[SessionDate])
				VALUES
				   (@userId
				   ,1
				   ,GETDATE())
			
				RETURN SCOPE_IDENTITY()
			END
			ELSE
			BEGIN
				IF(@sessionTime <= DATEDIFF(minute, @sessionDate, GETDATE()) )
				BEGIN

					INSERT INTO [dbo].[UserSessions]
					([UserId]
					,[SessionId]
					,[SessionDate])
					VALUES
					   (@userId
					   ,1
					   ,GETDATE())
				
					RETURN SCOPE_IDENTITY()

				END			
				ELSE
				BEGIN
					RETURN  -1001
				END
			END

	  END

END
go