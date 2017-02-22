IF OBJECT_ID('dbo.roc_spGetInternationalProductsByParentId') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetInternationalProductsByParentId
go

/* 
	Author:			Daniel Campos. / Elizabeth Lazo.
				 
	Object:			dbo.roc_spGetInternationalProductsByParentId.
	
	Description:	Retrieves all internacional products by parentId.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetInternationalProductsByParentId  @clientId = 1, @categoryId= 5,  @productParentId = 248


 */ 


CREATE PROCEDURE dbo.roc_spGetInternationalProductsByParentId
(
  @clientId             int,
  @categoryId           int,
  @productParentId      int
   
)
AS
  BEGIN
  --The parameters shouldn't be null:
       IF (@clientId IS NULL AND @categoryId IS NULL AND @productParentId IS NULL) 
  BEGIN

    RAISERROR ('The current operation requires more parameters', 16, 1)
    RETURN -1

  END

	SELECT IP.ParentId,ICP.IntProductId,IP.Description,C.CategoryId FROM IntProducts AS IP
		INNER JOIN IntClientProducts AS ICP ON ICP.IntProductId=IP.IntProductId
		INNER JOIN Categories AS C ON C.CategoryId=ICP.CategoryId 
		WHERE ICP.IntClientId=@clientId AND C.CategoryId=@categoryId AND ICP.Active='1' AND C.Active='1' 
		AND IP.Active='1' AND IP.ParentId =@productParentId 
		order by IP.Description



END
go


