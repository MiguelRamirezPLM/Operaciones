IF OBJECT_ID('dbo.roc_spGetCropsByLetter') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetCropsByLetter
go

/* 
	Author:			Daniel Campos / Elizabeth Lazo.				 
	Object:			dbo.roc_spGetCropsByLetter
	
	Description:	Retrieves all crops by letter.
	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetCropsByLetter @numberByPage=10,  @page=0,  @editionId=2, @letter= 'f'


 */ 

CREATE PROCEDURE dbo.roc_spGetCropsByLetter
(   
  @numberByPage     int,   
  @page             int,
  @editionId        int,
  @letter           varchar(1)
) 
AS
  BEGIN
  --The parameters shouldn't be null:
       IF (@numberByPage IS NULL OR @page IS NULL OR @editionId IS NULL OR @letter IS NULL) 
             
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
			 WHERE C.Active='1' AND EDP.EditionId=@editionId AND C.CropName LIKE @letter + '%'
			 ) as contador
		) AS TOTAL,*   
	 
	FROM (
			 SELECT DISTINCT C.CropId, C.CropName ,
			 ROW_NUMBER() OVER (ORDER BY C.CropName ASC) AS RowNumber
			 FROM Crops AS C
			 INNER JOIN ProductCrops AS PC ON PC.CropId=C.CropId
			 INNER JOIN EditionDivisionProducts AS EDP ON EDP.ProductId=PC.ProductId
	 
     WHERE C.Active='1' AND EDP.EditionId=@editionId AND C.CropName LIKE @letter + '%' 
	 GROUP BY C.CropId, C.CropName
	  )AS INDICE
	WHERE RowNumber BETWEEN @numberByPage * @page + 1 AND @numberByPage * (@page + 1)

	END

go


