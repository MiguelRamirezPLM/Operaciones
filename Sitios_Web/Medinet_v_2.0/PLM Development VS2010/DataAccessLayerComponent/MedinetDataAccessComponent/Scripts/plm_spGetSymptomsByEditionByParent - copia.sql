IF OBJECT_ID('dbo.plm_spGetSymptomsByEditionByParent') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetSymptomsByEditionByParent
go

/* 
	Author:			Ramiro Sánchez				 
	Object:			dbo.plm_spGetSymptomsByEditionByParent
	
	Description:	Get Symptoms associated to products given by EditionId and Parent.

	Company:		PLM Latina.

	EXECUTE dbo.plm_spGetSymptomsByEditionByParent @editionId = 52, @parentId = 12

 */ 

CREATE PROCEDURE dbo.plm_spGetSymptomsByEditionByParent
(
	@editionId			int,
	@parentId			int
)
AS
BEGIN

	-- The parameters shouldn't be null:
	IF (@editionId IS NULL OR @parentId IS NULL)
	BEGIN
	
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
		
	END

		Select s.SymptomId, s.ParentId, s.SymptomName, es.SymptomColorIn, es.SymptomColorOut 
			From Symptoms s
			Inner Join EditionSymptoms es On(s.SymptomId = es.SymptomId)
			Where es.EditionId = @editionId
				And s.ParentId = @parentId

	RETURN @@ROWCOUNT
	
END
go