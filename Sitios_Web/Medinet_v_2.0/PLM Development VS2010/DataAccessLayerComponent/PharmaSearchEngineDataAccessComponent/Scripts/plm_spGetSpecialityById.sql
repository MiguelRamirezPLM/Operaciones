IF OBJECT_ID('dbo.plm_spGetSpecialityById') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetSpecialityById
go

/* 
	Author:			Ulises Orihuela		 
	Object:			dbo.dbo.plm_spGetSpecialityById
				
	Company:		PLM Latina.

	EXECUTE dbo.plm_spGetSpecialityById	32
	
 */ 

CREATE PROCEDURE dbo.plm_spGetSpecialityById
(
    @specialityid   INT
)
AS
BEGIN
	
	IF ( @specialityid is null )
	BEGIN
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
	END
	
	SELECT SpecialityName, ShortName
	FROM Specialities
	WHERE SpecialityId = @specialityid
	
END
