IF OBJECT_ID('dbo.roc_spGetSeeds') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetSeeds
go

/* 
	Author:			Daniel Campos / Elizabeth Lazo.				 
	Object:			dbo.roc_spGetSeeds
	
	Description:	Retrieves all information the seeds.
	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetSeeds @numberByPage=10, @page=0


 */ 

CREATE PROCEDURE dbo.roc_spGetSeeds
(   
  @numberByPage    int,
  @page            int
) 
AS
  BEGIN
  --The parameters shouldn't be null:
 
       IF (@numberByPage IS NULL OR @page  IS NULL ) 
             
  BEGIN

    RAISERROR ('The current operation requires more parameters', 16, 1)
    RETURN -1

  END
			
		SELECT (
	       SELECT count(*) from 
			  (
			 SELECT *
			 FROM Seeds
			 WHERE Active='1' 
			 ) as contador
			) AS TOTAL,*   
		 
	   FROM (
		 SELECT *,
		 ROW_NUMBER() OVER (ORDER BY SeedName ASC) AS RowNumber
		 FROM Seeds
		 WHERE Active='1'  
		  )AS INDICE

	WHERE RowNumber BETWEEN @numberByPage * @page + 1 AND @numberByPage * (@page + 1)

	END

go


