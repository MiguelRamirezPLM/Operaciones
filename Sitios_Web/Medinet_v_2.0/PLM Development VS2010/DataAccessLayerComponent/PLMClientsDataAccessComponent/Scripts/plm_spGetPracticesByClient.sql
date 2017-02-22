IF OBJECT_ID('dbo.plm_spGetPracticesByClient') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetPracticesByClient
go

/* 
	Author:			Juan Ramírez.				 
	Object:			dbo.plm_spGetPracticesByClient
	
	Description:	Retrieves all Practices by Client.

	Company:		PLM Latina.

	
	EXECUTE dbo.plm_spGetPracticesByClient @clientId

	

 */ 

CREATE PROCEDURE dbo.plm_spGetPracticesByClient
(
	@clientId		int,
	@practiceId		tinyint = null
)
AS
BEGIN
	IF(@clientId IS NOT NULL AND @practiceId IS NULL)
	BEGIN
		
		SELECT [ClientId]
			  ,[PracticeId]
		  FROM [dbo].[ClientPractices]	
		 WHERE [ClientId] = @clientId

		RETURN @@ROWCOUNT

	END

	IF(@clientId IS NOT NULL AND @practiceId IS NOT NULL)
	BEGIN

		DECLARE @clientPractice int
		
		SELECT @clientPractice = COUNT(*)
		  FROM [dbo].[ClientPractices]	
		 WHERE [ClientId] = @clientId AND
			   [PracticeId] = @practiceId

		RETURN @clientPractice

	END
END
go