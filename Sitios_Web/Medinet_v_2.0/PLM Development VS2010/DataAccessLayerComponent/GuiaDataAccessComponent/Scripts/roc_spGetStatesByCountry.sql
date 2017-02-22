IF OBJECT_ID('dbo.roc_spGetStatesByCountry') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetStatesByCountry
go

/* 
	Author:			Benjamín Castillo Arriaga.				 
	Object:			dbo.roc_spGetStatesByCountry
	
	Description:	Retrieves all states by country.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetStatesByCountry @countryId = 1


 */ 

CREATE PROCEDURE dbo.roc_spGetStatesByCountry
(
	@countryId				tinyint
)
AS
BEGIN

	-- The parameters shouldn't be null:
	IF (@countryId IS NULL)
	BEGIN
	
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
		
	END

	SELECT * 
	FROM States 
	WHERE CountryId= @countryId AND 
		  Active = 1
	
	
END
go