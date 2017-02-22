IF OBJECT_ID('dbo.roc_spGetClientsWithoutSubProduct') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetClientsWithoutSubProduct
go

/* 
	Author:			Benjamín Castillo Arriaga.				 
	Object:			dbo.roc_spGetClientsWithoutSubProduct
	
	Description:	Retrieves all Clients by Product without subproducts.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetClientsWithoutSubProduct @editionId = 5, @productId = 3705, @numberByPage = 100, @page = 0


 */ 

CREATE PROCEDURE dbo.roc_spGetClientsWithoutSubProduct
(
	@editionId				int,
	@productId				int,
	@numberByPage				int,
	@page					int
)
AS
BEGIN

	-- The parameters shouldn't be null:
	IF (@editionId IS NULL OR @productId IS NULL OR @numberByPage IS NULL OR @page IS NULL)
	BEGIN
	
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
		
	END

	SELECT 
		(SELECT COUNT(*) 
		 FROM 
			(SELECT Clients.ClientId,
				    Clients.ClientTypeId,
					Clients.CompanyName 
			 FROM ClientProducts  
				INNER JOIN Clients ON Clients.ClientId = ClientProducts.ClientId 
			 WHERE Clients.EditionId = @editionId AND 
				   Clients.Active = 1 AND 
				   ClientProducts.ProductId = @productId AND 
				   ISNULL(ClientProducts.SubProductId,0) = 0  
			 GROUP BY Clients.ClientId,
					  Clients.ClientTypeId,
					  Clients.CompanyName)AS TOTAL) AS TOTAL,* 
	FROM 
		(SELECT Clients.ClientId,
				Clients.ClientTypeId,
				Clients.CompanyName,
				ROW_NUMBER() OVER (ORDER BY Clients.CompanyName) AS RowNumber 
		 FROM ClientProducts  
			INNER JOIN Clients ON Clients.ClientId = ClientProducts.ClientId 
		 WHERE Clients.EditionId = @editionId AND 
			   Clients.Active = 1 AND 
			   ClientProducts.ProductId = @productId AND 
			   ISNULL(ClientProducts.SubProductId,0) = 0   
		 GROUP BY Clients.ClientId,
				  Clients.ClientTypeId,
				  Clients.CompanyName)AS Productos 
	WHERE RowNumber BETWEEN @numberByPage * @page + 1 AND @numberByPage* (@page + 1)

		
END
go