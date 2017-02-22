IF OBJECT_ID('dbo.roc_spGetSectionsWithCompanies') IS NOT NULL
     DROP PROCEDURE dbo.roc_spGetSectionsWithCompanies

go

/* 
	Author:			Daniel Campos. / Elizabeth Lazo.				 
	Object:			dbo.roc_spGetSectionsWithCompanies
	
	Description:	Retrieves all sections whith companies.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetSectionsWithCompanies @editionId=3,  @parentId=9, @page=0 , @numberbypage=10


 */

CREATE PROCEDURE dbo.roc_spGetSectionsWithCompanies 
(
    @editionId                  int,
    @parentId					int,
	@page						int,
	@numberByPage				int
)


AS
BEGIN

   --The parametres shouldn't be null:
   IF(@editionId IS NULL OR  @parentId IS NULL OR @page IS NULL OR @numberbypage IS NULL)
   BEGIN
	
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
	
   END

		SELECT (
		  SELECT count(*) from 
		  ( 
		   SELECT s.SectionId,s.SectionName, s.ParentId FROM Sections AS s
		   INNER JOIN CompanySections AS cs ON cs.SectionId = s.SectionId
		   INNER JOIN CompanyEditions AS ce ON ce.CompanyId = cs.CompanyId
		   WHERE s.ParentId = @parentId AND s.Active = '1' AND ce.EditionId=@editionId
		   GROUP BY s.SectionName, s.SectionId, s.ParentId
		  ) as contador
		) AS TOTAL,*   
		FROM (  
		  SELECT s.SectionId,s.SectionName , s.ParentId,
		  ROW_NUMBER() OVER (ORDER BY s.SectionName ASC) AS RowNumber
		  FROM Sections AS s
		  INNER JOIN CompanySections AS cs ON cs.SectionId = s.SectionId
		  INNER JOIN CompanyEditions AS ce ON ce.CompanyId = cs.CompanyId
		  WHERE s.ParentId = @parentId AND s.Active = '1' AND ce.EditionId=@editionId  
		  GROUP BY s.SectionName, s.SectionId, s.ParentId   
		  )AS SECTIONS_COMPANIES
		WHERE RowNumber BETWEEN @numberByPage * @page + 1 AND @numberByPage * (@page + 1)

END

 go