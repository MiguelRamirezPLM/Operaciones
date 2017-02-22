IF OBJECT_ID('dbo.roc_spGetQualityControlIndexByFullText') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetQualityControlIndexByFullText
go

/* 
	Author:			Daniel Campos / Ramiro Sánchez				 
	Object:			dbo.roc_spGetQualityControlIndexByFullText
	
	Description:	Retrieves information from all Sections By Parent and fulltext.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetQualityControlIndexByFullText @parentId = 10, @page = 0, @numberByPage = 10, @fulltext = 'monitoreo'
 */ 

CREATE PROCEDURE dbo.roc_spGetQualityControlIndexByFullText
(
	@parentId		int,
	@page			int,
	@numberByPage	int,
	@fullText			varchar(30)
)
AS
BEGIN

	-- The parameters shouldn't be null:
	IF (@parentId IS NULL OR @page IS NULL OR @numberByPage IS NULL OR @fullText IS NULL)
	BEGIN
	
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
		
	END

	SELECT (
		SELECT COUNT(*) FROM(
			SELECT SectionId 
			FROM Sections 
			WHERE ParentId IN (	SELECT Sections.SectionId 
									FROM Sections 
									WHERE Sections.ParentId = @parentId
										AND Sections.Active = 1
									GROUP BY Sections.SectionId, Sections.SectionName)
				AND FREETEXT(SectionName,@fullText)
				AND Active = 1 
		) AS contador
	) AS TOTAL,*   
	FROM(
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
			AND FREETEXT(SectionName,@fullText)
			AND Active = 1    
	)AS SubsectionIndex
	WHERE RowNumber BETWEEN @numberByPage * @page + 1 AND @numberByPage * (@page + 1)	

END
go