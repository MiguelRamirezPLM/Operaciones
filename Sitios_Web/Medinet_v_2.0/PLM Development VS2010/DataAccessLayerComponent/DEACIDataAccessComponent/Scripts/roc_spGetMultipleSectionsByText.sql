IF OBJECT_ID('dbo.roc_spGetMultipleSectionsByText') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetMultipleSectionsByText
go

/* 
	Author:			Daniel Campos / Ramiro Sánchez				 
	Object:			dbo.roc_spGetMultipleSectionsByText
	
	Description:	Retrieves all Sections By Text.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetMultipleSectionsByText @page = 0, @numberByPage = 10, @text = 'drogas', @sectionId = 8

 */ 

CREATE PROCEDURE dbo.roc_spGetMultipleSectionsByText
(
	@page				int,
	@numberByPage		int,
	@text				varchar(30),
	@sectionId			int
)
AS
BEGIN

	-- The parameters shouldn't be null:
	IF (@page IS NULL OR @numberByPage IS NULL OR @text IS NULL OR @sectionId IS NULL)
	BEGIN
	
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
		
	END
		SELECT ( 
			SELECT count(*) From 
			(
				Select SectionIdParent From sections 
					Where Sections.SectionIdParent IN(Select SectionId From sections 
						Where Sections.SectionIdParent = @sectionId OR Sections.SectionId = @sectionId) 
						And Description Like '%'+@text+'%' COLLATE SQL_LATIN1_GENERAL_CP1_CI_AI 
			) As contador 
		) AS TOTAL,*
		FROM (
			select x.SectionId as SubSectionId, x.Description As SubSectionDescription, 
			Sections.SectionId, Sections.SectionIdParent,Sections.Description as SectionDescription,
			ROW_NUMBER() OVER (order by x.SectionId,Sections.Description,x.Description) AS RowNumber 
				From(
					Select SectionIdParent,SectionId,Description From sections 
					where Sections.SectionIdParent IN(Select SectionId From Sections Where Sections.SectionIdParent = @sectionId OR Sections.SectionId = @sectionId) 
						and Description like '%'+@text+'%' COLLATE SQL_LATIN1_GENERAL_CP1_CI_AI 
				) as x
			Inner Join Sections on x.SectionIdParent=Sections.SectionId
		)AS INDICE
		WHERE RowNumber BETWEEN @numberByPage * @page + 1 AND @numberByPage * (@page + 1)

END
go