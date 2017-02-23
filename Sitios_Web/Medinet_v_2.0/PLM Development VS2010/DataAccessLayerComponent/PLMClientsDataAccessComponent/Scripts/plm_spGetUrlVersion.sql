IF OBJECT_ID('dbo.plm_spGetUrlVersion') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetUrlVersion
go

/* 
	Author:			Ramiro Sánchez				 
	Object:			dbo.plm_spGetUrlVersion
	
	Description:	Get URL Package Version By CodeString

	Company:		PLM Latina.
	
	EXECUTE dbo.plm_spGetUrlVersion @codeString = 'prueba'
	
 */ 

CREATE PROCEDURE dbo.plm_spGetUrlVersion
(
	@codeString				varchar(100)
)
AS
BEGIN

	-- The parameters shouldn't be null:
	IF (@codeString IS NULL)
	BEGIN
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
	END

	SELECT m.[VersionId]
      ,m.[DistributionId]
      ,m.[PrefixId]
      ,m.[TargetId]
      ,m.[UrlPackage]
      ,m.[AddedDate]
      ,m.[Active]
	FROM MetadataVersion m
		Inner Join Codes c On m.PrefixId = c.PrefixId
	Where c.CodeString = @codeString
		And m.Active = 1

	RETURN @@ROWCOUNT	
	
END
go