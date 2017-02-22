IF OBJECT_ID('dbo.roc_spGetClientsByType') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetClientsByType
go

/* 
	Author:			Benjamín Castillo Arriaga.				 
	Object:			dbo.roc_spGetClientsByType
	
	Description:	Retrieves all clients by Type.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetClientsByType @editionId = 5,@clientTypeId = 2,@numberByPage = 5,@page = 0


 */ 

CREATE PROCEDURE dbo.roc_spGetClientsByType
(
	@editionId				int,
	@clientTypeId			tinyint,
	@numberByPage			int,
	@page					int
)
AS
BEGIN

	-- The parameters shouldn't be null:
	IF (@editionId IS NULL OR @clientTypeId IS NULL OR @numberByPage IS NULL OR @page IS NULL)
	BEGIN
	
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
		
	END

	SELECT 
		(SELECT COUNT(ClientId)
		 FROM Clients 
		 WHERE ClientTypeId =	@clientTypeId	AND 
			   EditionId	=	@editionId		AND 
			   Active		=	1	) AS TOTAL,* 
	FROM 
		(SELECT  *,ROW_NUMBER() OVER (ORDER BY CompanyName) AS RowNumber 
		 FROM Clients 
		 WHERE ClientTypeId	=  @clientTypeId	AND 
			   EditionId	=  @editionId		AND 
			   Active		=	1)AS Clients 
	WHERE 
		RowNumber BETWEEN @numberByPage * @page + 1 AND @numberByPage * (@page + 1)
	

	
END
go