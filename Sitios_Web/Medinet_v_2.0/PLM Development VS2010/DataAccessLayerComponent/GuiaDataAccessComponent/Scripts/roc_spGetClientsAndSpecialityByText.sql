IF OBJECT_ID('dbo.roc_spGetClientsAndSpecialityByText') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetClientsAndSpecialityByText
go

/* 
	Author:			Daniel Campos / Ramiro Sánchez				 
	Object:			dbo.roc_spGetClientsAndSpecialityByText
	
	Description:	Retrieves information from a Company and Speciality by Edition.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetClientsAndSpecialityByText @editionId = 6, @numberByPage = 10, @page = 0, @text = 'ambulancia' 

 */ 

CREATE PROCEDURE dbo.roc_spGetClientsAndSpecialityByText
(
	@editionId			int,
	@numberByPage		int,
	@page				int,
	@text				varchar(30)
)
AS
BEGIN

	-- The parameters shouldn't be null:
	IF (@editionId IS NULL OR @numberByPage IS NULL OR @page IS NULL OR @text IS NULL)
	BEGIN
	
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
		
	END

		SELECT(
			SELECT COUNT(Clients.ClientId)
				FROM Clients 
				INNER JOIN ClientSpecialities ON ClientSpecialities.ClientId=Clients.ClientId 
				INNER JOIN Specialities ON Specialities.SpecialityId=ClientSpecialities.SpecialityId 
				WHERE (Clients.CompanyName LIKE '%'+@text+'%' COLLATE SQL_LATIN1_GENERAL_CP1_CI_AI 
					OR Clients.ShortName LIKE '%'+@text+'%' COLLATE SQL_LATIN1_GENERAL_CP1_CI_AI 
					OR Specialities.Description LIKE '%'+@text+'%' COLLATE SQL_LATIN1_GENERAL_CP1_CI_AI) 
					AND (Clients.ClientTypeId = 1 OR Clients.ClientTypeId = 2 OR Clients.ClientTypeId = 3) 
					AND Specialities.Active = 1
					AND Clients.EditionId = @editionId AND Clients.Active = 1) 
		AS TOTAL,* 
		FROM 
			(SELECT Clients.ClientId,Clients.CompanyName, Clients.ClientTypeId,ClientSpecialities.SpecialityId,
				Specialities.Description as SpecialityDescription, ROW_NUMBER() OVER (ORDER BY Clients.CompanyName) AS RowNumber 
				FROM Clients 
				INNER JOIN ClientSpecialities ON ClientSpecialities.ClientId=Clients.ClientId 
				INNER JOIN Specialities ON Specialities.SpecialityId=ClientSpecialities.SpecialityId 
				WHERE (Clients.CompanyName LIKE '%'+@text+'%' COLLATE SQL_LATIN1_GENERAL_CP1_CI_AI 
				OR Clients.ShortName LIKE '%'+@text+'%' COLLATE SQL_LATIN1_GENERAL_CP1_CI_AI 
				OR Specialities.Description LIKE '%'+@text+'%' COLLATE SQL_LATIN1_GENERAL_CP1_CI_AI) 
				AND (Clients.ClientTypeId = 1 OR Clients.ClientTypeId = 2 OR Clients.ClientTypeId = 3)
				AND Specialities.Active = 1
				AND Clients.EditionId = @editionId AND Clients.Active = 1)
		AS Clients 
		WHERE RowNumber BETWEEN @numberByPage * @page + 1 AND @numberByPage * (@page + 1)
	
END
go