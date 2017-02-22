IF OBJECT_ID('dbo.roc_spGetProductsByEditionByText') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetProductsByEditionByText
go

/* 
	Author:			Benjamín Castillo Arriaga.				 
	Object:			dbo.roc_spGetProductsByEditionByText
	
	Description:	Retrieves all products by edition by text.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetProductsByEditionByText @editionId = 5, @numberPage = 10,@page=1,@text = 'ab'


 */ 

CREATE PROCEDURE dbo.roc_spGetProductsByEditionByText
(
	@editionId				int,
	@numberPage				int,
	@page					int,
	@text                   varchar(255)
)
AS
BEGIN

	-- The parameters shouldn't be null:
	IF (@editionId IS NULL OR @numberPage IS NULL OR @page IS NULL)
	BEGIN
	
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
		
	END

	SELECT 
		(SELECT COUNT(*) 
		 FROM 
			(SELECT ClientProducts.ProductId,
					Products.ProductName 
			 FROM ClientProducts 
				INNER JOIN SubProducts ON(SubProducts.SubProductId = ClientProducts.SubProductId)
				INNER JOIN Products ON(Products.ProductId = ClientProducts.ProductId)
				INNER JOIN Clients ON(Clients.ClientId = ClientProducts.ClientId)
			 WHERE EditionId = @editionId AND 
				   Products.Active = 1 AND 
				   Products.ProductName LIKE '%' + @text + '%' OR 
				   SubProducts.Description LIKE '%' + @text + '%' 
			 GROUP BY ClientProducts.ProductId,
					  Products.ProductName)AS TOTAL) AS TOTAL,* 
	FROM 
		(SELECT ClientProducts.ProductId,
				Products.ProductName,
				ROW_NUMBER() OVER (ORDER BY ClientProducts.ProductId) AS RowNumber 
		 FROM ClientProducts 
			INNER JOIN SubProducts ON(SubProducts.SubProductId = ClientProducts.SubProductId)
			INNER JOIN Products ON(Products.ProductId = ClientProducts.ProductId)
			INNER JOIN Clients ON(Clients.ClientId = ClientProducts.ClientId)
		 WHERE EditionId = @editionId AND 
			   Products.Active = 1 AND 
			   Products.ProductName LIKE '%' + @text + '%' OR 
			   SubProducts.Description LIKE '%' + @text + '%' 
		 GROUP BY ClientProducts.ProductId,
				  Products.ProductName)AS Productos 
	WHERE RowNumber BETWEEN @numberPage * @page + 1 AND @numberPage * (@page + 1)
	
END
go