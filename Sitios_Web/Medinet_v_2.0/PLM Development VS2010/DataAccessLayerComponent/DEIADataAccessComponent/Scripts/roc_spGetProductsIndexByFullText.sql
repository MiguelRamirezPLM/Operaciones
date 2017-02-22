IF OBJECT_ID('dbo.roc_spGetProductsIndexByFullText') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetProductsIndexByFullText
go

/* 
	Author:			Daniel Campos. / Elizabeth Lazo.				 
	Object:			dbo.roc_spGetProductsIndexByFullText
	
	Description:	Retrieves all products by fulltext.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetProductsIndexByFullText @indexId=1, @editionId=3,  @page = 0, @numberbypage = 10, @text ='mineral'


 */ 

CREATE PROCEDURE dbo.roc_spGetProductsIndexByFullText 
(
    @indexId    tinyint,
    @editionId  int,
    @page       int,
    @numberbypage  int,
    @text     varchar(250)
)

AS
 BEGIN
     --The parameters shouldn't be null:
       IF (@indexId IS NULL OR @editionId IS NULL OR @page IS NULL OR @numberbypage  IS NULL OR @text IS NULL) 
  BEGIN

    RAISERROR ('The current operation requires more parameters', 16, 1)
    RETURN -1

  END 

SELECT(
	 SELECT count(*) FROM
	 (
	  SELECT p.* 
	  FROM Products AS p
	  INNER JOIN EditionProductSections AS eps ON eps.ProductId=p.ProductId
	  INNER JOIN ProductIndexes AS pin ON p.ProductId=pin.ProductId
	  WHERE pin.IndexId=@indexId And p.Active='1' AND eps. EditionId=@editionId 
	 AND (FREETEXT(P.ProductName,@text) 
	 OR FREETEXT(eps.Description, @text))
	 )as contador
	)AS TOTAL,*
	FROM(
	 SELECT eps.EdProdId,p.ProductId,p.ProductTypeId,p.CompanyId,p.ProductName,eps.Description,
	 eps.HtmlFile,eps.HtmlContent,ROW_NUMBER() OVER (ORDER BY p.Productname ASC) AS RowNumber
	 FROM Products AS p
	 INNER JOIN EditionProductSections AS eps ON eps.ProductId=p.ProductId
	 INNER JOIN ProductIndexes AS pin ON p.ProductId=pin.ProductId
	 WHERE pin.IndexId=@indexId And p.Active='1' AND eps.EditionId=@editionId 
	  AND (FREETEXT(P.ProductName,@text) 
	 OR FREETEXT(eps.Description, @text))
	)AS x
	WHERE RowNumber BETWEEN @numberbypage * @page + 1 AND @numberbypage * (@page+1)


  END

 go


