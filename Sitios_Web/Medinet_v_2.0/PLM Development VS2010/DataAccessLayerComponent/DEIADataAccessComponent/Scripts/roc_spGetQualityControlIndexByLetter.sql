IF OBJECT_ID('dbo.roc_spGetQualityControlIndexByLetter') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetQualityControlIndexByLetter
go

/* 
	Author:			Daniel Campos / Ramiro Sánchez				 
	Object:			dbo.roc_spGetQualityControlIndexByLetter
	
	Description:	Retrieves information from all Sections By Parent and letter.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetQualityControlIndexByLetter @parentId = 10, @page = 0, @numberByPage = 10, @letter = 'c'
 */ 

CREATE PROCEDURE dbo.roc_spGetQualityControlIndexByLetter
(
	@parentId		int,
	@page			int,
	@numberByPage	int,
	@letter			varchar(10)
)
AS
BEGIN

	-- The parameters shouldn't be null:
	IF (@parentId IS NULL OR @page IS NULL OR @numberByPage IS NULL OR @letter IS NULL)
	BEGIN
	
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
		
	END

	SELECT (
		SELECT COUNT(*) FROM (
			SELECT SectionId 
			FROM Sections
			WHERE ParentId IN (	SELECT Sections.SectionId 
									FROM Sections 
									WHERE Sections.ParentId = @parentId
										AND Sections.Active = 1
									GROUP BY Sections.SectionId, Sections.SectionName)
				AND SectionName LIKE @letter+'%'
				AND Active = 1 
		) AS contador
	) AS TOTAL,*   
	FROM (
		SELECT SectionId,
			SectionName, 
			ParentId,
			ROW_NUMBER() OVER (ORDER BY SectionName ASC) AS RowNumber 
		FROM Sections 
		WHERE ParentId IN (	SELECT Sections.SectionId 
								FROM Sections 
								WHERE Sections.ParentId = @parentId
									AND Sections.Active = 1
								GROUP BY Sections.SectionId, Sections.SectionName)
			AND SectionName LIKE @letter+'%'
			AND Active = 1 
	)AS SubsectionIndex
	WHERE RowNumber BETWEEN @numberByPage * @page + 1 AND @numberByPage * (@page + 1)

END
go