IF OBJECT_ID('dbo.roc_spGetProvidersByLetter') IS NOT NULL
     DROP PROCEDURE dbo.roc_spGetProvidersByLetter

go

/* 
	Author:			Daniel Campos. / Elizabeth Lazo.				 
	Object:			dbo.roc_spGetProvidersByLetter
	
	Description:	Retrieves all providers by letter.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetProvidersByLetter  @editionId=2, @page=0 , @numberbypage=10, @companyTypeId=6, @letter ='a' 


 */

CREATE PROCEDURE dbo.roc_spGetProvidersByLetter 
(
  @editionId      int,
  @page           int,
  @numberbypage   int,
  @companyTypeId   int, 
  @letter          varchar(1)
)


AS
BEGIN

   --The parametres shouldn't be null:
   IF(@editionId IS NULL OR @page IS NULL OR @numberbypage IS NULL OR 
      @companyTypeId IS NULL OR @letter IS NULL)
   BEGIN
	
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
	
   END

  SELECT 
  (
   SELECT count(*) 
   from 
    (
      SELECT Companies.CompanyId, Companies.CompanyName
      FROM Companies
  
       INNER JOIN CompanyEditions ON CompanyEditions.CompanyId = Companies.CompanyId
       INNER JOIN Editions ON Editions.EditionId = Editions.EditionId
  
      WHERE Editions.EditionId = @editionId  AND 
            Companies.Active = '1' AND 
            Companies.CompanyTypeId = @companyTypeId and 
            Companies.CompanyName LIKE @letter + '%' 
 
     GROUP BY Companies.CompanyId, 
              Companies.CompanyName) as contador) AS TOTAL,*   
     FROM 
     (
     SELECT Companies.CompanyId, 
            Companies.CompanyName ,
            ROW_NUMBER() OVER (ORDER BY Companies.CompanyName ASC) AS RowNumber
  
    FROM Companies
       INNER JOIN CompanyEditions ON CompanyEditions.CompanyId = Companies.CompanyId
       INNER JOIN Editions ON Editions.EditionId = Editions.EditionId
  
    WHERE Editions.EditionId = @editionId  AND 
          Companies.Active = '1' AND 
          Companies.CompanyTypeId = @companyTypeId and 
          Companies.CompanyName LIKE @letter + '%' 

   GROUP BY Companies.CompanyId,
            Companies.CompanyName)AS INDICE_GENERAL

   WHERE RowNumber BETWEEN @numberbypage  * @page + 1 AND @numberbypage  * (@page + 1)

END

go

