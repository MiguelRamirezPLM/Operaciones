IF OBJECT_ID('dbo.roc_spGetProductsByCrop') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetProductsByCrop
go

/* 
	Author:			Daniel Campos / Ramiro Sánchez				 
	Object:			dbo.roc_spGetProductsByCrop
	
	Description:	Retrieves all Products by Edition and Crop.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetProductsByCrop @editionId = 2, @cropId = 46

 */ 

CREATE PROCEDURE dbo.roc_spGetProductsByCrop
(
	@editionId			int,
	@cropId				int
)
AS
BEGIN

	-- The parameters shouldn't be null:
	IF (@editionId IS NULL OR @cropId IS NULL)
	
	BEGIN
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
	END

	SELECT P.ProductId, P.ProductName, P.Description, P.Register, P.LaboratoryId,
		L.LaboratoryName, D.DivisionName, EDP.CategoryId
		FROM Products AS P 
		INNER JOIN ProductCrops AS PC ON P.ProductId=PC.ProductId
		INNER JOIN Crops AS C ON PC.CropId=C.CropId
		INNER JOIN EditionDivisionProducts AS EDP ON EDP.ProductId = P.ProductId
		INNER JOIN Divisions AS D ON D.DivisionId = EDP.DivisionId
		INNER JOIN Laboratories AS L ON L.LaboratoryId = P.LaboratoryId
		WHERE P.Active = 1 
			AND C.Active = 1 
			AND PC.CropId = @cropId 
			AND EDP.EditionId = @editionId  
		ORDER BY P.ProductName ASC
END
go