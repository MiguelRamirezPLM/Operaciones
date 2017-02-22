IF OBJECT_ID('dbo.roc_spGetBrandsByText') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetBrandsByText
go

/* 
	Author:			Benjamín Castillo Arriaga.				 
	Object:			dbo.roc_spGetBrandsByText
	
	Description:	Retrieves all brands By Text.

	Company:		PLM Latina.

	

	EXECUTE dbo.roc_spGetBrandsByText @editionId = 5,@text = 'ab',@numberByPage = 100,@page = 1

	

 */ 

CREATE PROCEDURE dbo.roc_spGetBrandsByText
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
		 FROM (SELECT DISTINCT (ClientBrands.BrandId),
					  Brands.BrandName 
			   FROM ClientBrands 
					INNER JOIN Brands ON Brands.BrandId = ClientBrands.BrandId 
					INNER JOIN Clients ON Clients.ClientId = ClientBrands.ClientId 
			   WHERE Brands.BrandName LIKE '%' + @text + '%' COLLATE SQL_LATIN1_GENERAL_CP1_CI_AI AND 
					 ClientBrands.Active = 1 AND 
					 Clients.EditionId = @editionId) total) AS TOTAL,* 
	
	FROM 
		(SELECT DISTINCT (ClientBrands.BrandId),
				Brands.BrandName,
				Brands.Logo,
				ClientBrands.BaseUrl,
				ROW_NUMBER() OVER (ORDER BY ClientBrands.BrandId) AS RowNumber 
		 FROM ClientBrands 
				INNER JOIN Brands ON Brands.BrandId = ClientBrands.BrandId 
				INNER JOIN Clients ON Clients.ClientId = ClientBrands.ClientId 
		 WHERE Brands.BrandName LIKE '%' + @text + '%' COLLATE SQL_LATIN1_GENERAL_CP1_CI_AI AND 
			   ClientBrands.Active = 1 AND 
			   Clients.EditionId = @editionId 
		 GROUP BY ClientBrands.BrandId,
				  Brands.BrandName,
				  Brands.Logo,
				  ClientBrands.BaseUrl)AS MARCAS 
	
	WHERE RowNumber BETWEEN @numberByPage * @page + 1 AND @numberByPage * (@page + 1)


END
go