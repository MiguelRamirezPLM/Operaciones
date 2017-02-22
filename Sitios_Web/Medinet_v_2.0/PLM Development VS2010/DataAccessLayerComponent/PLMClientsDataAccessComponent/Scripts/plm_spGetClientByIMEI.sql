IF OBJECT_ID('dbo.plm_spGetClientByIMEI') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetClientByIMEI
go

/* 
	Author:			Erick Morales / Ramiro Sánchez
	Object:			dbo.plm_spGetClientByIMEI
	
	Description:	Get only one client from the database given its IMEI device.

	Company:		PLM Latina.
	
	EXECUTE dbo.plm_spGetClientByIMEI 'FE80::200:5EFE:195.192.2.153%36'
	EXECUTE dbo.plm_spGetClientByIMEI @IMEI = '74125', @prefix = 'Ven20120517'

 */ 

CREATE PROCEDURE dbo.plm_spGetClientByIMEI
(
	@IMEI			varchar(100),
	@prefix			varchar(15) = null
)
AS
BEGIN

	IF(@prefix IS NULL)
	BEGIN
		-- Clean any empty character from the given string:
		SET @IMEI = RTRIM(LTRIM(@IMEI))

		SELECT DISTINCT 
			c.[ClientId],
			c.[FirstName],
			c.[LastName],
			c.[SecondLastName],
			c.[CompleteName],
			c.[Gender],
			c.[Birthday],
			c.[Email],
			c.[Password],
			c.[AddedDate],
			c.[LastUpdate],
			c.[CountryId],
			c.[Active],
			pc.[ProfessionId] as Profession,
			pc.[OtherProfession],
			pc.[ProfessionalLicense],
			c.StateId,
			s.StateName,
			s.ShortName as StateShortName

		FROM Clients c
		
		LEFT JOIN ClientSources cs ON (c.ClientId = cs.ClientId)

		INNER JOIN ProfessionClients pc ON (c.ClientId = pc.ClientId)
		INNER JOIN ClientCodes cc ON (c.ClientId = cc.ClientId)
		INNER JOIN TargetClientCodes tcc ON (cc.ClientId = tcc.ClientId AND
													cc.CodeId = tcc.CodeId)
		LEFT JOIN States s ON (c.StateId = s.StateId)
		
		WHERE	tcc.HWIdentifier = @IMEI And tcc.DeviceId = 2

		RETURN @@ROWCOUNT	
	
	END
	ELSE
	BEGIN
	
		-- Clean any empty character from the given string:
		SET @IMEI = RTRIM(LTRIM(@IMEI))

		SELECT DISTINCT 
			c.[ClientId],
			c.[FirstName],
			c.[LastName],
			c.[SecondLastName],
			c.[CompleteName],
			c.[Gender],
			c.[Birthday],
			c.[Email],
			c.[Password],
			c.[AddedDate],
			c.[LastUpdate],
			c.[CountryId],
			c.[Active],
			pc.[ProfessionId] as Profession,
			pc.[OtherProfession],
			pc.[ProfessionalLicense],
			c.StateId,
			s.StateName,
			s.ShortName as StateShortName

		FROM Clients c
		
		LEFT JOIN ClientSources cs ON (c.ClientId = cs.ClientId)
		INNER JOIN ProfessionClients pc ON (c.ClientId = pc.ClientId)
		INNER JOIN ClientCodes cc ON (c.ClientId = cc.ClientId)
		INNER JOIN TargetClientCodes tcc ON (cc.ClientId = tcc.ClientId AND
													cc.CodeId = tcc.CodeId)
		INNER JOIN Codes cd ON (cc.CodeId = cd.CodeId)
		INNER JOIN CodePrefixes cp ON(cd.PrefixId = cp.PrefixId)
		
		LEFT JOIN States s ON (c.StateId = s.StateId)
		
		WHERE	tcc.HWIdentifier = @IMEI AND
				cp.PrefixName = @prefix And tcc.DeviceId = 2

		RETURN @@ROWCOUNT	
	
	END
END
go