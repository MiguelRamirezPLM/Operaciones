IF OBJECT_ID('dbo.roc_spGetAdvertisementsByCompany') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetAdvertisementsByCompany
go

/* 
	Author:			Daniel Campos / Ramiro Sánchez				 
	Object:			dbo.roc_spGetAdvertisementsByCompany
	
	Description:	Retrieves all Advertisements By Company and Edition.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetAdvertisementsByCompany @companyId = 1, @editionId = 2

 */ 

CREATE PROCEDURE dbo.roc_spGetAdvertisementsByCompany
(
	@companyId			int,
	@editionId			int
)
AS
BEGIN

	-- The parameters shouldn't be null:
	IF (@companyId IS NULL OR @editionId IS NULL)
	BEGIN
	
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
		
	END

		SELECT Advertisements.AdvId, Advertisements.HiredSpace, Advertisements.AdvFile, 
			AdvertisementEditions.CompanyId, AdvertisementEditions.EditionId 
			FROM Advertisements 
				INNER JOIN AdvertisementEditions ON Advertisements.AdvId = AdvertisementEditions.AdvId	
				WHERE AdvertisementEditions.CompanyId = @companyId
					AND AdvertisementEditions.EditionId = @editionId
	
END
go