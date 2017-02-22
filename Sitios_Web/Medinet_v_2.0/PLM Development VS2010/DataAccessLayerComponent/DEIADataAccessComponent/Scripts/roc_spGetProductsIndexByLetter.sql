IF OBJECT_ID('dbo.roc_spGetProductsIndexByLetter') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetProductsIndexByLetter
go

/* 
	Author:			Daniel Campos. / Elizabeth Lazo.				 
	Object:			dbo.roc_spGetProductsIndexByLetter
	
	Description:	Retrieves all products by letter.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetProductsIndexByLetter @indexId=1, @editionId= 3,  @page = 0, @numberbypage = 10, @letter ='c'


 */ 

CREATE PROCEDURE dbo.roc_spGetProductsIndexByLetter 
(
    @indexId    tinyint,
    @editionId  int,
    @page       int,
    @numberbypage  int,
    @letter     varchar(1)
)

AS
 BEGIN
     --The parameters shouldn't be null:
       IF (@indexId IS NULL OR @editionId IS NULL OR @page IS NULL OR @numberbypage  IS NULL OR @letter IS NULL) 
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
	  WHERE pin.IndexId=@indexId And p.Active='1' AND eps. EditionId=@editionId AND p.ProductName LIKE  @letter + '%'
	 )as contador
	)AS TOTAL,*
	FROM(
	 SELECT eps.EdProdId,p.ProductId,p.ProductTypeId,p.CompanyId,p.ProductName,eps.Description,
	 eps.HtmlFile,eps.HtmlContent,ROW_NUMBER() OVER (ORDER BY p.Productname ASC) AS RowNumber
	 FROM Products AS p
	 INNER JOIN EditionProductSections AS eps ON eps.ProductId=p.ProductId
	 INNER JOIN ProductIndexes AS pin ON p.ProductId=pin.ProductId
	 WHERE pin.IndexId=@indexId And p.Active='1' AND eps.EditionId=@editionId AND p.ProductName LIKE @letter + '%'
	)AS x
	WHERE RowNumber BETWEEN @numberbypage * @page  + 1 AND @numberbypage * (@page +1)



  
  END

 go



