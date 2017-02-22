IF OBJECT_ID('dbo.roc_spGetTherapeuticUses') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetTherapeuticUses
go

/* 
	Author:			Daniel Campos / Ramiro Sánchez				 
	Object:			dbo.roc_spGetTherapeuticUses
	
	Description:	Retrieves all Therapeutic Uses.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetTherapeuticUses @page = 0, @numberByPage = 10


 */ 

CREATE PROCEDURE dbo.roc_spGetTherapeuticUses
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
		SELECT COUNT(*) FROM (
			SELECT DISTINCT(TherapeuticUses.TherapeuticId),TherapeuticName, Nivel, ParentId FROM TherapeuticUses 
				WHERE TherapeuticUses.Active = 1 AND TherapeuticUses.Nivel = 1 
  
		) as total_count

	) AS TOTAL,*   

	FROM (
		SELECT DISTINCT(TherapeuticUses.TherapeuticId),TherapeuticName, Nivel, ParentId,
			ROW_NUMBER() OVER (ORDER BY TherapeuticName ASC) AS RowNumber FROM TherapeuticUses 
			WHERE TherapeuticUses.Active = 1 AND TherapeuticUses.Nivel = 1 

	)AS THERAPEUTIC_USE

	WHERE RowNumber BETWEEN @numberByPage * @page + 1 AND @numberByPage * (@page + 1)
	
END
go