IF OBJECT_ID('dbo.roc_spGetSpecialities') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetSpecialities
go

/* 
	Author:			Benjamín Castillo Arriaga.				 
	Object:			dbo.roc_spGetSpecialities
	
	Description:	Retrieves all specialities.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetClientsByType 


 */ 

CREATE PROCEDURE dbo.roc_spGetSpecialities
AS
BEGIN

	SELECT Specialities.SpecialityId,Specialities.Description,Specialities.Active 
	FROM Specialities 
		INNER JOIN ClientSpecialities ON(ClientSpecialities.SpecialityId = Specialities.SpecialityId) 
	WHERE Specialities.Active='1' 
	GROUP BY Specialities.SpecialityId,Specialities.Description,Specialities.Active 
	ORDER BY Specialities.Description ASC
	
	
END
go