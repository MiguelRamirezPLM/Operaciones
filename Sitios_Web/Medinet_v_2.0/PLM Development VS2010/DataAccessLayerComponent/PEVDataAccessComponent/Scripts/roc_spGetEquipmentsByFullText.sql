IF OBJECT_ID('dbo.roc_spGetEquipmentsByFullText') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetEquipmentsByFullText
go

/* 
	Author:			Daniel Campos / Ramiro Sánchez				 
	Object:			dbo.roc_spGetEquipmentsByFullText
	
	Description:	Retrieves all Equipments by FullText.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetEquipmentsByFullText @page = 0, @numberByPage = 10, @fullText = 'incubadoras pecuarias'


 */ 

CREATE PROCEDURE dbo.roc_spGetEquipmentsByFullText
(
	@page					int,
	@numberByPage			int,
	@fullText				varchar(20)
)
AS
BEGIN

	-- The parameters shouldn't be null:
	IF (@page < 0 OR @numberByPage < 0 OR @fullText IS NULL)
	BEGIN
	
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
		
	END

		SELECT (
			SELECT count(*) From 
			(
				SELECT Equipment.EquipmentId, Equipment.EquipmentName FROM Equipment
					WHERE Active = 1 AND FREETEXT(Equipment.EquipmentName ,@fullText)
			) As contador
		) AS TOTAL,*   

		FROM (
			SELECT Equipment.EquipmentId, Equipment.EquipmentName, Equipment.Active,
				ROW_NUMBER() OVER (ORDER BY Equipment.EquipmentName ASC) AS RowNumber FROM Equipment
				WHERE Active = 1 AND FREETEXT(Equipment.EquipmentName ,@fullText)
		)AS INDICE

		WHERE RowNumber BETWEEN @numberByPage * @page + 1 AND @numberByPage * (@page + 1)
	
END
go