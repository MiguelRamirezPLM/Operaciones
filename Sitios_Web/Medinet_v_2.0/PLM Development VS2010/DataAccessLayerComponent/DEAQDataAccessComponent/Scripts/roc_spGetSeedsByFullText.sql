IF OBJECT_ID('dbo.roc_spGetSeedsByFullText') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetSeedsByFullText
go

/* 
	Author:			Daniel Campos / Elizabeth Lazo.				 
	Object:			dbo.roc_spGetSeedsByFullText
	
	Description:	Retrieves all information the seeds.
	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetSeedsByFullText @numberByPage=10, @page=0, @text= 'sorgos forrajeros'


 */ 

CREATE PROCEDURE dbo.roc_spGetSeedsByFullText
(   
  @numberByPage    int,
  @page            int,
  @text            varchar(250)
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
		 WHERE Active='1' AND FREETEXT(SeedName,@text)
		 ) as contador
		) AS TOTAL,*   
		 
		FROM (
		 SELECT *,
		 ROW_NUMBER() OVER (ORDER BY SeedName ASC) AS RowNumber
		 FROM Seeds
		 WHERE Active='1' AND FREETEXT(SeedName,@text)
		  )AS INDICE

		WHERE RowNumber BETWEEN @numberByPage * @page + 1 AND @numberByPage * (@page + 1)


	END

go


