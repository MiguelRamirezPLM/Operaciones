IF OBJECT_ID('dbo.roc_spGetInternationalCategoriesByParentId') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetInternationalCategoriesByParentId
go

/* 
	Author:			Daniel Campos. / Elizabeth Lazo.
				 
	Object:			dbo.roc_spGetInternationalCategoriesByParentId.
	
	Description:	Retrieves all internacional categories by parent.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetInternationalCategoriesByParentId  @parentId = 1


 */ 


CREATE PROCEDURE dbo.roc_spGetInternationalCategoriesByParentId 
(
  @parentId             int  
  
)
AS
  BEGIN
  --The parameters shouldn't be null:
       IF (@parentId IS NULL) 
  BEGIN

    RAISERROR ('The current operation requires more parameters', 16, 1)
    RETURN -1

  END

	SELECT * FROM Categories AS C 
		WHERE C.ParentId =@parentId  AND Active='1'
		ORDER BY [Description]

 
END
go