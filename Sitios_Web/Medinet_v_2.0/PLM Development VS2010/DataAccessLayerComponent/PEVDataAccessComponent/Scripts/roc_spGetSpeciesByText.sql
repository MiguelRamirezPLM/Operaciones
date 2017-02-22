IF OBJECT_ID('dbo.roc_spGetSpeciesByText') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetSpeciesByText
go

/* 
	Author:			Daniel Campos / Ramiro Sánchez				 
	Object:			dbo.roc_spGetSpeciesByText
	
	Description:	Retrieves all Species by Letter Or by Text Or by FullText
					
	Company:		PLM México.

	EXECUTE dbo.roc_spGetSpeciesByText @text = 'ave', @page = 0, @numberByPage = 10
	
 */ 

CREATE PROCEDURE dbo.roc_spGetSpeciesByText
(
	@text				varchar(255),
	@page				int,
	@numberByPage		int

)AS
BEGIN

	IF (@text IS NULL OR @page < 0 OR @numberByPage < 0)
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
					WHERE Species.Active = 1 AND Species.SpecieName  LIKE '%'+@text+'%' COLLATE SQL_LATIN1_GENERAL_CP1_CI_AI
					GROUP BY Species.SpecieId, Species.SpecieName
  
			) as contador
		) AS TOTAL,*   

		FROM (
			SELECT Species.SpecieId,Species.SpecieName, ROW_NUMBER() OVER (ORDER BY Species.SpecieName ASC) AS RowNumber FROM Species
				INNER JOIN ProductSpecies ON ProductSpecies.SpecieId = Species.SpecieId 
				WHERE  Species.Active = 1 AND Species.SpecieName  LIKE '%'+@text+'%' COLLATE SQL_LATIN1_GENERAL_CP1_CI_AI
				GROUP BY Species.SpecieId, Species.SpecieName
 
		)AS INDICE

		WHERE RowNumber BETWEEN @numberByPage * @page + 1 AND @numberByPage * (@page + 1)
		
	END
END
go