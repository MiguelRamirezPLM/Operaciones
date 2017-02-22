IF OBJECT_ID('dbo.plm_spGetOptionsByQuestionnaire') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetOptionsByQuestionnaire
go

/* 
	Author:			Ramiro Sánchez				 
	Object:			dbo.plm_spGetOptionsByQuestionnaire
	
	Description:	Get Options By Questionnaire.

	Company:		PLM Latina.
	
	EXECUTE dbo.plm_spGetOptionsByQuestionnaire @questionnaireId = 24
	EXECUTE dbo.plm_spGetOptionsByQuestionnaire @questionnaireId = 34

 */ 

CREATE PROCEDURE dbo.plm_spGetOptionsByQuestionnaire
(
	@questionnaireId		int = null
)
AS
BEGIN

	IF (@questionnaireId IS NULL)
	BEGIN
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
	END
	
	Select Distinct o.OptionId
		,o.OptionDescription
		,o.Active
		,[Order]
	From Options o
		Inner Join QuestionnaireOptions qo On o.OptionId = qo.OptionId
		Inner Join QuestionnaireOptionSummaries qs On qo.QuestionnaireId = qs.QuestionnaireId
			And qo.OptionId = qs.OptionId
	Where o.Active = 1
		And qo.QuestionnaireId = @questionnaireId
	Order By qs.[Order]
	
	RETURN @@ROWCOUNT
		
END
