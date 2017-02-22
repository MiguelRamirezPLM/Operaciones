IF OBJECT_ID('dbo.roc_spGetAdvertisementsBySectionId') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetAdvertisementsBySectionId
go

/* 
	Author:			Daniel Campos. / Elizabeth Lazo.				 
	Object:			dbo.roc_spGetAdvertisementsBySectionId
	
	Description:	Retrieves all advertisements by sectionId.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetAdvertisementsBySectionId @sectionId = 66


 */ 

CREATE PROCEDURE dbo.roc_spGetAdvertisementsBySectionId
(
    @sectionId    int  
)

AS
 BEGIN
     --The parameters shouldn't be null:
       IF (@sectionId IS NULL) 
  BEGIN

    RAISERROR ('The current operation requires more parameters', 16, 1)
    RETURN -1

  END 

  SELECT Advertisements.* FROM SectionAdvertisements 
    INNER JOIN Advertisements ON SectionAdvertisements.AdvId = Advertisements.AdvId
     WHERE Sectionid = @sectionId  AND Advertisements.Active='1'


  
  END

 go



