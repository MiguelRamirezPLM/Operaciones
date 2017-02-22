IF OBJECT_ID('dbo.roc_spGetEquipment') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetEquipment
go

/* 
	Author:			Daniel Campos / Ramiro Sánchez				 
	Object:			dbo.roc_spGetEquipment
	
	Description:	Retrieves one Equipment by EquipmentId.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetEquipment @equipmentId = 1


 */ 

CREATE PROCEDURE dbo.roc_spGetEquipment
(
	@equipmentId			int
)
AS
BEGIN

	-- The parameters shouldn't be null:
	IF (@equipmentId < 1)
	BEGIN
	
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
		
	END

		SELECT Equipment.* FROM Equipment
			WHERE EquipmentId = @equipmentId AND Active = 1
	
END
go