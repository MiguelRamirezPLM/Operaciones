IF OBJECT_ID('dbo.roc_spGetSpecie') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetSpecie
go

/* 
	Author:			Daniel Campos / Ramiro Sánchez				 
	Object:			dbo.roc_spGetSpecie
	
	Description:	Retrieves all Species by SpecieId.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetSpecie @specieId = 1


 */ 

CREATE PROCEDURE dbo.roc_spGetSpecie
(
	@specieId				int
)

AS
BEGIN

	-- The parameters shouldn't be null:
	IF (@specieId IS NULL)
	BEGIN
	
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
		
	END

	SELECT Species.SpecieId,Species.SpecieName, Species.Active FROM Species
		WHERE Species.SpecieId = @specieId AND Species.Active = 1
	
END
go