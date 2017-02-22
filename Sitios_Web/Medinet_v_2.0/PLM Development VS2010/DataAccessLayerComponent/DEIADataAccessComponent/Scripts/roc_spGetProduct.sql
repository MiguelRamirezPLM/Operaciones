IF OBJECT_ID('dbo.roc_spGetProduct') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetProduct
go

/* 
	Author:			Daniel Campos / Ramiro Sánchez				 
	Object:			dbo.roc_spGetProduct
	
	Description:	Retrieves information from a product.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetProduct @productId = 1663, @editionId = 3
 */ 

CREATE PROCEDURE dbo.roc_spGetProduct
(
	@productId		int,
	@editionId		int
)
AS
BEGIN

	-- The parameters shouldn't be null:
	IF (@productId IS NULL OR @editionId IS NULL)
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
	FROM Products AS p
		INNER JOIN EditionProductSections AS eps ON eps.ProductId=p.ProductId
	WHERE p.ProductId = @productId
		AND p.Active = 1
		AND eps.EditionId = @editionId

END
go