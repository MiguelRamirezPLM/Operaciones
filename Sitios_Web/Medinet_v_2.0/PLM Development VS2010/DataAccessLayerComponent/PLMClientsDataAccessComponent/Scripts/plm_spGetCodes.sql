IF OBJECT_ID('dbo.plm_spGetCodes') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetCodes
go

/* 
	Author:			Juan Ramirez / Ramiro Sánchez
	Object:			dbo.plm_spGetCodes
	
	Description:	Retrieves the Code by CodeString.

	Company:		PLM Latina.

	EXECUTE dbo.plm_spGetCodes

 */ 

CREATE PROCEDURE dbo.plm_spGetCodes
(
	@codeString		VARCHAR(50) = NULL,
	@prefixId		INT = NULL,
	@codePrefix		VARCHAR(20) = NULL,
	@email			VARCHAR(100) = NULL
)
AS
BEGIN
	
	IF(@codeString IS NOT NULL AND @prefixId IS NULL AND @codePrefix IS NULL AND @email IS NULL)
	BEGIN	
		SELECT [CodeId]
			  ,[CodeStatusId]
			  ,[CodeString]
			  ,[PrefixId]
			  ,[Assign]
		  FROM [dbo].[Codes]
		 WHERE [CodeString] = @codeString 
		
		RETURN @@ROWCOUNT
	END
	IF(@codeString IS NULL AND @prefixId IS NOT NULL AND @codePrefix IS NULL AND @email IS NULL)
	BEGIN	
		SELECT TOP 1 [CodeId]
			  ,[CodeStatusId]
			  ,[CodeString]
			  ,[PrefixId]
			  ,[Assign]
		  FROM [dbo].[Codes]
		 WHERE [PrefixId] = @prefixId AND
			   [Assign] = 0
		
		RETURN @@ROWCOUNT
	END
	IF(@codeString IS NULL AND @prefixId IS NULL AND @codePrefix IS NOT NULL AND @email IS NOT NULL)
	BEGIN	
		SELECT TOP 1 co.[CodeId]
			  ,co.[CodeStatusId]
			  ,co.[CodeString]
			  ,co.[PrefixId]
			  ,co.[Assign]
		  FROM [dbo].[Codes] co
			INNER JOIN [CodePrefixes] cp ON co.[PrefixId] = cp.[PrefixId]
			INNER JOIN [ClientCodes] cc ON co.[CodeId] = cc.[CodeId]
			INNER JOIN [Clients] c ON cc.[ClientId] = c.[ClientId]
		 WHERE cp.[PrefixName] = @codePrefix 
			AND c.[Email] = @email
		
		RETURN @@ROWCOUNT
	END	
END
go