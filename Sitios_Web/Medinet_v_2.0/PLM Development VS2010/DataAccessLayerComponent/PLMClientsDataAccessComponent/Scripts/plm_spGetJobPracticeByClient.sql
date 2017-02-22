IF OBJECT_ID('dbo.plm_spGetJobPracticeByClient') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetJobPracticeByClient
go

/* 
	Author:			Juan Ramírez.				 
	Object:			dbo.plm_spGetJobPracticeByClient
	
	Description:	Retrieves all Practices with Position by Client.

	Company:		PLM Latina.

	
	EXECUTE dbo.plm_spGetJobPracticeByClient @clientId

	

 */ 

CREATE PROCEDURE dbo.plm_spGetJobPracticeByClient
(
	@clientId		int
)
AS
BEGIN
	
		SELECT [JobPractices].[ClientId]
			  ,[JobPractices].[PracticeId]
			  ,[JobPractices].[JobPlaceId]
			  ,[JobPractices].[InstitutionName]
			  ,[JobPractices].[PositionName]
		  FROM [dbo].[JobPractices]
			   
		 WHERE [JobPractices].[ClientId] = @clientId

		RETURN @@ROWCOUNT
END
go