IF OBJECT_ID('dbo.roc_spGetProvidersAndManufacturesByFulltext') IS NOT NULL
     DROP PROCEDURE dbo.roc_spGetProvidersAndManufacturesByFulltext

go

/* 
	Author:			Daniel Campos. / Elizabeth Lazo.				 
	Object:			dbo.roc_spGetProvidersAndManufacturesByFulltext
	
	Description:	Retrieves all manufactures by full text.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetProvidersAndManufacturesByFulltext  @parentId=14, @page=0 , @numberbypage=10, @text ='vinos' 


 */

CREATE PROCEDURE dbo.roc_spGetProvidersAndManufacturesByFulltext
(
  @parentId     int,
  @page           int,
  @numberbypage   int,
  @text          varchar(250)
)


AS
BEGIN

   --The parametres shouldn't be null:
   IF(@parentId IS NULL OR @page IS NULL OR @numberbypage IS NULL OR 
      @text IS NULL)
   BEGIN
	
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
	
   END


  SELECT (
  SELECT count(*) from 
  (
  SELECT*FROM Sections WHERE ParentId = @parentId  AND Sections.Active = '1' AND FREETEXT (SectionName, @text)

  ) as contador
) AS TOTAL,*   
FROM (    
  SELECT
  ROW_NUMBER() OVER (ORDER BY SectionName ASC) AS RowNumber,*
  FROM Sections WHERE ParentId = @parentId  AND Sections.Active = '1' AND FREETEXT (SectionName, @text)

  )AS FABRICANTES_PROVEEDORES
WHERE RowNumber BETWEEN @numberbypage * @page  + 1 AND @numberbypage * (@page  + 1)


END

 go
