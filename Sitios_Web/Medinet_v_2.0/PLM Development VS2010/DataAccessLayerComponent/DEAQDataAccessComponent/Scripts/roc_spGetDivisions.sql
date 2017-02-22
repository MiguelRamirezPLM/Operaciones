IF OBJECT_ID('dbo.roc_spGetDivisions') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetDivisions
go

/* 
	Author:			Daniel Campos / Elizabeth Lazo.				 
	Object:			dbo.roc_spGetDivisions
	
	Description:	Retrieves all divisions by editionId.
	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetDivisions @numberByPage=10, @page=0 , @editionId=2


 */ 

CREATE PROCEDURE dbo.roc_spGetDivisions
(   
  
  @numberByPage     int,   
  @page             int,
  @editionId        int
) 
AS
  BEGIN
  --The parameters shouldn't be null:
       IF (@numberByPage IS NULL OR @page IS NULL OR @editionId IS NULL ) 
             
  BEGIN

    RAISERROR ('The current operation requires more parameters', 16, 1)
    RETURN -1

  END
			
			
		SELECT (
		  SELECT count(*) from 
		  (
		 SELECT DISTINCT D.DivisionId, D.DivisionName, D.ShortName
		 FROM Divisions AS D
		 INNER JOIN EditionDivisionProducts AS EDP ON EDP.DivisionId = D.DivisionId
		 WHERE D.Active='1' AND ParentId IS NULL AND EDP.EditionId=@editionId 
		 ) as contador
		) AS TOTAL,*   
	 
	FROM (
		 SELECT D.DivisionId, D.DivisionName, D.ShortName,
		 ROW_NUMBER() OVER (ORDER BY D.DivisionName ASC) AS RowNumber
		 FROM Divisions AS D
		 INNER JOIN DivisionInformation AS DI ON D.DivisionId=DI.DivisionId
		 INNER JOIN EditionDivisionProducts AS EDP ON EDP.DivisionId = D.DivisionId
		 WHERE D.Active='1' AND ParentId IS NULL AND EDP.EditionId=@editionId
		 GROUP BY D.DivisionId, D.DivisionName,D.ShortName
		  )AS INDICE

	WHERE RowNumber BETWEEN @numberByPage* @page  + 1 AND @numberByPage * (@page  + 1)



	END

go


