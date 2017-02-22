IF OBJECT_ID('dbo.roc_spGetSpecies') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetSpecies
go

/* 
	Author:			Daniel Campos / Ramiro Sánchez				 
	Object:			dbo.roc_spGetSpecies
	
	Description:	Retrieves all Species.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetSpecies @page = 0, @numberByPage = 10


 */ 

CREATE PROCEDURE dbo.roc_spGetSpecies
(
	@page				int,
	@numberByPage		int
)

AS
BEGIN

	-- The parameters shouldn't be null:
	IF (@page IS NULL OR @numberByPage IS NULL)
	BEGIN
	
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
		
	END

	SELECT (
		SELECT count(*) from 
		(
			SELECT Species.SpecieId,Species.SpecieName FROM Species
				INNER JOIN ProductSpecies ON ProductSpecies.SpecieId = Species.SpecieId   
				WHERE  Species.Active = 1
				GROUP BY Species.SpecieId, Species.SpecieName, Species.Active 
  
		) as contador
	) AS TOTAL,*   

	FROM (
		SELECT Species.SpecieId,Species.SpecieName,
		ROW_NUMBER() OVER (ORDER BY Species.SpecieName ASC) AS RowNumber FROM Species
		INNER JOIN ProductSpecies ON ProductSpecies.SpecieId = Species.SpecieId 
		WHERE Species.Active = 1
		GROUP BY Species.SpecieId, Species.SpecieName, Species.Active
	)AS INDICE

	WHERE RowNumber BETWEEN @numberByPage * @page + 1 AND @numberByPage * (@page + 1)
	
END
go