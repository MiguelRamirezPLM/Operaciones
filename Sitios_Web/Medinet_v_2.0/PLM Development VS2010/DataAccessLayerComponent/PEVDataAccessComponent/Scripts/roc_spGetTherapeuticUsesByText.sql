IF OBJECT_ID('dbo.roc_spGetTherapeuticUsesByText') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetTherapeuticUsesByText
go

/* 
	Author:			Daniel Campos / Ramiro Sánchez				 
	Object:			dbo.roc_spGetTherapeuticUsesByText
	
	Description:	Retrieves all Therapeutic Uses by Text
					
	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetTherapeuticUsesByText @text = 'Amortiguador', @page = 0, @numberByPage = 10
	
 */ 

CREATE PROCEDURE dbo.roc_spGetTherapeuticUsesByText
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
			SELECT COUNT(*) FROM (
				SELECT DISTINCT(TherapeuticUses.TherapeuticId),TherapeuticName, Nivel, ParentId
					FROM TherapeuticUses 
					WHERE TherapeuticUses.Active = 1 AND TherapeuticName LIKE '%'+@text+'%' COLLATE SQL_LATIN1_GENERAL_CP1_CI_AI
  
			) as total_count

		) AS TOTAL,*   

		FROM (
			SELECT DISTINCT(TherapeuticUses.TherapeuticId),TherapeuticName, Nivel, ParentId,
				ROW_NUMBER() OVER (ORDER BY TherapeuticName ASC) AS RowNumber 
				FROM TherapeuticUses 
				WHERE TherapeuticUses.Active = 1 AND TherapeuticName LIKE '%'+@text+'%' COLLATE SQL_LATIN1_GENERAL_CP1_CI_AI   

		)AS THERAPEUTIC_USE

		WHERE RowNumber BETWEEN @numberByPage * @page + 1 AND @numberByPage * (@page + 1)
		
	END
END
go