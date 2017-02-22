IF OBJECT_ID('dbo.roc_spGetAdvertisementsByDivision') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetAdvertisementsByDivision
go

/* 
	Author:			Daniel Campos / Ramiro Sánchez				 
	Object:			dbo.roc_spGetAdvertisementsByDivision
	
	Description:	Retrieves all Advertisements by Division.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetAdvertisementsByDivision @divisionId = 50, @editionId = 1


 */ 

CREATE PROCEDURE dbo.roc_spGetAdvertisementsByDivision
(
	@divisionId				int,
	@editionId				int
)
AS
BEGIN

	-- The parameters shouldn't be null:
	IF (@divisionId IS NULL OR @editionId IS NULL)
	BEGIN
	
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
		
	END

		SELECT Advertisements.*,EditionDivisionAds.BaseUrl 
			FROM Advertisements 
			INNER JOIN EditionDivisionAds ON EditionDivisionAds.AdId = Advertisements.AdId
			WHERE DivisionId = @divisionId AND EditionId = @editionId AND Active = 1
	
END
go