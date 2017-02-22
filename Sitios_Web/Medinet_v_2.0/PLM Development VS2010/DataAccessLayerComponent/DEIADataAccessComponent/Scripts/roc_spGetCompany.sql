IF OBJECT_ID('dbo.roc_spGetCompany') IS NOT NULL
     DROP PROCEDURE dbo.roc_spGetCompany

go

/* 
	Author:			Daniel Campos. / Elizabeth Lazo. / Ramiro Sánchez.
	Object:			dbo.roc_spGetCompany
	
	Description:	Retrieves all information company.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetCompany  @companyId=1, @editionId = 3
 */

CREATE PROCEDURE dbo.roc_spGetCompany
(
	@companyId  int,
	@editionId int
)

AS
BEGIN

   --The parametres shouldn't be null:
   IF(@companyId IS NULL OR @editionId IS NULL)
   BEGIN
	
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
	
   END

	SELECT c.CompanyId,
		c.ParentId,
		c.CompanyName,
		c.CompanyShortName,
		c.CompanyTypeId,
		c.CommercialBusiness,
		c.Address,
		c.CityId,
		c.Suburb,
		c.Location,
		c.ZipCode,
		c.Email,
		c.Web,
		c.ClientId,
		c.Active,
		ce.HtmlFile,
		ce.HtmlContent 
	FROM Companies AS c
		INNER JOIN CompanyEditions AS ce ON ce.CompanyId=c.CompanyId
	WHERE c.CompanyId = @companyId
		AND ce.EditionId = @editionId
		AND c.Active = 1

  END
go