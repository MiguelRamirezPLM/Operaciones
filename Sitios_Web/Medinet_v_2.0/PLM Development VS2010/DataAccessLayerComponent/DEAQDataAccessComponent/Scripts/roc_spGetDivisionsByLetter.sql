IF OBJECT_ID('dbo.roc_spGetDivisionsByLetter') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetDivisionsByLetter
go

/* 
	Author:			Daniel Campos / Elizabeth Lazo.				 
	Object:			dbo.roc_spGetDivisionsByLetter
	
	Description:	Retrieves all divisions by letter.
	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetDivisionsByLetter @numberByPage=10, @page=0, @editionId=2 , @letter='P'


 */ 

CREATE PROCEDURE dbo.roc_spGetDivisionsByLetter
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
			 SELECT DISTINCT D.DivisionId, D.DivisionName,D.ShortName
			 FROM Divisions AS D
			 INNER JOIN EditionDivisionProducts AS EDP ON EDP.DivisionId = D.DivisionId
			 WHERE D.Active='1' AND ParentId IS NULL AND EDP.EditionId=@editionId AND D.DivisionNAme LIKE  @letter + '%'
			 ) as contador
			) AS TOTAL,*   
		 
		FROM (
			 SELECT D.DivisionId, D.DivisionName,D.ShortName,
			 ROW_NUMBER() OVER (ORDER BY D.DivisionName ASC) AS RowNumber
			 FROM Divisions AS D
			 INNER JOIN DivisionInformation AS DI ON D.DivisionId=DI.DivisionId
			 INNER JOIN EditionDivisionProducts AS EDP ON EDP.DivisionId = D.DivisionId
			 WHERE D.Active='1' AND ParentId IS NULL AND EDP.EditionId=@editionId AND D.DivisionNAme LIKE @letter + '%'

		 GROUP BY D.DivisionId, D.DivisionName,D.ShortName
		  )AS INDICE
		WHERE RowNumber BETWEEN @numberByPage * @page + 1 AND @numberByPage * (@page + 1)



	END

go


