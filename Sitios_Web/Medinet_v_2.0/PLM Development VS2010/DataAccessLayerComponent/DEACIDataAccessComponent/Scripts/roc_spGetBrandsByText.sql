IF OBJECT_ID('dbo.roc_spGetBrandsByText') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetBrandsByText
go

/* 
	Author:			Daniel Campos. / Elizabeth Lazo.				 
	Object:			dbo.roc_spGetBrandsByText
	
	Description:	Retrieves all brands by letter. 

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetBrandsByText @page = 0,  @numberByPage = 10, @indexId = 4, @editionId = 3, @text = wa  


 */ 

CREATE PROCEDURE dbo.roc_spGetBrandsByText
(
  @page  int,
  @numberByPage int,
  @indexId     tinyint,
  @editionId   int,
  @text      varchar(250)
)
AS
  BEGIN
  --The parameters shouldn't be null:
       IF (@page IS NULL OR @numberByPage IS NULL OR @indexId IS NULL OR @editionId IS NULL OR @text IS NULL  ) 
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
 WHERE b.Active='1' AND cbe.EditionId=@editionId AND cbi.IndexId=@indexId AND b.BrandName  LIKE '%' + @text + '%' COLLATE SQL_LATIN1_GENERAL_CP1_CI_AI
)
AS TOTAL,* FROM 
(
 select distinct b.BrandId,b.BrandName,ROW_NUMBER() OVER (ORDER BY B.BrandName) AS RowNumber  
 FROM Brands b
 INNER JOIN CompanyBrands AS cb ON cb.BrandId=b.BrandId
 INNER JOIN CompanyBrandEditions AS cbe ON cbe.BrandId=b.BrandId AND cbe.CompanyId=cb.CompanyId
 INNER JOIN CompanyBrandIndexes AS cbi ON cbi.CompanyId=cb.CompanyId AND cbi.BrandId=cb.BrandId
 WHERE b.Active='1' AND cbe.EditionId=@editionId AND cbi.IndexId=@indexId AND b.BrandName  LIKE '%' + @text + '%' COLLATE SQL_LATIN1_GENERAL_CP1_CI_AI
 GROUP BY b.BrandId,b.BrandName
)
AS Brands
WHERE RowNumber BETWEEN @numberByPage * @page + 1 AND @numberByPage * (@page + 1)





END
 go




