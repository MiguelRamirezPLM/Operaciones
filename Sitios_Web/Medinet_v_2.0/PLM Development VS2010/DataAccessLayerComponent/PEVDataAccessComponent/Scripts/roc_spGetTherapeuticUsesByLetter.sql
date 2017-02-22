IF OBJECT_ID('dbo.roc_spGetTherapeuticUsesByLetter') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetTherapeuticUsesByLetter
go

/* 
	Author:			Daniel Campos / Ramiro Sánchez				 
	Object:			dbo.roc_spGetTherapeuticUsesByLetter
	
	Description:	Retrieves all Therapeutic Uses by Letter
					
	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetTherapeuticUsesByLetter @letter = 'a', @page = 0, @numberByPage = 10
	
 */ 

CREATE PROCEDURE dbo.roc_spGetTherapeuticUsesByLetter
(
	@letter				varchar(25),
	@page				int,
	@numberByPage		int
)AS

BEGIN

	IF (@letter IS NULL OR @page < 0 OR @numberByPage < 0)
	BEGIN
		
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
	
	END

	BEGIN
	
		SELECT (
			SELECT COUNT(*) FROM (
				SELECT DISTINCT(TherapeuticUses.TherapeuticId),TherapeuticName, Nivel, ParentId FROM TherapeuticUses 
					WHERE TherapeuticUses.Active = 1 AND TherapeuticUses.Nivel = 1  AND TherapeuticName LIKE @letter+'%'
  
			) as total_count

		) AS TOTAL,*   

		FROM (
			SELECT DISTINCT(TherapeuticUses.TherapeuticId),TherapeuticName, Nivel, ParentId,
				ROW_NUMBER() OVER (ORDER BY TherapeuticName ASC) AS RowNumber FROM TherapeuticUses 
				WHERE TherapeuticUses.Active = 1 AND TherapeuticUses.Nivel = 1  AND TherapeuticName LIKE @letter+'%'   

		)AS THERAPEUTIC_USE

		WHERE RowNumber BETWEEN @numberByPage * @page + 1 AND @numberByPage * (@page + 1)
		
	END
END
go