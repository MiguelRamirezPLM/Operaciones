IF OBJECT_ID('dbo.roc_spGetParticipantCompaniesByText') IS NOT NULL
   DROP PROCEDURE  dbo.roc_spGetParticipantCompaniesByText

go

  /*

     Author:	   Daniel Campos / Elizabeth Lazo.				 
	 Object:	   dbo.roc_spGetParticipantCompaniesByText
	
	Description:	Retrieves all  companies participant by text
				
	Company:		PLM México.

	
    EXECUTE dbo.roc_spGetParticipantCompaniesByText @editionId=2, @companyTypeId=1, @text=ind,  @page=0, @numberByPage=10
                                                             
*/

CREATE PROCEDURE dbo.roc_spGetParticipantCompaniesByText
(
  @editionid         int,
  @companyTypeId     tinyint,
  @text              varchar(250),
  @page              int,
  @numberByPage      int 
)
 
 AS
  BEGIN

 --The parametres shouldn't be null:
   IF(@editionId IS NULL OR @companyTypeId IS NULL OR  
      @text IS NULL OR @page IS NULL OR @numberByPage IS NULL)
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
   WHERE Companies.Active = '1' AND Editions.EditionId = @editionid AND 
         Companies.CompanyTypeId =  @companyTypeId AND 
        (Companies.CompanyName LIKE '%' + @text + '%' COLLATE SQL_LATIN1_GENERAL_CP1_CI_AI OR 
         Companies.CompanyShortName LIKE '%' + @text + '%' COLLATE SQL_LATIN1_GENERAL_CP1_CI_AI)) as contador) AS TOTAL,*   

FROM (
  SELECT Companies.*, CompanyEditions.HTMLContent,  
  ROW_NUMBER() OVER (ORDER BY Companies.CompanyName ASC) AS RowNumber FROM Companies
  INNER JOIN CompanyEditions ON CompanyEditions.CompanyId = Companies.CompanyId
  INNER JOIN Editions ON Editions.EditionId = CompanyEditions.EditionId
  WHERE Companies.Active = '1' AND Editions.EditionId = @editionid AND 
        Companies.CompanyTypeId =  @companyTypeId AND 
        (Companies.CompanyName LIKE '%' + @text + '%' COLLATE SQL_LATIN1_GENERAL_CP1_CI_AI OR 
         Companies.CompanyShortName LIKE '%' + @text + '%' COLLATE SQL_LATIN1_GENERAL_CP1_CI_AI)
  )AS INDICE

WHERE RowNumber BETWEEN @numberByPage  * @page + 1 AND 10 * (@numberByPage  + 1)

END
go 



