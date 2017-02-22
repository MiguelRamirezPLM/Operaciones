IF OBJECT_ID('dbo.roc_spGetLaboratories') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetLaboratories
go

/* 
	Author:			Daniel Campos. / Elizabeth Lazo.				 
	Object:			dbo.roc_spGetLaboratories
	
	Description:	Retrieves all laboratories.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetLaboratories @page=0, @numberByPage= 10,  @editionId = 2, @companyTypeId =5


 */ 

CREATE PROCEDURE dbo.roc_spGetLaboratories
(   
  @page               int,   
  @numberByPage       int,
  @editionId          int,
  @companyTypeId      tinyint             
)
AS
  BEGIN
  --The parameters shouldn't be null:
       IF (@page IS NULL OR @numberByPage IS NULL OR  @editionId IS NULL
            OR @companyTypeId IS NULL ) 
             
  BEGIN

    RAISERROR ('The current operation requires more parameters', 16, 1)
    RETURN -1

  END


SELECT (
  SELECT count(*) from 
  (
   SELECT Companies.*, States.StateId, States.Name as estado,Cities.Name FROM Companies
   INNER JOIN CompanyEditions ON CompanyEditions.CompanyId = Companies.CompanyId
   INNER JOIN Cities ON Cities.CityId = Companies.CityId
   INNER JOIN States ON States.StateId = Cities.StateId
   WHERE Companies.Active='1' AND CompanyEditions.EditionId = @editionId AND  Companies.CompanyTypeId = @companyTypeId
  ) as contador
 ) AS TOTAL,*
FROM (
   SELECT Companies.*, States.StateId, States.Name as estado,Cities.Name,
   ROW_NUMBER() OVER (ORDER BY States.Name,Cities.Name, Companies.CompanyName) AS RowNumber FROM Companies
   INNER JOIN CompanyEditions ON CompanyEditions.CompanyId = Companies.CompanyId
   INNER JOIN Cities ON Cities.CityId = Companies.CityId
   INNER JOIN States ON States.StateId = Cities.StateId
   WHERE Companies.Active='1' AND CompanyEditions.EditionId = @editionId AND  Companies.CompanyTypeId = @companyTypeId
 )AS INDICE
WHERE RowNumber BETWEEN @numberByPage * @page  + 1 AND @numberByPage * (@page  + 1)

END
go