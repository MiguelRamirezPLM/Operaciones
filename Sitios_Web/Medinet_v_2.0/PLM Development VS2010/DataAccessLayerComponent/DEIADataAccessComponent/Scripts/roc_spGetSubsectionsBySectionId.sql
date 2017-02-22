IF OBJECT_ID('dbo.roc_spGetSubsectionsBySectionId') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetSubsectionsBySectionId
go

/* 
	Author:			Daniel Campos. / Elizabeth Lazo.				 
	Object:			dbo.roc_spGetSubsectionsBySectionId
	
	Description:	Retrieves all subsection by sectionId.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetSubsectionsBySectionId  @editionId = 3, @parentId = 166 


 */ 

CREATE PROCEDURE dbo.roc_spGetSubsectionsBySectionId
(
    @editionId     int,
    @parentId      int
        
)


AS
 BEGIN
     --The parameters shouldn't be null:
       IF (@editionId IS NULL OR @parentId IS NULL) 
  BEGIN

    RAISERROR ('The current operation requires more parameters', 16, 1)
    RETURN -1

  END 

	SELECT s.SectionId, s.ParentId, s.SectionName, s.Active 
	  FROM Sections AS s
	  INNER JOIN EditionProductSections AS eps ON eps.SectionId=s.SectionId
	  WHERE (s.ParentId = @parentId)  
	  AND s.Active =  '1' AND eps.EditionId=@editionId
	  GROUP BY s.SectionId, s.ParentId, s.SectionName,  s.Active  
	  ORDER BY s.SectionName ASC
  
  END

 go
