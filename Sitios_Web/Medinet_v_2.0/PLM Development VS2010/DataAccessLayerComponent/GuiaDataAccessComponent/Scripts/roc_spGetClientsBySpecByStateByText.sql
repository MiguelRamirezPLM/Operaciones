IF OBJECT_ID('dbo.roc_spGetClientsBySpecByStateByText') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetClientsBySpecByStateByText
go

/* 
	Author:			Benjamín Castillo Arriaga.				 
	Object:			dbo.roc_spGetClientsBySpecByState
	
	Description:	Retrieves all clients by Speciality by State and text.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetClientsBySpecByStateByText @editionId = 5,@specialityId = 11,@stateId = 2,@numberByPage = 15,@page = 0,@text = 'medica'


 */ 

CREATE PROCEDURE dbo.roc_spGetClientsBySpecByStateByText
(
	@editionId				int,
	@specialityId			int,
	@stateId				int,
	@numberByPage			int,
	@page					int,
	@text					varchar(255)
)
AS
BEGIN

	-- The parameters shouldn't be null:
	IF (@editionId IS NULL OR @specialityId IS NULL OR @numberByPage IS NULL OR @page IS NULL OR @stateId IS NULL)
	BEGIN
	
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
		
	END

	SELECT 
		(SELECT COUNT(*) 
		 FROM Clients 
			INNER JOIN ClientSpecialities ON ClientSpecialities.ClientId = Clients.ClientId 
		 WHERE ClientSpecialities.SpecialityId = @specialityId AND 
			   Clients.EditionId = @editionId AND 
			   Clients.Active = 1 AND 
			   Clients.StateId = @stateId AND 
			  (Clients.CompanyName LIKE '%' + @text + '%' COLLATE SQL_LATIN1_GENERAL_CP1_CI_AI OR 
			   Clients.ShortName LIKE '%' + @text + '%' COLLATE SQL_LATIN1_GENERAL_CP1_CI_AI OR 
			   Clients.Address LIKE '%' + @text + '%' COLLATE SQL_LATIN1_GENERAL_CP1_CI_AI  OR 
			   Clients.Suburb LIKE '%' + @text + '%' COLLATE SQL_LATIN1_GENERAL_CP1_CI_AI OR 
			   Clients.City LIKE '%' + @text + '%' COLLATE SQL_LATIN1_GENERAL_CP1_CI_AI)) AS TOTAL,Clientes.* 
	
	FROM 
		(SELECT Clients.*,
				ROW_NUMBER() OVER (ORDER BY Clients.StateId,Clients.CountryId, Clients.City ASC) AS RowNumber 
		 FROM Clients 
			INNER JOIN ClientSpecialities ON ClientSpecialities.ClientId = Clients.ClientId 
		 WHERE ClientSpecialities.SpecialityId = @specialityId AND 
			   Clients.EditionId = @editionId AND 
			   Clients.Active = 1 AND 
			   Clients.StateId = @stateId AND 
			  (Clients.CompanyName LIKE '%' + @text + '%' COLLATE SQL_LATIN1_GENERAL_CP1_CI_AI OR 
			   Clients.ShortName LIKE '%' + @text + '%' COLLATE SQL_LATIN1_GENERAL_CP1_CI_AI OR 
			   Clients.Address LIKE '%' + @text + '%' COLLATE SQL_LATIN1_GENERAL_CP1_CI_AI  OR 
			   Clients.Suburb LIKE '%' + @text + '%' COLLATE SQL_LATIN1_GENERAL_CP1_CI_AI OR 
			   Clients.City LIKE '%' + @text + '%' COLLATE SQL_LATIN1_GENERAL_CP1_CI_AI)) AS Clientes 
	
	WHERE RowNumber BETWEEN @numberByPage * @page + 1 AND @numberByPage * (@page + 1)

	
END
go