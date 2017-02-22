IF OBJECT_ID('dbo.roc_spGetCompaniesByLetter') IS NOT NULL
     DROP PROCEDURE dbo.roc_spGetCompaniesByLetter

go

/* 
	Author:			Daniel Campos. / Elizabeth Lazo.				 
	Object:			dbo.roc_spGetCompaniesByLetter
	
	Description:	Retrieves all information the company by letter.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetCompaniesByLetter  @page = 0, @numberByPage = 10, @editionId = 2,  @letter= 'b'


 */

CREATE PROCEDURE dbo.roc_spGetCompaniesByLetter
(
  @page         int,
  @numberByPage int,
  @editionId    int,
  @letter       varchar(1)
)


AS
BEGIN

   --The parametres shouldn't be null:
   IF(@page IS NULL OR @numberByPage IS NULL OR @editionId IS NULL OR @letter IS NULL )
   BEGIN
	
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
	
   END

   SELECT (
  SELECT count(*) from 
  (
   SELECT Companies.*, Cities.Name as ciudad, States.StateId, States.Name FROM Companies 
   INNER JOIN CompanyEditions ON CompanyEditions.CompanyId = Companies.CompanyId
   INNER JOIN Cities ON Cities.CityId = Companies.CityId 
   INNER JOIN States ON States.StateId = Cities.StateId
   WHERE (Companies.CompanyTypeId = '2' OR Companies.CompanyTypeId = '3' OR Companies.CompanyTypeId = '4')
   AND CompanyEditions.EditionId = @editionId  AND Companies.Active = '1'  AND Companies.Active = '1'  AND States.Name LIKE @letter + '%' 
  ) as contador
   ) AS TOTAL,*
  FROM (

   SELECT Companies.*,  Cities.Name as ciudad, States.StateId, States.Name,
   ROW_NUMBER() OVER (ORDER BY States.Name,Cities.Name, Companies.CompanyName) AS RowNumber FROM Companies 
   INNER JOIN CompanyEditions ON CompanyEditions.CompanyId = Companies.CompanyId
   INNER JOIN Cities ON Cities.CityId = Companies.CityId 
   INNER JOIN States ON States.StateId = Cities.StateId
   WHERE (Companies.CompanyTypeId = '2' OR Companies.CompanyTypeId = '3' OR Companies.CompanyTypeId = '4')
   AND CompanyEditions.EditionId = @editionId  AND Companies.Active = '1'  AND States.Name LIKE @letter + '%'
)AS INDICE
WHERE RowNumber BETWEEN @numberByPage * @page  + 1 AND @numberByPage * (@page  + 1)




  END

go