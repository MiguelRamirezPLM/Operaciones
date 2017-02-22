IF OBJECT_ID('dbo.roc_spGetCityWhitCompaniesByState') IS NOT NULL
     DROP PROCEDURE dbo.roc_spGetCityWhitCompaniesByState

go

/* 
	Author:			Daniel Campos. / Elizabeth Lazo.				 
	Object:			dbo.roc_spGetCityWhitCompaniesByState
	
	Description:	Retrieves all cities.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetCityWhitCompaniesByState @stateId=7, @sectionId = 190, @editionId=2


 */

CREATE PROCEDURE dbo.roc_spGetCityWhitCompaniesByState
(
   @stateId     int,
   @sectionId   int,
   @editionId   int   
)

AS
 BEGIN
   --The parameters shouldn't be null:
    IF(@stateId IS NULL OR @sectionId IS NULL OR @editionId IS NULL)
 
 BEGIN 

 RAISERROR ('The current operation requires more parameters', 16, 1)
    RETURN -1

 END


  SELECT Cities.CityId,
         Cities.StateId,
         Cities.CityName,
         Cities.Active  
 
     FROM Companies
     INNER JOIN CompanySections ON CompanySections.CompanyId = Companies.CompanyId
     INNER JOIN Cities ON Cities.CityId = Companies.CityId
     INNER JOIN States ON States.StateId  = Cities.StateId
     INNER JOIN CompanyEditions ON CompanyEditions.CompanyId = Companies.CompanyId
     INNER JOIN Editions ON Editions.EditionId = CompanyEditions.EditionId

  WHERE CompanySections.SectionId = @sectionId AND 
        Cities.StateId = @stateId AND 
        States.Active = '1' AND 
        Cities.Active = '1' AND 
        Companies.Active = '1' AND 
        Editions.EditionId = @editionId

  GROUP BY Cities.CityId,
           Cities.StateId, 
           Cities.CityName,
           Cities.Active 

END

go
 