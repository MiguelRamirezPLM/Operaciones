IF OBJECT_ID('dbo.roc_spGetLaboratoriesByText') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetLaboratoriesByText
go

/* 
	Author:			Daniel Campos. / Elizabeth Lazo.				 
	Object:			dbo.roc_spGetLaboratoriesByText
	
	Description:	Retrieves all laboratories by text.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetLaboratoriesByText @page=0, @numberByPage= 10,  @editionId = 2, @companyTypeId =5, @text = 'issste'


 */ 

CREATE PROCEDURE dbo.roc_spGetLaboratoriesByText
(   
  @page               int,   
  @numberByPage       int,
  @editionId          int,
  @companyTypeId      tinyint,
  @text               varchar(250)
) 
AS
  BEGIN
  --The parameters shouldn't be null:
       IF (@page IS NULL OR @numberByPage IS NULL OR  @editionId IS NULL
            OR @companyTypeId IS NULL OR @text IS NULL) 
             
  BEGIN

    RAISERROR ('The current operation requires more parameters', 16, 1)
    RETURN -1

  END

 SELECT (
  SELECT count(*) from 
  (
   SELECT Companies.*,States.Name as estado, States.StateId, Cities.Name FROM Companies
   INNER JOIN CompanyEditions ON CompanyEditions.CompanyId = Companies.CompanyId
   INNER JOIN Cities ON Cities.CityId = Companies.CityId
   INNER JOIN States ON States.StateId = Cities.StateId
   WHERE Companies.Active='1' AND CompanyEditions.EditionId = @editionId AND  Companies.CompanyTypeId = @companyTypeId 
    AND (States.Name LIKE '%' + @text + '%'  COLLATE SQL_LATIN1_GENERAL_CP1_CI_AI OR Cities.Name LIKE '%' + @text + '%' COLLATE SQL_LATIN1_GENERAL_CP1_CI_AI OR Companies.CompanyName LIKE '%' + @text + '%'  COLLATE SQL_LATIN1_GENERAL_CP1_CI_AI OR Companies.CompanyShortName LIKE '%' + @text + '%'  COLLATE SQL_LATIN1_GENERAL_CP1_CI_AI)
  ) as contador
 ) AS TOTAL,*
FROM (
   SELECT Companies.*,States.Name as estado, States.StateId, Cities.Name,
   ROW_NUMBER() OVER (ORDER BY States.Name,Cities.Name, Companies.CompanyName) AS RowNumber FROM Companies
   INNER JOIN CompanyEditions ON CompanyEditions.CompanyId = Companies.CompanyId
   INNER JOIN Cities ON Cities.CityId = Companies.CityId
   INNER JOIN States ON States.StateId = Cities.StateId
   WHERE Companies.Active='1' AND CompanyEditions.EditionId = @editionId AND  Companies.CompanyTypeId = @companyTypeId 
    AND (States.Name LIKE '%' + @text + '%'  COLLATE SQL_LATIN1_GENERAL_CP1_CI_AI OR Cities.Name LIKE '%' + @text + '%'  COLLATE SQL_LATIN1_GENERAL_CP1_CI_AI OR Companies.CompanyName LIKE '%' + @text + '%'  COLLATE SQL_LATIN1_GENERAL_CP1_CI_AI OR Companies.CompanyShortName LIKE '%' + @text + '%'  COLLATE SQL_LATIN1_GENERAL_CP1_CI_AI)
 )AS INDICE
WHERE RowNumber BETWEEN @numberByPage * @page + 1 AND @numberByPage * (@page + 1)

END
go