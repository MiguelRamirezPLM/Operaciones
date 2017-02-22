IF OBJECT_ID('dbo.roc_spGetAdvertisementsByClient') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetAdvertisementsByClient
go

/* 
	Author:			Daniel Campos.				 
	Object:			dbo.roc_spGetAdvertisementsByClient
	
	Description:	Retrieves all advertisements asociated to a specific client.

	Company:		PLM Latina.

	

	EXECUTE dbo.roc_spGetAdvertisementsByClient @clientId = 28098
	

 */ 

CREATE PROCEDURE dbo.roc_spGetAdvertisementsByClient
(
	@clientId				int
)
AS
BEGIN

	-- The parameters shouldn't be null:
	IF (@clientId IS NULL)
	BEGIN
	
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
		
	END

	SELECT AdvertId,
		   ClientId,
		   AdvertName,
		   BaseUrl,
		   Orden,
		   Active 
	FROM Advertisements 
	WHERE ClientId = @clientId AND 
		  Active = 1 
	ORDER BY Orden ASC

END
go