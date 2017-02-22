IF OBJECT_ID('dbo.plm_spGetDivisionsByCountry') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetDivisionsByCountry
go

/* 
	Author:			Ramiro Sánchez
	Object:			dbo.plm_spGetDivisionsByCountry
	
	Description:	Gets all the divisions from database given a country.

	Company:		PLM Latina.

	EXECUTE dbo.plm_spGetDivisionsByCountry @countryId = 11

 */ 

CREATE PROCEDURE dbo.plm_spGetDivisionsByCountry
(
	@countryId		int
)
AS
BEGIN

	Select Distinct d.DivisionId,
					d.ParentId,
					d.LaboratoryId,
					d.CountryId,
					d.[Description],
					d.ShortName,
					d.Active

	--FROM plm_vwProductsByEdition v
	--	Inner Join Divisions d On v.DivisionId = d.DivisionId
	--Where v.CountryId = @countryId
	FROM Divisions d 
	Where d.CountryId = @countryId
    Order By 6

END
go