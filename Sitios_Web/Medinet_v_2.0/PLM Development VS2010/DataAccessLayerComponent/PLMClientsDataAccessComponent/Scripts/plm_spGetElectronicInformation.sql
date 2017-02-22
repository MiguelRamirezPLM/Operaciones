IF OBJECT_ID('dbo.plm_spGetElectronicInformation') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetElectronicInformation
go

/* 
	Author:			Ramiro Sánchez				 
	Modified:		Angel Eduardo Hernández Aguilar
	Object:			dbo.plm_spGetElectronicInformation
	
	Description:	Get Electronic Information By Type or by Prefix or by Target.

	Company:		PLM Latina.
	
	EXECUTE dbo.plm_spGetElectronicInformation @infoTypeId = 4, @country = 'MEX'
	EXECUTE dbo.plm_spGetElectronicInformation @prefix = 'DEF58', @country = 'MEX'
	EXECUTE dbo.plm_spGetElectronicInformation @prefix = 'DEF58', @targetId = 1, @country = 'MEX'
	EXECUTE dbo.plm_spGetElectronicInformation @prefix = 'prueba quimio', @targetId = 5, @infoTypeId = 4, @country = 'MEX'
	EXECUTE dbo.plm_spGetElectronicInformation @prefix = 'DEF58', @targetId = 1, @infoTypeId = 1, @country = 'MEX'
	EXECUTE dbo.plm_spGetElectronicInformation @prefix = 'PLMDEFMOV2', @targetId = 8, @infoTypeId = 9, @country = 'MEX', @specialityId = 87

 */ 

