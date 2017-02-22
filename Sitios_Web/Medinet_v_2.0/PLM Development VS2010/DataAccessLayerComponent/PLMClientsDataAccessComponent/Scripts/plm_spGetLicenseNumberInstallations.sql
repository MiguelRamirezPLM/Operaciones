IF OBJECT_ID('dbo.plm_spGetLicenseNumberInstallations') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetLicenseNumberInstallations
go

/* 
	Author:			Ramiro Sánchez
	Object:			dbo.plm_spGetLicenseNumberInstallations
	
	Description:	Verify if the client is permitted installations.

	Company:		PLM Latina.

	
	EXECUTE dbo.plm_spGetLicenseNumberInstallations @licenseKey = 'ABCDEFGHIJKLMNPQ', @codePrefix = 'Prueba20120504'

 */ 

CREATE PROCEDURE dbo.plm_spGetLicenseNumberInstallations
(
	@licenseKey		varchar(16),
	@codePrefix		varchar(15)
)
AS
BEGIN

	-- The parameters shouldn't be null:
	IF (@licenseKey IS NULL OR @codePrefix IS NULL)
	BEGIN
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
	END
	
		DECLARE @licenserCounter int
		
		SELECT @licenserCounter = d.AllowedInstallations
		FROM Licenses l
			INNER JOIN DistributionLicenses dl On l.LicenseId = dl.LicenseId
			INNER JOIN DistributionCodePrefixes d ON dl.DistributionId = d.DistributionId
									AND dl.PrefixId = d.PrefixId
									AND dl.TargetId = d.TargetId
			INNER JOIN CodePrefixes c ON d.PrefixId = c.PrefixId
			WHERE l.LicenseKey = @licenseKey AND c.PrefixName = @codePrefix

		RETURN @licenserCounter

END
go