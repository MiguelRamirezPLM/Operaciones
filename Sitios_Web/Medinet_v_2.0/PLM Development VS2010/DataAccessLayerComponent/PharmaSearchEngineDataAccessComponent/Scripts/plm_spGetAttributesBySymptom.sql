IF OBJECT_ID('dbo.plm_spGetAttributesBySymptom') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetAttributesBySymptom
go

/* 
	Author:			Ramiro Sánchez				 
	Object:			dbo.plm_spGetAttributesBySymptom
	
	Description:	Get Attributes associated to Symptom given by EditionId and SymptomId.

	Company:		PLM Latina.

	EXECUTE dbo.plm_spGetAttributesBySymptom @editionId = 72, @symptomId = 2

 */ 

CREATE PROCEDURE dbo.plm_spGetAttributesBySymptom
(
	@editionId			int,
	@symptomId			int
)

AS
BEGIN

	-- The parameters shouldn't be null:
	IF (@editionId IS NULL OR @symptomId IS NULL)
	BEGIN
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
	END

	SELECT  
		a.AttributeId,
		a.Description,
		esa.Content,
		esa.PlainContent,
		esa.AttributeOrder,
		es.HeaderImage
	FROM dbo.EditionSymptomAttributes esa 
		INNER JOIN dbo.Attributes a ON (esa.AttributeId = a.AttributeId)
		INNER JOIN dbo.EditionSymptoms es On (esa.EditionId = es.EditionId)
			AND (esa.SymptomId = es.SymptomId)
	WHERE 
		esa.EditionId	  = @editionId AND
		esa.SymptomId		  = @symptomId							
	ORDER BY esa.AttributeOrder
	
	RETURN @@ROWCOUNT

END
