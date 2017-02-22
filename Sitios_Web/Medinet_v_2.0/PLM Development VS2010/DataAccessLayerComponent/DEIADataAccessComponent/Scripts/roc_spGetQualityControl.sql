IF OBJECT_ID('dbo.roc_spGetQualityControl') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetQualityControl
go

/* 
	Author:			Daniel Campos. / Elizabeth Lazo.				 
	Object:			dbo.roc_spGetQualityControl
	
	Description:	Retrieves all sections by parentId.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetQualityControl @parentId=10,  @page = 0, @numberbypage = 10


 */ 

CREATE PROCEDURE dbo.roc_spGetQualityControl 
(
    @parentId   int,
    @page       int,
    @numberbypage  int
)

AS
 BEGIN
     --The parameters shouldn't be null:
       IF (@parentId IS NULL OR @page IS NULL OR @numberbypage  IS NULL) 
  BEGIN

    RAISERROR ('The current operation requires more parameters', 16, 1)
    RETURN -1

  END 

SELECT (
	  SELECT count(*) from 
	  (
	  SELECT * FROM Sections WHERE Sections.ParentId = @parentId AND Active =  '1' 
	AND (SELECT count(*) FROM Sections AS SubSection WHERE SubSection.ParentId = Sections.SectionId) > 0
	  ) as contador
	) AS TOTAL,*   
	FROM (
	SELECT Sections.SectionId, Sections.ParentId, Sections.SectionName, ROW_NUMBER() OVER (ORDER BY Sections.SectionName ASC) AS RowNumber FROM Sections
	 WHERE Sections.ParentId = @parentId AND Active =  '1' 
	AND (SELECT count(*) FROM Sections AS SubSection WHERE SubSection.ParentId = Sections.SectionId) > 0
	  )AS SubsectionIndex
	WHERE RowNumber BETWEEN @numberbypage* @page + 1 AND @numberbypage * (@page + 1)



  END

 go


