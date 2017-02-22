IF OBJECT_ID('dbo.roc_spGetCategoriesByDivision') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetCategoriesByDivision
go

/* 
	Author:			Daniel Campos / Ramiro Sánchez				 
	Object:			dbo.roc_spGetCategoriesByDivision
	
	Description:	Retrieves all Active Categories by DivisionId.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetCategoriesByDivision @divisionId = 1


 */ 

CREATE PROCEDURE dbo.roc_spGetCategoriesByDivision
(
	@divisionId				int
)

AS
BEGIN

	-- The parameters shouldn't be null:
	IF (@divisionId IS NULL)
	BEGIN
	
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
		
	END

	SELECT Categories.* FROM DivisionCategories
		INNER JOIN Categories ON Categories.CategoryId = DivisionCategories.CategoryId
		WHERE DivisionId = @divisionId AND Categories.Active = 1
	
END
go