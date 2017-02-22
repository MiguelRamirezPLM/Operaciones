IF OBJECT_ID('dbo.plm_spGetQuestionnaires') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetQuestionnaires
go

/* 
	Author:			Ramiro Sánchez				 
	Object:			dbo.plm_spGetQuestionnaires
	
	Description:	Get Medical Questionnaires By Prefix or by Speciality.

	Company:		PLM Latina.
	
	EXECUTE dbo.plm_spGetQuestionnaires @questionnaireId = 1
	EXECUTE dbo.plm_spGetQuestionnaires @prefix = 'IMCardio_mex'
	EXECUTE dbo.plm_spGetQuestionnaires @specialityId = 37
	EXECUTE dbo.plm_spGetQuestionnaires @prefix = 'Prueba Quimio', @specialityId = 61

 */ 

CREATE PROCEDURE dbo.plm_spGetQuestionnaires
(
	@questionnaireId		int = null,
	@prefix					varchar(15) = null,
	@specialityId			int = null
)
AS
BEGIN

	IF(@questionnaireId IS NOT NULL AND @prefix IS NULL AND @specialityId IS NULL)
	BEGIN
	
		Select Distinct q.QuestionnaireId
			,q.QuestionnaireName
			,q.QuestionnaireDescription
			,q.QuestionnaireInstructions
			,q.QuestionnaireReferences
			,q.WebPage
			,q.Active
		From Questionnaires q
		Where q.Active = 1
			And q.QuestionnaireId = @questionnaireId
		Order By q.QuestionnaireName

		RETURN @@ROWCOUNT	
	
	END	

	IF(@questionnaireId IS NULL AND @prefix IS NOT NULL AND @specialityId IS NULL)
	BEGIN
	
		Select Distinct q.QuestionnaireId
			,q.QuestionnaireName
			,q.QuestionnaireDescription
			,q.QuestionnaireInstructions
			,q.QuestionnaireReferences
			,q.WebPage
			,q.Active
		From Questionnaires q
			Inner Join DistributionQuestionnaires dq On q.QuestionnaireId = dq.QuestionnaireId
			Inner Join CodePrefixes cp On dq.PrefixId = cp.PrefixId
		Where q.Active = 1
			And cp.Active = 1
			And cp.PrefixName = @prefix
		Order By q.QuestionnaireName

		RETURN @@ROWCOUNT	
	
	END
	
	IF(@questionnaireId IS NULL AND @prefix IS NULL AND @specialityId IS NOT NULL)
	BEGIN
	
		Select Distinct q.QuestionnaireId
			,q.QuestionnaireName
			,q.QuestionnaireDescription
			,q.QuestionnaireInstructions
			,q.QuestionnaireReferences
			,q.WebPage
			,q.Active
		From Questionnaires q
			Inner Join SpecialityQuestionnaires sq On q.QuestionnaireId = sq.QuestionnaireId
		Where q.Active = 1
			And sq.SpecialityId = @specialityId
		Order By q.QuestionnaireName

		RETURN @@ROWCOUNT	
	
	END
	
	IF(@questionnaireId IS NULL AND @prefix IS NOT NULL AND @specialityId IS NOT NULL)
	BEGIN
	
		Select Distinct q.QuestionnaireId
			,q.QuestionnaireName
			,q.QuestionnaireDescription
			,q.QuestionnaireInstructions
			,q.QuestionnaireReferences
			,q.WebPage
			,q.Active
		From Questionnaires q
			Inner Join DistributionQuestionnaires dq On q.QuestionnaireId = dq.QuestionnaireId
			Inner Join CodePrefixes cp On dq.PrefixId = cp.PrefixId
			Inner Join SpecialityQuestionnaires sq On q.QuestionnaireId = sq.QuestionnaireId
		Where q.Active = 1
			And cp.Active = 1
			And cp.PrefixName = @prefix
			And sq.SpecialityId = @specialityId
		Order By q.QuestionnaireName

		RETURN @@ROWCOUNT	
	
	END

END
