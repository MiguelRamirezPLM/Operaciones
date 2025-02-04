USE [PLMClients 20130415]
GO
/****** Object:  StoredProcedure [dbo].[plm_spGetSearchElectronicInformation]    Script Date: 09/19/2013 10:42:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/* 
	Author:			Pilar Nájera				 
	Object:			dbo.plm_spGetSearchElectronicInformation
	
	Description:	Get Electronic Information By Text

	Company:		PLM Latina.
	
	EXECUTE dbo.plm_spGetSearchElectronicInformation @prefix = 'PLMPTWIN8', @text = 'aguda'

 */ 

ALTER PROCEDURE [dbo].[plm_spGetSearchElectronicInformation]
(
	 @prefix			varchar(15) = null,
	 @text				varchar(250)
)
AS
BEGIN

	--IF(@country IS NOT NULL AND @infoTypeId IS NOT NULL AND @prefix IS NULL AND @targetId IS NULL AND @specialityId IS NULL)
	IF(@prefix IS NOT NULL AND @text IS NOT NULL)
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
			--And dcp.TargetId = @targetId
			 And ei.InfoTypeId = 9
			--And c.ID = @country
			And ei.Active = 1
			And dt.FinalDate >= GETDATE()
			And ei.ElectronicTitle like '%' + @text + '%'
		Order By [Order]

		RETURN @@ROWCOUNT	
	
	END
	
END
