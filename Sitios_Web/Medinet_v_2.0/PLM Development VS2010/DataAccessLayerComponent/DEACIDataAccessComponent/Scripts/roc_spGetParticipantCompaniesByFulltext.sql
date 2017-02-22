IF OBJECT_ID('dbo.roc_spGetParticipantCompaniesByFulltext') IS NOT NULL
     DROP PROCEDURE dbo.roc_spGetParticipantCompaniesByFulltext

go

/* 
	Author:			Daniel Campos. / Elizabeth Lazo.				 
	Object:			dbo.roc_spGetParticipantCompaniesByFulltext
	
	Description:	Retrieves all companies participant by full text.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetParticipantCompaniesByFulltext  @editionId=2, @page=0 , @numberbypage=10, @CompanyTypeId=1, @text ='medidores siemens' 


 */

CREATE PROCEDURE dbo.roc_spGetParticipantCompaniesByFulltext 
(
  @editionId      int,
  @page           int,
  @numberbypage   int,
  @companyTypeId   tinyint, 
  @text          varchar(250)
)


AS
BEGIN

   --The parametres shouldn't be null:
   IF(@editionId IS NULL OR @page IS NULL OR @numberbypage IS NULL OR 
      @companyTypeId IS NULL OR @text IS NULL)
   BEGIN
	
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
	
   END

   SELECT 
     (SELECT count(*) 
        from 
         (SELECT Companies.*,CompanyEditions.HTMLContent 
           FROM Companies
			   INNER JOIN CompanyEditions ON CompanyEditions.CompanyId = Companies.CompanyId
			   INNER JOIN Editions ON Editions.EditionId = CompanyEditions.EditionId
			   WHERE Companies.Active = '1' AND 
                     Editions.EditionId = @editionId  AND 
                     Companies.CompanyTypeId = @companyTypeId AND 
                    (FREETEXT(Companies.CompanyName ,@text ) OR 
                    FREETEXT(Companies.CompanyShortName ,@text ))) as contador) AS TOTAL,*   

			FROM 
              (SELECT Companies.*,CompanyEditions.HTMLContent,  
				  ROW_NUMBER() OVER (ORDER BY Companies.CompanyName ASC) AS RowNumber 
                   FROM Companies
				  INNER JOIN CompanyEditions ON CompanyEditions.CompanyId = Companies.CompanyId
				  INNER JOIN Editions ON Editions.EditionId = CompanyEditions.EditionId
		           WHERE Companies.Active = '1' AND 
                     Editions.EditionId = @editionId  AND 
                     Companies.CompanyTypeId = @companyTypeId AND (
                     FREETEXT(Companies.CompanyName , @text ) OR 
                     FREETEXT(Companies.CompanyShortName ,@text )))AS INDICE

				WHERE RowNumber BETWEEN @numberbypage * @page  + 1 AND @numberbypage * (@page  + 1)

 END
  go