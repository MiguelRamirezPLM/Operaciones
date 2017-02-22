IF OBJECT_ID('dbo.roc_spGetIndexProductsBySection') IS NOT NULL
     DROP PROCEDURE dbo.roc_spGetIndexProductsBySection

go

/* 
	Author:			Daniel Campos. / Ramiro Sánchez.
	Object:			dbo.roc_spGetIndexProductsBySection
	
	Description:	Retrieves Product Information by Edition and Section.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetIndexProductsBySection  @editionId = 3, @sectionId = 34
 */

CREATE PROCEDURE dbo.roc_spGetIndexProductsBySection
(
	@editionId  int,
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

	SELECT p.ProductId,
		p.ParentId,
		p.ProductTypeId,
		p.CompanyId,
		p.ProductName,
		p.Description As ProductDescription,
		eps.EditionId,
		eps.SectionId,
		eps.HtmlFile,
		eps.HtmlContent,
		eps.Description As EPSDescription
	FROM EditionProductSections AS eps
		INNER JOIN Products AS p ON p.ProductId = eps.ProductId
	WHERE eps.EditionId = @editionId
		AND eps.SectionId = @sectionId
		AND p.Active = 1
	ORDER BY p.ProductName

  END
go