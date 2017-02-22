IF OBJECT_ID('dbo.plm_spCRUDLicenseClientDownload') IS NOT NULL
	DROP PROCEDURE dbo.plm_spCRUDLicenseClientDownload
go

/* 
	Author:			Ulises Orihuela		 
	Object:			dbo.plm_spCRUDLicenseClientDownload
	
	Description:	Update TargetClientCodes
					
	Company:		PLM Latina.

	DECLARE
		@CRUDType		tinyint,
		@addressId		int



	EXECUTE dbo.plm_spCRUDLicenseClientDownload 	@hwidentifier = 'a', @licensekey = 'a'
		
 */ 

CREATE PROCEDURE dbo.plm_spCRUDLicenseClientDownload
(
		 @hwidentifier    varchar( 100),
		 @licensekey   varchar( 100)
)
AS
BEGIN

DECLARE 
	@clientId		    INT,
	@codeId			INT,
	@deviceId			INT,
	@targetId		    INT,
	 @licenseId   INT = 0
	
	IF ( @hwidentifier is null AND @licensekey IS NULL )
	BEGIN
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
	END
	
	SELECT @licenseId= LicenseId
	FROM Licenses
	WHERE LicenseKey = @licensekey
	
	IF (@licenseId = 0)
	BEGIN
		RETURN 0
	END
	
	SELECT @clientId = ClientId , 
				  @codeId= CodeId, 
				  @deviceId = DeviceId, 
				  @targetId= TargetId
	FROM LicenseTargetCodes
	WHERE LicenseId = @licenseId
	
	UPDATE TargetClientCodes
	SET HWIdentifier = @hwidentifier
	WHERE ClientId = @clientId
		AND   CodeId = @codeId
		AND DeviceId =@deviceId
		AND TargetId = @targetId

  RETURN @clientid
END