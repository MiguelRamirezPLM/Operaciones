IF OBJECT_ID('dbo.roc_spGetProductsByEquipment') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetProductsByEquipment
go

/* 
	Author:			Daniel Campos / Ramiro Sánchez				 
	Object:			dbo.roc_spGetProductsByEquipment
	
	Description:	Retrieves all Products by Equipment.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetProductsByEquipment @editionId = 1, @equipmentId = 1, @countryId = 10, @page = 0, @numberByPage = 10


 */ 

CREATE PROCEDURE dbo.roc_spGetProductsByEquipment
(
	@editionId				int,
	@equipmentId			int,
	@countryId				int,
	@page					int,
	@numberByPage			int
)
AS
BEGIN

	-- The parameters shouldn't be null:
	IF (@page < 0 OR @numberByPage < 0 OR @editionId < 1 OR @equipmentId < 1 OR @countryId < 1)
	BEGIN
	
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
		
	END

		SELECT (
			SELECT count(*) From 
			(
				SELECT Products.*,Divisions.DivisionId,Divisions.DivisionName,Divisions.ShortName
					FROM Products
					INNER JOIN ProductEquipment ON Products.ProductId = ProductEquipment.ProductId
					INNER JOIN EditionDivisionProducts ON EditionDivisionProducts.ProductId = ProductEquipment.ProductId
					INNER JOIN Divisions ON Divisions.DivisionId = EditionDivisionProducts.DivisionId 
					WHERE ProductEquipment.EquipmentId = @equipmentId AND Products.Active = 1 
						AND Products.CountryId = @countryId AND EditionDivisionProducts.EditionId = @editionId 
			) As contador
		) AS TOTAL,*   

		FROM (
			SELECT Products.*,Divisions.DivisionId,Divisions.DivisionName,Divisions.ShortName,
				ROW_NUMBER() OVER (ORDER BY Products.ProductName ASC) AS RowNumber 
				FROM Products
				INNER JOIN ProductEquipment ON Products.ProductId = ProductEquipment.ProductId
				INNER JOIN EditionDivisionProducts ON EditionDivisionProducts.ProductId = ProductEquipment.ProductId
				INNER JOIN Divisions ON Divisions.DivisionId = EditionDivisionProducts.DivisionId 
				WHERE ProductEquipment.EquipmentId = @equipmentId AND Products.Active = 1 
					AND Products.CountryId = @countryId AND EditionDivisionProducts.EditionId = @editionId
		)AS INDICE
		
		WHERE RowNumber BETWEEN @numberByPage * @page + 1 AND @numberByPage * (@page + 1)
	
END
go