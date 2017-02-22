IF OBJECT_ID('dbo.roc_spGetEquipmentsByLetter') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetEquipmentsByLetter
go

/* 
	Author:			Daniel Campos / Ramiro Sánchez				 
	Object:			dbo.roc_spGetEquipmentsByLetter
	
	Description:	Retrieves all Equipments by Letter.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetEquipmentsByLetter @page = 0, @numberByPage = 10, @letter = 'i'


 */ 

CREATE PROCEDURE dbo.roc_spGetEquipmentsByLetter
(
	@page					int,
	@numberByPage			int,
	@letter					varchar(10)
)
AS
BEGIN

	-- The parameters shouldn't be null:
	IF (@page < 0 OR @numberByPage < 0 OR @letter IS NULL)
	BEGIN
	
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
		
	END

		SELECT (
			SELECT count(*) From 
			(
				SELECT Equipment.EquipmentId, Equipment.EquipmentName FROM Equipment
					WHERE Active = 1 AND Equipment.EquipmentName LIKE @letter+'%'
			) As contador
		) AS TOTAL,*   

		FROM (
			SELECT Equipment.EquipmentId, Equipment.EquipmentName, Equipment.Active,
				ROW_NUMBER() OVER (ORDER BY Equipment.EquipmentName ASC) AS RowNumber FROM Equipment
				WHERE Active = 1 AND Equipment.EquipmentName LIKE @letter+'%'
		)AS INDICE

		WHERE RowNumber BETWEEN @numberByPage * @page + 1 AND @numberByPage * (@page + 1)
	
END
go