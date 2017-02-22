IF OBJECT_ID('dbo.roc_spGetProvidersAndManufacturesByLetter') IS NOT NULL
	DROP PROCEDURE roc_spGetProvidersAndManufacturesByLetter
go

/* 
	Author:			Daniel Campos. / Elizabeth Lazo.				 
	Object:			dbo.roc_spGetProvidersAndManufacturesByLetter
	
	Description:	Retrieves all manufacures by letter.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetProvidersAndManufacturesByLetter @parentId = 14, @page=0, @numberbypage=10, @letter='c'


 */ 

CREATE PROCEDURE dbo.roc_spGetProvidersAndManufacturesByLetter
(
  @parentId   int,
  @page       int,
  @numberbypage int,
  @letter     varchar(1)
)

AS
  BEGIN
  --The parameters shouldn't be null:
       IF (@parentId IS NULL OR @page IS NULL OR
           @numberbypage IS NULL OR @letter IS NULL) 
  BEGIN

    RAISERROR ('The current operation requires more parameters', 16, 1)
    RETURN -1

  END

  SELECT (
  SELECT count(*) from 
  (
  SELECT*FROM Sections WHERE ParentId = @parentId AND Sections.Active = '1' AND SectionName LIKE @letter  + '%'

  ) as contador
) AS TOTAL,*   
FROM (    
  SELECT
  ROW_NUMBER() OVER (ORDER BY SectionName ASC) AS RowNumber,*
  FROM Sections WHERE ParentId = @parentId AND Sections.Active = '1' AND SectionName LIKE @letter + '%'

  )AS FABRICANTES_PROVEEDORES
WHERE RowNumber BETWEEN @numberbypage * @page + 1 AND @numberbypage * (@page + 1)


END 

 go
