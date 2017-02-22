IF OBJECT_ID('dbo.roc_spGetQualityControlByLetter') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetQualityControlByLetter
go

/* 
	Author:			Daniel Campos. / Elizabeth Lazo.				 
	Object:			dbo.roc_spGetQualityControlByLetter
	
	Description:	Retrieves all sections by parentId.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetQualityControlByLetter @parentId=10,  @page = 0, @numberbypage = 10, @letter = i


 */ 

CREATE PROCEDURE dbo.roc_spGetQualityControlByLetter 
(
    @parentId   int,
    @page       int,
    @numberbypage  int,
    @letter      varchar(1)
)

AS
 BEGIN
     --The parameters shouldn't be null:
       IF (@parentId IS NULL OR @page IS NULL OR @numberbypage IS NULL OR @letter IS NULL) 
  BEGIN

    RAISERROR ('The current operation requires more parameters', 16, 1)
    RETURN -1

  END 

SELECT (
	  SELECT count(*) from 
	  (
	  SELECT * FROM Sections WHERE Sections.ParentId = @parentId AND Active =  '1' 
	AND (SELECT count(*) FROM Sections AS SubSection WHERE SubSection.ParentId = Sections.SectionId) > 0 AND Sections.SectionName LIKE @letter + '%'
	  ) as contador
	) AS TOTAL,*   
	FROM (
	SELECT Sections.SectionId, Sections.ParentId, Sections.SectionName, ROW_NUMBER() OVER (ORDER BY Sections.SectionName ASC) AS RowNumber FROM Sections
	 WHERE Sections.ParentId = @parentId AND Active =  '1' 
	AND (SELECT count(*) FROM Sections AS SubSection WHERE SubSection.ParentId = Sections.SectionId) > 0 AND Sections.SectionName LIKE @letter + '%'
	  )AS SubsectionIndex
	WHERE RowNumber BETWEEN @numberbypage * @page + 1 AND @numberbypage * (@page + 1)





  END

 go


