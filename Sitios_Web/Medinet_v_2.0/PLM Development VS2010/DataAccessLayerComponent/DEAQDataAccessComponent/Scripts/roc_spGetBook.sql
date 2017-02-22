IF OBJECT_ID('dbo.roc_spGetBook') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetBook
go

/* 
	Author:			Daniel Campos / Elizabeth Lazo.				 
	Object:			dbo.roc_spGetBook
	
	Description:	Retrieves all information the  book by Id.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetBook @bookId = 2


 */ 

CREATE PROCEDURE dbo.roc_spGetBook
(   
  @bookId      int   
  
) 
AS
  BEGIN
  --The parameters shouldn't be null:
       IF (@bookId IS NULL) 
             
  BEGIN

    RAISERROR ('The current operation requires more parameters', 16, 1)
    RETURN -1

  END

   SELECT * FROM Books 
      WHERE BookId = @bookId

END
go


