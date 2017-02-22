IF OBJECT_ID('dbo.roc_spGetSectionsByParentIdByLetter') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetSectionsByParentIdByLetter
go

/* 
	Author:			Daniel Campos. / Elizabeth Lazo.				 
	Object:			dbo.roc_spGetSectionsByParentIdByLetter
	
	Description:	Retrieves all sections by parentId and by letter.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetSectionsByParentIdByLetter @editionId = 3, @parentId = 23, @numberByPage = 10 , @page = 0, @letter = 'A' 


 */ 

CREATE PROCEDURE dbo.roc_spGetSectionsByParentIdByLetter
(
	@editionId		int,
	@parentId		int,
    @page			int,
	@numberByPage	int,
    @letter         varchar(1)
	
)

AS
 BEGIN
     --The parameters shouldn't be null:
       IF (@editionId IS NULL OR @parentId IS NULL OR @page IS NULL OR @numberByPage IS NULL OR @letter IS NULL)
  BEGIN

    RAISERROR ('The current operation requires more parameters', 16, 1)
    RETURN -1

  END 

SELECT (
	  SELECT count(*) from 
	  (
	  SELECT Sections.SectionId, Sections.SectionName,Sections.ParentId FROM Sections 
	  INNER JOIN EditionProductSections AS eps ON eps.SectionId=Sections.SectionId
	  WHERE Sections.ParentId = @parentId AND Sections.Active =  '1' AND eps.EditionId=@editionId AND Sections.SectionName LIKE @letter +'%'
	  GROUP BY Sections.SectionId, Sections.SectionName,Sections.ParentId
	  ) as contador
	) AS TOTAL,*   
	FROM (
	  SELECT Sections.SectionId, Sections.SectionName,Sections.ParentId,
	  ROW_NUMBER() OVER (ORDER BY Sections.SectionName ASC) AS RowNumber
	  FROM Sections 
	  INNER JOIN EditionProductSections AS eps ON eps.SectionId=Sections.SectionId
	  WHERE Sections.ParentId = @parentId AND Sections.Active =  '1' AND eps.EditionId=@editionId AND Sections.SectionName LIKE @letter +'%'
	  GROUP BY Sections.SectionId, Sections.SectionName,Sections.ParentId  
	  )AS INDICE_MPRIMAS
	WHERE RowNumber BETWEEN @numberByPage * @page + 1 AND @numberByPage * (@page + 1)


  
  END

 go



