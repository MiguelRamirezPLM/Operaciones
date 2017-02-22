IF OBJECT_ID('dbo.roc_spGetProvidersAndManufactures') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetProvidersAndManufactures
go

/* 
	Author:			Daniel Campos. / Elizabeth Lazo.				 
	Object:			dbo.roc_spGetProvidersAndManufactures
	
	Description:	Retrieves all providers and manufacures.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetProvidersAndManufactures @parentId = 14, @page=0, @numberbypage=10


 */ 

CREATE PROCEDURE dbo.roc_spGetProvidersAndManufactures
(
  @parentId   int,
  @page       int,
  @numberbypage int
)

AS
  BEGIN
  --The parameters shouldn't be null:
       IF (@parentId IS NULL OR @page IS NULL OR @numberbypage IS NULL) 
  BEGIN

    RAISERROR ('The current operation requires more parameters', 16, 1)
    RETURN -1

  END


  SELECT (
  SELECT count(*) from 
  (
  SELECT*FROM Sections WHERE ParentId = @parentId AND Sections.Active = '1'

  ) as contador
) AS TOTAL,*   
FROM (    
  SELECT
  ROW_NUMBER() OVER (ORDER BY SectionName ASC) AS RowNumber,*
  FROM Sections WHERE ParentId = @parentId AND Sections.Active = '1'

  )AS FABRICANTES_PROVEEDORES
WHERE RowNumber BETWEEN @numberbypage * @page + 1 AND @numberbypage * (@Page + 1)

 
END
 
go