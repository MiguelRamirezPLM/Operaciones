IF OBJECT_ID('dbo.roc_spGetParticipantCompaniesByLetter') IS NOT NULL
      DROP PROCEDURE dbo.roc_spGetParticipantCompaniesByLetter
 
  go

  /* 
	Author:			Daniel Campos / Elizabeth Lazo.				 
	Object:			dbo.roc_spGetParticipantCompaniesByLetter
	
	Description:	Retrieves all companies participant by text
 
	Company:		PLM México.

	EXECUTE dbo.roc_spGetParticipantCompaniesByLetter @editionId=2, @companyTypeId=1,  @letter ='s',  @page=0, @numberByPage=10
                                                             


 */ 

CREATE PROCEDURE dbo.roc_spGetParticipantCompaniesByLetter
(
  @editionId        int,
  @companyTypeId    tinyint,
  @letter           varchar(1),
  @page             int,
  @numberByPage     int
)

AS
BEGIN

   --The parametres shouldn't be null:
   IF(@editionId IS NULL OR @companyTypeId IS NULL OR  
      @letter IS NULL OR @page IS NULL OR @numberByPage IS NULL)
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
           AND Companies.CompanyTypeId = @companyTypeId 
           AND Companies.CompanyName LIKE @letter + '%') as contador) AS TOTAL,*   

  FROM 
   (SELECT Companies.*,CompanyEditions.HTMLContent,   
           ROW_NUMBER() OVER (ORDER BY Companies.CompanyName ASC) AS RowNumber 
    FROM Companies
      INNER JOIN CompanyEditions ON CompanyEditions.CompanyId = Companies.CompanyId
      INNER JOIN Editions ON Editions.EditionId = CompanyEditions.EditionId
    WHERE Companies.Active = '1' 
      AND Editions.EditionId = @editionId 
      AND Companies.CompanyTypeId = @companyTypeId 
      AND Companies.CompanyName LIKE @letter + '%')AS INDICE

  WHERE RowNumber BETWEEN @numberByPage * @page  + 1 AND @numberByPage * (@page  + 1)

END
go