IF OBJECT_ID('dbo.roc_spGetSectionsWithCompaniesByText') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetSectionsWithCompaniesByText
go

/* 
	Author:			Daniel Campos. / Elizabeth Lazo.				 
	Object:			dbo.roc_spGetSectionsWithCompaniesByText
	
	Description:	Retrieves all section whith companyes by text.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetSectionsWithCompaniesByText @parentId = 9, @editionId = 3, @page = 0, @numberbyPage = 10, @text =flexi


 */ 

CREATE PROCEDURE dbo.roc_spGetSectionsWithCompaniesByText
(
    @parentId      int,
    @editionId     int,
    @page          int,
    @numberbyPage  int,
    @text          varchar(30)  
)


AS
 BEGIN
     --The parameters shouldn't be null:
       IF (@parentId IS NULL OR @editionId IS NULL OR @page IS NULL OR @numberbyPage IS NULL OR @text IS NULL) 
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
	   WHERE s.ParentId = @parentId AND s.Active = '1' AND ce.EditionId=@editionId AND s.SectionName LIKE '%'+ @text +'%' COLLATE SQL_LATIN1_GENERAL_CP1_CI_AI
	   GROUP BY s.SectionName, s.SectionId, s.ParentId
	  ) as contador
	) AS TOTAL,*   
	FROM (  
	  SELECT s.SectionId,s.SectionName, s.ParentId,
	  ROW_NUMBER() OVER (ORDER BY s.SectionName ASC) AS RowNumber
	  FROM Sections AS s
	  INNER JOIN CompanySections AS cs ON cs.SectionId = s.SectionId
	  INNER JOIN CompanyEditions AS ce ON ce.CompanyId = cs.CompanyId
	  WHERE s.ParentId = @parentId AND s.Active = '1' AND ce.EditionId=@editionId AND s.SectionName LIKE '%'+ @text +'%' COLLATE SQL_LATIN1_GENERAL_CP1_CI_AI 
	  GROUP BY s.SectionName, s.SectionId, s.ParentId   
	  )AS SECTIONS_COMPANIES
	WHERE RowNumber BETWEEN @numberbyPage * @page + 1 AND @numberbyPage * (@page + 1)

  
  END

 go
