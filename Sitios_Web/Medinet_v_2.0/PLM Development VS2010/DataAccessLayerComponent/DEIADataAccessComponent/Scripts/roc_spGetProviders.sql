IF OBJECT_ID('dbo.roc_spGetProviders') IS NOT NULL
     DROP PROCEDURE dbo.roc_spGetProviders

go

/* 
	Author:			Daniel Campos. / Elizabeth Lazo.				 
	Object:			dbo.roc_spGetProviders
	
	Description:	Retrieves all providers.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetProviders  @editionId=2, @page=0 , @numberbypage=10, @CompanyTypeId=6


 */

CREATE PROCEDURE dbo.roc_spGetProviders
(
  @editionId      int,
  @page           int,
  @numberbypage   int,
  @CompanyTypeId   int   
)


AS
BEGIN

   --The parametres shouldn't be null:
   IF(@editionId IS NULL OR @page IS NULL OR @numberbypage IS NULL OR @CompanyTypeId IS NULL )
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
  
      WHERE Editions.EditionId = @editionId AND 
            Companies.Active = '1' AND 
            Companies.CompanyTypeId = @CompanyTypeId
  
     GROUP BY Companies.CompanyId, Companies.CompanyName) as contador) AS TOTAL,*   
 
     FROM 
      (
       SELECT Companies.CompanyId, Companies.CompanyName ,
       ROW_NUMBER() OVER (ORDER BY Companies.CompanyName ASC) AS RowNumber
     FROM Companies
        INNER JOIN CompanyEditions ON CompanyEditions.CompanyId = Companies.CompanyId
        INNER JOIN Editions ON Editions.EditionId = Editions.EditionId
  
      WHERE Editions.EditionId = @editionId AND 
           Companies.Active = '1' AND 
           Companies.CompanyTypeId = @CompanyTypeId
  
     GROUP BY Companies.CompanyId, Companies.CompanyName)AS INDICE_GENERAL

     WHERE RowNumber BETWEEN @numberbypage * @page + 1 AND @numberbypage * (@page + 1)

 END

  go