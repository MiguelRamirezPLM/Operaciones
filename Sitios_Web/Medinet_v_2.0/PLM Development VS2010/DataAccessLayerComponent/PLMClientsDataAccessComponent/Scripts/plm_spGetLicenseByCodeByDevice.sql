IF OBJECT_ID('dbo.plm_spGetLicenseByCodeByDevice') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetLicenseByCodeByDevice
go

/* 
	Author:			Ramiro Sánchez
	Object:			dbo.plm_spGetLicenseByCodeByDevice
	
	Description:	Get only one License from the database given its License Code and Device.

	Company:		PLM Latina.

	
	EXECUTE dbo.plm_spGetLicenseByCodeByDevice @clientId = 1, @codeId = 1, @targetId = 1, @deviceId = 1, @licenseId = 1

 */ 

CREATE PROCEDURE dbo.plm_spGetLicenseByCodeByDevice
(
	@clientId		int,
	@codeId			int,
	@targetId		tinyint,
	@deviceId		tinyint,
	@licenseId		int
)
AS
BEGIN

	-- The parameters shouldn't be null:
	IF (@clientId IS NULL OR @codeId IS NULL OR @targetId IS NULL OR  @deviceId IS NULL OR @licenseId IS NULL)
	BEGIN
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
	END
	
	SELECT	l.ClientId
			,l.CodeId
			,dcp.DistributionId
			,dcp.PrefixId
			,l.TargetId
			,l.DeviceId
			,l.LicenseId
			,l.InitialDate
			,l.FinalDate
			,c.CodeString
			,cp.PrefixName
	FROM LicenseTargetCodes l
		INNER JOIN ClientCodes cc ON l.ClientId = cc.ClientId
			AND l.CodeId = cc.CodeId
		INNER JOIN Codes c ON cc.CodeId = c.CodeId
		INNER JOIN Licenses lc ON l.LicenseId = lc.LicenseId
		INNER JOIN DistributionLicenses dl ON lc.LicenseId = dl.LicenseId
		INNER JOIN DistributionCodePrefixes dcp ON dl.DistributionId = dcp.DistributionId
			And dl.PrefixId = dcp.PrefixId
			And dl.TargetId = dcp.TargetId
		INNER JOIN CodePrefixes cp ON c.PrefixId = cp.PrefixId
	Where l.ClientId = @clientId
		AND l.CodeId = @codeId
		AND l.TargetId = @targetId
		AND l.DeviceId = @deviceId
		AND l.LicenseId = @licenseId

	RETURN @@ROWCOUNT
	
END
go