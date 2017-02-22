IF OBJECT_ID('dbo.roc_spGetMultipleSectionsByFulltext') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetMultipleSectionsByFulltext
go

/* 
	Author:			Daniel Campos / Ramiro Sánchez				 
	Object:			dbo.roc_spGetMultipleSectionsByFulltext
	
	Description:	Retrieves all Sections By FullText.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetMultipleSectionsByFulltext @page = 0, @numberByPage = 10, @fullText = 'sistemas', @sectionId = 8

 */ 

CREATE PROCEDURE dbo.roc_spGetMultipleSectionsByFulltext
(
	@page				int,
	@numberByPage		int,
	@fullText			varchar(30),
	@sectionId			int
)
AS
BEGIN

	-- The parameters shouldn't be null:
	IF (@page IS NULL OR @numberByPage IS NULL OR @fullText IS NULL OR @sectionId IS NULL)
	BEGIN
	
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
		
	END
		SELECT ( 
			SELECT Count(*) From 
			(
				Select SectionIdParent from sections 
					Where Sections.SectionIdParent IN(Select SectionId 
						From Sections 
						Where Sections.SectionIdParent = @sectionId OR Sections.SectionId = @sectionId) 
						And FREETEXT(Description,@fullText)
			) As contador 
		) AS TOTAL,*
		FROM (
			Select x.SectionId as SubSectionId,x.Description As SubSectionDescription, 
			Sections.SectionId, Sections.SectionIdParent,Sections.Description as SectionDescription,
			ROW_NUMBER() OVER (order by x.SectionId,Sections.Description,x.Description) AS RowNumber From(
				Select SectionIdParent,SectionId,Description from sections 
					Where Sections.SectionIdParent in (select SectionId 
						From sections Where Sections.SectionIdParent = @sectionId OR Sections.SectionId = @sectionId) 
						And FREETEXT(Description,@fullText) 
			) As x
			Inner Join Sections ON x.SectionIdParent=Sections.SectionId
		)AS INDICE
		WHERE RowNumber BETWEEN @numberByPage * @page + 1 AND @numberByPage * (@page + 1)

END
go