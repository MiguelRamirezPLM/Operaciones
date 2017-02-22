IF OBJECT_ID('dbo.roc_spGetDivisionsByFullText') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetDivisionsByFullText
go

/* 
	Author:			Daniel Campos / Elizabeth Lazo.				 
	Object:			dbo.roc_spGetDivisionsByFullText
	
	Description:	Retrieves all divisions by fulltext.
	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetDivisionsByFullText @numberByPage=10, @page=0, @editionId=4, @text= 'QUIMICA'


 */ 

CREATE PROCEDURE dbo.roc_spGetDivisionsByFullText
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
				 SELECT DISTINCT D.DivisionId, D.DivisionName,D.ShortName
				 FROM Divisions AS D
				 INNER JOIN EditionDivisionProducts AS EDP ON EDP.DivisionId = D.DivisionId
				 WHERE D.Active='1' AND ParentId IS NULL AND EDP.EditionId=@editionId AND 
				 (FREETEXT(D.DivisionName,@text) OR FREETEXT(D.ShortName, @text))
				 ) as contador
				) AS TOTAL,*   
			 
			FROM (
				 SELECT D.DivisionId, D.DivisionName,D.ShortName,
				 ROW_NUMBER() OVER (ORDER BY D.DivisionName ASC) AS RowNumber
				 FROM Divisions AS D
				 INNER JOIN DivisionInformation AS DI ON D.DivisionId=DI.DivisionId
				 INNER JOIN EditionDivisionProducts AS EDP ON EDP.DivisionId = D.DivisionId
				 WHERE D.Active='1' AND ParentId IS NULL AND EDP.EditionId=@editionId AND 
				 (FREETEXT(D.DivisionName,@text) OR FREETEXT(D.ShortName,@text))
				 GROUP BY D.DivisionId, D.DivisionName,D.ShortName
				  )AS INDICE

			WHERE RowNumber BETWEEN @numberByPage * @page + 1 AND @numberByPage * (@page + 1)



	END

go


