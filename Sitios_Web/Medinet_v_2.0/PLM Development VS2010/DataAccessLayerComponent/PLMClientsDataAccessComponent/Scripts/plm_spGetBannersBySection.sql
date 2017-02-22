IF OBJECT_ID('dbo.plm_spGetBannersBySection') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetBannersBySection
go

/* 
	Author:			Ramiro Sánchez				 
	Object:			dbo.plm_spGetBannersBySection
	
	Description:	Get Banners By Prefix and Section and DPI.

	Company:		PLM Latina.
	
	EXECUTE dbo.plm_spGetBannersBySection @prefix = 'DEF58', @targetId = 1, @country = 'MEX', @sectionId = 13, @resolutionKey = '160'
	EXECUTE dbo.plm_spGetBannersBySection @prefix = 'PLMQUIMIOMEX', @targetId = 3, @country = 'MEX', @sectionId = 16, @resolutionKey = '160'

 */ 

CREATE PROCEDURE dbo.plm_spGetBannersBySection
(
	@prefix				varchar(20) = Null,
	@targetId			tinyint = Null,
	@country			varchar(10) = Null,
	@sectionId			int = Null,
	@resolutionKey		varchar(10) = Null
)
AS
BEGIN

	IF (@prefix IS NULL OR @targetId IS NULL OR @country IS NULL OR @sectionId IS NULL OR @resolutionKey IS NULL)
	BEGIN
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
	END
	
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
		,ir.BaseUrl AS ResolutionBaseUrl
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
		Inner Join ElectronicResolutions er On ei.ElectronicId = er.ElectronicId
		Inner Join ResolutionScreens ir On er.ScreenId = ir.ScreenId
			And er.ResolutionId = ir.ResolutionId
		Inner Join Resolutions r On ir.ResolutionId = r.ResolutionId
	WHERE cp.PrefixName = @prefix
		And dt.TargetId = @targetId
		And c.ID = @country
		And dt.SectionId = @sectionId
		And r.ResolutionKey = @resolutionKey
		And ei.InfoTypeId = 4
		And ei.Active = 1
		And dt.FinalDate >= GETDATE()
	Order By [Order]

	RETURN @@ROWCOUNT

END
go