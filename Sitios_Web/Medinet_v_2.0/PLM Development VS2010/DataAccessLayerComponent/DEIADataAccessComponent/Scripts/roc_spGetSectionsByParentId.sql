IF OBJECT_ID('dbo.roc_spGetSectionsByParentId') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetSectionsByParentId
go

/* 
	Author:			Daniel Campos. / Elizabeth Lazo.				 
	Object:			dbo.roc_spGetSectionsByParentId
	
	Description:	Retrieves sections by parentId.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetSectionsByParentId @editionId = 3, @parentId = 9, @numberByPage = 10 , @page = 0 


 */ 

CREATE PROCEDURE dbo.roc_spGetSectionsByParentId
(
	@editionId		int,
	@parentId		int,
	@numberByPage	int,
	@page			int
)

AS
 BEGIN
     --The parameters shouldn't be null:
       IF (@editionId IS NULL OR @parentId IS NULL OR @page IS NULL OR @numberByPage IS NULL)
  BEGIN

    RAISERROR ('The current operation requires more parameters', 16, 1)
    RETURN -1

  END 

 SELECT (
	  SELECT count(*) from 
	  (
	  SELECT Sections.SectionId, Sections.SectionName,Sections.ParentId FROM Sections 
	inner join EditionProductSections AS eps ON eps.SectionId=Sections.SectionId
	  WHERE Sections.ParentId = @parentId AND Sections.Active =  '1' AND eps.EditionId=@editionId 
	  GROUP BY Sections.SectionId, Sections.SectionName,Sections.ParentId
	  ) as contador
	) AS TOTAL,*   
	FROM (
	  SELECT Sections.SectionId, Sections.SectionName,Sections.ParentId,
	  ROW_NUMBER() OVER (ORDER BY Sections.SectionName ASC) AS RowNumber
	  FROM Sections 
	  inner join EditionProductSections AS eps ON eps.SectionId=Sections.SectionId
	  WHERE Sections.ParentId = @parentId AND Sections.Active =  '1' AND eps.EditionId=@editionId
	  GROUP BY Sections.SectionId, Sections.SectionName,Sections.ParentId  

	  )AS INDICE_MPRIMAS
	WHERE RowNumber BETWEEN @numberByPage * @page + 1 AND @numberByPage * (@page + 1)

  
  END

 go



