IF OBJECT_ID('dbo.roc_spGetInternationalSubcategoriesByClientId') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetInternationalSubcategoriesByClientId
go

/* 
	Author:			Daniel Campos. / Elizabeth Lazo.
				 
	Object:			dbo.roc_spGetInternationalSubcategoriesByClientId.
	
	Description:	Retrieves all internacional sub categories by client.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetInternationalSubcategoriesByClientId  @clientId = 1 


 */ 


CREATE PROCEDURE dbo.roc_spGetInternationalSubcategoriesByClientId
(
  @clientId             int
   
)
AS
  BEGIN
  --The parameters shouldn't be null:
       IF (@clientId IS NULL) 
  BEGIN

    RAISERROR ('The current operation requires more parameters', 16, 1)
    RETURN -1

  END

	SELECT DISTINCT C.CategoryId, C.ParentId, C.Description, C.Active FROM IntProducts AS IP
		INNER JOIN IntClientProducts AS ICP ON ICP.IntProductId=IP.IntProductId
		INNER JOIN Categories AS C ON C.CategoryId=ICP.CategoryId 
		WHERE ICP.IntClientId=@clientId  AND C.ParentId IS NOT NULL
		GROUP BY C.CategoryId, C.ParentId, C.Description, C.Active


END
go