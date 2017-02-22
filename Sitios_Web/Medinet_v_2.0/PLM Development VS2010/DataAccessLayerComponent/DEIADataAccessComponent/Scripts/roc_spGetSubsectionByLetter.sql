IF OBJECT_ID('dbo.roc_spGetSubsectionByLetter') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetSubsectionByLetter
go

/* 
	Author:			Daniel Campos. / Elizabeth Lazo.				 
	Object:			dbo.roc_spGetSubsectionByLetter
	
	Description:	Retrieves all subsection by letter.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetSubsectionByLetter @parentId = 166, @editionId = 3, @page = 0, @numberByPage = 10, @letter = a 


 */ 

CREATE PROCEDURE dbo.roc_spGetSubsectionByLetter
(
    @parentId      int,
    @editionId     int,
    @page          int,
    @numberByPage  int,
    @letter        varchar(1)  
)


AS
 BEGIN
     --The parameters shouldn't be null:
       IF (@parentId IS NULL OR  @editionId IS NULL OR @page IS NULL OR @numberByPage IS NULL OR @letter IS NULL) 
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
	  AND Active =  '1' AND eps.EditionId=@editionId AND SectionName LIKE @letter +'%'  
	  GROUP BY s.SectionId
	  ) as contador
	) AS TOTAL,*   
	FROM (
	  SELECT s.SectionId,s.SectionName, s.ParentId, ROW_NUMBER() OVER (ORDER BY s.SectionName ASC) AS RowNumber 
	  FROM Sections AS s
	  INNER JOIN EditionProductSections AS eps ON eps.SectionId=s.SectionId
	  WHERE (s.ParentId = @parentId)  
	  AND s.Active =  '1' AND eps.EditionId=@editionId AND SectionName LIKE @letter +'%'  
	  GROUP BY s.SectionId,s.SectionName, s.ParentId
	  )AS SubsectionIndex
	WHERE RowNumber BETWEEN @numberByPage * @page + 1 AND @numberByPage * (@page + 1)
  
  END

 go
