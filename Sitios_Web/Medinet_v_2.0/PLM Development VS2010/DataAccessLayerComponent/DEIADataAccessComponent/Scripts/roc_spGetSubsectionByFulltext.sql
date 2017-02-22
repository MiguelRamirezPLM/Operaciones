IF OBJECT_ID('dbo.roc_spGetSubsectionByFulltext') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetSubsectionByFulltext
go

/* 
	Author:			Daniel Campos. / Elizabeth Lazo.				 
	Object:			dbo.roc_spGetSubsectionByFulltext
	
	Description:	Retrieves all subsection by full text.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetSubsectionByFulltext @parentId = 166, @editionId = 3, @page = 0, @numberByPage = 10, @fulltext = 'analisis y sistemas' 


 */ 

CREATE PROCEDURE dbo.roc_spGetSubsectionByFulltext
(
    @parentId      int,
    @editionId     int,
    @page          int,
    @numberByPage  int,
    @fulltext      varchar(30)  
)


AS
 BEGIN
     --The parameters shouldn't be null:
       IF (@parentId IS NULL OR  @editionId IS NULL OR @page IS NULL OR @numberByPage IS NULL OR @fulltext IS NULL) 
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
	  AND Active =  '1' AND eps.EditionId=@editionId AND FREETEXT (SectionName, @fulltext) 
	  GROUP BY s.SectionId
	  ) as contador
	) AS TOTAL,*   
	FROM (
	  SELECT s.SectionId,s.SectionName, s.ParentId, ROW_NUMBER() OVER (ORDER BY s.SectionName ASC) AS RowNumber 
	  FROM Sections AS s
	  INNER JOIN EditionProductSections AS eps ON eps.SectionId=s.SectionId
	  WHERE (s.ParentId = @parentId)  
	  AND s.Active =  '1' AND eps.EditionId=@editionId AND FREETEXT (SectionName, @fulltext) 
	  GROUP BY s.SectionId,s.SectionName, s.ParentId
	  )AS SubsectionIndex
	WHERE RowNumber BETWEEN @numberByPage * @page + 1 AND @numberByPage * (@page + 1)

  
  END

 go
