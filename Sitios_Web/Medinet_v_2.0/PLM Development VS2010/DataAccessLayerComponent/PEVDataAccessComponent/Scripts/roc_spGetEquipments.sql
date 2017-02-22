IF OBJECT_ID('dbo.roc_spGetEquipments') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetEquipments
go

/* 
	Author:			Daniel Campos / Ramiro Sánchez				 
	Object:			dbo.roc_spGetEquipments
	
	Description:	Retrieves all Equipments.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetEquipments @page = 0, @numberByPage = 10


 */ 

CREATE PROCEDURE dbo.roc_spGetEquipments
(
	@page					int,
	@numberByPage			int
)
AS
BEGIN

	-- The parameters shouldn't be null:
	IF (@page < 0 OR @numberByPage < 0)
	BEGIN
	
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
		
	END

		SELECT (
			SELECT count(*) From 
			(
				SELECT Equipment.* FROM Equipment
					WHERE Active = 1
			) As contador
		) AS TOTAL,*   
		FROM (
			SELECT Equipment.*,  
				ROW_NUMBER() OVER (ORDER BY Equipment.EquipmentName ASC) AS RowNumber FROM Equipment
				WHERE Active = 1
		)AS INDICE
		WHERE RowNumber BETWEEN @numberByPage * @page + 1 AND @numberByPage * (@page + 1)
	
END
go