IF OBJECT_ID('dbo.roc_spGetBrandsByEditionText') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetBrandsByEditionText
go

/* 
	Author:			Benjamín Castillo Arriaga.				 
	Object:			dbo.roc_spGetBrandsByEditionText
	
	Description:	Retrieves all brands by edition by Text.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetBrandsByEditionText @editionId = 5, @numberPage = 10,@page=1,@text = 'ab'


 */ 

CREATE PROCEDURE dbo.roc_spGetBrandsByEditionText
(
	@editionId				int,
	@numberPage				int,
	@page					int,
	@text					varchar(255)
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
			(SELECT DISTINCT (Brands.BrandName) 
			 FROM ClientBrands 
				INNER JOIN Brands ON(Brands.BrandId = ClientBrands.BrandId)
				INNER JOIN Clients ON(Clients.ClientId = ClientBrands.ClientId)
			 WHERE  Clients.EditionId = @editionId AND 
					Brands.Active = 1 AND 
					Brands.BrandName LIKE '%' + @text +'%') total) AS TOTAL,* 
	FROM 
		(SELECT Brands.BrandId,
				Brands.BrandName,
				Brands.Logo,
				ClientBrands.BaseUrl,
				ROW_NUMBER() OVER (ORDER BY Brands.BrandName) AS RowNumber 
		 FROM ClientBrands 
			INNER JOIN Brands ON(Brands.BrandId = ClientBrands.BrandId)
			INNER JOIN Clients ON(Clients.ClientId = ClientBrands.ClientId)
		 WHERE Clients.EditionId = @editionId AND 
			   Brands.Active = 1 AND 
			   Brands.BrandName LIKE '%' + @text +'%'
		 GROUP BY Brands.BrandId,
				  Brands.BrandName,
				  Brands.Logo,
				  ClientBrands.BaseUrl )AS MARCAS 
	WHERE RowNumber BETWEEN @numberPage * @page + 1 AND @numberPage * (@page + 1)
	
	
END
go