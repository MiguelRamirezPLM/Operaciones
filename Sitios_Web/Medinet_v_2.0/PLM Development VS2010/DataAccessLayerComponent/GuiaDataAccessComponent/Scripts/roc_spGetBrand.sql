IF OBJECT_ID('dbo.roc_spGetBrand') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetBrand
go

/* 
	Author:			Daniel Campos.				 
	Object:			dbo.roc_spGetBrand
	
	Description:	Retrieves a brand Id.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetBrand @editionId = 5, @brandId = 1493,@numberPage = 10,@page=0


 */ 

CREATE PROCEDURE dbo.roc_spGetBrand
(
	@editionId				int,
	@brandId				int,
	@numberPage				int,
	@page					int
)
AS
BEGIN

	-- The parameters shouldn't be null:
	IF (@editionId IS NULL OR @brandId IS NULL OR @numberPage IS NULL OR @page IS NULL)
	BEGIN
	
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
		
	END
		SELECT (
				SELECT count(*) 
				FROM (
					SELECT DISTINCT (Brands.BrandName) 
					FROM ClientBrands 
						INNER JOIN Brands ON (Brands.BrandId = ClientBrands.BrandId) 
						INNER JOIN Clients ON (Clients.ClientId = ClientBrands.ClientId) 
					WHERE  Clients.EditionId = @editionId AND 
						   Brands.Active = 1 AND 
						   Brands.BrandId = @brandId) total) AS TOTAL,* 
		FROM (
				SELECT Brands.BrandId,
					   Brands.BrandName,
					   Brands.Logo,
					   ClientBrands.BaseUrl,
					   ROW_NUMBER() OVER (ORDER BY Brands.BrandName) AS RowNumber 
				FROM ClientBrands 
					INNER JOIN Brands ON (Brands.BrandId = ClientBrands.BrandId) 
					INNER JOIN Clients ON (Clients.ClientId = ClientBrands.ClientId) 
				WHERE Clients.EditionId = @editionId AND 
					  Brands.Active = 1 AND 
					  Brands.BrandId = @brandId
				GROUP BY Brands.BrandId,
						 Brands.BrandName,
						 Brands.Logo,
					     ClientBrands.BaseUrl)AS MARCAS 
		WHERE RowNumber BETWEEN @numberPage * @page + 1 AND @numberPage * (@page + 1)
	
END
go