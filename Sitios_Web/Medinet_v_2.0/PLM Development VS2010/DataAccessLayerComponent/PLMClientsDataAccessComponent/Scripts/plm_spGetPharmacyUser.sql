IF OBJECT_ID('dbo.plm_spGetPharmacyUser') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetPharmacyUser
go

/* 
	Author:			Ramiro Sánchez				 
	Object:			dbo.plm_spGetPharmacyUser
	
	Description:	Get Pharmacy user information.

	Company:		PLM Latina.
	
 */ 

CREATE PROCEDURE dbo.plm_spGetPharmacyUser
(
	@userName			varchar(60) = null,
	@userPassword		varchar(100) = null,
	@codeString			varchar(60) = null
)
AS
BEGIN

	IF(@userName IS NOT NULL AND @userPassword IS NOT NULL AND @codeString IS NULL)
	BEGIN

		SELECT DISTINCT cu.[CompanyUserId]
			,cu.[CompanyClientId]
			,cu.[FirstName]
			,cu.[LastName]
			,cu.[UserName]
			,cu.[UserPassword]
			,c.[CodeId]
			,c.[CodeString]
			,cu.[Active]
		FROM [dbo].[CompanyUsers] cu
			Inner Join [dbo].[DistributionCompanyUsers] dcu On cu.[CompanyUserId] = dcu.[CompanyUserId]
			Inner Join [dbo].[Codes] c On dcu.[CodeId] = c.[CodeId]
		WHERE cu.[UserName] = @userName
			And cu.[UserPassword] = @userPassword

		RETURN @@ROWCOUNT	
	
	END

	IF(@userName IS NULL AND @userPassword IS NULL AND @codeString IS NOT NULL)
	BEGIN

		SELECT DISTINCT cu.[CompanyUserId]
			,cu.[CompanyClientId]
			,cu.[FirstName]
			,cu.[LastName]
			,cu.[UserName]
			,cu.[UserPassword]
			,c.[CodeId]
			,c.[CodeString]
			,cu.[Active]
		FROM [dbo].[CompanyUsers] cu
			Inner Join [dbo].[DistributionCompanyUsers] dcu On cu.[CompanyUserId] = dcu.[CompanyUserId]
			Inner Join [dbo].[Codes] c On dcu.[CodeId] = c.[CodeId]
		WHERE c.[CodeString] = @codeString

		RETURN @@ROWCOUNT	
	
	END		
END
go