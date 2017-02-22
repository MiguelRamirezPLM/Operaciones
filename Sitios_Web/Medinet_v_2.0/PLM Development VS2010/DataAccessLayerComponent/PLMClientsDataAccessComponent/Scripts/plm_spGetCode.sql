IF OBJECT_ID('dbo.plm_spGetCode') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetByCode
go

/* 
	Author:			Pilar Nájera.				 
	Object:			dbo.plm_spGetCode
	
	Description:	Retrieve the CodeString by code.

	Company:		PLM Latina.

	EXECUTE dbo.plm_spGetCodePrefixes

 */ 

CREATE PROCEDURE dbo.plm_spGetByCode
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