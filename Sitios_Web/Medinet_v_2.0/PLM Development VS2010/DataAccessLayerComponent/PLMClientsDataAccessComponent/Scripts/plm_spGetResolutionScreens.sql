IF OBJECT_ID('dbo.plm_spGetResolutionScreens') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetResolutionScreens
go

/* 
	Author:			Ramiro Sánchez				 
	Object:			dbo.plm_spGetResolutionScreens
	
	Description:	

	Company:		PLM Latina.
		
 */ 

CREATE PROCEDURE dbo.plm_spGetResolutionScreens
(
	@resolutionKey		varchar(10)
)
AS
BEGIN

	IF (@resolutionKey IS NULL)
	BEGIN
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
	END

	SELECT	rs.[ScreenId],
			rs.[ResolutionId],
			rs.[BaseUrl]
	FROM dbo.ResolutionScreens rs
		Inner Join Resolutions r On rs.ResolutionId = r.ResolutionId
	WHERE	r.ResolutionKey = @resolutionKey 
		AND r.Active = 1
	 
	RETURN @@ROWCOUNT

END
go
