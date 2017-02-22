IF OBJECT_ID('dbo.plm_spGetCodeByHwIdByPrefix') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetCodeByHwIdByPrefix
go

/* 
	Author:			Ramiro Sánchez
	Object:			dbo.plm_spGetCodeByHwIdByPrefix
	
	Description:	Checks if the MAC address exists given a code.

	Company:		PLM Latina.

	
	EXECUTE dbo.plm_spGetCodeByHwIdByPrefix @codePrefix = 'Prueba20120504', @hwIdentifier = '12345'

 */ 

CREATE PROCEDURE dbo.plm_spGetCodeByHwIdByPrefix
(
	@codePrefix			varchar(15),
	@hwIdentifier		varchar(MAX)
)
AS
BEGIN

	-- The parameters shouldn't be null:
	IF (@codePrefix IS NULL OR @hwIdentifier IS NULL)
	BEGIN
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
	END
	
	SELECT	tcd.ClientId, 
			tcd.CodeId, 
			tcd.TargetId, 
			tcd.DeviceId, 
			tcd.HWIdentifier,
			tcd.InstallationDate,
			tcd.Active
		FROM LicenseTargetCodes ltc
		INNER JOIN TargetClientCodes tcd ON ltc.ClientId = tcd.ClientId
			AND ltc.CodeId = tcd.CodeId
			AND ltc.TargetId = tcd.TargetId
			AND ltc.DeviceId = tcd.DeviceId
		INNER JOIN ClientCodes cc ON tcd.ClientId = cc.ClientId
			AND tcd.CodeId = cc.CodeId
		INNER JOIN Codes c ON cc.CodeId = c.CodeId
		INNER JOIN CodePrefixes cp ON c.PrefixId = cp.PrefixId
		WHERE cp.PrefixName = @codePrefix AND tcd.HWIdentifier Like '%'+@hwIdentifier+'%' AND tcd.Active = 1

	RETURN @@ROWCOUNT
	
END
go