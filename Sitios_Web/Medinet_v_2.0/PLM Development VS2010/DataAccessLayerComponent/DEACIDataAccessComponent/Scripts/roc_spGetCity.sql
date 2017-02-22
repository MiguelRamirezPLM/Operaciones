IF OBJECT_ID('dbo.roc_spGetCity') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetCity
go

/* 
	Author:			Daniel Campos / Ramiro Sánchez				 
	Object:			dbo.roc_spGetCity
	
	Description:	Retrieves all Cities By Country.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetCity @countryId = 4, @cityId = 29

 */ 

CREATE PROCEDURE dbo.roc_spGetCity
(
	@CountryId			int,
	@cityId				int
)
AS
BEGIN

	-- The parameters shouldn't be null:
	IF (@countryId IS NULL OR @cityId IS NULL)
	BEGIN
	
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
		
	END

		SELECT c.CityId, c.StateId, c.Name AS City, s.Name AS State,co.CountryName 
			FROM Cities AS c,States AS s, Countries AS co
			WHERE CityId = @cityId 
				AND c.Active = 1 
				AND s.Active = 1 
				AND c.StateId = s.StateId
				AND s.CountryId = co.CountryId 
				AND co.CountryId = @countryId
	
END
go