IF OBJECT_ID('dbo.roc_spGetQualityControlByFullText') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetQualityControlByFullText
go

/* 
	Author:			Daniel Campos. / Elizabeth Lazo.				 
	Object:			dbo.roc_spGetQualityControlByFullText
	
	Description:	Retrieves all sections by parentId.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetQualityControlByFullText @parentId=10,  @page = 0, @numberbypage = 10, @text = 'equipo y kits'


 */ 

CREATE PROCEDURE dbo.roc_spGetQualityControlByFullText
(
    @parentId   int,
    @page       int,
    @numberbypage  int,
    @text      varchar(250)
)

AS
 BEGIN
     --The parameters shouldn't be null:
       IF (@parentId IS NULL OR @page IS NULL OR @numberbypage IS NULL OR @text IS NULL) 
  BEGIN

    RAISERROR ('The current operation requires more parameters', 16, 1)
    RETURN -1

  END 

SELECT (
	  SELECT count(*) from 
	  (
	  SELECT * FROM Sections WHERE Sections.ParentId = @parentId AND Active =  '1' 
	AND (SELECT count(*) FROM Sections AS SubSection WHERE SubSection.ParentId = Sections.SectionId) > 0 AND FREETEXT(Sections.SectionName,@text)
	  ) as contador
	) AS TOTAL,*   
	FROM (
	SELECT Sections.SectionId, Sections.ParentId, Sections.SectionName, ROW_NUMBER() OVER (ORDER BY Sections.SectionName ASC) AS RowNumber FROM Sections
	 WHERE Sections.ParentId = @parentId AND Active =  '1' 
	AND (SELECT count(*) FROM Sections AS SubSection WHERE SubSection.ParentId = Sections.SectionId) > 0 AND FREETEXT(Sections.SectionName,@text)
	  )AS SubsectionIndex
	WHERE RowNumber BETWEEN @numberbypage * @page + 1 AND @numberbypage * (@page + 1)







  END

 go

