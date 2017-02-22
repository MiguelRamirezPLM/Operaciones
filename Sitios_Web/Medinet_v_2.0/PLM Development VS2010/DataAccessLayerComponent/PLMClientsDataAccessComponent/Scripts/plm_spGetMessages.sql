IF OBJECT_ID('dbo.plm_spGetMessages') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetMessages
go

/* 
	Author:			Juan Ramírez.				 
	Object:			dbo.plm_spGetMessages
	
	Description:	Get messages by Code.

	Company:		PLM México.

	DECLARE
		@messageCode		int

	SELECT 
		@messageCode	

	EXECUTE dbo.plm_spGetMessages @messageCode

 */ 

CREATE PROCEDURE dbo.plm_spGetMessages
(
	@messageCode			INT
)
AS
BEGIN

	SELECT [MessageId]
		  ,[MessageCode]
		  ,[MessageText]
		  ,[Active]
	FROM [dbo].[Messages]
	WHERE [MessageCode] = @messageCode


	RETURN @@ROWCOUNT

END
go