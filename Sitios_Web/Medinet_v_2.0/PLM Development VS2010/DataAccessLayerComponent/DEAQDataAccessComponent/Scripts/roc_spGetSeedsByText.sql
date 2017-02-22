IF OBJECT_ID('dbo.roc_spGetSeedsByText') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetSeedsByText
go

/* 
	Author:			Daniel Campos / Elizabeth Lazo.				 
	Object:			dbo.roc_spGetSeedsByText
	
	Description:	Retrieves all information the seeds by text.
	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetSeedsByText @numberByPage=10, @page=0, @text= 'alfa'


 */ 

CREATE PROCEDURE dbo.roc_spGetSeedsByText
(   
  @numberByPage    int,
  @page            int,
  @text          varchar(250)
) 
AS
  BEGIN
  --The parameters shouldn't be null:
 
       IF (@numberByPage IS NULL OR @page  IS NULL OR @text IS NULL ) 
             
  BEGIN

    RAISERROR ('The current operation requires more parameters', 16, 1)
    RETURN -1

  END
				
		SELECT (
  SELECT count(*) from 
  (
 SELECT *
 FROM Seeds
 WHERE Active='1' AND SeedName LIKE '%' + @text + '%' COLLATE SQL_LATIN1_GENERAL_CP1_CI_AI
 ) as contador
) AS TOTAL,*   
 
FROM (
 SELECT *,
 ROW_NUMBER() OVER (ORDER BY SeedName ASC) AS RowNumber
 FROM Seeds
 WHERE Active='1' AND SeedName LIKE '%' + @text + '%' COLLATE SQL_LATIN1_GENERAL_CP1_CI_AI
  )AS INDICE

WHERE RowNumber BETWEEN @numberByPage * @page + 1 AND @numberByPage * (@page + 1)


	END

go


