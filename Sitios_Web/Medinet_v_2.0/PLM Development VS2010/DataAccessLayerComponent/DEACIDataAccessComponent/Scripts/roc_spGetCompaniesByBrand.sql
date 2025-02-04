USE [DEACI]
GO
/****** Objeto:  StoredProcedure [dbo].[roc_spGetCompaniesByBrand]    Fecha de la secuencia de comandos: 02/29/2012 15:23:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/* 
	Author:			Daniel Campos. / Elizabeth Lazo.				 
	Object:			dbo.roc_spGetCompaniesByBrand
	
	Description:	Retrieves all company by brand. 

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetCompaniesByBrand @editionId = 3, @brandId = 9  


 */ 

ALTER PROCEDURE [dbo].[roc_spGetCompaniesByBrand]
(

  @editionId   int,
  @brandId     int
)
AS
  BEGIN
  --The parameters shouldn't be null:
       IF (@editionId IS NULL OR @brandId IS NULL) 
  BEGIN

    RAISERROR ('The current operation requires more parameters', 16, 1)
    RETURN -1

  END

SELECT cb.BrandId,cb.CompanyId,c.CompanyTypeId,c.Address,c.Suburb,c.PostalCode,c.Email,c.Web,c.CompanyName,
c.CompanyShortName,c.Client_ID
FROM CompanyBrands AS cb
INNER JOIN Companies AS c ON c.CompanyId=cb.CompanyId
INNER JOIN CompanyBrandEditions AS cbe ON cbe.CompanyId=cb.CompanyId AND cbe.BrandId=cb.BrandId
WHERE cb.BrandId=@brandId AND c.Active='1' AND cbe.EditionId=@editionId
ORDER BY c.CompanyName





END
