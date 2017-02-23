IF OBJECT_ID('dbo.plm_spGetSummariesByOption') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetSummariesByOption
go

/* 
	Author:			Ramiro Sánchez				 
	Object:			dbo.plm_spGetSummariesByOption
	
	Description:	Get Summaries By Questionnaire and Option.

	Company:		PLM Latina.
	
	EXECUTE dbo.plm_spGetSummariesByOption @questionnaireId = 24, @optionId = 34, @order = 1
	EXECUTE dbo.plm_spGetSummariesByOption @questionnaireId = 34, @optionId = 265, @order = 10

 */ 

CREATE PROCEDURE dbo.plm_spGetSummariesByOption
(
	@questionnaireId	int = null,
	@optionId			int = null,
	@order				tinyint = null
)
AS
BEGIN

	IF (@questionnaireId IS NULL OR @optionId IS NULL OR @order IS NULL)
	BEGIN
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
	END
	
	Select Distinct s.SummaryId
		,s.SummaryDescription
		,s.Active
		,[Order]
	From Summaries s
		Inner Join QuestionnaireOptionSummaries qs On s.SummaryId = qs.SummaryId
	Where s.Active = 1
		And qs.QuestionnaireId = @questionnaireId
		And qs.OptionId = @optionId
		And qs.[Order] = @order
	Order By s.SummaryDescription
	
	RETURN @@ROWCOUNT
		
END