CREATE PROCEDURE dbo.plm_spGetElectronicInformation
(
	@country			varchar(10),
	 @infoTypeId		tinyint = null,
	 @prefix			varchar(15) = null,
	 @targetId			tinyint = null,
	 @specialityId		smallint = null
)
AS
BEGIN

	IF(@country IS NOT NULL AND @infoTypeId IS NOT NULL AND @prefix IS NULL AND @targetId IS NULL AND @specialityId IS NULL)
	BEGIN
	
		SELECT Distinct ei.[ElectronicId]
			,ei.[InfoTypeId]
			,it.[InfoDescription]
			,ei.[CompanyClientId]
			,ei.[ElectronicTitle]
			,ei.[ElectronicDescription]
			,ei.[PublishedDate]
			,ei.[FileName]
			,ei.[HTMLFileName]
			,ei.[Link]
			,dt.[BaseUrl]
			,Null As ResolutionBaseUrl
			,dt.[Order]
		FROM ElectronicInformation ei
			Inner Join InformationTypes it On ei.InfoTypeId = it.InfoTypeId
			Inner Join CountryTools ct On ei.ElectronicId = ct.ElectronicId
			Inner Join Countries c On ct.CountryId = c.CountryId
			Inner Join DistributionTools dt On ct.ElectronicId = dt.ElectronicId
				And ct.CountryId = dt.CountryId
		Where it.InfoTypeId = @infoTypeId
			And c.ID = @country
			And ei.Active = 1
			And dt.FinalDate >= GETDATE()
		Order By [Order]

		RETURN @@ROWCOUNT	
	
	END
	
	IF(@country IS NOT NULL AND @infoTypeId IS NULL AND @prefix IS NOT NULL AND @targetId IS NULL AND @specialityId IS NULL)
	BEGIN
	
		SELECT Distinct ei.[ElectronicId]
			,ei.[InfoTypeId]
			,it.[InfoDescription]
			,ei.[CompanyClientId]
			,ei.[ElectronicTitle]
			,ei.[ElectronicDescription]
			,ei.[PublishedDate]
			,ei.[FileName]
			,ei.[HTMLFileName]
			,ei.[Link]
			,dt.[BaseUrl]
			,Null As ResolutionBaseUrl
			,dt.[Order]
		FROM ElectronicInformation ei
			Inner Join InformationTypes it On ei.InfoTypeId = it.InfoTypeId
			Inner Join CountryTools ct On ei.ElectronicId = ct.ElectronicId
			Inner Join Countries c On ct.CountryId = c.CountryId
			Inner Join DistributionTools dt On ct.ElectronicId = dt.ElectronicId
				And ct.CountryId = dt.CountryId
			Inner Join DistributionConfigurations dc On dt.DistributionId = dc.DistributionId
				And dt.PrefixId = dc.PrefixId
				And dt.TargetId = dc.TargetId
				And dt.ModuleId = dc.ModuleId
				And dt.SectionId = dc.SectionId
				And dt.ObjectId = dc.ObjectId
			Inner Join DistributionCodePrefixes dcp On dc.DistributionId = dcp.DistributionId
				And dc.PrefixId = dcp.PrefixId
				And dc.TargetId = dcp.TargetId
			Inner Join CodePrefixes cp On dcp.PrefixId = cp.PrefixId
		WHERE cp.PrefixName = @prefix
			And c.ID = @country
			And ei.Active = 1
			And dt.FinalDate >= GETDATE()
		Order By [Order]

		RETURN @@ROWCOUNT	
	
	END

	IF(@country IS NOT NULL AND @infoTypeId IS NULL AND @prefix IS NOT NULL AND @targetId IS NOT NULL AND @specialityId IS NULL)
	BEGIN
	
		SELECT Distinct ei.[ElectronicId]
			,ei.[InfoTypeId]
			,it.[InfoDescription]
			,ei.[CompanyClientId]
			,ei.[ElectronicTitle]
			,ei.[ElectronicDescription]
			,ei.[PublishedDate]
			,ei.[FileName]
			,ei.[HTMLFileName]
			,ei.[Link]
			,dt.[BaseUrl]
			,Null As ResolutionBaseUrl
			,dt.[Order]
		FROM ElectronicInformation ei
			Inner Join InformationTypes it On ei.InfoTypeId = it.InfoTypeId
			Inner Join CountryTools ct On ei.ElectronicId = ct.ElectronicId
			Inner Join Countries c On ct.CountryId = c.CountryId
			Inner Join DistributionTools dt On ct.ElectronicId = dt.ElectronicId
				And ct.CountryId = dt.CountryId
			Inner Join DistributionConfigurations dc On dt.DistributionId = dc.DistributionId
				And dt.PrefixId = dc.PrefixId
				And dt.TargetId = dc.TargetId
				And dt.ModuleId = dc.ModuleId
				And dt.SectionId = dc.SectionId
				And dt.ObjectId = dc.ObjectId
			Inner Join DistributionCodePrefixes dcp On dc.DistributionId = dcp.DistributionId
				And dc.PrefixId = dcp.PrefixId
				And dc.TargetId = dcp.TargetId
			Inner Join CodePrefixes cp On dcp.PrefixId = cp.PrefixId
		WHERE cp.PrefixName = @prefix 
			And dcp.TargetId = @targetId
			And c.ID = @country
			And ei.Active = 1
			And dt.FinalDate >= GETDATE()
		Order By [Order]

		RETURN @@ROWCOUNT	
	
	END
	
	IF(@infoTypeId IS NOT NULL AND @prefix IS NOT NULL AND @targetId IS NOT NULL AND @specialityId IS NULL)
	BEGIN
	
		SELECT Distinct ei.[ElectronicId]
			,ei.[InfoTypeId]
			,it.[InfoDescription]
			,ei.[CompanyClientId]
			,ei.[ElectronicTitle]
			,ei.[ElectronicDescription]
			,ei.[PublishedDate]
			,ei.[FileName]
			,ei.[HTMLFileName]
			,ei.[Link]
			,dt.[BaseUrl]
			,Null As ResolutionBaseUrl
			,dt.[Order]
		FROM ElectronicInformation ei
			Inner Join InformationTypes it On ei.InfoTypeId = it.InfoTypeId
			Inner Join CountryTools ct On ei.ElectronicId = ct.ElectronicId
			Inner Join Countries c On ct.CountryId = c.CountryId
			Inner Join DistributionTools dt On ct.ElectronicId = dt.ElectronicId
				And ct.CountryId = dt.CountryId
			Inner Join DistributionConfigurations dc On dt.DistributionId = dc.DistributionId
				And dt.PrefixId = dc.PrefixId
				And dt.TargetId = dc.TargetId
				And dt.ModuleId = dc.ModuleId
				And dt.SectionId = dc.SectionId
				And dt.ObjectId = dc.ObjectId
			Inner Join DistributionCodePrefixes dcp On dc.DistributionId = dcp.DistributionId
				And dc.PrefixId = dcp.PrefixId
				And dc.TargetId = dcp.TargetId
			Inner Join CodePrefixes cp On dcp.PrefixId = cp.PrefixId
		WHERE cp.PrefixName = @prefix 
			And dcp.TargetId = @targetId
			And ei.InfoTypeId = @infoTypeId
			And c.ID = @country
			And ei.Active = 1
			And dt.FinalDate >= GETDATE()
		Order By [Order]

		RETURN @@ROWCOUNT	
	
	END
	
	IF(@infoTypeId IS NOT NULL AND @prefix IS NOT NULL AND @targetId IS NOT NULL AND @specialityId IS NOT NULL)
	BEGIN
	
	SELECT Distinct ei.[ElectronicId]
			,ei.[InfoTypeId]
			,it.[InfoDescription]
			,ei.[CompanyClientId]
			,ei.[ElectronicTitle]
			,ei.[ElectronicDescription]
			,ei.[PublishedDate]
			,ei.[FileName]
			,ei.[HTMLFileName]
			,ei.[Link]
			,dt.[BaseUrl]
			,Null As ResolutionBaseUrl
			,dt.[Order]
		FROM ElectronicInformation ei
			Inner Join InformationTypes it On ei.InfoTypeId = it.InfoTypeId
			Inner Join CountryTools ct On ei.ElectronicId = ct.ElectronicId
			Inner Join Countries c On ct.CountryId = c.CountryId
			Inner Join DistributionTools dt On ct.ElectronicId = dt.ElectronicId
				And ct.CountryId = dt.CountryId
			Inner Join DistributionConfigurations dc On dt.DistributionId = dc.DistributionId
				And dt.PrefixId = dc.PrefixId
				And dt.TargetId = dc.TargetId
				And dt.ModuleId = dc.ModuleId
				And dt.SectionId = dc.SectionId
				And dt.ObjectId = dc.ObjectId
			Inner Join DistributionCodePrefixes dcp On dc.DistributionId = dcp.DistributionId
				And dc.PrefixId = dcp.PrefixId
				And dc.TargetId = dcp.TargetId
			Inner Join CodePrefixes cp On dcp.PrefixId = cp.PrefixId
			Inner Join SpecialityTools st On ei.ElectronicId = st.ElectronicId
			Inner Join Specialities es On st.SpecialityId = es.SpecialityId
			WHERE cp.PrefixName = @prefix 
			And dcp.TargetId = @targetId
			And c.ID = @country
			And ei.InfoTypeId = @infoTypeId
			And es.SpecialityId = @specialityId
			And ei.Active = 1
			And dt.FinalDate >= GETDATE()
		Order By [Order]

		RETURN @@ROWCOUNT
	END	

END
go