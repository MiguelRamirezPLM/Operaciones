IF OBJECT_ID('dbo.roc_spGetCompany') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetCompany
go

/* 
	Author:			Daniel Campos. / Elizabeth Lazo.
				 
	Object:			dbo.roc_spGetCompany
	
	Description:	Retrieves all companies by edition.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetCompany  @editionId = 2, @companyId=435


 */ 

CREATE PROCEDURE dbo.roc_spGetCompany
(
    
  @editionId           int,
  @companyId           int 
)
AS
  BEGIN
  --The parameters shouldn't be null:
       IF (@editionId IS NULL OR @companyId IS NULL) 
  BEGIN

    RAISERROR ('The current operation requires more parameters', 16, 1)
    RETURN -1

  END


SELECT (
  SELECT count(*) from 
  (
SELECT Companies.*, CompanyEditions.HTMLContent FROM CompanyEditions
INNER JOIN Companies ON Companies.CompanyId=CompanyEditions.CompanyId
WHERE CompanyEditions.EditionId =  @editionId AND CompanyEditions.CompanyId = @companyId 
) as contador
) AS TOTAL,*
FROM (
SELECT Companies.*, CompanyEditions.HTMLContent,
        ROW_NUMBER() OVER (ORDER BY Companies.CompanyName ASC) AS RowNumber  
FROM CompanyEditions
INNER JOIN Companies ON Companies.CompanyId=CompanyEditions.CompanyId
WHERE CompanyEditions.EditionId =  @editionId AND CompanyEditions.CompanyId = @companyId 
)AS INDICE
WHERE RowNumber BETWEEN 1 * 0 + 1 AND 1 * (0 + 1)

 END
  go





