IF OBJECT_ID('dbo.plm_spGetBranches') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetBranches
go

/* 
	Author:			Ramiro Sánchez				 
	Object:			dbo.plm_spGetBranches
	
	Description:	Get Branch information By CompanyClient or by Agent or by Type.

	Company:		PLM Latina.
	
	EXECUTE dbo.plm_spGetBranches @companyClientId = 30
	EXECUTE dbo.plm_spGetBranches @CCtypeId = 2
	EXECUTE dbo.plm_spGetBranches @stateId = 7
	EXECUTE dbo.plm_spGetBranches @agentId = 1
	EXECUTE dbo.plm_spGetBranches @zoneId = 3
	EXECUTE dbo.plm_spGetBranches @CCtypeId = 2, @zoneId = 3, @agentId = 3

 */ 

CREATE PROCEDURE dbo.plm_spGetBranches
(
	@companyClientId	int = Null,
	@CCtypeId			tinyint = Null,
	@stateId			int = Null,
	@agentId			int = Null,
	@zoneId				tinyint = Null
)
AS
BEGIN

	--Select all branches
	IF(@companyClientId IS NULL AND @CCtypeId IS NULL AND @stateId IS NULL AND @agentId IS NULL AND @zoneId IS NULL)
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
		Order By ccb.[BranchName]

		RETURN @@ROWCOUNT
	END

	--Select By CompanyClient
	IF(@companyClientId IS NOT NULL AND @CCtypeId IS NULL AND @stateId IS NULL AND @agentId IS NULL AND @zoneId IS NULL)
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
		WHERE ccb.CompanyClientId = @companyClientId
			And ccb.Active = 1
		Order By ccb.[BranchName]

		RETURN @@ROWCOUNT	
	END

	--Select By CompanyClientType
	IF(@companyClientId IS NULL AND @CCtypeId IS NOT NULL AND @stateId IS NULL AND @agentId IS NULL AND @zoneId IS NULL)
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
			Left Join Addresses a ON ccb.AddressId = a.AddressId
			Inner Join CompanyClients cc ON ccb.CompanyClientId = cc.CompanyClientId
			Inner Join CompanyClientTypes cct ON cc.CCTypeId = cct.CCTypeId
		WHERE cct.CCTypeId = @CCtypeId
		Order By ccb.[BranchName]

		RETURN @@ROWCOUNT	
	
	END

	--Select By State
	IF(@companyClientId IS NULL AND @CCtypeId IS NULL AND @stateId IS NOT NULL AND @agentId IS NULL AND @zoneId IS NULL)
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
		WHERE a.StateId = @stateId
			And ccb.Active = 1
		Order By ccb.[BranchName]

		RETURN @@ROWCOUNT	
	
	END

	--Select By Agent
	IF(@companyClientId IS NULL AND @CCtypeId IS NULL AND @stateId IS NULL AND @agentId IS NOT NULL AND @zoneId IS NULL)
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
			LEFT JOIN Addresses a ON ccb.AddressId = a.AddressId
			LEFT JOIN ZoneBranches zb ON ccb.BranchId = zb.BranchId
			LEFT JOIN Agents ag ON zb.AgentId = ag.AgentId 
		WHERE ag.AgentId = @agentId
			And ccb.Active = 1
		Order By ccb.[BranchName]

		RETURN @@ROWCOUNT	
	
	END

	--Select By Zone
	IF(@companyClientId IS NULL AND @CCtypeId IS NULL AND @stateId IS NULL AND @agentId IS NULL AND @zoneId IS NOT NULL)
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
			Inner Join ZoneBranches zb On ccb.BranchId = zb.BranchId 
		WHERE zb.ZoneId = @zoneId
			And ccb.Active = 1
		Order By ccb.[BranchName]

		RETURN @@ROWCOUNT	
	
	END

	--Select By CpmpanyClientType and Agent and Zone
	IF(@companyClientId IS NULL AND @CCtypeId IS NOT NULL AND @stateId IS NULL AND @agentId IS NOT NULL AND @zoneId IS NOT NULL)
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
			LEFT JOIN Addresses a ON ccb.AddressId = a.AddressId
			Inner Join ZoneBranches zb On ccb.BranchId = zb.BranchId
			Inner Join CompanyClients cc On ccb.CompanyClientId = cc.CompanyClientId
		WHERE zb.AgentId = @agentId
			And zb.ZoneId = @zoneId
			And cc.CCTypeId = @CCtypeId
			And ccb.Active = 1
		Order By ccb.[BranchName]

		RETURN @@ROWCOUNT	
	
	END
	
END
