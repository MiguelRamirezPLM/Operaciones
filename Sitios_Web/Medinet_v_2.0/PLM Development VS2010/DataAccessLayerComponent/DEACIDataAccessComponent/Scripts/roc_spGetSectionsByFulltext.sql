IF OBJECT_ID('dbo.roc_spGetSectionsByFulltext') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetSectionsByFulltext
go

/* 
	Author:			Daniel Campos / Ramiro Sánchez				 
	Object:			dbo.roc_spGetSectionsByFulltext
	
	Description:	Retrieves all Sections By SectionIdParent and FullText.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetSectionsByFulltext @page = 0, @numberByPage = 10, @sectionIdParent = 7, @fullText = 'sistemas y equipos'

 */ 

CREATE PROCEDURE dbo.roc_spGetSectionsByFulltext
(
	@page				int,
	@numberByPage		int,
	@sectionIdParent	int,
	@fullText			varchar(30)
)
AS
BEGIN

	-- The parameters shouldn't be null:
	IF (@page IS NULL OR @numberByPage IS NULL OR @sectionIdParent IS NULL OR @fullText IS NULL)
	BEGIN
	
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
		
	END

		SELECT (
			SELECT count(*) from 
			(
				SELECT Sections.SectionId, Sections.Description 
					FROM Sections 
					WHERE Sections.SectionIdParent = @sectionIdParent AND FREETEXT(Sections.Description,@fullText)
					GROUP BY Sections.SectionId, Sections.Description
			) as contador
		) AS TOTAL,*
		FROM (
			SELECT Sections.SectionId, Sections.Description, 
				ROW_NUMBER() OVER (ORDER BY Sections.Description ASC) AS RowNumber  
				FROM Sections 
				WHERE Sections.SectionIdParent = @sectionIdParent AND FREETEXT(Sections.Description,@fullText)
				GROUP BY Sections.SectionId, Sections.Description
		)AS INDICE
		WHERE RowNumber BETWEEN @numberByPage * @page + 1 AND @numberByPage * (@page + 1)

END
go