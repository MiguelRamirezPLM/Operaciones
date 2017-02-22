IF OBJECT_ID('dbo.plm_spGetFamilyPrefixesByEdition') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetFamilyPrefixesByEdition
go

/* 
	Author:			Ramiro Sánchez
	Object:			dbo.plm_spGetFamilyPrefixesByEdition
	
	Description:	Gets all Families by Division
	Company:		PLM Latina.

	EXECUTE dbo.plm_spGetFamilyPrefixesByEdition @editionId = 80, @prefixTypeId = 2

 */ 

CREATE PROCEDURE dbo.plm_spGetFamilyPrefixesByEdition
(
	@editionId			int,
	@prefixTypeId		tinyint
)
AS
BEGIN

	SELECT DISTINCT PrefixId
			,EditionId
			,PrefixTypeId
			,PrefixName
			,CurrentValue
			,PrefixDescription
			,Active
	FROM FamilyPrefixes
	WHERE Active = 1 
		AND EditionId = @editionId 
		AND PrefixTypeId = @prefixTypeId
	ORDER BY 1
END
go