IF OBJECT_ID('dbo.roc_spGetAdvertisementsByDivision') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetAdvertisementsByDivision
go

/* 
	Author:			Daniel Campos / Ramiro Sánchez				 
	Object:			dbo.roc_spGetAdvertisementsByDivision
	
	Description:	Retrieves information all Advertisements by Edition and Division.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetAdvertisementsByDivision @divisionId = 86, @editionId = 4

 */ 

CREATE PROCEDURE dbo.roc_spGetAdvertisementsByDivision
(
	@divisionId			int,
	@editionId			int
)
AS
BEGIN

	-- The parameters shouldn't be null:
	IF (@divisionId IS NULL OR @editionId IS NULL)
	
	BEGIN
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
	END

	SELECT Advertisements.AdId, Advertisements.AdFile, EditionDivisionAds.EditionId, EditionDivisionAds.DivisionId, EditionDivisionAds.BaseUrl 
		FROM Advertisements 
		INNER JOIN EditionDivisionAds ON Advertisements.AdId = EditionDivisionAds.AdId
		WHERE EditionDivisionAds.DivisionId = @divisionId  
			AND Advertisements.Active = 1 
			AND EditionDivisionAds.EditionId = @editionId
	
END
go