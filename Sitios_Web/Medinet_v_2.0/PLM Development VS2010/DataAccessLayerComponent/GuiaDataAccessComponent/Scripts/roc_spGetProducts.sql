IF OBJECT_ID('dbo.roc_spGetProducts') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetProducts
go

/* 
	Author:			Benjamín Castillo Arriaga.				 
	Object:			dbo.roc_spGetProducts
	
	Description:	Retrieves all products by edition.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetProducts @editionId = 5, @numberPage = 10,@page=1


 */ 

CREATE PROCEDURE dbo.roc_spGetProducts
(
	@editionId				int,
	@numberPage				int,
	@page					int
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
				INNER JOIN Products ON(Products.ProductId = ClientProducts.ProductId)
				INNER JOIN Clients ON(Clients.ClientId = ClientProducts.ClientId)
			 WHERE EditionId = @editionId AND 
				   Products.Active = 1 
			 GROUP BY ClientProducts.ProductId,
					  Products.ProductName )AS TOTAL) AS TOTAL,* 
	FROM 
		(SELECT ClientProducts.ProductId,
				Products.ProductName,
				ROW_NUMBER() OVER (ORDER BY ClientProducts.ProductId) AS RowNumber 
		 FROM ClientProducts 
			INNER JOIN Products ON(Products.ProductId = ClientProducts.ProductId)
			INNER JOIN Clients ON(Clients.ClientId = ClientProducts.ClientId)
		 WHERE EditionId = @editionId AND 
			   Products.Active = 1 
		 GROUP BY ClientProducts.ProductId,
				  Products.ProductName)AS Productos 
	WHERE RowNumber BETWEEN @numberPage * @page + 1 AND @numberPage * (@page + 1)
	
	
END
go