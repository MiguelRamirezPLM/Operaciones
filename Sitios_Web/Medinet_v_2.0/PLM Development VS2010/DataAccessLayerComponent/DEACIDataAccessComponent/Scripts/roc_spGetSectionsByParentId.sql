IF OBJECT_ID('dbo.roc_spGetSectionsByParentId') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetSectionsByParentId
go

/* 
	Author:			Daniel Campos / Ramiro Sánchez				 
	Object:			dbo.roc_spGetSectionsByParentId
	
	Description:	Retrieves all Sections By SectionParent.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetSectionsByParentId @sectionIdParent = 71

 */ 

CREATE PROCEDURE dbo.roc_spGetSectionsByParentId
(
	@sectionIdParent		int
)
AS
BEGIN

	-- The parameters shouldn't be null:
	IF (@sectionIdParent IS NULL)
	BEGIN
	
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
		
	END

		SELECT SectionId, [Description] 
			FROM Sections 
			WHERE SectionIdParent = @sectionIdParent 
				AND Active = 1
			ORDER BY [Description]
	
END
go