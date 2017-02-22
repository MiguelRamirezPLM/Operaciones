IF OBJECT_ID('dbo.plm_spGetPositions') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetPositions
go

/* 
	Author:			Juan Ramírez.				 
	Object:			dbo.plm_spGetPositions
	
	Description:	Retrieves all Positions by Client.

	Company:		PLM Latina.

	
	EXECUTE dbo.plm_spGetPositions @clientId

	

 */ 

CREATE PROCEDURE dbo.plm_spGetPositions
(
	@clientId		int,
	@practiceId		tinyint,
	@jobPlaceId		tinyint
)
AS
BEGIN
	
		SELECT [PositionId]
			  ,[ClientId]
			  ,[PracticeId]
			  ,[JobPlaceId]
			  ,[PositionName]
			  ,[Active]
		  FROM [dbo].[Positions]	
		 WHERE [ClientId] = @clientId AND
			   [PracticeId] = @practiceId AND 
			   [JobPlaceId] = @jobPlaceId AND
			   [Active] = 1
				

END
go