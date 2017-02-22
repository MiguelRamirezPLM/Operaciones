IF OBJECT_ID('dbo.roc_spGetEditionProduct') IS NOT NULL
     DROP PROCEDURE dbo.roc_spGetEditionProduct

go

/* 
	Author:			Daniel Campos. / Ramiro Sánchez.
	Object:			dbo.roc_spGetEditionProduct
	
	Description:	Retrieves Product information By Edition.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetEditionProduct  @productId=1663, @editionId = 3
 */

CREATE PROCEDURE dbo.roc_spGetEditionProduct
(
	@productId  int,
	@editionId int
)

AS
BEGIN

   --The parametres shouldn't be null:
   IF(@productId IS NULL OR @editionId IS NULL)
   BEGIN
	
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
	
   END

	SELECT p.ProductId,
		p.ProductName,
		eps.Description,
		eps.SectionId,
		eps.HtmlFile,
		eps.HtmlContent
	FROM Products AS p
		INNER JOIN EditionProductSections AS eps ON eps.EdProdId=p.ProductId
	WHERE p.ProductId = @productId
		AND p.Active = 1
		AND eps.EditionId = @editionId

  END
go