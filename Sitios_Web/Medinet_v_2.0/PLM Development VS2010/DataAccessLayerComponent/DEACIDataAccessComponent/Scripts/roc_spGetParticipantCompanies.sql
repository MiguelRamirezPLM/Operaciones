IF OBJECT_ID('dbo.roc_spGetParticipantCompanies') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetParticipantCompanies
go

/* 
	Author:			Daniel Campos. / Elizabeth Lazo.				 
	Object:			dbo.roc_spGetParticipantCompanies
	
	Description:	Retrieves all companies participant.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetParticipantCompanies @page = 0, @numberByPage = 10, @editionId = 2, @companyTypeId = 1


 */ 

CREATE PROCEDURE dbo.roc_spGetParticipantCompanies
(
  @page                int,
  @numberByPage        int,   
  @editionId           int,
  @companyTypeId        tinyint 
)
AS
  BEGIN
  --The parameters shouldn't be null:
       IF (@page IS NULL OR @numberByPage IS NULL OR @editionId IS NULL OR @companyTypeId  IS NULL) 
  BEGIN

    RAISERROR ('The current operation requires more parameters', 16, 1)
    RETURN -1

  END

    SELECT 
     (SELECT count(*) 
      from 
       (SELECT Companies.* 
         FROM Companies
          INNER JOIN CompanyEditions ON CompanyEditions.CompanyId = Companies.CompanyId
          INNER JOIN Editions ON Editions.EditionId = CompanyEditions.EditionId
   
         WHERE Companies.Active = '1' 
           AND Editions.EditionId = @editionId  
           AND Companies.CompanyTypeId = @companyTypeId) as contador) AS TOTAL,*   

    FROM 
    (SELECT Companies.*, CompanyEditions.HTMLContent, 
            ROW_NUMBER() OVER (ORDER BY Companies.CompanyName ASC) AS RowNumber 
     FROM Companies
       INNER JOIN CompanyEditions ON CompanyEditions.CompanyId = Companies.CompanyId
       INNER JOIN Editions ON Editions.EditionId = CompanyEditions.EditionId
  
     WHERE Companies.Active = '1' 
       AND Editions.EditionId = @editionId  
       AND Companies.CompanyTypeId = @companyTypeId)AS INDICE

    WHERE RowNumber BETWEEN @numberByPage * @page + 1 AND @numberByPage * (@page + 1)


END
 go




