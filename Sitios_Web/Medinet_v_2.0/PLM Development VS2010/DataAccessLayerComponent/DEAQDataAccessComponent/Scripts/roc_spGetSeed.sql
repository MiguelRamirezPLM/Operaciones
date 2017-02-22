IF OBJECT_ID('dbo.roc_spGetSeed') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetSeed
go

/* 
	Author:			Daniel Campos / Elizabeth Lazo.				 
	Object:			dbo.roc_spGetSeed
	
	Description:	Retrieves all information the seeds.
	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetSeed @seedId = 6


 */ 

CREATE PROCEDURE dbo.roc_spGetSeed
(   
  @seedId   int 
) 
AS
  BEGIN
  --The parameters shouldn't be null:
 
       IF (@seedId IS NULL) 
             
  BEGIN

    RAISERROR ('The current operation requires more parameters', 16, 1)
    RETURN -1

  END
			
	SELECT *
	 FROM Seeds
	 WHERE Active='1' AND SeedId=@seedId

	END

go


