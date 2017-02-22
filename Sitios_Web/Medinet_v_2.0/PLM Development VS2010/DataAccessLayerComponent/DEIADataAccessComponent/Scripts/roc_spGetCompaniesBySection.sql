IF OBJECT_ID('dbo.roc_spGetCompaniesBySection') IS NOT NULL
     DROP PROCEDURE dbo.roc_spGetCompaniesBySection

go

/* 
	Author:			Daniel Campos. / Elizabeth Lazo. / Ramiro Sánchez.				 
	Object:			dbo.roc_spGetCompaniesBySection
	
	Description:	Retrieves all information company by section.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetCompaniesBySection  @editionId=3, @sectionId=164
 */

CREATE PROCEDURE dbo.roc_spGetCompaniesBySection
(
  @editionId int,
  @sectionId int
)

AS
BEGIN

   --The parametres shouldn't be null:
   IF(@editionId IS NULL OR @sectionId IS NULL)
   BEGIN
	
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
	
   END

	SELECT c.CompanyId,
		c.CompanyName,
		c.CompanyShortName,
		ce.HtmlFile,
		ce.HtmlContent,
		c.Address,
		c.Suburb,
		c.Zipcode,
		c.CityId,
		ci.CityName,
		s.StateId,
		s.StateName,
		c.Web,
		c.CommercialBusiness
	FROM Companies AS c
		INNER JOIN CompanySections AS cs ON cs.CompanyId = c.CompanyId
		INNER JOIN CompanyEditions AS ce ON ce.CompanyId = c.CompanyId
		INNER JOIN Cities AS ci ON ci.CityId = c.CityId
		INNER JOIN States AS s ON s.StateId = ci.StateId
		INNER JOIN Editions AS e ON e.EditionId = ce.EditionId
	WHERE cs.SectionId = @sectionId 
		AND c.Active = 1 
		and e.EditionId = @editionId 
	ORDER BY s.StateName, ci.CityName, c.CompanyName

END
go