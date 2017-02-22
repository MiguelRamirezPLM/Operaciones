IF OBJECT_ID('dbo.roc_spGetSectionsWithCompaniesByLetter') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetSectionsWithCompaniesByLetter 
go

/* 
	Author:			Daniel Campos. / Elizabeth Lazo.				 
	Object:			dbo.roc_spGetSectionsWithCompaniesByLetter
	
	Description:	Retrieves all section whith companyes by letter.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetSectionsWithCompaniesByLetter @parentId = 9, @editionId = 3, @page = 0, @numberbyPage = 10, @letter = b 


 */ 

CREATE PROCEDURE dbo.roc_spGetSectionsWithCompaniesByLetter
(
    @parentId      int,
    @editionId     int,
    @page          int,
    @numberbyPage  int,
    @letter        varchar(1)  
)


AS
 BEGIN
     --The parameters shouldn't be null:
       IF (@parentId IS NULL OR @editionId IS NULL OR @page IS NULL OR @numberbyPage IS NULL OR @letter IS NULL) 
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
	   WHERE s.ParentId = @parentId AND s.Active = '1' AND ce.EditionId=@editionId AND s.SectionName LIKE @letter +'%'
	   GROUP BY s.SectionName, s.SectionId, s.ParentId
	  ) as contador
	) AS TOTAL,*   
	FROM (  
	  SELECT s.SectionId,s.SectionName, s.ParentId,
	  ROW_NUMBER() OVER (ORDER BY s.SectionName ASC) AS RowNumber
	  FROM Sections AS s
	  INNER JOIN CompanySections AS cs ON cs.SectionId = s.SectionId
	  INNER JOIN CompanyEditions AS ce ON ce.CompanyId = cs.CompanyId
	  WHERE s.ParentId = @parentId AND s.Active = '1' AND ce.EditionId=@editionId AND s.SectionName LIKE @letter +'%' 
	  GROUP BY s.SectionName, s.SectionId, s.ParentId   
	  )AS SECTIONS_COMPANIES
	WHERE RowNumber BETWEEN @numberbyPage * @page + 1 AND @numberbyPage * (@page + 1)


  
  END

 go
