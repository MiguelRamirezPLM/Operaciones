IF OBJECT_ID('dbo.plm_spGetVariablesCalculator') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetVariablesCalculator
go

/* 
	Author:			Ramiro Sánchez				 
	Object:			dbo.plm_spGetVariablesCalculator
	
	Description:	Get Variables Calculator By Calculator.

	Company:		PLM Latina.
	
	EXECUTE dbo.plm_spGetVariablesCalculator @calculatorId = 1
	
 */ 

CREATE PROCEDURE dbo.plm_spGetVariablesCalculator
(
	@calculatorId		int
)
AS
BEGIN

	IF (@calculatorId IS NULL)
	BEGIN
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
	END

	Select Distinct v.VariableId
		,v.VariableName
		,v.Nomenclature
		,v.Active
	From Variables v
		Inner Join CalculatorVariables cv On v.VariableId = cv.VariableId
		Inner Join MedicalCalculators mc On cv.CalculatorId = mc.CalculatorId
	Where v.Active = 1
		And mc.CalculatorId = @calculatorId

	RETURN @@ROWCOUNT

END
go