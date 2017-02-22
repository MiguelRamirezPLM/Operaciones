IF OBJECT_ID('dbo.plm_spCheckLicense') IS NOT NULL
	DROP PROCEDURE dbo.plm_spCheckLicense
go

/* 
	Author:			Ramiro Sánchez
	Object:			dbo.plm_spCheckLicense
	
	Description:	Verify if the License is valid to the CodePrefix.

	Company:		PLM Latina.

	
	EXECUTE dbo.plm_spCheckLicense @licenseKey = 'ABCDEFGHIJKLMNP', @codePrefix = 'Prueba20120504'

 */ 

CREATE PROCEDURE dbo.plm_spCheckLicense
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
		
		SELECT @licenserCounter = COUNT(*)
		FROM DistributionCodePrefixes d
			INNER JOIN DistributionLicenses dl On d.DistributionId = dl.DistributionId
									AND d.PrefixId = dl.PrefixId
									AND d.TargetId = dl.TargetId
			INNER JOIN Licenses l ON dl.LicenseId = l.LicenseId
			INNER JOIN CodePrefixes c ON d.PrefixId = c.PrefixId
			WHERE l.LicenseKey = @licenseKey AND c.PrefixName = @codePrefix

		RETURN @licenserCounter

END
go