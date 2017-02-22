IF OBJECT_ID('dbo.plm_spCheckUserCodes') IS NOT NULL
	DROP PROCEDURE dbo.plm_spCheckUserCodes
go

/* 
	Author:			Juan Ramírez.				 
	Object:			dbo.plm_spCheckUserCodes
	
	Description:	Check if a code was assigned to a user.

	Company:		PLM Latina.

	
	EXECUTE dbo.plm_spCheckUserCodes @folioId

	

 */ 

CREATE PROCEDURE dbo.plm_spCheckUserCodes
(
	@userId		int,
	@codeId		int
)
AS
BEGIN
	
		DECLARE @userCode int
		
		SELECT @userCode = COUNT(*)
		  FROM [dbo].[UserCodes]	
		 WHERE [UserId] = @userId AND
			   [CodeId] = @codeId

		RETURN @userCode

END
go