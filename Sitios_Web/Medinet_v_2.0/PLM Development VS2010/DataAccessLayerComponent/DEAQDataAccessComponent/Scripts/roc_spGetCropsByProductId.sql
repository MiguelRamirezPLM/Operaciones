IF OBJECT_ID('dbo.roc_spGetCropsByProductId') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetCropsByProductId
go

/* 
	Author:			Daniel Campos / Elizabeth Lazo.				 
	Object:			dbo.roc_spGetCropsByProductId
	
	Description:	Retrieves all information the crops by product.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetCropsByProductId @productId=157, @editionId=4


 */ 

CREATE PROCEDURE dbo.roc_spGetCropsByProductId
(   
  
  @productId  int,
  @editionId  int   
  
) 
AS
  BEGIN
  --The parameters shouldn't be null:
       IF (@productId IS NULL OR @editionId IS NULL ) 
             
  BEGIN

    RAISERROR ('The current operation requires more parameters', 16, 1)
    RETURN -1

  END
  
  SELECT DISTINCT C.CropId, C.CropName 
	FROM Crops AS C
	INNER JOIN ProductCrops AS PC ON PC.CropId=C.CropId
	INNER JOIN EditionDivisionProducts AS EDP ON EDP.ProductId=PC.ProductId
	 WHERE C.Active='1' AND EDP.EditionId=@editionId AND PC.ProductId=@productId

END
go

