IF OBJECT_ID('dbo.roc_spGetSection') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetSection
go

/* 
	Author:			Daniel Campos / Ramiro Sánchez				 
	Object:			dbo.roc_spGetSection
	
	Description:	Retrieves all Sections By SectionParent and SectionId.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetSection @sectionIdParent = 7, @sectionId = 20

 */ 

CREATE PROCEDURE dbo.roc_spGetSection
(
	@sectionIdParent		int,
	@sectionId				int
)
AS
BEGIN

	-- The parameters shouldn't be null:
	IF (@sectionIdParent IS NULL OR @sectionId IS NULL)
	BEGIN
	
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
		
	END

		SELECT * 
			FROM Sections 
			WHERE Sections.SectionIdParent = @sectionIdParent 
				AND SectionId = @sectionId
	
END
go