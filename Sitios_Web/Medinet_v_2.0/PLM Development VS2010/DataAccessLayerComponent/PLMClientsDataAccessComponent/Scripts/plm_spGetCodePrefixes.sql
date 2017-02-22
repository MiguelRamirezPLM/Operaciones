IF OBJECT_ID('dbo.plm_spGetCodePrefixes') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetCodePrefixes
go

/* 
	Author:			Juan Ramirez.				 
	Object:			dbo.plm_spGetCodePrefixes
	
	Description:	Retrieve the Code prefix by name.

	Company:		PLM Latina.

	EXECUTE dbo.plm_spGetCodePrefixes

 */ 

CREATE PROCEDURE dbo.plm_spGetCodePrefixes
(
	@prefixName		VARCHAR(15) = null,
	@parentId		INT = null
)
AS
BEGIN
	IF(@prefixName IS NOT NULL AND @parentId IS NULL)
	BEGIN
		SELECT [PrefixId]
			  ,[ParentId]
			  ,[PrefixTypeId]
			  ,[CompanyClientId]
			  ,[PrefixName]
			  ,[PrefixValue]
			  ,[CurrentValue]
			  ,[FinalValue]
			  ,[PrefixDescription]
			  ,[AddedDate]
			  ,[Active]
		  FROM [dbo].[CodePrefixes]

		 WHERE [PrefixName] = @prefixName AND 
			   [Active] = 1
		
		RETURN @@ROWCOUNT
	END
	IF(@prefixName IS NULL AND @parentId IS NOT NULL)
	BEGIN
		SELECT [PrefixId]
			  ,[ParentId]
			  ,[PrefixTypeId]
			  ,[CompanyClientId]
			  ,[PrefixName]
			  ,[PrefixValue]
			  ,[CurrentValue]
			  ,[FinalValue]
			  ,[PrefixDescription]
			  ,[AddedDate]
			  ,[Active]
		  FROM [dbo].[CodePrefixes]

		 WHERE [ParentId] = @parentId AND 
			   [Active] = 1
		
		RETURN @@ROWCOUNT
	END
END
go