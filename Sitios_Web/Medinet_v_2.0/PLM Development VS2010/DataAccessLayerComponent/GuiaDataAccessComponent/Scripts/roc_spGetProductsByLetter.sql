IF OBJECT_ID('dbo.roc_spGetProductsByLetter') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetProductsByLetter
go

/* 
	Author:			Benjamín Castillo Arriaga.				 
	Object:			dbo.roc_spGetProductsByLetter
	
	Description:	Retrieves all products by letter.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetProductsByLetter @editionId = 5, @numberPage = 10,@page=1,@letter = 'A'


 */ 

CREATE PROCEDURE dbo.roc_spGetProductsByLetter
(
	@editionId				int,
	@numberPage				int,
	@page					int,
	@letter                 varchar(1)
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
				   Products.Active = 1 AND 
				   Products.ProductName LIKE @letter + '%' 
			 GROUP BY ClientProducts.ProductId,
					  Products.ProductName)AS TOTAL) AS TOTAL,* 
	FROM 
		(SELECT ClientProducts.ProductId,
				Products.ProductName,
				ROW_NUMBER() OVER (ORDER BY ClientProducts.ProductId) AS RowNumber 
		 FROM ClientProducts 
			INNER JOIN Products ON(Products.ProductId = ClientProducts.ProductId)
			INNER JOIN Clients ON(Clients.ClientId = ClientProducts.ClientId)
		 WHERE EditionId = @editionId AND 
			   Products.Active = 1 AND 
			   Products.ProductName LIKE @letter + '%' 
		 GROUP BY ClientProducts.ProductId,
				  Products.ProductName)AS Productos 
	WHERE RowNumber BETWEEN @numberPage * @page + 1 AND @numberPage * (@page + 1)
	
END
go