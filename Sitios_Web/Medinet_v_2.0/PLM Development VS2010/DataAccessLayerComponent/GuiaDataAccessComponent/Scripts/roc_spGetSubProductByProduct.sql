IF OBJECT_ID('dbo.roc_spGetSubProductByProduct') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetSubProductByProduct
go

/* 
	Author:			Benjamín Castillo Arriaga.				 
	Object:			dbo.roc_spGetSubProductByProduct
	
	Description:	Retrieves all Subproducts by  Product.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetSubProductByProduct @editionId = 5, @productId = 3405, @numberPage = 100, @page = 0


 */ 

CREATE PROCEDURE dbo.roc_spGetSubProductByProduct
(
	@editionId				int,
	@productId				int,
	@numberPage				int,
	@page					int
)
AS
BEGIN

	-- The parameters shouldn't be null:
	IF (@editionId IS NULL OR @productId IS NULL OR @numberPage IS NULL OR @page IS NULL)
	BEGIN
	
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
		
	END

	SELECT 
		(SELECT COUNT(*) 
		 FROM 
			(SELECT SubProducts.SubProductId,
					SubProducts.Description,
					Clients.ClientId,
					Clients.ClientTypeId,
					Clients.CompanyName 
			 FROM ClientProducts  
					INNER JOIN SubProducts ON SubProducts.SubProductId = ClientProducts.SubProductId 
					INNER JOIN Clients ON Clients.ClientId = ClientProducts.ClientId 
			 WHERE Clients.EditionId = @editionId AND 
				   Clients.Active = 1 AND 
				   ClientProducts.ProductId = @productId AND 
				   SubProducts.Active = 1
			 GROUP BY SubProducts.SubProductId,
					  SubProducts.Description,
					  Clients.ClientId,
					  Clients.ClientTypeId,Clients.CompanyName)AS TOTAL) AS TOTAL,* 
	FROM 
		(SELECT SubProducts.SubProductId,
				SubProducts.Description,
				Clients.ClientId,
				Clients.ClientTypeId,
				Clients.CompanyName ,
				ROW_NUMBER() OVER (ORDER BY SubProducts.SubProductId,SubProducts.Description) AS RowNumber 
		 FROM ClientProducts  
				INNER JOIN SubProducts ON SubProducts.SubProductId = ClientProducts.SubProductId 
				INNER JOIN Clients ON Clients.ClientId = ClientProducts.ClientId 
		 WHERE Clients.EditionId = @editionId AND 
			   Clients.Active = 1 AND 
			   ClientProducts.ProductId = @productId AND 
			   SubProducts.Active = 1  
		 GROUP BY SubProducts.SubProductId,
				  SubProducts.Description,
				  Clients.ClientId,
				  Clients.ClientTypeId,
				  Clients.CompanyName)AS Productos 
		 WHERE RowNumber BETWEEN @numberPage * @page + 1 AND @numberPage * (@page + 1)



		
END
go