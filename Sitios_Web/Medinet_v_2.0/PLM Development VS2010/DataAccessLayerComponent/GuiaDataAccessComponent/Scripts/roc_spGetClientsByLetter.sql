IF OBJECT_ID('dbo.roc_spGetClientsByLetter') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetClientsByLetter
go

/* 
	Author:			Benjamín Castillo Arriaga.				 
	Object:			dbo.roc_spGetClientsByLetter
	
	Description:	Retrieves all clients by Type By Letter.

	Company:		PLM Latina.

	

	EXECUTE dbo.roc_spGetClientsByLetter @editionId = 5,@clientTypeId = 2,@letter = 'a',@numberByPage = 5,@page = 0

	

 */ 

CREATE PROCEDURE dbo.roc_spGetClientsByLetter
(
	@editionId				int,
	@clientTypeId			tinyint,
	@letter					varchar(1),
	@numberByPage			int,
	@page					int
)
AS
BEGIN

	-- The parameters shouldn't be null:
	IF (@editionId IS NULL OR @letter IS NULL OR @clientTypeId IS NULL OR @numberByPage IS NULL OR @page IS NULL)
	BEGIN
	
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
		
	END

	SELECT 
		(SELECT COUNT(ClientId)
		 FROM Clients 
		 WHERE CompanyName	LIKE @letter + '%'	AND 
			   ClientTypeId	=	@clientTypeId	AND 
			   EditionID	=	@editionId		AND 
			   Active		=	1) AS TOTAL,* 
	
	FROM 
		(SELECT *,ROW_NUMBER() OVER (ORDER BY CompanyName) AS RowNumber 
		 FROM Clients 
		 WHERE CompanyName	LIKE @letter + '%'	AND 
			   ClientTypeId	=	 @clientTypeId	AND 
			   EditionID	=	 @editionId		AND 
			   Active		=	 1)AS Clients 
	
	WHERE RowNumber BETWEEN @numberByPage * @page + 1 AND @numberByPage * (@page + 1)
	


END
go