IF OBJECT_ID('dbo.plm_spGetMedicalCalculators') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetMedicalCalculators
go

/* 
	Author:			Ramiro Sánchez				 
	Object:			dbo.plm_spGetMedicalCalculators
	
	Description:	Get Medical Calculators By Prefix or by Speciality.

	Company:		PLM Latina.
	
	EXECUTE dbo.plm_spGetMedicalCalculators @calculatorId = 37
	EXECUTE dbo.plm_spGetMedicalCalculators @prefix = 'IMCardio_mex'
	EXECUTE dbo.plm_spGetMedicalCalculators @specialityId = 37
	EXECUTE dbo.plm_spGetMedicalCalculators @prefix = 'Prueba Quimio', @specialityId = 61

 */ 

CREATE PROCEDURE dbo.plm_spGetMedicalCalculators
(
	@calculatorId		int = null,
	@prefix				varchar(15) = null,
	@specialityId		int = null
)
AS
BEGIN

	IF(@calculatorId IS NOT NULL AND @prefix IS NULL AND @specialityId IS NULL)
	BEGIN
	
		Select Distinct mc.CalculatorId
			,mc.CalculatorName
			,mc.CalculatorDescription
			,mc.CalculatorFormula
			,mc.SQLSyntax
			,mc.CalculatorReferences
			,mc.WebPage
			,mc.Active
		From MedicalCalculators mc
		Where mc.CalculatorId = @calculatorId
			AND mc.Active = 1
		Order By mc.CalculatorName

		RETURN @@ROWCOUNT	
	
	END

	IF(@prefix IS NOT NULL AND @specialityId IS NULL)
	BEGIN
	
		Select Distinct mc.CalculatorId
			,mc.CalculatorName
			,mc.CalculatorDescription
			,mc.CalculatorFormula
			,mc.SQLSyntax
			,mc.CalculatorReferences
			,mc.WebPage
			,mc.Active
		From MedicalCalculators mc
			Inner Join DistributionCalculators dc On mc.CalculatorId = dc.CalculatorId
			Inner Join CodePrefixes cp On dc.PrefixId = cp.PrefixId
		Where mc.Active = 1
			And cp.Active = 1
			And cp.PrefixName = @prefix
		Order By mc.CalculatorName

		RETURN @@ROWCOUNT	
	
	END
	
	IF(@prefix IS NULL AND @specialityId IS NOT NULL)
	BEGIN
	
		Select Distinct mc.CalculatorId
			,mc.CalculatorName
			,mc.CalculatorDescription
			,mc.CalculatorFormula
			,mc.SQLSyntax
			,mc.CalculatorReferences
			,mc.WebPage
			,mc.Active
		From MedicalCalculators mc
			Inner Join SpecialityCalculators sc On mc.CalculatorId = sc.CalculatorId
		Where mc.Active = 1
			And sc.SpecialityId = @specialityId
		Order By mc.CalculatorName

		RETURN @@ROWCOUNT	
	
	END
	
	IF(@prefix IS NOT NULL AND @specialityId IS NOT NULL)
	BEGIN
	
		Select Distinct mc.CalculatorId
			,mc.CalculatorName
			,mc.CalculatorDescription
			,mc.CalculatorFormula
			,mc.SQLSyntax
			,mc.CalculatorReferences
			,mc.WebPage
			,mc.Active
		From MedicalCalculators mc
			Inner Join DistributionCalculators dc On mc.CalculatorId = dc.CalculatorId
			Inner Join CodePrefixes cp On dc.PrefixId = cp.PrefixId
			Inner Join SpecialityCalculators sc On mc.CalculatorId = sc.CalculatorId
		Where mc.Active = 1
			And cp.Active = 1
			And cp.PrefixName = @prefix
			And sc.SpecialityId = @specialityId
		Order By mc.CalculatorName

		RETURN @@ROWCOUNT	
	
	END

END
go