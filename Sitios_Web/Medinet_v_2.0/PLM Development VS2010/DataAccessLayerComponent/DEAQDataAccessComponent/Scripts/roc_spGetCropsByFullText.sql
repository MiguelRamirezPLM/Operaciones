IF OBJECT_ID('dbo.roc_spGetCropsByFullText') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetCropsByFullText
go

/* 
	Author:			Daniel Campos / Elizabeth Lazo.				 
	Object:			dbo.roc_spGetCropsByFullText
	
	Description:	Retrieves all crops by fulltext.
	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetCropsByFullText @numberByPage=10,  @page=0,  @editionId=2, @text= 'cebolla'


 */ 

CREATE PROCEDURE dbo.roc_spGetCropsByFullText
(   
  @numberByPage     int,   
  @page             int,
  @editionId        int,
  @text             varchar(250)
) 
AS
  BEGIN
  --The parameters shouldn't be null:
       IF (@numberByPage IS NULL OR @page IS NULL OR @editionId IS NULL OR @text IS NULL) 
             
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
					 WHERE C.Active='1' AND EDP.EditionId=@editionId AND C.CropName like @text
					 ) as contador
					) AS TOTAL,*   
				 
				FROM (
						 SELECT DISTINCT C.CropId, C.CropName ,
						 ROW_NUMBER() OVER (ORDER BY C.CropName ASC) AS RowNumber
						 FROM Crops AS C
						INNER JOIN ProductCrops AS PC ON PC.CropId=C.CropId
						INNER JOIN EditionDivisionProducts AS EDP ON EDP.ProductId=PC.ProductId
						 WHERE C.Active='1' AND EDP.EditionId=@editionId AND C.CropName like @text
						 GROUP BY C.CropId, C.CropName
						  )AS INDICE
						WHERE RowNumber BETWEEN @numberByPage * @page  + 1 AND @numberByPage * (@page  + 1)
	END

go