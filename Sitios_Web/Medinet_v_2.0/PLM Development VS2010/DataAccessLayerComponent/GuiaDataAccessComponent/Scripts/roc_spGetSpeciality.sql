IF OBJECT_ID('dbo.roc_spGetSpeciality') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetSpeciality
go

/* 
	Author:			Daniel Campos.				 
	Object:			dbo.roc_spGetSpeciality
	
	Description:	Retrieves a speciality by Id.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetSpeciality @specialityId = 11


 */ 

CREATE PROCEDURE dbo.roc_spGetSpeciality
(
	@specialityId			int
)
AS
BEGIN

	-- The parameters shouldn't be null:
	IF (@specialityId IS NULL)
	BEGIN
	
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
		
	END

	SELECT Specialities.* 
	FROM Specialities  
	WHERE Specialities.SpecialityId = @specialityId
	
END
go