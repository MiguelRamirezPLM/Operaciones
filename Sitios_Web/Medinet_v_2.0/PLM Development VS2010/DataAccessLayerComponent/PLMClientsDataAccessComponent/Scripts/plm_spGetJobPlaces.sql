IF OBJECT_ID('dbo.plm_spGetJobPlaces') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetJobPlaces
go

/* 
	Author:			Juan Ramirez.				 
	Object:			dbo.plm_spGetJobPlaces
	
	Description:	Retrieves all Job Places stored in the database.

	Company:		PLM Latina.

	EXECUTE dbo.plm_spGetJobPlaces

 */ 

CREATE PROCEDURE dbo.plm_spGetJobPlaces
AS
BEGIN

	SELECT [JobPlaceId]
		  ,[JobPlaceName]
		  ,[Active]
	FROM [dbo].[JobPlaces]
	WHERE [Active] = 1
	ORDER BY [JobPlaceName]
	
	RETURN @@ROWCOUNT

END
go