IF OBJECT_ID('dbo.roc_spGetAdvertisement') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetAdvertisement
go

/* 
	Author:			Daniel Campos. / Elizabeth Lazo.
				 
	Object:			dbo.roc_spGetAdvertisement
	
	Description:	Retrieves all advertisements.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetAdvertisement  @editionId = 2, @companyTypeId = 1, @cityId=123


 */ 

CREATE PROCEDURE dbo.roc_spGetAdvertisement
(
  @cityId              int,  
  @editionId           int,
  @companyTypeId       tinyint
  
)
AS
  BEGIN
  --The parameters shouldn't be null:
       IF (@editionId IS NULL OR @companyTypeId  IS NULL
           OR @cityId IS NULL) 
  BEGIN

    RAISERROR ('The current operation requires more parameters', 16, 1)
    RETURN -1

  END


SELECT Companies.*, CompanyEditions.EditionId, CompanyEditions.HtmlFile, Editions.CountryId, Editions.ParentId,    
       Editions.BookId, Editions.NumberEdition, Editions.ISBN, Editions.Barcode
FROM Companies 
INNER JOIN CompanyEditions   ON CompanyEditions.CompanyId = Companies.CompanyId
INNER JOIN Editions  ON Editions.EditionId = CompanyEditions.EditionId
WHERE Companies.CityId = @cityId AND Companies.CompanyTypeId = @companyTypeId AND Editions.EditionId = @editionId 
 
END
go