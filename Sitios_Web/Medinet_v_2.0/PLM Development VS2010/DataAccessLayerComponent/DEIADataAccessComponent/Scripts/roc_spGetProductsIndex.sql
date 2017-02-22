IF OBJECT_ID('dbo.roc_spGetProductsIndex') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetProductsIndex
go

/* 
	Author:			Daniel Campos. / Elizabeth Lazo.				 
	Object:			dbo.roc_spGetProductsIndex
	
	Description:	Retrieves all products by indexId.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetProductsIndex @indexId=1, @editionId = 3,  @page = 0, @numberbypage = 10


 */ 

CREATE PROCEDURE dbo.roc_spGetProductsIndex 
(
    @indexId    tinyint,
    @editionId  int, 
    @page       int,
    @numberbypage   int
)

AS
 BEGIN
     --The parameters shouldn't be null:
       IF (@indexId IS NULL OR @editionId IS NULL OR @page IS NULL OR @numberbypage  IS NULL ) 
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
	 )as contador
	)AS TOTAL,*
	FROM(
	 SELECT eps.EdProdId,p.ProductId,p.ProductTypeId,p.CompanyId,p.ProductName,eps.Description,
	 eps.HtmlFile,eps.HtmlContent,ROW_NUMBER() OVER (ORDER BY p.Productname ASC) AS RowNumber
	 FROM Products AS p
	 INNER JOIN EditionProductSections AS eps ON eps.ProductId=p.ProductId
	 INNER JOIN ProductIndexes AS pin ON p.ProductId=pin.ProductId
	 WHERE pin.IndexId=@indexId And p.Active='1' AND eps.EditionId=@editionId
	)AS x
	WHERE RowNumber BETWEEN @numberbypage * @page + 1 AND @numberbypage * (@page+1)




  
  END

 go



