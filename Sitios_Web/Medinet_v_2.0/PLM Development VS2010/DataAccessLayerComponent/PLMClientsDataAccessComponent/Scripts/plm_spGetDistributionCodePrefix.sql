IF OBJECT_ID('dbo.plm_spGetDistributionCodePrefix') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetDistributionCodePrefix
go

/* 
	Author:			Ramiro Sánchez				 
	Object:			dbo.plm_spGetDistributionCodePrefix
	
	Description:	
	Company:		PLM Latina.
	
	EXECUTE dbo.plm_spGetDistributionCodePrefix @distributionId = 9, @prefixId = 278, @targetId = 3
	EXECUTE dbo.plm_spGetDistributionCodePrefix @prefixId = 278
	
 */ 

CREATE PROCEDURE dbo.plm_spGetDistributionCodePrefix
(
	@distributionId		int = null,
	@prefixId			int = null,
	@targetId			tinyint = null
)
AS
BEGIN

	IF (@distributionId IS NOT NULL AND @prefixId IS NOT NULL AND @targetId IS NOT NULL)
	BEGIN
	
		Select Distinct DistributionId,
			PrefixId,
			TargetId,
			InitialDate,
			FinalDate,
			AllowedInstallations
		From DistributionCodePrefixes
		Where DistributionId = @distributionId And
			PrefixId = @prefixId And
			TargetId = @targetId
	
		Return @@ROWCOUNT

	END
	
	IF (@distributionId IS NULL AND @prefixId IS NOT NULL AND @targetId IS NULL)
	BEGIN
	
		Select Distinct DistributionId,
			PrefixId,
			TargetId,
			InitialDate,
			FinalDate,
			AllowedInstallations
		From DistributionCodePrefixes
		Where PrefixId = @prefixId
	
		Return @@ROWCOUNT

	END


		
END
go