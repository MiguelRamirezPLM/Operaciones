IF OBJECT_ID('roc_spGetSectionsByParentIdByFulltext') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetSectionsByParentIdByFulltext
go

/* 
	Author:			Daniel Campos. / Elizabeth Lazo.				 
	Object:			dbo.roc_spGetSectionsByParentIdByFulltext
	
	Description:	Retrieves all sections by parentId and by fulltext.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetSectionsByParentIdByFulltext @editionId = 3, @parentId = 3, @numberByPage = 10 , @page = 0, @fullText = 'AGENTES PESO' 


 */ 

CREATE PROCEDURE dbo.roc_spGetSectionsByParentIdByFulltext
(
	@editionId		int,
	@parentId		int,
    @page			int,
	@numberByPage	int,
    @fullText           varchar(30)
	
)

AS
 BEGIN
     --The parameters shouldn't be null:
       IF (@editionId IS NULL OR @parentId IS NULL OR @page IS NULL OR @numberByPage IS NULL OR @fullText IS NULL)
  BEGIN

    RAISERROR ('The current operation requires more parameters', 16, 1)
    RETURN -1

  END 

SELECT (
	  SELECT count(*) from 
	  (
	  SELECT Sections.SectionId, Sections.SectionName,Sections.ParentId FROM Sections 
	  INNER JOIN EditionProductSections AS eps ON eps.SectionId=Sections.SectionId
	  WHERE Sections.ParentId = @parentId AND Sections.Active =  '1' AND eps.EditionId=@editionId AND FREETEXT (Sections.SectionName, @fullText)
	  GROUP BY Sections.SectionId, Sections.SectionName,Sections.ParentId
	  ) as contador
	) AS TOTAL,*   
	FROM (
	  SELECT Sections.SectionId, Sections.SectionName,Sections.ParentId,
	  ROW_NUMBER() OVER (ORDER BY Sections.SectionName ASC) AS RowNumber
	  FROM Sections 
	  INNER JOIN EditionProductSections AS eps ON eps.SectionId=Sections.SectionId
	  WHERE Sections.ParentId = @parentId AND Sections.Active =  '1' AND eps.EditionId=@editionId AND FREETEXT (Sections.SectionName, @fullText)
	  GROUP BY Sections.SectionId, Sections.SectionName,Sections.ParentId  
	  )AS INDICE_MPRIMAS
	WHERE RowNumber BETWEEN @numberByPage * @page + 1 AND @numberByPage * (@page + 1)

  
  END

 go



