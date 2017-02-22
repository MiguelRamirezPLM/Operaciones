IF OBJECT_ID('dbo.roc_spGetClientsBySpecByState') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetClientsBySpecByState
go

/* 
	Author:			Benjamín Castillo Arriaga.				 
	Object:			dbo.roc_spGetClientsBySpecByState
	
	Description:	Retrieves all clients by Speciality by State.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetClientsBySpecByState @editionId = 5,@specialityId = 11,@numberByPage = 15,@page = 1,@stateId = 2


 */ 

CREATE PROCEDURE dbo.roc_spGetClientsBySpecByState
(
	@editionId				int,
	@specialityId			int,
	@numberByPage			int,
	@page					int,
	@stateId				int
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
			INNER JOIN ClientSpecialities ON(ClientSpecialities.ClientId = Clients.ClientId) 
		 WHERE ClientSpecialities.SpecialityId = @specialityId AND 
			   Clients.EditionId = @editionId AND 
			   Clients.Active = 1 AND 
			   Clients.StateId = @stateId) AS TOTAL,Clientes.* 
	FROM 
		(SELECT Clients.*,ROW_NUMBER() OVER (ORDER BY Clients.StateId,Clients.CountryId, Clients.City ASC) AS RowNumber 
		 FROM Clients 
			INNER JOIN ClientSpecialities ON(ClientSpecialities.ClientId = Clients.ClientId) 
		 WHERE ClientSpecialities.SpecialityId = @specialityId AND 
			   Clients.EditionId = @editionId AND 
			   Clients.Active = 1 AND 
			   Clients.StateId = @stateId) AS Clientes 
	WHERE RowNumber BETWEEN @numberByPage * @page + 1 AND @numberByPage * (@page + 1)

	
END
go