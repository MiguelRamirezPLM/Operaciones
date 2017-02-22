IF OBJECT_ID('dbo.plm_spGetActivitiesByCalculatorResult') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetActivitiesByCalculatorResult
go

/* 
	Author:			Ramiro Sánchez				 
	Object:			dbo.plm_spGetActivitiesByCalculatorResult
	
	Description:	Get Physical Activities By Calculator Result.

	Company:		PLM Latina.
	
	EXECUTE dbo.plm_spGetActivitiesByCalculatorResult @branchId = 30

 */ 

CREATE PROCEDURE dbo.plm_spGetActivitiesByCalculatorResult
(
	@calculatorId		int = Null,
	@resultId			int = Null
)
AS
BEGIN

	SELECT Distinct p.ActivityId, p.ActivityName
	FROM PhysicalActivities p
		Inner Join ActivityItems ai ON p.ActivityId = ai.ActivityId
		Inner Join CalculatorResultActivityItems c ON ai.ActivityId = c.ActivityId
			And ai.ItemId = c.ItemId
	WHERE p.Active = 1
		And c.CalculatorId = @calculatorId
		And c.ResultId = @resultId
	Order By p.ActivityName

	RETURN @@ROWCOUNT

	
END
go