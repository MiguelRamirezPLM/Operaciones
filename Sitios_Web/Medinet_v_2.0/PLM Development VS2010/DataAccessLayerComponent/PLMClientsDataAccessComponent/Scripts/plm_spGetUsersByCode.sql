IF OBJECT_ID('dbo.plm_spGetUsersByCode') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetUsersByCode
go

/* 
	Author:			Juan Ramirez.				 
	Object:			dbo.plm_spGetUsersByCode
	
	Description:	Retrieve User by NickName, Password or Email.

	Company:		PLM Latina.

	EXECUTE dbo.plm_spGetUsersByCode

 */ 

CREATE PROCEDURE dbo.plm_spGetUsersByCode
(
	@userId			INT = NULL,
	@codeString		VARCHAR(50) = NULL,
	@nickName		VARCHAR(40) = NULL
	
)
AS
BEGIN

	IF(@nickName IS NOT NULL AND @codeString IS NOT NULL AND @userId IS NULL)
	BEGIN

		SELECT U.[UserId],
			   CD.[CodeId],
			   U.[NickName],
			   U.[Password],
			   CD.[CodeString],
			   CD.[CodeStatusId]
		  FROM [dbo].[Users] U
			   INNER JOIN [dbo].[UserCodes] UC ON(U.[UserId] = UC.[UserId])
			   INNER JOIN [dbo].[Codes] CD ON(UC.[CodeId] = CD.[CodeId])
		 WHERE U.[Active] = 1 AND
			   U.[NickName] = @nickName AND
			   CD.[CodeString] = @codeString
		
	
		RETURN @@ROWCOUNT
	END
	
	IF(@nickName IS NULL AND @codeString IS NOT NULL AND @userId IS NULL)
	BEGIN

		SELECT U.[UserId],
			   CD.[CodeId],
			   U.[NickName],
			   U.[Password],
			   CD.[CodeString],
			   CD.[CodeStatusId]
		  FROM [dbo].[Users] U
			   INNER JOIN [dbo].[UserCodes] UC ON(U.[UserId] = UC.[UserId])
			   INNER JOIN [dbo].[Codes] CD ON(UC.[CodeId] = CD.[CodeId])
		 WHERE U.[Active] = 1 AND
			   CD.[CodeString] = @codeString
		
	
		RETURN @@ROWCOUNT
	END

	IF(@nickName IS NULL AND @codeString IS NULL AND @userId IS NOT NULL)
	BEGIN

		SELECT U.[UserId],
			   CD.[CodeId],
			   U.[NickName],
			   U.[Password],
			   CD.[CodeString],
			   CD.[CodeStatusId]
		  FROM [dbo].[Users] U
			   INNER JOIN [dbo].[UserCodes] UC ON(U.[UserId] = UC.[UserId])
			   INNER JOIN [dbo].[Codes] CD ON(UC.[CodeId] = CD.[CodeId])
		 WHERE U.[Active] = 1 AND
			   U.[UserId] = @userId
		
	
		RETURN @@ROWCOUNT
	END

END
go