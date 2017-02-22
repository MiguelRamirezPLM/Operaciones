IF OBJECT_ID('dbo.roc_spGetDivisionCategories') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetDivisionCategories
go

/* 
	Author:			Daniel Campos / Elizabeth Lazo.				 
	Object:			dbo.roc_spGetDivisionCategories
	
	Description:	Retrieves all divisions by divisionId.
	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetDivisionCategories @divisionId=11 ,@editionId=4


 */ 

CREATE PROCEDURE dbo.roc_spGetDivisionCategories
(   
  
  @divisionId     int,
  @editionId      int
  
) 
AS
  BEGIN
  --The parameters shouldn't be null:
       IF (@divisionId IS NULL OR @editionId IS NULL ) 
             
  BEGIN

    RAISERROR ('The current operation requires more parameters', 16, 1)
    RETURN -1

  END
			
			
	 SELECT DISTINCT C.CategoryId,C.CategoryName, C.Active
		FROM DivisionCAtegories AS DC
			INNER JOIN Categories AS C ON DC.CategoryId=C.CategoryId
			INNER JOIN EditionDivisionProducts AS EDP ON EDP.DivisionId = DC.DivisionId
		WHERE C.Active='1' AND DC.DivisionId=@divisionId AND EDP.EditionId=@editionId



	END

go


