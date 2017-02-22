IF OBJECT_ID('dbo.plm_spGetDistributionLicense') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetDistributionLicense
go

/* 
	Author:			Ramiro Sánchez				 
	Object:			dbo.plm_spGetDistributionLicense
	
	Description:	
	Company:		PLM Latina.
	
	EXECUTE dbo.plm_spGetDistributionLicense @targetId = 5, @licenseId = 99
	
 */ 

CREATE PROCEDURE dbo.plm_spGetDistributionLicense
(	
	@targetId			tinyint = null,
	@licenseId			int = null
)
AS
BEGIN

	--The parameters shouldn't be null:
	IF (@targetId IS NULL OR @licenseId IS NULL)
	BEGIN
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
	END

	Select DistributionId,
		PrefixId,
		TargetId,
		LicenseId
	From DistributionLicenses
	Where TargetId = @targetId
		And LicenseId = @licenseId
	
	Return @@ROWCOUNT
		
END
go