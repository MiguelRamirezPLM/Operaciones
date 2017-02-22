IF OBJECT_ID('dbo.plm_spGetNickNamePrefixes') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetNickNamePrefixes
go

/* 
	Author:			Juan Ramirez.				 
	Object:			dbo.plm_spGetNickNamePrefixes
	
	Description:	Retrieves the NickName prefix by name or prefixId.

	Company:		PLM Latina.

	EXECUTE dbo.plm_spGetNickNamePrefixes

 */ 

CREATE PROCEDURE dbo.plm_spGetNickNamePrefixes
(
	@prefixName		VARCHAR(15) = null,
	@prefixId		INT = null
)
AS
BEGIN
	
	IF(@prefixName IS NOT NULL AND @prefixId IS NULL)
	BEGIN
		SELECT [NickPrefixId]
			  ,[PrefixId]
			  ,[PrefixName]
			  ,[InitialValue]
			  ,[FinalValue]
			  ,[CurrentNumber]
			  ,[PrefixDescription]
			  ,[Active]
		  FROM [dbo].[NickNamePrefixes]

		 WHERE [PrefixName] = @prefixName AND 
			   [Active] = 1
		
		RETURN @@ROWCOUNT

	END
	IF(@prefixName IS NULL AND @prefixId IS NOT NULL)
	BEGIN
		SELECT [NickPrefixId]
			  ,[PrefixId]
			  ,[PrefixName]
			  ,[InitialValue]
			  ,[FinalValue]
			  ,[CurrentNumber]
			  ,[PrefixDescription]
			  ,[Active]
		  FROM [dbo].[NickNamePrefixes]

		 WHERE [PrefixId] = @prefixId AND 
			   [Active] = 1
		
		RETURN @@ROWCOUNT

	END

END
go