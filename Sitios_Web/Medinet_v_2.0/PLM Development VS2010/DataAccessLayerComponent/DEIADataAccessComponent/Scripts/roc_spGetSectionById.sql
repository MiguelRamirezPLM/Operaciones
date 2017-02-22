IF OBJECT_ID('dbo.roc_spGetSectionById') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetSectionById
go

/* 
	Author:			Daniel Campos / Ramiro Sánchez				 
	Object:			dbo.roc_spGetSectionById
	
	Description:	Retrieves information from a Section.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetSectionById @sectionId = 166


 */ 

CREATE PROCEDURE dbo.roc_spGetSectionById
(
	@sectionId					int
)
AS
BEGIN

	-- The parameters shouldn't be null:
	IF (@sectionId IS NULL)
	BEGIN
	
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
		
	END

		SELECT * 
		FROM Sections 
		WHERE Sections.SectionId = @sectionId AND Active = 1
	
END
go