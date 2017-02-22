IF OBJECT_ID('dbo.plm_spGetBranchesByPrefixByCompanyType') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetBranchesByPrefixByCompanyType
go

/* 
	Author:			Ramiro Sánchez				 
	Object:			dbo.plm_spGetBranchesByPrefixByCompanyType
	
	Description:	Get Branch information By Prefix and by Company Type

	Company:		PLM Latina.
	
	EXECUTE dbo.plm_spGetBranchesByPrefixByCompanyType @prefix = 'PLMOBEMEDMEX', @ccTypeId = 2

 */ 

CREATE PROCEDURE dbo.plm_spGetBranchesByPrefixByCompanyType
(
	@prefix		varchar(20) = Null,
	@ccTypeId	tinyint = Null
)
AS
BEGIN

	IF (@prefix IS NULL OR @ccTypeId IS NULL)
	BEGIN
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
	END

	SELECT Distinct ccb.[BranchId]
		,ccb.[CompanyClientId]
		,cc.[CompanyName]
		,ccb.[BranchKey]
		,ccb.[BranchName]
		,ccb.[WebPage]
		,ccb.[Email]
		,ccb.[BaseUrl]
		,ccb.[Logo]
		,ccb.AttentionSchedules
		,ccb.HomeService
		,ccb.ServiceType
		,ccb.[Active] As BranchActive
		,a.[AddressId]
		,a.[Street]
		,a.[InternalNumber]
		,a.[Suburb]
		,a.[ZipCode]
		,a.[Location]
		,a.[CountryId]
		,a.[StateId]
		,a.[StateName]
		,a.[Lada]
		,a.[PhoneOne]
		,a.[PhoneTwo]
		,a.[Ext]
		,a.[Latitude]
		,a.[Longitude]
		,a.[Active] As AddressActive
		,dp.DisplayPharmacies
	FROM CompanyClientBranches ccb
		Inner Join CompanyClients cc ON ccb.CompanyClientId = cc.CompanyClientId
		Left Join Addresses a ON ccb.AddressId = a.AddressId
		Inner Join DistributionPharmacies dp ON ccb.CompanyClientId = dp.CompanyClientId
		Inner Join DistributionCodePrefixes dcp On dp.DistributionId = dcp.DistributionId
			And dp.PrefixId = dcp.PrefixId
			And dp.TargetId = dcp.TargetId
		Inner Join CodePrefixes cp ON dcp.PrefixId = cp.PrefixId
	WHERE ccb.Active = 1
		And cc.CCTypeId = @ccTypeId
		And cp.PrefixName = @prefix
		And a.Longitude Is Not Null
		And a.Latitude Is Not Null
	Order By ccb.[BranchName]

	RETURN @@ROWCOUNT
	
END
