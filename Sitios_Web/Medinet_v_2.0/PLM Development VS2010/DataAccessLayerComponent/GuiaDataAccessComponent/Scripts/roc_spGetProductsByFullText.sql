IF OBJECT_ID('dbo.roc_spGetProductsByFullText') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetProductsByFullText
go


/* 
	Author:			Benjamín Castillo Arriaga/Elizabeth Lazo.				 
	Object:			[dbo].[roc_spGetProductsByFullText] 

	
	Description:	Retrieves all products by fulltext .

	Company:		PLM Latina.

	

	EXECUTE [dbo].[roc_spGetProductsByFullText]  @editionId = 6, @text = 'UNIDADES DE RESUCITACIÓN', @numberByPage = 10,@page = 0

	

 */ 

CREATE PROCEDURE [dbo].[roc_spGetProductsByFullText]
(
	@editionId				int,
	@text					varchar(255),
	@numberByPage			int,
	@page					int
)
AS
BEGIN

	-- The parameters shouldn't be null:
	IF (@editionId IS NULL OR @text IS NULL OR @numberByPage IS NULL OR @page IS NULL)
	BEGIN
	
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
		
	END


set @text =  '"'  + @text + '"' 


SELECT 
(
    SELECT COUNT(*) 
   FROM 
(
    SELECT Products.ProductName, 
           SubProducts.Description 

  FROM ClientProducts 
       FULL OUTER JOIN Products ON Products.ProductId = ClientProducts.ProductId 
       FULL OUTER JOIN SubProducts ON SubProducts.SubProductId = ClientProducts.SubProductId 
       FULL OUTER JOIN Clients ON Clients.ClientId = ClientProducts.ClientId 

 WHERE (CONTAINS(Products.ProductName , @text) OR 
        CONTAINS(SubProducts.Description , @text)) AND 
                 EditionId=@editionId AND Products.Active = 'true' AND 
                (SubProducts.Active = 'true'  or 
                 ISNULL(ClientProducts.SubProductId,0) = 0)
 
 GROUP BY Products.ProductId,
          SubProducts.SubProductId,
          Products.ProductName,
          SubProducts.Description
)
AS TOTAL
) 
AS TOTAL,* 
 FROM 
(
    SELECT ClientProducts.ProductId,
           ClientProducts.SubProductId,
           Products.ProductName,
           SubProducts.Description,
           ROW_NUMBER() OVER 

  (ORDER BY Products.ProductName) AS RowNumber 

  FROM ClientProducts 
       FULL OUTER JOIN Products ON Products.ProductId = ClientProducts.ProductId 
       FULL OUTER JOIN SubProducts ON SubProducts.SubProductId = ClientProducts.SubProductId 
       FULL OUTER JOIN Clients ON Clients.ClientId = ClientProducts.ClientId 

  WHERE (CONTAINS(Products.ProductName , @text) OR 
         CONTAINS(SubProducts.Description , @text)) AND 
         EditionId=@editionId AND 
         Products.Active = 'true' AND 
        (SubProducts.Active = 'true' or 
         ISNULL(ClientProducts.SubProductId,0) = 0) 

  GROUP BY ClientProducts.ProductId,
           ClientProducts.SubProductId,
           Products.ProductName,
           SubProducts.Description
)

AS Productos 

  WHERE RowNumber BETWEEN @numberByPage *@page+ 1 AND @numberByPage* (@page + 1)

END