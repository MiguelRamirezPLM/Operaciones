IF OBJECT_ID('dbo.plm_spGetActivityItemsByCalculatorResult') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetActivityItemsByCalculatorResult
go

/* 
	Author:			Ramiro Sánchez				 
	Object:			dbo.plm_spGetActivityItemsByCalculatorResult
	
	Description:	Get Physical Activities By Calculator Result.

	Company:		PLM Latina.
	
	EXECUTE dbo.plm_spGetActivityItemsByCalculatorResult @branchId = 30

 */ 

CREATE PROCEDURE dbo.plm_spGetActivityItemsByCalculatorResult
(
	@calculatorId		int = Null,
	@resultId			int = Null,
	@activityId			tinyint = Null
)
AS
BEGIN

	SELECT i.ItemId, i.ItemName, cr.Content, i.Active
	FROM Items i
		Inner Join ActivityItems ai ON i.ItemId = ai.ItemId
		Inner Join CalculatorResultActivityItems cr on ai.ActivityId = cr.ActivityId
			And ai.ItemId = cr.ItemId
	WHERE i.Active = 1
		And cr.CalculatorId = @calculatorId
		And cr.ResultId = @resultId
		And cr.ActivityId = @activityId
	Order By i.ItemName

	RETURN @@ROWCOUNT

	
END
go