USE [Guia]
GO
/****** Object:  StoredProcedure [dbo].[roc_spGetClientsByFullText]    Script Date: 06/03/2013 09:31:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*
Modified:		Angel Eduardo Hernández Aguilar

EXECUTE roc_spGetClientsByFullText
	 @editionId = 9
	,@clientTypeId = 2
	,@text = REMSA
	,@numberByPage = 95
	,@page = 0

*/
CREATE PROCEDURE [dbo].[roc_spGetClientsByFullText]
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

    --set @text =  '"'  + @text + '"'   
    set @text =  '%'  + @text + '%'   

	
	SELECT 
	(
		SELECT COUNT(Clients.ClientId)FROM Clients
		INNER JOIN States ON States.StateId=Clients.StateId 
		WHERE (Clients.CompanyName LIKE @text OR
			   Clients.ShortName LIKE @text OR
			   Clients.Address LIKE @text OR
			   Clients.Suburb LIKE @text OR
			   Clients.City LIKE @text OR
			   States.StateName LIKE @text)AND
			   Clients.ClientTypeId =  @clientTypeId  AND 
			   Clients.EditionID =  @editionId  AND 
			   Clients.Active = 1
		) AS TOTAL, Clients.* 
		
	FROM 
		(
		SELECT  Clients.*,StateName,ROW_NUMBER() OVER (ORDER BY Clients.CompanyName) AS RowNumber 
		FROM Clients 
			INNER JOIN States ON States.StateId=Clients.StateId 
		WHERE (Clients.CompanyName LIKE @text OR
			   Clients.ShortName LIKE @text OR
			   Clients.Address LIKE @text OR
			   Clients.Suburb LIKE @text OR
			   Clients.City LIKE @text OR
			   States.StateName LIKE @text)AND
			   Clients.ClientTypeId = @clientTypeId AND 
			   Clients.EditionID = @editionId AND 
			   Clients.Active = 1
		)
	AS Clients 
	WHERE RowNumber BETWEEN @numberByPage * @page + 1 AND @numberByPage * (@page + 1)	
	Order By CompanyName,City,StateName

END