IF OBJECT_ID('dbo.plm_spCheckNickName') IS NOT NULL
	DROP PROCEDURE dbo.plm_spCheckNickName
go

/* 
	Author:			Juan Ramírez.				 
	Object:			dbo.plm_spCheckNickName
	
	Description:	Check if NickName exist in database.

	Company:		PLM Latina.

	
	EXECUTE dbo.plm_spCheckNickName @nickName

	

 */ 

CREATE PROCEDURE dbo.plm_spCheckNickName
(
	@nickName			varchar(40)
)
AS
BEGIN
	
		DECLARE @userNick int
		
		SELECT @userNick = COUNT(*)
		  FROM [dbo].[Users]	
		 WHERE [NickName] = @nickName

		RETURN @userNick

END
go