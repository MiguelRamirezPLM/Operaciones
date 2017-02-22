IF OBJECT_ID('dbo.plm_spGetCodeByClientByPrefix') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetCodeByClientByPrefix
go

/* 
	Author:			Ramiro Sánchez
	Object:			dbo.plm_spGetCodeByClientByPrefix
	
	Description:	Get only one Code Info from the database by Client by CodePrefix.

	Company:		PLM Latina.

	
	EXECUTE dbo.plm_spGetCodeByClientByPrefix @clientId = 53, @codePrefix = 'Prueba20120504', @hwIdentifier = '44asd12a35as1d2a;as1da3s21da5sd113as1d;asd1a3sd1a23sd1;'

 */ 

CREATE PROCEDURE dbo.plm_spGetCodeByClientByPrefix
(
	@clientId			int,
	@codePrefix			varchar(15),
	@hwIdentifier		varchar(max)
)
AS
BEGIN

	-- The parameters shouldn't be null:
	IF (@clientId IS NULL OR @codePrefix IS NULL OR @hwIdentifier IS NULL)
	BEGIN
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
	END
	
	SELECT c.CodeId, c.CodeString, c.PrefixId, c.Assign, c.CodeStatusId
		FROM Codes c
		INNER JOIN CodePrefixes cp ON c.PrefixId = cp.PrefixId
		INNER JOIN ClientCodes cc ON c.CodeId = cc.CodeId
		INNER JOIN TargetClientCodes tcc On cc.ClientId = tcc.ClientId
			AND cc.CodeId = tcc.CodeId
		WHERE cp.PrefixName = @codePrefix 
			AND cc.ClientId = @clientId
			AND tcc.HWIdentifier LIKE '%'+@hwIdentifier+'%'

	RETURN @@ROWCOUNT
	
END
go