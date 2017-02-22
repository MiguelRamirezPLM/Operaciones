IF OBJECT_ID('dbo.roc_spGetStates') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetStates
go

/* 
	Author:			Daniel Campos / Ramiro Sánchez				 
	Object:			dbo.roc_spGetStates
	
	Description:	Retrieves all States By Country.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetStates @countryId = 4

 */ 

CREATE PROCEDURE dbo.roc_spGetStates
(
	@countryId				int
)
AS
BEGIN

	-- The parameters shouldn't be null:
	IF (@countryId IS NULL)
	BEGIN
	
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
		
	END

		SELECT * FROM States 
			WHERE CountryId = @countryId AND ACtive = 1
			ORDER BY Name
	
END
go