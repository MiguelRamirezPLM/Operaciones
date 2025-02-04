USE [Guia]
GO
/****** Object:  StoredProcedure [dbo].[roc_spGetClientsBySpecByStateByFullText]    Script Date: 06/03/2013 09:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/* 
	Author:			Benjamín Castillo Arriaga.				 
	Object:			dbo.roc_spGetClientsBySpecByStateByFullText
	
	Modified:		Angel Eduardo Hernández Aguilar
	
	Description:	Retrieves all clients by Speciality by State and text.

	Company:		PLM Latina.

	EXECUTE dbo.[roc_spGetClientsBySpecByStateByFullText] 
	@editionId = 9,
	@specialityId = 24,
	@stateId = 9,
	@numberByPage = 10,
	@page = 0,
	@text = medi


 */ 

CREATE PROCEDURE [dbo].[roc_spGetClientsBySpecByStateByFullText]
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

	set @text =  '%'  + @text + '%' 

	SELECT 
		(
		SELECT COUNT(*) 
		FROM Clients 
			INNER JOIN ClientSpecialities ON ClientSpecialities.ClientId = Clients.ClientId 
		WHERE ClientSpecialities.SpecialityId = @specialityId AND 
			  Clients.EditionId = @editionId AND 
			  Clients.Active = 1 AND 
			  Clients.StateId = @stateId AND 
			 (Clients.CompanyName LIKE @text OR
			   Clients.ShortName LIKE @text OR
			   Clients.Address LIKE @text OR
			   Clients.Suburb LIKE @text OR
			   Clients.City LIKE @text)
		) 
		AS TOTAL,Clientes.*
		
	FROM 
		(
		SELECT Clients.*,ROW_NUMBER() OVER (ORDER BY Clients.StateId,Clients.CountryId, Clients.City ASC) AS RowNumber 
		FROM Clients 
			INNER JOIN ClientSpecialities ON ClientSpecialities.ClientId = Clients.ClientId 
		WHERE ClientSpecialities.SpecialityId = @specialityId AND 
			  Clients.EditionId = @editionId AND 
			  Clients.Active = 1 AND 
			  Clients.StateId = @stateId AND 
			  (Clients.CompanyName LIKE @text OR
			   Clients.ShortName LIKE @text OR
			   Clients.Address LIKE @text OR
			   Clients.Suburb LIKE @text OR
			   Clients.City LIKE @text)
		)
		 AS Clientes 
	WHERE RowNumber BETWEEN @numberByPage * @page + 1 AND @numberByPage * (@page + 1)
	Order By CompanyName
	
END
