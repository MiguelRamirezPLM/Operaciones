IF OBJECT_ID('dbo.roc_spGetCities') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetCities
go

/* 
	Author:			Daniel Campos / Ramiro Sánchez				 
	Object:			dbo.roc_spGetCities
	
	Description:	Retrieves all Cities By State.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetCities @stateId = 78

 */ 

CREATE PROCEDURE dbo.roc_spGetCities
(
	@stateId				int
)
AS
BEGIN

	-- The parameters shouldn't be null:
	IF (@stateId IS NULL)
	BEGIN
	
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
		
	END

		SELECT * FROM Cities 
			WHERE StateId = @stateId AND Active = 1
			ORDER BY Name
	
END
go