IF OBJECT_ID('dbo.roc_spGetCrops') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetCrops
go

/* 
	Author:			Daniel Campos / Elizabeth Lazo.				 
	Object:			dbo.roc_spGetCrops
	
	Description:	Retrieves all information the crop by ID.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetCrops @numberByPage=10,  @page=0,  @editionId=2


 */ 

CREATE PROCEDURE dbo.roc_spGetCrops
(   
  @numberByPage     int,   
  @page             int,
  @editionId        int
) 
AS
  BEGIN
  --The parameters shouldn't be null:
       IF (@numberByPage IS NULL OR @page IS NULL OR @editionId IS NULL) 
             
  BEGIN

    RAISERROR ('The current operation requires more parameters', 16, 1)
    RETURN -1

  END
			SELECT (
			  SELECT count(*) from 
			  (
			 SELECT DISTINCT C.CropId, C.CropName 
			FROM Crops AS C
			INNER JOIN ProductCrops AS PC ON PC.CropId=C.CropId
			INNER JOIN EditionDivisionProducts AS EDP ON EDP.ProductId=PC.ProductId
			 WHERE C.Active='1' AND EDP.EditionId=@editionId 
			 ) as contador
			) AS TOTAL,*   
			 
			FROM (
			 SELECT DISTINCT C.CropId, C.CropName ,
			 ROW_NUMBER() OVER (ORDER BY C.CropName ASC) AS RowNumber
			 FROM Crops AS C
			INNER JOIN ProductCrops AS PC ON PC.CropId=C.CropId
			INNER JOIN EditionDivisionProducts AS EDP ON EDP.ProductId=PC.ProductId
			 WHERE C.Active='1' AND EDP.EditionId=@editionId 
			 GROUP BY C.CropId, C.CropName
			  )AS INDICE
			WHERE RowNumber BETWEEN @numberByPage * @page + 1 AND @numberByPage * (@page + 1)

END
go


