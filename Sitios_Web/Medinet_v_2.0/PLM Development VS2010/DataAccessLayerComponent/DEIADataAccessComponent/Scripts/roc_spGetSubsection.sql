IF OBJECT_ID('dbo.roc_spGetSubsection') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetSubsection
go

/* 
	Author:			Daniel Campos. / Elizabeth Lazo.				 
	Object:			dbo.roc_spGetSubsection
	
	Description:	Retrieves all material by parentId.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetSubsection @parentId = 166, @editionId = 3, @page = 0, @numberByPage = 10


 */ 

CREATE PROCEDURE dbo.roc_spGetSubsection
(
    @parentId      int,
    @editionId     int,
    @page          int,
    @numberByPage  int  
)


AS
 BEGIN
     --The parameters shouldn't be null:
       IF (@parentId IS NULL OR  @editionId IS NULL OR @page IS NULL OR @numberByPage IS NULL) 
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
	  AND Active =  '1' AND eps.EditionId=@editionId 
	  GROUP BY s.SectionId
	  ) as contador
	) AS TOTAL,*   
	FROM (
	  SELECT s.SectionId,s.SectionName, s.ParentId, ROW_NUMBER() OVER (ORDER BY s.SectionName ASC) AS RowNumber 
	  FROM Sections AS s
	  INNER JOIN EditionProductSections AS eps ON eps.SectionId=s.SectionId
	  WHERE (s.ParentId = @parentId)  
	  AND s.Active =  '1' AND eps.EditionId=@editionId
	  GROUP BY s.SectionId,s.SectionName, s.ParentId
	  )AS SubsectionIndex
	WHERE RowNumber BETWEEN @numberByPage * @page + 1 AND @numberByPage * (@page + 1)

  
  END

 go
