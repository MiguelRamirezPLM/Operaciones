IF OBJECT_ID('dbo.roc_spGetSeedsByLetter') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetSeedsByLetter
go

/* 
	Author:			Daniel Campos / Elizabeth Lazo.				 
	Object:			dbo.roc_spGetSeedsByLetter
	
	Description:	Retrieves all information the seeds by letter.
	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetSeedsByLetter @numberByPage=10, @page=0, @letter= 's'


 */ 

CREATE PROCEDURE dbo.roc_spGetSeedsByLetter
(   
  @numberByPage    int,
  @page            int,
  @letter          varchar(1)
) 
AS
  BEGIN
  --The parameters shouldn't be null:
 
       IF (@numberByPage IS NULL OR @page  IS NULL OR @letter IS NULL ) 
             
  BEGIN

    RAISERROR ('The current operation requires more parameters', 16, 1)
    RETURN -1

  END
				
		SELECT (
		  SELECT count(*) from 
		  (
		 SELECT *
		 FROM Seeds
		 WHERE Active='1' AND SeedName LIKE @letter + '%'
		 ) as contador
		) AS TOTAL,*   
		 
		FROM (
		 SELECT *,
		 ROW_NUMBER() OVER (ORDER BY SeedName ASC) AS RowNumber
		 FROM Seeds
		 WHERE Active='1' AND SeedName LIKE @letter + '%' 
		  )AS INDICE

		WHERE RowNumber BETWEEN @numberByPage * @page + 1 AND @numberByPage * (@page + 1)


	END

go


