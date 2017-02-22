IF OBJECT_ID('dbo.roc_spGetCompaniesByFulltext') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetCompaniesByFulltext
go

/* 
	Author:			Daniel Campos. / Elizabeth Lazo.				 
	Object:			dbo.roc_spGetCompaniesByFulltext
	
	Description:	Retrieves all companies by full text.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetCompaniesByFulltext @page=0, @numberByPage= 10,  @editionId = 2, @text='infra colima'


 */ 

CREATE PROCEDURE dbo.roc_spGetCompaniesByFulltext
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

SELECT (
  SELECT count(*) from 
  (
   SELECT Companies.*, Cities.Name as ciudad, States.StateId, States.Name FROM Companies 
   INNER JOIN CompanyEditions ON CompanyEditions.CompanyId = Companies.CompanyId
   INNER JOIN Cities ON Cities.CityId = Companies.CityId 
   INNER JOIN States ON States.StateId = Cities.StateId
   WHERE (Companies.CompanyTypeId = '2' OR Companies.CompanyTypeId = '3' OR Companies.CompanyTypeId = '4')
   AND CompanyEditions.EditionId = @editionId AND Companies.Active = '1' 
   AND (FREETEXT(Companies.CompanyName,@text ) OR FREETEXT(Cities.Name,@text ) OR FREETEXT(States.Name,@text ) OR FREETEXT(CompanyShortName,@text ))
  ) as contador
   ) AS TOTAL,*
  FROM (

   SELECT Companies.*,  Cities.Name as ciudad, States.StateId, States.Name,
   ROW_NUMBER() OVER (ORDER BY States.Name,Cities.Name, Companies.CompanyName) AS RowNumber FROM Companies 
   INNER JOIN CompanyEditions ON CompanyEditions.CompanyId = Companies.CompanyId
   INNER JOIN Cities ON Cities.CityId = Companies.CityId 
   INNER JOIN States ON States.StateId = Cities.StateId
   WHERE (Companies.CompanyTypeId = '2' OR Companies.CompanyTypeId = '3' OR Companies.CompanyTypeId = '4')
   AND CompanyEditions.EditionId = @editionId AND Companies.Active = '1' 
   AND (FREETEXT(Companies.CompanyName,@text ) OR FREETEXT(Cities.Name, @text ) OR FREETEXT(States.Name, @text ) OR FREETEXT(CompanyShortName, @text ))
)AS INDICE
WHERE RowNumber BETWEEN @numberByPage * @page  + 1 AND @numberByPage * (@page  + 1)

END
go
