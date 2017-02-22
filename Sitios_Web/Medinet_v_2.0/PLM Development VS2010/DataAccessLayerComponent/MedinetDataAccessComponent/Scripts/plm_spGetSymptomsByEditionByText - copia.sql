IF OBJECT_ID('dbo.plm_spGetSymptomsByEditionByText') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetSymptomsByEditionByText
go

/* 
	Author:			Ramiro Sánchez				 
	Object:			dbo.plm_spGetSymptomsByEditionByText
	
	Description:	Get Symptoms associated to products given by EditionId and text.

	Company:		PLM Latina.

	EXECUTE dbo.plm_spGetSymptomsByEditionByText @editionId = 52, @quest = 'a'
	EXECUTE dbo.plm_spGetSymptomsByEditionByText @editionId = 52

 */ 

CREATE PROCEDURE dbo.plm_spGetSymptomsByEditionByText
(
	@editionId			int,
	@quest				varchar(20) = NULL
)
AS
BEGIN

	-- The parameters shouldn't be null:
	IF (@editionId IS NULL)
	BEGIN
	
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
		
	END

		SELECT DISTINCT 
			s.SymptomId,
			s.ParentId,
			s.SymptomName,
			es.SymptomColorIn,
			es.SymptomColorOut
		FROM dbo.plm_vwProductsByEdition p
			INNER JOIN dbo.ProductSymptoms ps ON (p.ProductId = ps.ProductId)
			INNER JOIN dbo.Symptoms s ON (ps.SymptomId = s.SymptomId)
			INNER JOIN dbo.EditionSymptoms es ON (s.SymptomId = es.SymptomId)
		WHERE p.ProductActive = 1 AND
			p.DivisionActive  = 1 AND
			s.Active		  = 1 AND
			p.EditionId		  = @editionId AND
			s.SymptomName LIKE CASE WHEN @quest IS NOT NULL THEN @quest+'%' ELSE '%' END
								
	ORDER BY s.SymptomName
	
	RETURN @@ROWCOUNT
	
END
go