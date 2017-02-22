IF OBJECT_ID('dbo.plm_spGetImagesByPhysicalActivity') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetImagesByPhysicalActivity
go

/* 
	Author:			Ramiro Sánchez				 
	Object:			dbo.plm_spGetImagesByPhysicalActivity
	
	Description:	Get Images By Physical Activity.

	Company:		PLM Latina.
	
	EXECUTE dbo.plm_spGetImagesByPhysicalActivity @branchId = 30

 */ 

CREATE PROCEDURE dbo.plm_spGetImagesByPhysicalActivity
(
	@activityId			tinyint = Null
)
AS
BEGIN

	SELECT Distinct ai.ImageId, ai.ActivityId, ai.ImageName, ai.BaseUrl, ai.Active
	FROM ActivityImages ai
		Inner Join PhysicalActivities p ON ai.ActivityId = p.ActivityId
	WHERE p.Active = 1
		And ai.ActivityId = @activityId
	Order By ai.ImageName

	RETURN @@ROWCOUNT
	
END
go