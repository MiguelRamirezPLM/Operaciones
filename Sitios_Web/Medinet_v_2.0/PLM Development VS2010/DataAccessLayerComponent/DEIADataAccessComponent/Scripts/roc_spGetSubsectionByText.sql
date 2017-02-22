IF OBJECT_ID('dbo.roc_spGetSubsectionByText') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetSubsectionByText
go

/* 
	Author:			Daniel Campos. / Elizabeth Lazo.				 
	Object:			dbo.roc_spGetSubsectionByText
	
	Description:	Retrieves all subsection by text.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetSubsectionByText @parentId = 166, @editionId = 3, @page = 0, @numberByPage = 10, @text = 'analizador'


 */ 

CREATE PROCEDURE dbo.roc_spGetSubsectionByText
(
    @parentId      int,
    @editionId     int,
    @page          int,
    @numberByPage  int,
    @text          varchar(30)  
)


AS
 BEGIN
     --The parameters shouldn't be null:
       IF (@parentId IS NULL OR  @editionId IS NULL OR @page IS NULL OR @numberByPage IS NULL OR @text IS NULL) 
  BEGIN

    RAISERROR ('The current operation requires more parameters', 16, 1)
    RETURN -1

  END 

	SELECT (
	  SELECT count(*) from 
	  (
	  SELECT S.SectionId FROM Sections AS s
	  INNER JOIN EditionProductSections AS eps ON eps.SectionId=s.SectionId
	  WHERE (ParentId = @parentId)  
	  AND Active =  '1' AND eps.EditionId=@editionId AND SectionName LIKE '%'+ @text +'%' COLLATE SQL_LATIN1_GENERAL_CP1_CI_AI
	  GROUP BY s.SectionId
	  ) as contador
	) AS TOTAL,*   
	FROM (
	  SELECT s.SectionId,s.SectionName, s.ParentId, ROW_NUMBER() OVER (ORDER BY s.SectionName ASC) AS RowNumber 
	  FROM Sections AS s
	  INNER JOIN EditionProductSections AS eps ON eps.SectionId=s.SectionId
	  WHERE (s.ParentId = @parentId)  
	  AND s.Active =  '1' AND eps.EditionId=@editionId AND SectionName LIKE '%'+ @text +'%' COLLATE SQL_LATIN1_GENERAL_CP1_CI_AI
	  GROUP BY s.SectionId,s.SectionName, s.ParentId
	  )AS SubsectionIndex
	WHERE RowNumber BETWEEN @numberByPage * @page + 1 AND @numberByPage * (@page + 1)
  
  END

 go
