IF OBJECT_ID('dbo.roc_spGetTherapeuticUsesByFullText') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetTherapeuticUsesByFullText
go

/* 
	Author:			Daniel Campos / Ramiro Sánchez				 
	Object:			dbo.roc_spGetTherapeuticUsesByFullText
	
	Description:	Retrieves all Therapeutic Uses by FullText
					
	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetTherapeuticUsesByFullText @fullText = 'adsorbente', @page = 0, @numberByPage = 10

 */ 

CREATE PROCEDURE dbo.roc_spGetTherapeuticUsesByFullText
(
	@fullText			varchar(50),
	@page				int,
	@numberByPage		int
)AS
BEGIN

	IF (@fullText IS NULL OR @page < 0 OR @numberByPage < 0)
	BEGIN
		
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
	
	END

	BEGIN
	
		SELECT (
			SELECT COUNT(*) FROM (
				SELECT DISTINCT(TherapeuticUses.TherapeuticId),TherapeuticName, Nivel, ParentId
					FROM TherapeuticUses 
					WHERE TherapeuticUses.Active = 1 AND FREETEXT(TherapeuticName,@fullText) 
  
			) as total_count

		) AS TOTAL,*   

		FROM (
			SELECT DISTINCT(TherapeuticUses.TherapeuticId),TherapeuticName, Nivel, ParentId,
				ROW_NUMBER() OVER (ORDER BY TherapeuticName ASC) AS RowNumber 
				FROM TherapeuticUses 
				WHERE TherapeuticUses.Active = 1 AND FREETEXT(TherapeuticName,@fullText)    

		)AS THERAPEUTIC_USE

		WHERE RowNumber BETWEEN @numberByPage * @page + 1 AND @numberByPage * (@page + 1)
		
	END

END
go