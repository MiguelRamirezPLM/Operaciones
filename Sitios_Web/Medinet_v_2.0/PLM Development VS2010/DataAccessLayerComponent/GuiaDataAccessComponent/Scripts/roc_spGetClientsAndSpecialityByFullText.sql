USE [Guia]
GO
/****** Object:  StoredProcedure [dbo].[roc_spGetClientsAndSpecialityByFullText]    Script Date: 06/03/2013 09:31:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*
Modified:		Angel Eduardo Hernández Aguilar

*/

CREATE PROCEDURE [dbo].[roc_spGetClientsAndSpecialityByFullText]
(
	@editionId			int,
	@numberByPage		int,
	@page				int,
	@fullText			varchar(40)
)
AS
BEGIN

      

	-- The parameters shouldn't be null:
	IF (@editionId IS NULL OR @numberByPage IS NULL OR @page IS NULL OR @fullText IS NULL)
	BEGIN
	
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
		
	END

     set @fullText =  '%'  + @fullText + '%' 

		SELECT (
			SELECT COUNT(Specialities.Description)FROM Specialities
				INNER JOIN ClientSpecialities  ON ClientSpecialities.SpecialityId=Specialities.SpecialityId 
				INNER JOIN Clients ON Clients.ClientId=ClientSpecialities.ClientId
				INNER JOIN States ON States.StateId=Clients.StateId  
				WHERE Specialities.Description LIKE @fullText
				OR Clients.CompanyName LIKE @fullText
				OR States.StateName LIKE @fullText AND Clients.EditionID = @editionId

				AND Clients.Active = 1 AND Specialities.Active = 1
		) AS TOTAL,* 
		FROM 
		(
			SELECT  Clients.ClientId,Clients.CompanyName, Clients.ClientTypeId,ClientSpecialities.SpecialityId,
				Specialities.Description as SpecialityDescription, ROW_NUMBER() OVER (ORDER BY Specialities.SpecialityId) AS RowNumber 
				FROM Specialities 
				INNER JOIN ClientSpecialities  ON ClientSpecialities.SpecialityId=Specialities.SpecialityId 
				INNER JOIN Clients ON Clients.ClientId=ClientSpecialities.ClientId 
				INNER JOIN States ON States.StateId=Clients.StateId 
				WHERE Specialities.Description LIKE @fullText
				OR Clients.CompanyName LIKE @fullText
				OR States.StateName LIKE @fullText AND Clients.EditionID = @editionId
				AND Clients.Active = 1  AND Specialities.Active = 1
		)AS Clients 
		WHERE RowNumber BETWEEN @numberByPage * @page + 1 AND @numberByPage * (@page + 1)
		Order By CompanyName,SpecialityDescription
END
