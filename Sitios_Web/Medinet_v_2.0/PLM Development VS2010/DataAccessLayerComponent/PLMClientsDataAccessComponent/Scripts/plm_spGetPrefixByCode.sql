
IF OBJECT_ID('dbo.plm_spGetPrefixByCode') IS NOT NULL
	DROP PROCEDURE dbo.plm_GetPrefixByCode
go

/* 
	Author:			Pilar Nájera.				 
	Object:			dbo.plm_spGetPrefixByCode
	
	Description:	Retrieve the CodeString by code.

	Company:		PLM Latina.

	EXECUTE dbo.plm_spGetPrefixByCode

 */ 

CREATE PROCEDURE dbo.plm_spGetPrefixByCode
(
	@codeString		VARCHAR(50) = NULL
)
AS
BEGIN
	IF(@codeString IS NOT NULL)
	BEGIN
		SELECT [c].PrefixId
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
		  FROM [dbo].[CodePrefixes] cp
		  
		  INNER JOIN dbo.Codes c ON cp.PrefixId = c.PrefixId

		 WHERE [codeString] = @codeString AND 
			   [Active] = 1
		
		RETURN @@ROWCOUNT
	END
	
END
go