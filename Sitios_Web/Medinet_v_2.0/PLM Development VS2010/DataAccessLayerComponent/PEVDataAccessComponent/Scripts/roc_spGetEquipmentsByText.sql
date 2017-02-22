IF OBJECT_ID('dbo.roc_spGetEquipmentsByText') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetEquipmentsByText
go

/* 
	Author:			Daniel Campos / Ramiro Sánchez				 
	Object:			dbo.roc_spGetEquipmentsByText
	
	Description:	Retrieves all Equipments by Text.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetEquipmentsByText @page = 0, @numberByPage = 10, @text = 'inc'


 */ 

CREATE PROCEDURE dbo.roc_spGetEquipmentsByText
(
	@page					int,
	@numberByPage			int,
	@text					varchar(15)
)
AS
BEGIN

	-- The parameters shouldn't be null:
	IF (@page < 0 OR @numberByPage < 0 OR @text IS NULL)
	BEGIN
	
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
		
	END

		SELECT (
			SELECT count(*) from 
			(
				SELECT Equipment.EquipmentId, Equipment.EquipmentName FROM Equipment
					WHERE Active = 1 AND Equipment.EquipmentName LIKE '%'+@text+'%' COLLATE SQL_LATIN1_GENERAL_CP1_CI_AI
			) as contador
		) AS TOTAL,*   

		FROM (
			SELECT Equipment.EquipmentId, Equipment.EquipmentName, Equipment.Active,
				ROW_NUMBER() OVER (ORDER BY Equipment.EquipmentName ASC) AS RowNumber FROM Equipment
				WHERE Active = 1 AND Equipment.EquipmentName LIKE '%'+@text+'%' COLLATE SQL_LATIN1_GENERAL_CP1_CI_AI
		)AS INDICE

		WHERE RowNumber BETWEEN @numberByPage * @page + 1 AND @numberByPage * (@page + 1)
	
END
go