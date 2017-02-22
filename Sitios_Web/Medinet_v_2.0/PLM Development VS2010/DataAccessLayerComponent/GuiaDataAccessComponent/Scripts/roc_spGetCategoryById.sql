IF OBJECT_ID('dbo.roc_spGetCategoryById') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetCategoryById
go

/* 
	Author:			Daniel Campos. / Elizabeth Lazo.
				 
	Object:			dbo.roc_spGetCategoryById.
	
	Description:	Retrieves all categories by Id.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetCategoryById  @categoryId = 5


 */ 


CREATE PROCEDURE dbo.roc_spGetCategoryById
(
  @categoryId             int
   
)
AS
  BEGIN
  --The parameters shouldn't be null:
       IF (@categoryId IS NULL) 
  BEGIN

    RAISERROR ('The current operation requires more parameters', 16, 1)
    RETURN -1

  END

		SELECT * FROM Categories
		WHERE CategoryId=@categoryId




END
go


