IF OBJECT_ID('dbo.roc_spGetProvidersAndManufacturesByText') IS NOT NULL
     DROP PROCEDURE dbo.roc_spGetProvidersAndManufacturesByText

go

/* 
	Author:			Daniel Campos. / Elizabeth Lazo.				 
	Object:			dbo.roc_spGetProvidersAndManufacturesByText
	
	Description:	Retrieves all manufactures by text.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetProvidersAndManufacturesByText  @parentId=14, @page=0 , @numberbypage=10,  @text ='vin' 


 */

CREATE PROCEDURE dbo.roc_spGetProvidersAndManufacturesByText 
(
  @parentId      int,
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
  SELECT*FROM Sections WHERE ParentId = @parentId  AND Sections.Active = '1' AND SectionName LIKE '%' + @text + '%' COLLATE SQL_LATIN1_GENERAL_CP1_CI_AI

  ) as contador
) AS TOTAL,*   
FROM (    
  SELECT
  ROW_NUMBER() OVER (ORDER BY SectionName ASC) AS RowNumber,*
  FROM Sections WHERE ParentId = @parentId  AND Sections.Active = '1' AND SectionName LIKE '%' + @text + '%' COLLATE SQL_LATIN1_GENERAL_CP1_CI_AI

  )AS FABRICANTES_PROVEEDORES
WHERE RowNumber BETWEEN @numberbypage * @page  + 1 AND @numberbypage * (@page  + 1)


END

 go