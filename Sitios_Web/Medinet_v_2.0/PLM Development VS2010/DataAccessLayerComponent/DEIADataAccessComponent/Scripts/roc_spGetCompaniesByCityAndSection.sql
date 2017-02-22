IF OBJECT_ID('dbo.roc_spGetCompaniesByCityAndSection') IS NOT NULL
     DROP PROCEDURE dbo.roc_spGetCompaniesByCityAndSection

go

/* 
	Author:			Daniel Campos. / Elizabeth Lazo.				 
	Object:			dbo.roc_spGetCompaniesByCityAndSection
	
	Description:	Retrieves all information company by city.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetCompaniesByCityAndSection  @sectionId=233, @cityId=55, @editionId=2 


 */

CREATE PROCEDURE dbo.roc_spGetCompaniesByCityAndSection
(
  @sectionId  int,
  @cityId     int,
  @editionId  int

)


AS
BEGIN

   --The parametres shouldn't be null:
   IF(@sectionId IS NULL OR @cityId IS NULL OR @editionId IS NULL )
   BEGIN
	
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
	
   END

   SELECT Companies.*
   FROM Companies
     INNER JOIN CompanySections ON CompanySections.CompanyId = Companies.CompanyId
     INNER JOIN CompanyEditions ON CompanyEditions.CompanyId = Companies.CompanyId
     INNER JOIN Editions ON Editions.EditionId = CompanyEditions.EditionId

  WHERE CompanySections.SectionId = @sectionId AND 
        Companies.CityId = @cityId  AND 
        Companies.Active = '1'  AND 
        Editions.EditionId = @editionId



  END

go