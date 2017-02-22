IF OBJECT_ID('dbo.roc_spGetSectionListByParentId') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetSectionListByParentId
go

/* 
	Author:			Daniel Campos / Ramiro Sánchez				 
	Object:			dbo.roc_spGetSectionListByParentId
	
	Description:	Retrieves information from Section List by ParentId.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetSectionListByParentId @parentId = 10

 */ 

CREATE PROCEDURE dbo.roc_spGetSectionListByParentId
(
	@parentId					int
)
AS
BEGIN

	-- The parameters shouldn't be null:
	IF (@parentId IS NULL)
	BEGIN
	
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
		
	END

		SELECT * FROM Sections 
			WHERE Sections.ParentId = @parentId 
				AND Active = 1
				AND (SELECT count(*) 
						FROM Sections AS SubSection 
						WHERE SubSection.ParentId = Sections.SectionId) > 0
			ORDER BY SectionName ASC
	
END
go