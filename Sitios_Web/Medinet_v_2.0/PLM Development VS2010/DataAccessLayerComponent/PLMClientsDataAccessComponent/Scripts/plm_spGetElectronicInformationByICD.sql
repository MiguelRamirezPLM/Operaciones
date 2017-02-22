IF OBJECT_ID('dbo.plm_spGetElectronicInformationByICD') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetElectronicInformationByICD

GO

/*
       Author:          César Avila	
       		 
	   Object:			dbo.plm_spGetElectronicInformationByICD
	
	   Description:	    Get Electronic Information By Type or by Prefix or by Target by ICD.

	   EXECUTE          dbo.plm_spGetElectronicInformationByICD  @country		
	                                                             @infoTypeId		
	                                                             @prefix			
	                                                             @targetId			
	                                                             @icdId     
	
	*/
CREATE PROCEDURE dbo.plm_spGetElectronicInformationByICD

	 @country			varchar(10),
	 @infoTypeId		tinyint = null,
	 @prefix			varchar(15) = null,
	 @targetId			tinyint = null,
	 @icdId             int= null
	
AS
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
			Inner Join ICDTools icdt ON ei.ElectronicId=icdt.ElectronicId
			Inner Join ICD ic        ON icdt.ICDId=ic.ICDId
			
			WHERE cp.PrefixName = @prefix 
			And dcp.TargetId = @targetId
			And c.ID = @country
			And ei.InfoTypeId = @infoTypeId
			And ic.ICDId= @icdId
			And ei.Active = 1
			And dt.FinalDate >= GETDATE()
		Order By [Order]

		RETURN @@ROWCOUNT
     

  END  