IF OBJECT_ID('dbo.roc_spGetBrands') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetBrands
go

/* 
	Author:			Daniel Campos. / Elizabeth Lazo.				 
	Object:			dbo.roc_spGetBrands
	
	Description:	Retrieves all brands by page and indexId.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetBrands @page = 0,  @numberByPage = 10, @indexId = 4, @editionId = 3 


 */ 

CREATE PROCEDURE dbo.roc_spGetBrands
(
  @page  int,
  @numberByPage int,
  @indexId     tinyint,
  @editionId   int
)
AS
  BEGIN
  --The parameters shouldn't be null:
       IF (@page IS NULL OR @numberByPage IS NULL OR @indexId IS NULL OR @editionId IS NULL  ) 
  BEGIN

    RAISERROR ('The current operation requires more parameters', 16, 1)
    RETURN -1

  END

     SELECT 
		(
		 select COUNT(distinct b.BrandName) FROM Brands b
		 INNER JOIN CompanyBrands AS cb ON cb.BrandId=b.BrandId
		 INNER JOIN CompanyBrandEditions AS cbe ON cbe.BrandId=b.BrandId AND cbe.CompanyId=cb.CompanyId
		 INNER JOIN CompanyBrandIndexes AS cbi ON cbi.CompanyId=cb.CompanyId AND cbi.BrandId=cb.BrandId
		 WHERE b.Active='1' AND cbe.EditionId=@editionId AND cbi.IndexId=@indexId 
		)
		AS TOTAL,* FROM 
		(
		 select distinct b.BrandId,b.BrandName,ROW_NUMBER() OVER (ORDER BY B.BrandName) AS RowNumber  
		 FROM Brands b
		 INNER JOIN CompanyBrands AS cb ON cb.BrandId=b.BrandId
		 INNER JOIN CompanyBrandEditions AS cbe ON cbe.BrandId=b.BrandId AND cbe.CompanyId=cb.CompanyId
		 INNER JOIN CompanyBrandIndexes AS cbi ON cbi.CompanyId=cb.CompanyId AND cbi.BrandId=cb.BrandId
		 WHERE b.Active='1' AND cbe.EditionId=@editionId AND cbi.IndexId=@indexId 
		 GROUP BY b.BrandId,b.BrandName
		)
		AS Brands
		WHERE RowNumber BETWEEN @numberByPage * @page  + 1 AND @numberByPage * (@page  + 1)



END
 go




