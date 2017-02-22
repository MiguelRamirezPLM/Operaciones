IF OBJECT_ID('dbo.plm_spGetCodeByIMEI') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetCodeByIMEI
go

/* 
	Author:			Erick Morales.				 
	Object:			dbo.plm_spGetCodeByIMEI
	
	Description:	Get only one Code from the database given its IMEI device.

	Company:		PLM Latina.

	
	EXECUTE dbo.plm_spGetCodeByIMEI 'FE80::200:5EFE:195.192.2.153%40'

 */ 

CREATE PROCEDURE dbo.plm_spGetCodeByIMEI
(
	@IMEI			varchar(100),
	@prefix			varchar(100) = null,
	@ISBN			varchar(100) = null
)
AS
BEGIN

	-- Clean any empty character from the given string:
	SET @IMEI = RTRIM(LTRIM(@IMEI))

	IF(@prefix IS NULL AND @ISBN IS NULL)
	BEGIN
		SELECT
			c.[CodeId],
			c.[CodeStatusId],
			c.[CodeString],
			c.[PrefixId],
			c.[Assign]

		FROM [dbo].[Codes] c
		
		INNER JOIN ClientCodes cc ON (c.CodeId = cc.CodeId)
		INNER JOIN TargetClientCodes tcc ON (cc.ClientId = tcc.ClientId AND
													cc.CodeId = tcc.CodeId)
		WHERE	tcc.HWIdentifier = @IMEI And tcc.DeviceId = 2

		RETURN @@ROWCOUNT	
	
	END
	
	IF(@prefix IS NOT NULL AND @ISBN IS NULL)
	BEGIN
		
		SET @prefix = RTRIM(LTRIM(@prefix))
	
		SELECT
			c.[CodeId],
			c.[CodeStatusId],
			c.[CodeString],
			c.[PrefixId],
			c.[Assign]

		FROM [dbo].[Codes] c
		
		INNER JOIN ClientCodes cc ON (c.CodeId = cc.CodeId)
		INNER JOIN TargetClientCodes tcc ON (cc.ClientId = tcc.ClientId AND
													cc.CodeId = tcc.CodeId)
		INNER JOIN CodePrefixes cp ON (c.PrefixId = cp.PrefixId)
		
		WHERE	tcc.HWIdentifier = @IMEI And
				cp.PrefixName = @prefix And tcc.DeviceId = 2

		RETURN @@ROWCOUNT	
	
	END

	IF(@prefix IS NULL AND @ISBN IS NOT NULL)
	BEGIN
		
		SET @isbn = RTRIM(LTRIM(@isbn))
	
		SELECT
			c.[CodeId],
			c.[CodeStatusId],
			c.[CodeString],
			c.[PrefixId],
			c.[Assign]

		FROM [dbo].[Codes] c
		
		INNER JOIN ClientCodes cc ON (c.CodeId = cc.CodeId)
		INNER JOIN TargetClientCodes tcc ON (cc.ClientId = tcc.ClientId AND
													cc.CodeId = tcc.CodeId)
		INNER JOIN CodePrefixes cp ON (c.PrefixId = cp.PrefixId)
		INNER JOIN CodePrefixEditions cpe ON (cp.PrefixId = cpe.PrefixId)
		INNER JOIN Editions e ON (cpe.EditionId = e.EditionId)
		
		WHERE	tcc.HWIdentifier = @IMEI And
				e.ISBN = @ISBN And tcc.DeviceId = 2

		RETURN @@ROWCOUNT	
	
	END
	
END
go