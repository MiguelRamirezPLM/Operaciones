IF OBJECT_ID('dbo.plm_spGetSessionsByUser') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetSessionsByUser
go

/* 
	Author:			Juan Ramírez.				 
	Object:			dbo.plm_spGetSessionsByUser
	
	Description:	Get all user sessions.

	Company:		PLM México.

	DECLARE
		@userId		int

	SELECT 
		@userId	

	EXECUTE dbo.plm_spGetSessionsByUser @userId

 */ 

CREATE PROCEDURE dbo.plm_spGetSessionsByUser
(
	@userId			INT
)
AS
BEGIN

	SELECT [UserSessionId]
		  ,[UserId]
          ,[SessionId]
          ,[SessionDate]
	FROM [dbo].[UserSessions]		
	WHERE [UserId] = @userId


	RETURN @@ROWCOUNT

END
go