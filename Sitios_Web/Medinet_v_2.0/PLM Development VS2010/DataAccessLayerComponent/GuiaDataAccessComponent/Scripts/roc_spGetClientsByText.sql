IF OBJECT_ID('dbo.roc_spGetClientsByText') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetClientsByText
go

/* 
	Author:			Benjamín Castillo Arriaga.				 
	Object:			dbo.roc_spGetClientsByText
	
	Description:	Retrieves all clients by Type By Text.

	Company:		PLM Latina.

	

	EXECUTE dbo.roc_spGetClientsByText @editionId = 5,@clientTypeId = 2,@text = 'danamedica',@numberByPage = 95,@page = 0

	

 */ 

CREATE PROCEDURE dbo.roc_spGetClientsByText
(
	@editionId				int,
	@clientTypeId			tinyint,
	@text					varchar(255),
	@numberByPage			int,
	@page					int
)
AS
BEGIN

	-- The parameters shouldn't be null:
	IF (@editionId IS NULL OR @text IS NULL OR @clientTypeId IS NULL OR @numberByPage IS NULL OR @page IS NULL)
	BEGIN
	
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
		
	END


	SELECT 
		(SELECT COUNT(Clients.ClientId)
		 FROM Clients 
			INNER JOIN States ON States.StateId = Clients.StateId 
		 WHERE (Clients.CompanyName LIKE '%' + @text + '%' COLLATE SQL_LATIN1_GENERAL_CP1_CI_AI OR 
				Clients.ShortName LIKE '%' + @text + '%' COLLATE SQL_LATIN1_GENERAL_CP1_CI_AI OR 
				Clients.Address LIKE '%' + @text + '%' COLLATE SQL_LATIN1_GENERAL_CP1_CI_AI OR 
				Clients.Suburb LIKE '%' + @text + '%' COLLATE SQL_LATIN1_GENERAL_CP1_CI_AI OR 
				Clients.City LIKE '%' + @text + '%' COLLATE SQL_LATIN1_GENERAL_CP1_CI_AI OR 
				States.StateName LIKE '%' + @text + '%' COLLATE SQL_LATIN1_GENERAL_CP1_CI_AI) AND 
				Clients.ClientTypeId = @clientTypeId AND 
				Clients.EditionID = @editionId AND 
				Clients.Active = 1 ) AS TOTAL,* 
	
	FROM 
		(SELECT Clients.*,
				StateName,
				ROW_NUMBER() OVER (ORDER BY Clients.CompanyName) AS RowNumber 
		 FROM Clients INNER JOIN States ON States.StateId=Clients.StateId 
		 WHERE (Clients.CompanyName LIKE '%' + @text + '%' COLLATE SQL_LATIN1_GENERAL_CP1_CI_AI OR 
				Clients.ShortName LIKE '%' + @text + '%' COLLATE SQL_LATIN1_GENERAL_CP1_CI_AI OR 
				Clients.Address LIKE '%' + @text + '%' COLLATE SQL_LATIN1_GENERAL_CP1_CI_AI OR 
				Clients.Suburb LIKE '%' + @text + '%' COLLATE SQL_LATIN1_GENERAL_CP1_CI_AI OR 
				Clients.City LIKE '%' + @text + '%' COLLATE SQL_LATIN1_GENERAL_CP1_CI_AI OR 
				States.StateName LIKE '%' + @text + '%' COLLATE SQL_LATIN1_GENERAL_CP1_CI_AI) AND 
				Clients.ClientTypeId = @clientTypeId AND 
				Clients.EditionID = @editionId AND 
				Clients.Active = 1)AS Clients 
		 
	WHERE RowNumber BETWEEN @numberByPage * @page + 1 AND @numberByPage * (@page + 1)

END
go