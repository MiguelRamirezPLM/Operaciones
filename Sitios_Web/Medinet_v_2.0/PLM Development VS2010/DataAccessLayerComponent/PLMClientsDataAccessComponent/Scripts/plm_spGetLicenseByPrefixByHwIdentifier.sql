IF OBJECT_ID('dbo.plm_spGetLicenseByPrefixByHwIdentifier') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetLicenseByPrefixByHwIdentifier
go

/* 
	Author:			Ramiro Sánchez
	Object:			dbo.plm_spGetLicenseByPrefixByHwIdentifier
	
	Description:	Get only one License from the database given its License Code and Device.

	Company:		PLM Latina.

	
	EXECUTE dbo.plm_spGetLicenseByPrefixByHwIdentifier @prefix = 'hola', @hwIdentifier = '12345', @licenseId = 1

 */ 

CREATE PROCEDURE dbo.plm_spGetLicenseByPrefixByHwIdentifier
(
	@prefix			varchar(30),
	@hwIdentifier	varchar(MAX),
	@licenseId		int
)
AS
BEGIN

	-- The parameters shouldn't be null:
	IF (@prefix IS NULL OR @hwIdentifier IS NULL OR @licenseId IS NULL)
	BEGIN
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
	END
	
	SELECT	l.ClientId
			,l.CodeId
			,l.DeviceId
			,l.TargetId
			,l.LicenseId
			,l.InitialDate
			,l.FinalDate
	FROM LicenseTargetCodes l
		INNER JOIN TargetClientCodes tcc ON l.ClientId = tcc.ClientId
			And l.CodeId = tcc.CodeId
			And l.DeviceId = tcc.DeviceId
			And l.TargetId = tcc.TargetId
		INNER JOIN ClientCodes cc ON tcc.ClientId = cc.ClientId
			AND tcc.CodeId = cc.CodeId
		INNER JOIN Codes c ON cc.CodeId = c.CodeId
		INNER JOIN Licenses lc ON l.LicenseId = lc.LicenseId
		INNER JOIN CodePrefixes cp ON c.PrefixId = cp.PrefixId
	Where cp.PrefixName = @prefix
		AND tcc.HWIdentifier Like '%'+@hwIdentifier+'%' 
		AND l.LicenseId = @licenseId

	RETURN @@ROWCOUNT
	
END
go