IF OBJECT_ID('dbo.plm_spGetBranch') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetBranch
go

/* 
	Author:			Ramiro Sánchez				 
	Object:			dbo.plm_spGetBranch
	
	Description:	Get Branch information By Id.

	Company:		PLM Latina.
	
	EXECUTE dbo.plm_spGetBranch @branchId = 30

 */

CREATE PROCEDURE dbo.plm_spGetBranch
(
	@branchId		int = Null
)
AS
BEGIN

	SELECT ccb.[BranchId]
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
		,Null As DisplayPharmacies
	FROM CompanyClientBranches ccb
		Inner Join CompanyClients cc ON ccb.CompanyClientId = cc.CompanyClientId
		Left Join Addresses a ON ccb.AddressId = a.AddressId
	WHERE ccb.Active = 1
		And a.Longitude Is Not Null
		And a.Latitude Is Not Null
		And ccb.BranchId = @branchId
	Order By ccb.[BranchName]

	RETURN @@ROWCOUNT

	
END
