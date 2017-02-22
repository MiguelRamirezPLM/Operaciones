IF OBJECT_ID('dbo.roc_spGetTherapeuticUse') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetTherapeuticUse
go

/* 
	Author:			Daniel Campos / Ramiro Sánchez				 
	Object:			dbo.roc_spGetTherapeuticUse
	
	Description:	Retrieves an Therapeutic Use by TherapeuticId.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetTherapeuticUse @therapeuticId = 107


 */ 

CREATE PROCEDURE dbo.roc_spGetTherapeuticUse
(
	@therapeuticId			int
)

AS
BEGIN

	-- The parameters shouldn't be null:
	IF (@therapeuticId IS NULL)
	BEGIN
	
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
		
	END

	SELECT DISTINCT(TherapeuticUses.TherapeuticId),TherapeuticName,Active,ParentId,Llave,CountryId,IDUso,ID,Nivel FROM TherapeuticUses 
		WHERE TherapeuticUses.Active = 1 AND TherapeuticUses.TherapeuticId =  @therapeuticId
	
END
go