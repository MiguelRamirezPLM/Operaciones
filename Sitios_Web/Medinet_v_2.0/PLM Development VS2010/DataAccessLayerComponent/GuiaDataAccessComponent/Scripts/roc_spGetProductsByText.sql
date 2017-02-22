IF OBJECT_ID('dbo.roc_spGetProductsByText') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetProductsByText
go

/* 
	Author:			Benjamín Castillo Arriaga.				 
	Object:			dbo.roc_spGetProductsByText
	
	Description:	Retrieves all products By Text.

	Company:		PLM Latina.

	

	EXECUTE dbo.roc_spGetProductsByText @editionId = 5,@text = 'AGUJAS',@numberByPage = 100,@page = 0

	

 */ 

CREATE PROCEDURE dbo.roc_spGetProductsByText
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

	SELECT 
		(SELECT COUNT(*) 
		 FROM (SELECT Products.ProductName, 
					  SubProducts.Description 
			   FROM ClientProducts 
					FULL OUTER JOIN Products ON Products.ProductId = ClientProducts.ProductId 
					FULL OUTER JOIN SubProducts ON SubProducts.SubProductId = ClientProducts.SubProductId 
					FULL OUTER JOIN Clients ON Clients.ClientId = ClientProducts.ClientId 
				WHERE (Products.ProductName LIKE '%' + @text + '%' COLLATE SQL_LATIN1_GENERAL_CP1_CI_AI OR 
					   SubProducts.Description LIKE '%' + @text + '%' COLLATE SQL_LATIN1_GENERAL_CP1_CI_AI) AND 
					   EditionId = @editionId AND 
					   Products.Active = 1 AND 
					  (SubProducts.Active = 1  OR 
					   ISNULL(ClientProducts.SubProductId,0) = 0)
				GROUP BY Products.ProductId,
						 SubProducts.SubProductId,
						 Products.ProductName,
						 SubProducts.Description)AS TOTAL) AS TOTAL,* 
	
	FROM 
		(SELECT ClientProducts.ProductId,
				ClientProducts.SubProductId,
				Products.ProductName,
				SubProducts.Description,
				ROW_NUMBER() OVER (ORDER BY ClientProducts.ProductId) AS RowNumber 
		 FROM ClientProducts 
				FULL OUTER JOIN Products ON Products.ProductId = ClientProducts.ProductId 
				FULL OUTER JOIN SubProducts ON SubProducts.SubProductId = ClientProducts.SubProductId 
				FULL OUTER JOIN Clients ON Clients.ClientId = ClientProducts.ClientId 
		 WHERE (Products.ProductName LIKE '%' + @text + '%' COLLATE SQL_LATIN1_GENERAL_CP1_CI_AI OR 
				SubProducts.Description LIKE '%' + @text + '%' COLLATE SQL_LATIN1_GENERAL_CP1_CI_AI) AND 
				EditionId = @editionId AND 
				Products.Active = 1 AND 
			   (SubProducts.Active = 1 OR 
				ISNULL(ClientProducts.SubProductId,0) = 0)
		 GROUP BY ClientProducts.ProductId,
				  ClientProducts.SubProductId,
				  Products.ProductName,
				  SubProducts.Description)AS Productos 

	WHERE RowNumber BETWEEN @numberByPage * @page + 1 AND @numberByPage * (@page + 1)
	ORDER BY PRODUCTNAME,[DESCRIPTION]

END
go