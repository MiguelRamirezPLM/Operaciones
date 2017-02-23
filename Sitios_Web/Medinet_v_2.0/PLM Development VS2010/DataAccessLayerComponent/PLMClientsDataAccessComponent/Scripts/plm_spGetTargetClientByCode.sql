IF OBJECT_ID('dbo.plm_spGetTargetClientByCode') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetTargetClientByCode
go

/* 
	Author:			Ramiro Sánchez
	Object:			dbo.plm_spGetTargetClientByCode
	
	Description:	

	Company:		PLM Latina.

	
	EXECUTE dbo.plm_spGetTargetClientByCode @code = 'Prueba20120504'

 */ 

CREATE PROCEDURE dbo.plm_spGetTargetClientByCode
(
	@code		varchar(50) = Null
)
AS
BEGIN

	-- The parameters shouldn't be null:
	IF (@code IS NULL)
	BEGIN
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
	END
	
	SELECT	tcc.ClientId, 
			tcc.CodeId, 
			tcc.TargetId, 
			tcc.DeviceId, 
			tcc.HWIdentifier,
			tcc.InstallationDate,
			tcc.Active
		FROM TargetClientCodes tcc
		INNER JOIN ClientCodes cc ON tcc.ClientId = cc.ClientId
			AND tcc.CodeId = cc.CodeId
		INNER JOIN Codes c ON cc.CodeId = c.CodeId
		WHERE c.CodeStatusId = 2
			And c.CodeString = @code

	RETURN @@ROWCOUNT
	
END
go