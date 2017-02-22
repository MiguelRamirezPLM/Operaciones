IF OBJECT_ID('dbo.roc_spGetState') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetState
go

/* 
	Author:			Daniel Campos / Ramiro Sánchez				 
	Object:			dbo.roc_spGetState
	
	Description:	Retrieves all States By Country and StateId.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetState @countryId = 4, @stateId = 75

 */ 

CREATE PROCEDURE dbo.roc_spGetState
(
	@countryId		int,
	@stateId		int
)
AS
BEGIN

	-- The parameters shouldn't be null:
	IF (@countryId IS NULL OR @stateId IS NULL)
	BEGIN
	
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
		
	END

		SELECT * FROM States 
		WHERE CountryId = @countryId
			AND Active = 1 
			AND StateId = @stateId
		ORDER BY Name

	
END
go