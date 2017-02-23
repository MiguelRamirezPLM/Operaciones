IF OBJECT_ID('dbo.plm_spGetUsers') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetUsers
go

/* 
	Author:			Juan Ramirez.				 
	Object:			dbo.plm_spGetUsers
	
	Description:	Retrieve User by NickName, Password or Email.

	Company:		PLM Latina.

	EXECUTE dbo.plm_spGetUsers @nickName = 'esilva2902'

	SELECT * FROM Users WHERE [NickName] = 'esilva2902'

 */ 

CREATE PROCEDURE dbo.plm_spGetUsers
(
	@userId			INT = NULL,
	@nickName		VARCHAR(40) = NULL,
	@password		VARCHAR(100) = NULL,
	@email			VARCHAR(60) = NULL,
	@codeString		VARCHAR(50) = NULL
)
AS
BEGIN

	-- Get user only by @nickName:
	IF (@nickName IS NOT NULL AND @password IS NULL AND @email IS NULL AND @codeString IS NULL AND @userId IS NULL)
	BEGIN

		DECLARE @tmpUsrId int
	
		-- Take the younger entry:
		SELECT @tmpUsrId = MAX(UserId) FROM Users WHERE Active = 1 AND [NickName] = @nickName

		-- Inactivate all entries except the UserId was found:
		UPDATE Users SET Active = 0 WHERE [NickName] = @nickName AND UserId NOT IN (@tmpUsrId)

		-- Return younger entry:
		SELECT [UserId]
			  ,[NickName]
			  ,[NickPrefixId]
			  ,[Password]
			  ,[Active]

		FROM [dbo].[Users]

		WHERE	UserId = @tmpUsrId

		RETURN @@ROWCOUNT
	END

	-- Get user only by @nickName and @password:
	IF(@nickName IS NOT NULL AND @password IS NOT NULL AND @email IS NULL AND @codeString IS NULL AND @userId IS NULL)
	BEGIN

		SELECT U.[UserId],
			   C.[ClientId],
			   CD.[CodeId],
			   U.[NickName],
			   U.[Password],
			   CD.[CodeString],
			   C.[Email],
			   U.[Active] AS UserActive
		  FROM [dbo].[Users] U
			   INNER JOIN [dbo].[ClientUsers] CU ON(U.[UserId] = CU.[UserId])
			   INNER JOIN [dbo].[Clients] C ON(CU.[ClientId] = C.[ClientId])
			   INNER JOIN [dbo].[UserCodes] UC ON(U.[UserId] = UC.[UserId])
			   INNER JOIN [dbo].[Codes] CD ON(UC.[CodeId] = CD.[CodeId])
		 WHERE U.[Active] = 1 AND
			   C.[Active] = 1 AND
			   CD.[CodeStatusId] = 2 AND
			   U.[NickName] = @nickName AND
			   U.[Password] = @password
		
	
		RETURN @@ROWCOUNT
	END
	
	-- Get user only by @email:
	IF(@nickName IS NULL AND @password IS NULL AND @email IS NOT NULL AND @codeString IS NULL AND @userId IS NULL)
	BEGIN

		SELECT U.[UserId],
			   C.[ClientId],
			   CD.[CodeId],
			   U.[NickName],
			   U.[Password],
			   CD.[CodeString],
			   C.[Email],
			   U.[Active] AS UserActive
		  FROM [dbo].[Users] U
			   INNER JOIN [dbo].[ClientUsers] CU ON(U.[UserId] = CU.[UserId])
			   INNER JOIN [dbo].[Clients] C ON(CU.[ClientId] = C.[ClientId])
			   INNER JOIN [dbo].[UserCodes] UC ON(U.[UserId] = UC.[UserId])
			   INNER JOIN [dbo].[Codes] CD ON(UC.[CodeId] = CD.[CodeId])
		 WHERE U.[Active] = 1 AND
			   C.[Active] = 1 AND
			   C.[Email] = @email
		
	
		RETURN @@ROWCOUNT
	END
	
	-- Get user only by @nickName and @codeString:
	IF(@nickName IS NOT NULL AND @password IS NULL AND @email IS NULL AND @codeString IS NOT NULL AND @userId IS NULL)
	BEGIN

		SELECT U.[UserId],
			   C.[ClientId],
			   CD.[CodeId],
			   U.[NickName],
			   U.[Password],
			   CD.[CodeString],
			   C.[Email],
			   U.[Active] AS UserActive
		  FROM [dbo].[Users] U
			   LEFT JOIN [dbo].[ClientUsers] CU ON(U.[UserId] = CU.[UserId])
			   LEFT JOIN [dbo].[Clients] C ON(CU.[ClientId] = C.[ClientId])
			   INNER JOIN [dbo].[UserCodes] UC ON(U.[UserId] = UC.[UserId])
			   INNER JOIN [dbo].[Codes] CD ON(UC.[CodeId] = CD.[CodeId])
		 WHERE U.[Active] = 1 AND
			   C.[Active] = 1 AND
			   U.[NickName] = @nickName AND
			   CD.[CodeString] = @codeString
		
	
		RETURN @@ROWCOUNT
	END

	-- Get user only by @userId:
	IF(@nickName IS NULL AND @password IS NULL AND @email IS NULL AND @codeString IS NULL AND @userId IS NOT NULL)
	BEGIN

		SELECT U.[UserId],
			   C.[ClientId],
			   CD.[CodeId],
			   U.[NickName],
			   U.[Password],
			   CD.[CodeString],
			   C.[Email],
			   U.[Active] AS UserActive
		  FROM [dbo].[Users] U
			   INNER JOIN [dbo].[ClientUsers] CU ON(U.[UserId] = CU.[UserId])
			   INNER JOIN [dbo].[Clients] C ON(CU.[ClientId] = C.[ClientId])
			   INNER JOIN [dbo].[UserCodes] UC ON(U.[UserId] = UC.[UserId])
			   INNER JOIN [dbo].[Codes] CD ON(UC.[CodeId] = CD.[CodeId])
		 WHERE U.[Active] = 1 AND
			   C.[Active] = 1 AND
			   U.[UserId] = @userId 
		
	
		RETURN @@ROWCOUNT
	END

END
go