IF OBJECT_ID('dbo.roc_spGetStatesWithCompaniesBySection') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetStatesWithCompaniesBySection
go

/* 
	Author:			Daniel Campos. / Elizabeth Lazo.				 
	Object:			dbo.roc_spGetStatesWithCompaniesBySection
	
	Description:	Retrieves all states by company.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetStatesWithCompaniesBySection @sectionId= 190, @editionId = 2

 */ 

CREATE PROCEDURE dbo.roc_spGetStatesWithCompaniesBySection
(
  @sectionId  int,
  @editionId  int
)

AS
 BEGIN
  --The parameters shouldn't be null:
       IF (@sectionId IS NULL OR @editionId IS NULL) 
  BEGIN

    RAISERROR ('The current operation requires more parameters', 16, 1)
    RETURN -1

  END

    SELECT States.StateId, 
           States.CountryId,
           States.StateName,
           States.Active
  
    FROM Companies
      INNER JOIN CompanySections ON CompanySections.CompanyId = Companies.CompanyId
      INNER JOIN Cities ON Cities.CityId = Companies.CityId
      INNER JOIN States ON States.StateId  = Cities.StateId
      INNER JOIN CompanyEditions ON CompanyEditions.CompanyId = Companies.CompanyId
      INNER JOIN Editions ON Editions.EditionId = CompanyEditions.EditionId

    WHERE CompanySections.SectionId = @sectionId AND 
          States.Active = '1' AND 
          Cities.Active = '1' AND 
          Companies.Active = '1' AND 
          Editions.EditionId = @editionId

   GROUP BY States.StateId, 
            States.CountryId,
            States.StateName,
            States.Active

END
  go
















