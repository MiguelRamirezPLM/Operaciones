IF OBJECT_ID('dbo.roc_spGetBrandsByFullText') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetBrandsByFullText
go
/* 
	Author:			Benjamín Castillo Arriaga/Elizabeth Lazo.				 
	Object:			dbo.roc_spGetBrandsByFullText
	
	Description:	Retrieves all brands By Text.

	Company:		PLM Latina.

	

	EXECUTE dbo.roc_spGetBrandsByFullText @editionId = 5,@text = 'BMI',@numberByPage = 10,@page = 0

	

 */ 

CREATE PROCEDURE [dbo].[roc_spGetBrandsByFullText]
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

set @text =  '"'  + @text + '*"' 

SELECT 
(
SELECT count(*) 
FROM 
(
    SELECT DISTINCT (ClientBrands.BrandId),Brands.BrandName 
    FROM ClientBrands 
        INNER JOIN Brands ON Brands.BrandId = ClientBrands.BrandId 
        INNER JOIN Clients ON Clients.ClientId = ClientBrands.ClientId 

    WHERE CONTAINS(Brands.BrandName,  @text ) AND 
                   ClientBrands.Active='1' AND 
                   Clients.EditionId = @editionId
) 
total
) 
    AS TOTAL,* 
         FROM 
(
    SELECT DISTINCT (ClientBrands.BrandId),
                     Brands.BrandName,Brands.Logo,ClientBrands.BaseUrl,
                     ROW_NUMBER() OVER (ORDER BY Brands.BrandName) AS RowNumber 

    FROM ClientBrands 
         INNER JOIN Brands ON Brands.BrandId = ClientBrands.BrandId 
         INNER JOIN Clients ON Clients.ClientId = ClientBrands.ClientId 

   WHERE FREETEXT(Brands.BrandName,  @text ) AND 
                  ClientBrands.Active='1' AND 
                  Clients.EditionId = @editionId

   GROUP BY ClientBrands.BrandId,
            Brands.BrandName,
            Brands.Logo,
			ClientBrands.BaseUrl
   )AS MARCAS 

  WHERE RowNumber BETWEEN @numberByPage * @page+ 1 AND @numberByPage * (@page + 1)

 END