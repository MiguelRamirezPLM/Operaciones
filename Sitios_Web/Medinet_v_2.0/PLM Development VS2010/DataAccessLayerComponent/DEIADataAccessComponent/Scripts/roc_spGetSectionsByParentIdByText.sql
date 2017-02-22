IF OBJECT_ID('dbo.roc_spGetSectionsByParentIdByText') IS NOT NULL
     DROP PROCEDURE dbo.roc_spGetSectionsByParentIdByText

go

/* 
	Author:			Daniel Campos. / Elizabeth Lazo.				 
	Object:			dbo.roc_spGetSectionsByParentIdByText
	
	Description:	Retrieves all sections by parentId and by Text.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetSectionsByParentIdByText @editionId=3,  @parentId=3, @page=0 , @numberbypage=10,  @text =veg 


 */

CREATE PROCEDURE dbo.roc_spGetSectionsByParentIdByText 
(
    @editionId                  int,
    @parentId					int,
	@page						int,
	@numberByPage				int,
	@text						varchar(30)
)


AS
BEGIN

   --The parametres shouldn't be null:
   IF(@editionId IS NULL OR  @parentId IS NULL OR @page IS NULL OR @numberbypage IS NULL OR 
      @text IS NULL)
   BEGIN
	
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
	
   END


  
SELECT (
  SELECT count(*) from 
  (
  SELECT Sections.SectionId, Sections.SectionName,Sections.ParentId FROM Sections 
  INNER JOIN EditionProductSections AS eps ON eps.SectionId=Sections.SectionId
  WHERE Sections.ParentId = @parentId AND Sections.Active =  '1' AND eps.EditionId=@editionId AND Sections.SectionName LIKE '%'+ @text +'%'  COLLATE SQL_LATIN1_GENERAL_CP1_CI_AI
  GROUP BY Sections.SectionId, Sections.SectionName,Sections.ParentId
  ) as contador
) AS TOTAL,*   
FROM (
  SELECT Sections.SectionId, Sections.SectionName,Sections.ParentId,
  ROW_NUMBER() OVER (ORDER BY Sections.SectionName ASC) AS RowNumber
  FROM Sections 
  INNER JOIN EditionProductSections AS eps ON eps.SectionId=Sections.SectionId
  WHERE Sections.ParentId = @parentId AND Sections.Active =  '1' AND eps.EditionId=@editionId AND Sections.SectionName LIKE '%'+ @text +'%'  COLLATE SQL_LATIN1_GENERAL_CP1_CI_AI
  GROUP BY Sections.SectionId, Sections.SectionName,Sections.ParentId  

  )AS INDICE_MPRIMAS
WHERE RowNumber BETWEEN @numberByPage * @page + 1 AND @numberByPage * (@page + 1)

END

 go