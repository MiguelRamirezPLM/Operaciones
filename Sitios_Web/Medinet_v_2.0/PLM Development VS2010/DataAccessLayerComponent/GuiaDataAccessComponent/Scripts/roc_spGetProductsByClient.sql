IF OBJECT_ID('dbo.roc_spGetProductsByClient') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetProductsByClient
go

/* 
	Author:			Benjamín Castillo Arriaga.				 
	Object:			dbo.roc_spGetProductsByClient
	
	Description:	Retrieves all products asociated to a specific client.

	Company:		PLM Latina.

	

	EXECUTE dbo.roc_spGetProductsByClient @clientId = 28098

	EXECUTE dbo.roc_spGetProductsByClient @clientId = 28098, @top = 10
	

 */ 

CREATE PROCEDURE dbo.roc_spGetProductsByClient
(
	@clientId				int,
	@top					int = null
)
AS
BEGIN

	-- The parameters shouldn't be null:
	IF (@clientId IS NULL)
	BEGIN
	
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
		
	END

	IF(@top IS NULL)
	BEGIN

		SELECT ClientProducts.ClientProductId,
			   ClientProducts.ClientId,
			   ClientProducts.Page,
			   ClientProducts.Active as ProductClientActive,
			   Products.ProductId,
			   Products.ProductName,
			   Products.Active as ProductActive,
			   SubProducts.SubProductId,
			   SubProducts.Description as SubProductName,
			   SubProducts.Active as SubProductActive	
		FROM 
			 ClientProducts 
			 FULL OUTER JOIN Products ON (Products.ProductId = ClientProducts.ProductId)
			 FULL OUTER JOIN SubProducts ON (SubProducts.SubProductId = ClientProducts.SubProductId)
		
		WHERE ClientProducts.ClientId = @clientId AND 
			  Products.Active = 1 
		ORDER BY ProductName,SubProductName
	END
	ELSE
	BEGIN
		
		DECLARE @sql varchar(4000)

		SET @sql =	'SELECT TOP ' + CONVERT(varchar(10), @top) + ' ClientProducts.ClientProductId, ' + Char(13) + 
					'		ClientProducts.ClientId,' + Char(13) + 
					'		ClientProducts.Page,' + Char(13) + 
					'		ClientProducts.Active as ProductClientActive,' + Char(13) + 
					'		Products.ProductId,' + Char(13) + 
					'		Products.ProductName,' + Char(13) + 
					'		Products.Active as ProductActive,' + Char(13) + 
					'		SubProducts.SubProductId,' + Char(13) + 
					'		SubProducts.Description as SubProductName,' + Char(13) + 
					'		SubProducts.Active as SubProductActive' + Char(13) + 
					'FROM ClientProducts ' + Char(13) + 

					'FULL OUTER JOIN Products ON (Products.ProductId = ClientProducts.ProductId)' + Char(13) + 
					'FULL OUTER JOIN SubProducts ON (SubProducts.SubProductId = ClientProducts.SubProductId)' + Char(13) + 
					
					'WHERE ClientProducts.ClientId 	= ' + CONVERT(varchar(10), @clientId) + ' AND ' + Char(13) + 
					'		Products.Active			= 1 ORDER BY ProductName,SubProductName' + Char(13) 


		PRINT 'SQL: ' + @sql

		EXEC (@sql)

		RETURN @@ROWCOUNT
	
	END

END
go