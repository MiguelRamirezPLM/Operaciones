IF OBJECT_ID('dbo.plm_spGetLicenseByKey') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetLicenseByKey
go

/* 
	Author:			Ramiro Sánchez
	Object:			dbo.plm_spGetLicenseByKey
	
	Description:	Get only one License from the database given its License Key.

	Company:		PLM Latina.

	
	EXECUTE dbo.plm_spGetLicenseByKey @licenseKey = 'ABCDEFGHIJKLMNPQ'

 */ 

CREATE PROCEDURE dbo.plm_spGetLicenseByKey
(
	@licenseKey			varchar(16)
)
AS
BEGIN

	-- The parameters shouldn't be null:
	IF (@licenseKey IS NULL)
	BEGIN
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
	END
	
	SELECT	LicenseId
			,LicenseKey
			,CurrentInstallation
			,Duration
			,Active
	FROM dbo.Licenses
	Where LicenseKey = @licenseKey

	RETURN @@ROWCOUNT
	
END
go