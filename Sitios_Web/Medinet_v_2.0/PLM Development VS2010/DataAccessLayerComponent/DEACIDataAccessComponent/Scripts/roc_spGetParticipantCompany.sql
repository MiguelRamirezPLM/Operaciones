IF OBJECT_ID('dbo.roc_spGetParticipantCompany') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetParticipantCompany
go

/* 
	Author:			Daniel Campos. / Elizabeth Lazo.
				 
	Object:			dbo.roc_spGetParticipantCompany
	
	Description:	Retrieves all companies participant.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetParticipantCompany  @editionId = 2, @companyTypeId = 1, @companyId=8


 */ 

CREATE PROCEDURE dbo.roc_spGetParticipantCompany
(
    
  @editionId           int,
  @companyTypeId       tinyint,
  @companyId           int 
)
AS
  BEGIN
  --The parameters shouldn't be null:
       IF (@editionId IS NULL OR @companyTypeId  IS NULL
           OR @companyId IS NULL) 
  BEGIN

    RAISERROR ('The current operation requires more parameters', 16, 1)
    RETURN -1

  END


SELECT (
  SELECT count(*) from 
  (
SELECT Companies.* FROM Companies
INNER JOIN CompanyEditions ON CompanyEditions.CompanyId = Companies.CompanyId
INNER JOIN Editions ON Editions.EditionId = CompanyEditions.EditionId
WHERE Companies.Active = '1' AND Editions.EditionId = @editionId AND Companies.CompanyTypeId = @companyTypeId  AND Companies.CompanyId = @companyId 
  ) as contador
) AS TOTAL,*
FROM (
SELECT Companies.*,CompanyEditions.HTMLContent,
        ROW_NUMBER() OVER (ORDER BY Companies.CompanyName ASC) AS RowNumber 
FROM Companies
INNER JOIN CompanyEditions ON CompanyEditions.CompanyId = Companies.CompanyId
INNER JOIN Editions ON Editions.EditionId = CompanyEditions.EditionId
WHERE Companies.Active = '1' AND Editions.EditionId = @editionId AND Companies.CompanyTypeId = @companyTypeId  AND Companies.CompanyId = @companyId 
)AS INDICE
WHERE RowNumber BETWEEN 1 * 0 + 1 AND 1 * (0 + 1)

END 
go
