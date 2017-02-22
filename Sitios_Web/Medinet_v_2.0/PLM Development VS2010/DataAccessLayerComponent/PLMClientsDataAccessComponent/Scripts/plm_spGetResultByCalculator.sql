IF OBJECT_ID('dbo.plm_spGetResultByCalculator') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetResultByCalculator
go

/* 
	Author:			Ramiro Sánchez				 
	Object:			dbo.plm_spGetResultByCalculator
	
	Description:	Get Result By Calculator.

	Company:		PLM Latina.
	
	EXECUTE dbo.plm_spGetResultByCalculator @calculatorId = 41, @sqlSyntax = '200 / POWER(1.70, 2)'
	EXECUTE dbo.plm_spGetResultByCalculator @calculatorId = 16, @sqlSyntax = '80 / POWER(1.70, 2)'
	EXECUTE dbo.plm_spGetResultByCalculator @calculatorId = 48, @sqlSyntax = '655+(9.6*80)+(1.8*170)-(4.7*24)'
	EXECUTE dbo.plm_spGetResultByCalculator @calculatorId = 51, @sqlSyntax = '66+(13.7*80)+(5.0*170)-(6.8*24)'
		
 */

CREATE PROCEDURE dbo.plm_spGetResultByCalculator
(
	@calculatorId		int,
	@sqlSyntax			varchar(500)
)
AS
BEGIN

	IF (@calculatorId IS NULL OR @sqlSyntax IS NULL)
	BEGIN
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
	END

	DECLARE 
		@finalResult	decimal(10,4),
		@sql			varchar(MAX)

	SET @sql =	'IF OBJECT_ID(''dbo.tmpResultado'') IS NOT NULL ' + char(13) +
				'	DROP TABLE dbo.tmpResultado ' + char(13) +
				'DECLARE ' + char(13) +
				'	@result decimal(10,4) ' + char(13) +
				'SET @result = ' + @sqlSyntax + char(13) +
				' Select @result As Resultado ' + char(13) +
				'	Into tmpResultado ' + char(13)
	print @sql
	EXEC (@sql)	
	
	Select @finalResult = Resultado 
		From tmpResultado
	
	DROP Table tmpResultado

	IF ((Select Count(*)
			From Results r
			Inner Join CalculatorResults cr On r.ResultId = cr.ResultId
			Where cr.CalculatorId = @calculatorId And LowerRange Is Null And UpperRange Is Null) > 0)

	BEGIN
		Select r.*, @finalResult As FinalResult
			From Results r
			Inner Join CalculatorResults cr On r.ResultId = cr.ResultId
			Where cr.CalculatorId = @calculatorId
	END
	
	ELSE
	BEGIN

		Select r.*, @finalResult As FinalResult
			From Results r
			Inner Join CalculatorResults cr On r.ResultId = cr.ResultId
			Where cr.CalculatorId = @calculatorId
				AND
				(
					(LowerRange Is Null 
						And UpperRange Is Not Null 
						And @finalResult <= UpperRange)
					
					OR

					(LowerRange Is Not Null 
						And UpperRange Is Not Null 
						And @finalResult > LowerRange 
						And @finalResult <= UpperRange)

					OR

					(UpperRange Is Null 
						And LowerRange Is Not Null 
						And @finalResult > LowerRange)
				)
	END

	RETURN @@ROWCOUNT
END
go