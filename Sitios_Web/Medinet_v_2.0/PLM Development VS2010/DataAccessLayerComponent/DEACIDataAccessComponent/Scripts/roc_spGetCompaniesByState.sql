IF OBJECT_ID('dbo.roc_spGetCompaniesByState') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetCompaniesByState
go

/* 
	Author:			Daniel Campos. / Elizabeth Lazo.				 
	Object:			dbo.roc_spGetCompaniesByState
	
	Description:	Retrieves all companies by state.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetCompaniesByState @page=0, @numberByPage= 10,  @editionId = 2, @stateID=67


 */ 

CREATE PROCEDURE dbo.roc_spGetCompaniesByState
(   
  @page               int,   
  @numberByPage       int,
  @editionId          int,
  @stateId            int             
)
AS
  BEGIN
  --The parameters shouldn't be null:
       IF (@page IS NULL OR @numberByPage IS NULL OR  @editionId IS NULL
            OR @stateId IS NULL ) 
             
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
   AND CompanyEditions.EditionId = @editionId  AND Companies.Active = '1' AND States.StateId=@stateId 
  ) as contador
   ) AS TOTAL,*
  FROM (

   SELECT Companies.*,  Cities.Name as ciudad, States.StateId, States.Name,
   ROW_NUMBER() OVER (ORDER BY States.Name,Cities.Name, Companies.CompanyName) AS RowNumber FROM Companies 
   INNER JOIN CompanyEditions ON CompanyEditions.CompanyId = Companies.CompanyId
   INNER JOIN Cities ON Cities.CityId = Companies.CityId 
   INNER JOIN States ON States.StateId = Cities.StateId
   WHERE (Companies.CompanyTypeId = '2' OR Companies.CompanyTypeId = '3' OR Companies.CompanyTypeId = '4')
   AND CompanyEditions.EditionId = @editionId  AND Companies.Active = '1' AND States.StateId=@stateId 
)AS INDICE
WHERE RowNumber BETWEEN @numberByPage * @page  + 1 AND @numberByPage * (@page  + 1)

END 
go