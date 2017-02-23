IF OBJECT_ID('dbo.plm_spGetWebApplicationUsers') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetWebApplicationUsers
go

/* 
	Author:			Ramiro Sánchez				 
	Object:			dbo.plm_spGetWebApplicationUsers
	
	Description:	Get Web Application user information.

	Company:		PLM Latina.
	
 */ 

CREATE PROCEDURE dbo.plm_spGetWebApplicationUsers
(
	@codeString			varchar(60) = null
)
AS
BEGIN

	IF(@codeString IS NOT NULL)
	BEGIN

		SELECT DISTINCT cc.[CompanyClientId]
			,cc.[CompanyName]
			,cct.[CCTypeId]
			,cct.[CCTypeName]
			,c.[CodeId]
			,c.[CodeString]
			,coc.[EditionId]
			,cc.[Active]
		FROM [dbo].[CompanyClients] cc
			Inner Join [dbo].[CompanyClientTypes] cct On cc.[CCTypeId] = cct.[CCTypeId]
			Inner Join [dbo].[CompanyClientCodes] coc On cc.[CompanyClientId] = coc.[CompanyClientId]
			Inner Join [dbo].[Codes] c On coc.[CodeId] = c.[CodeId]
		WHERE c.[CodeString] = @codeString
			And cc.Active = 1

		RETURN @@ROWCOUNT	
	
	END		
END
go