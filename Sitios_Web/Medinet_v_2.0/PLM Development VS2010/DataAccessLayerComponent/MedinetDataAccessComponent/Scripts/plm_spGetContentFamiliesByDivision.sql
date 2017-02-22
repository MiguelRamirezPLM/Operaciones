IF OBJECT_ID('dbo.plm_spGetContentFamiliesByDivision') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetContentFamiliesByDivision
go

/* 
	Author:			Ramiro Sánchez
	Object:			dbo.plm_spGetContentFamiliesByDivision
	
	Description:	Gets all Families by Division
	Company:		PLM Latina.

	EXECUTE dbo.plm_spGetContentFamiliesByDivision @editionId = 55, @divisionId = 3

 */ 

CREATE PROCEDURE dbo.plm_spGetContentFamiliesByDivision
(
	@editionId		int,
	@divisionId		int
)
AS
BEGIN

	SELECT DISTINCT f.FamilyId
		  ,f.PrefixId
		  ,f.FamilyString
		  ,f.Active
	FROM Families f
		INNER JOIN FamilyProducts fp ON f.FamilyId = fp.FamilyId
	WHERE f.Active = 1 
		AND fp.EditionId = @editionId 
		AND fp.DivisionId = @divisionId
	ORDER BY 1
END
go