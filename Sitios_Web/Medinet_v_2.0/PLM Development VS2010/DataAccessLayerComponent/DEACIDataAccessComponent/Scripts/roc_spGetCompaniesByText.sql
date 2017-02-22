IF OBJECT_ID('dbo.roc_spGetCompaniesByText') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetCompaniesByText
go

/* 
	Author:			Daniel Campos. / Elizabeth Lazo.				 
	Object:			dbo.roc_spGetCompaniesByText
	
	Description:	Retrieves all companies by text.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetCompaniesByText @page=0, @numberByPage= 10,  @editionId = 2, @text='infra'


 */ 

CREATE PROCEDURE dbo.roc_spGetCompaniesByText
(   
  @page               int,   
  @numberByPage       int,
  @editionId          int,
  @text               varchar(250)
              
)
AS
  BEGIN
  --The parameters shouldn't be null:
       IF (@page IS NULL OR @numberByPage IS NULL OR  @editionId IS NULL
            OR @text IS NULL ) 
             
  BEGIN

    RAISERROR ('The current operation requires more parameters', 16, 1)
    RETURN -1

  END

  SELECT 
   (SELECT count(*) 
       from 
	   (SELECT Companies.*, Cities.Name as ciudad, States.StateId, States.Name FROM Companies 
		   INNER JOIN CompanyEditions ON CompanyEditions.CompanyId = Companies.CompanyId
		   INNER JOIN Cities ON Cities.CityId = Companies.CityId 
		   INNER JOIN States ON States.StateId = Cities.StateId
		   WHERE (Companies.CompanyTypeId = '2' OR Companies.CompanyTypeId = '3' OR Companies.CompanyTypeId = '4')
	   AND CompanyEditions.EditionId = @editionId  AND Companies.Active = '1' 
	   AND (Companies.CompanyName LIKE '%' + @text + '%' COLLATE SQL_LATIN1_GENERAL_CP1_CI_AI OR 
           Cities.Name LIKE '%' + @text + '%' COLLATE SQL_LATIN1_GENERAL_CP1_CI_AI OR 
           States.Name LIKE '%' + @text + '%' COLLATE SQL_LATIN1_GENERAL_CP1_CI_AI OR 
           CompanyShortName LIKE '%' + @text + '%' COLLATE SQL_LATIN1_GENERAL_CP1_CI_AI)
	  ) as contador
	   ) AS TOTAL,*
	  FROM (

	   SELECT Companies.*,  Cities.Name as ciudad, States.StateId, States.Name,
	   ROW_NUMBER() OVER (ORDER BY States.Name,Cities.Name, Companies.CompanyName) AS RowNumber FROM Companies 
	   INNER JOIN CompanyEditions ON CompanyEditions.CompanyId = Companies.CompanyId
	   INNER JOIN Cities ON Cities.CityId = Companies.CityId 
	   INNER JOIN States ON States.StateId = Cities.StateId
	   WHERE (Companies.CompanyTypeId = '2' OR Companies.CompanyTypeId = '3' OR Companies.CompanyTypeId = '4')
	   AND CompanyEditions.EditionId = @editionId  AND Companies.Active = '1' 
	   AND (Companies.CompanyName LIKE '%' + @text + '%' COLLATE SQL_LATIN1_GENERAL_CP1_CI_AI OR
            Cities.Name LIKE '%' + @text + '%' COLLATE SQL_LATIN1_GENERAL_CP1_CI_AI OR 
            States.Name LIKE '%' + @text + '%' COLLATE SQL_LATIN1_GENERAL_CP1_CI_AI OR 
            CompanyShortName LIKE '%' + @text + '%' COLLATE SQL_LATIN1_GENERAL_CP1_CI_AI)
	)AS INDICE
	WHERE RowNumber BETWEEN @numberByPage * @page+ 1 AND @numberByPage * (@page + 1)


END
go