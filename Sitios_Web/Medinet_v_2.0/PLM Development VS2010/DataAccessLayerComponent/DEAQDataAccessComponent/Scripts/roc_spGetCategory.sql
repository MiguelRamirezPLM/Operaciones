IF OBJECT_ID('dbo.roc_spGetCategory') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetCategory
go

/* 
	Author:			Daniel Campos / Elizabeth Lazo.				 
	Object:			dbo.roc_spGetCategory
	
	Description:	Retrieves all information the category  by Id.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetCategory @categoryId = 16


 */ 

CREATE PROCEDURE dbo.roc_spGetCategory
(   
  @categoryId       int   
  
) 
AS
  BEGIN
  --The parameters shouldn't be null:
       IF ( @categoryId  IS NULL) 
             
  BEGIN

    RAISERROR ('The current operation requires more parameters', 16, 1)
    RETURN -1

  END

    SELECT * FROM Categories
       WHERE CategoryId = @categoryId 


END
go


