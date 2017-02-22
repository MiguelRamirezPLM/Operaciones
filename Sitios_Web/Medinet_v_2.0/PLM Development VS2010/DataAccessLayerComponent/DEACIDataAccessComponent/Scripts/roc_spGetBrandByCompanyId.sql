IF OBJECT_ID('dbo.roc_spGetBrandByCompanyId') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetBrandByCompanyId
go

/* 
	Author:			Daniel Campos. / Elizabeth Lazo.				 
	Object:			dbo.roc_spGetBrandByCompanyId
	
	Description:	Retrieves all brands by  companyId.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetBrandByCompanyId @brandId = 9, @editionId = 3, @companyId = 35


 */ 

CREATE PROCEDURE dbo.roc_spGetBrandByCompanyId
(
    
    @editionId int,
    @companyId int,
    @brandId   int
)
AS
  BEGIN
  --The parameters shouldn't be null:
       IF (@brandId IS NULL OR @editionId IS NULL ) 
  BEGIN

    RAISERROR ('The current operation requires more parameters', 16, 1)
    RETURN -1

  END

        SELECT b.BrandId,b.BrandName
			FROM Brands AS b
			INNER JOIN CompanyBrands AS cb ON cb.BrandId = b.BrandId
			INNER JOIN CompanyBrandEditions AS cbe ON cbe.CompanyId = cb.CompanyId 
			AND  cbe.BrandId=b.BrandId
			WHERE cbe.BrandId=@brandId and b.Active='1' AND cbe.EditionId=@editionId AND cb.CompanyId=@companyId


END
 go




