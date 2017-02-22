IF OBJECT_ID('dbo.roc_spGetInternationalByFullText') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetInternationalByFullText
go

/* 
	Author:			Daniel Campos. / Elizabeth Lazo.
				 
	Object:			dbo.roc_spGetInternationalByFullText
	
	Description:	Retrieves all internacional clients by edition and by full text.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetInternationalByFullText  @editionId = 6, @numberByPage = 10, @page=0, @text = 'AMERICAN'


 */ 

CREATE PROCEDURE dbo.roc_spGetInternationalByFullText
(
  @editionId              int,  
  @numberByPage           int,
  @page                   int,
  @text                   varchar(250)
  
)
AS
  BEGIN
  --The parameters shouldn't be null:
       IF (@editionId IS NULL OR @numberByPage  IS NULL
           OR @page IS NULL OR @text IS NULL) 
  BEGIN

    RAISERROR ('The current operation requires more parameters', 16, 1)
    RETURN -1

  END


		SELECT (
	  SELECT count(*) from 
	  (
	 SELECT IC.IntClientId,IC.CompanyName FROM IntClients AS IC
	 INNER JOIN EditionIntClients AS EIC ON IC.IntClientId=EIC.IntClientId
	 WHERE EIC.EditionId=@editionId  AND IC.Active='1' AND FREETEXT(IC.CompanyName,@text)
	 ) as contador
	) AS TOTAL,*   
	 
	FROM (
	 SELECT IC.IntClientId,IC.CompanyName, 
	 ROW_NUMBER() OVER (ORDER BY IC.CompanyName ASC) AS RowNumber
	 FROM IntClients AS IC
	 INNER JOIN EditionIntClients AS EIC ON IC.IntClientId=EIC.IntClientId
	 WHERE EIC.EditionId=@editionId  AND IC.Active='1' AND FREETEXT(IC.CompanyName,@text)
	)AS INDICE
	WHERE RowNumber BETWEEN @numberByPage * @page + 1 AND @numberByPage * (@page + 1)



 
END
go