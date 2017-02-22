IF OBJECT_ID('dbo.roc_spGetProduct') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetProduct
go

/* 
	Author:			Daniel Campos. / Elizabeth Lazo.				 
	Object:			dbo.roc_spGetProduct
	
	Description:	Retrieves all products news by edition.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetProduct  @editionId = 1, @countryId=10, @productId = 957


 */ 

CREATE PROCEDURE dbo.roc_spGetProduct
(
 @editionId      int,
 @countryId      tinyint,
 @productId      int
)

AS
  BEGIN
  --The parameters shouldn't be null:
       IF ( @editionId IS NULL OR @countryId IS NULL OR @productId IS NULL) 
  BEGIN

    RAISERROR ('The current operation requires more parameters', 16, 1)
    RETURN -1

  END

SELECT ParticipantProducts.ProductId,
       ParticipantProducts.PharmaFormId,
       ParticipantProducts.EditionId,
       ParticipantProducts.DivisionId,
       ParticipantProducts.CategoryId,
       ParticipantProducts.HTMLContent,
       ParticipantProducts.Page,
       Products.ProductName,
       Products.CountryId,
       Products.LaboratoryId,
       Products.Description,
       Products.Active 
       
FROM ParticipantProducts

   INNER JOIN Products ON Products.ProductId = ParticipantProducts.ProductId
WHERE ParticipantProducts.ProductId=@productId AND 
      EditionId=@editionId AND 
      Products.Active='1' AND 
      Products.CountryId=@countryId 
 
  END
 go