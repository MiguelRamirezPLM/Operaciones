IF OBJECT_ID('dbo.plm_spGetPSFamiliesByDivision') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetPSFamiliesByDivision
go

/* 
	Author:			Ramiro Sánchez
	Object:			dbo.plm_spGetPSFamiliesByDivision
	
	Description:	Gets all Families by Division
	Company:		PLM Latina.

	EXECUTE dbo.plm_spGetPSFamiliesByDivision @editionId = 55, @divisionId = 3

 */ 

CREATE PROCEDURE dbo.plm_spGetPSFamiliesByDivision
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
		INNER JOIN FamilyProductShots fps ON f.FamilyId = fps.FamilyId
		INNER JOIN EditionProductShots eps ON fps.EditionProductShotId = eps.EditionProductShotId
	WHERE f.Active = 1 
		AND eps.EditionId = @editionId 
		AND eps.DivisionId = @divisionId
	ORDER BY 1
END
go