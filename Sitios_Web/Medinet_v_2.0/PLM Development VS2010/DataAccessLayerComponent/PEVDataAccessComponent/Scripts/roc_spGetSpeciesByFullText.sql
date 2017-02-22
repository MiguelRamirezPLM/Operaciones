IF OBJECT_ID('dbo.roc_spGetSpeciesByFullText') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetSpeciesByFullText
go

/* 
	Author:			Daniel Campos / Ramiro Sánchez				 
	Object:			dbo.roc_spGetSpeciesByFullText
	
	Description:	Retrieves all Species by FullText
					
	Company:		PLM México.

	EXECUTE dbo.roc_spGetSpeciesByFullText @fullText = 'felinos', @page = 0, @numberByPage = 10

 */ 

CREATE PROCEDURE dbo.roc_spGetSpeciesByFullText
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
			SELECT count(*) from 
			(
				SELECT Species.SpecieId FROM Species
					INNER JOIN ProductSpecies ON ProductSpecies.SpecieId = Species.SpecieId
					WHERE Species.Active = 1 AND FREETEXT(Species.SpecieName ,@fullText)
					GROUP BY Species.SpecieId, Species.SpecieName
  
			) as contador
		) AS TOTAL,*   

		FROM (
			SELECT Species.SpecieId,Species.SpecieName, ROW_NUMBER() OVER (ORDER BY Species.SpecieName ASC) AS RowNumber FROM Species
				INNER JOIN ProductSpecies ON ProductSpecies.SpecieId = Species.SpecieId 
				WHERE  Species.Active = 1 AND FREETEXT(Species.SpecieName ,@fullText)
				GROUP BY Species.SpecieId, Species.SpecieName
 
		)AS INDICE

		WHERE RowNumber BETWEEN @numberByPage * @page + 1 AND @numberByPage * (@page + 1)
		
	END

END
go