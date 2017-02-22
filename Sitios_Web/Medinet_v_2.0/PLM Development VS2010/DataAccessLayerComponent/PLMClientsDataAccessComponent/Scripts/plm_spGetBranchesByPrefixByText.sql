IF OBJECT_ID('dbo.plm_spGetBranchesByPrefixByText') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetBranchesByPrefixByText
go

/* 
	Author:			Ramiro Sánchez				 
	Object:			dbo.plm_spGetBranchesByPrefixByText
	
	Description:	Get Branch information By Prefix and text.

	Company:		PLM Latina.
	
	EXECUTE dbo.plm_spGetBranchesByPrefixByText @prefix = 'PLMOTCMEX'
	EXECUTE dbo.plm_spGetBranchesByPrefixByText @prefix = 'PLMOTCMEX', @stateId = 23
	EXECUTE dbo.plm_spGetBranchesByPrefixByText @prefix = 'PLMOTCMEX', @stateId = 7, @companyClients = '121' 
	EXECUTE dbo.plm_spGetBranchesByPrefixByText @prefix = 'PLMOTCMEX', @stateId = 7, @companyClients = '121,30', @searchText = 'av'

 */ 

CREATE PROCEDURE dbo.plm_spGetBranchesByPrefixByText
(
	@prefix				varchar(20) = Null,
	@stateId			int = Null,
	@companyClients		varchar(100) = Null,
	@searchText			varchar(200) = Null
)
AS
BEGIN

	IF (@prefix IS NULL)
	BEGIN
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
	END

	DECLARE
		@sql	varchar(MAX)

	SET @sql =	'SELECT Distinct ccb.[BranchId] '		+ char(13) +
				'	,ccb.[CompanyClientId] '			+ char(13) +
				'	,cc.[CompanyName] '					+ char(13) +
				'	,ccb.[BranchKey] '					+ char(13) +
				'	,ccb.[BranchName] '					+ char(13) +
				'	,ccb.[WebPage] '					+ char(13) +
				'	,ccb.[Email] '						+ char(13) +
				'	,ccb.[BaseUrl] '					+ char(13) +
				'	,ccb.[Logo] '						+ char(13) +
				'	,ccb.AttentionSchedules '			+ char(13) +
				'	,ccb.HomeService '					+ char(13) +
				'	,ccb.ServiceType '					+ char(13) +
				'	,ccb.[Active] As BranchActive '		+ char(13) +
				'	,a.[AddressId] '					+ char(13) +
				'	,a.[Street] '						+ char(13) +
				'	,a.[InternalNumber] '				+ char(13) +
				'	,a.[Suburb] '						+ char(13) +
				'	,a.[ZipCode] '						+ char(13) +
				'	,a.[Location] '						+ char(13) +
				'	,a.[CountryId] '					+ char(13) +
				'	,a.[StateId] '						+ char(13) +
				'	,a.[StateName] '					+ char(13) +
				'	,a.[Lada] '							+ char(13) +
				'	,a.[PhoneOne] '						+ char(13) +
				'	,a.[PhoneTwo] '						+ char(13) +
				'	,a.[Ext] '							+ char(13) +
				'	,a.[Latitude] '						+ char(13) +
				'	,a.[Longitude] '					+ char(13) +
				'	,a.[Active] As AddressActive '		+ char(13) +
				'	,dp.DisplayPharmacies '				+ char(13) +
				'FROM CompanyClientBranches ccb '		+ char(13) +
				'	Inner Join CompanyClients cc ON ccb.CompanyClientId = cc.CompanyClientId '				+ char(13) +
				'	Left Join Addresses a ON ccb.AddressId = a.AddressId '									+ char(13) +
				'	Inner Join DistributionPharmacies dp ON ccb.CompanyClientId = dp.CompanyClientId '		+ char(13) +
				'	Inner Join CodePrefixes cp ON dp.PrefixId = cp.PrefixId '								+ char(13) +
				'WHERE cp.PrefixName = ''' + @prefix + ''''													+ char(13) +
				'	And ccb.Active = 1 '																	+ char(13) +
				'	And a.Longitude Is Not Null '															+ char(13) +
				'	And a.Latitude Is Not Null '															+ char(13)
				
				IF (@stateId Is Not Null)
				Begin
					SET @sql = @sql + '	And a.[StateId] = ' + CONVERT(varchar,@stateId) + char(13)
				End

				IF (@searchText Is Not Null)
				Begin

					SET @sql = @sql + '	And '										+ char(13) +
					'	( '															+ char(13) +
					'		(a.[Street] Is Not Null '								+ char(13) +
					'			And a.[Street] Like ''%' + @searchText + '%'' )'	+ char(13) +
					'		OR '													+ char(13) +
					'		(a.[Suburb] Is Not Null '								+ char(13) +
					'			And a.[Suburb] Like ''%' + @searchText + '%'' )'	+ char(13) +
					'		OR '													+ char(13) +
					'		(a.[Location] Is Not Null '								+ char(13) +
					'		And a.[Location] Like ''%' + @searchText + '%'' )'		+ char(13) +
					'	) '															+ char(13)

				End

				IF(@companyClients Is Not Null)
				Begin

					SET @sql = @sql + '	And cc.CompanyClientId In (' + @companyClients + ')'	+ char(13)
	
				End

				SET @sql = @sql + 'Order By ccb.[BranchName] '	+ char(13)

	--print(@sql)
	EXEC (@sql)

	RETURN @@ROWCOUNT
	
END
