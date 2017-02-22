IF OBJECT_ID('dbo.roc_spGetQualityControlIndexByText') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetQualityControlIndexByText
go

/* 
	Author:			Daniel Campos / Ramiro Sánchez				 
	Object:			dbo.roc_spGetQualityControlIndexByText
	
	Description:	Retrieves information from all Sections By Parent and Text.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetQualityControlIndexByText @parentId = 10, @page = 0, @numberByPage = 10, @text = 'sistema'

 */ 

CREATE PROCEDURE dbo.roc_spGetQualityControlIndexByText
(
	@parentId		int,
	@page			int,
	@numberByPage	int,
	@text			varchar(40)
)
AS
BEGIN

	-- The parameters shouldn't be null:
	IF (@parentId IS NULL OR @page IS NULL OR @numberByPage IS NULL OR @text IS NULL)
	BEGIN
	
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
		
	END

	SELECT (
		SELECT COUNT(*) From(
			SELECT SectionId 
			FROM Sections 
			WHERE ParentId IN (SELECT Sections.SectionId 
								FROM Sections 
								WHERE Sections.ParentId = @parentId 
									AND Sections.Active =  1
								GROUP BY Sections.SectionId, Sections.SectionName)
				AND SectionName LIKE '%'+@text+'%' COLLATE SQL_LATIN1_GENERAL_CP1_CI_AI
				AND Active = 1 
		) AS contador
	) AS TOTAL,*   
	FROM (
		SELECT SectionId,
			SectionName,
			ParentId,
			ROW_NUMBER() OVER (ORDER BY SectionName ASC) AS RowNumber 
		FROM Sections 
		WHERE ParentId IN (SELECT Sections.SectionId 
							FROM Sections 
							WHERE Sections.ParentId = @parentId
								AND Sections.Active = 1
							GROUP BY Sections.SectionId, Sections.SectionName)
			AND SectionName like '%'+@text+'%' COLLATE SQL_LATIN1_GENERAL_CP1_CI_AI
			AND Active = 1
	)AS SubsectionIndex
	WHERE RowNumber BETWEEN @numberByPage * @page + 1 AND @numberByPage * (@page + 1)

END
go