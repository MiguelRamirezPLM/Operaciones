IF OBJECT_ID('dbo.roc_spGetCrop') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetCrop
go

/* 
	Author:			Daniel Campos / Elizabeth Lazo.				 
	Object:			dbo.roc_spGetCrop
	
	Description:	Retrieves all information the crop by ID.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetCrop @cropId = 38


 */ 

CREATE PROCEDURE dbo.roc_spGetCrop
(   
  @cropId      int   
  
) 
AS
  BEGIN
  --The parameters shouldn't be null:
       IF ( @cropId IS NULL) 
             
  BEGIN

    RAISERROR ('The current operation requires more parameters', 16, 1)
    RETURN -1

  END

		   SELECT *
			FROM Crops AS C
			WHERE C.Active='1' AND C.CropId=@cropId

END
go


