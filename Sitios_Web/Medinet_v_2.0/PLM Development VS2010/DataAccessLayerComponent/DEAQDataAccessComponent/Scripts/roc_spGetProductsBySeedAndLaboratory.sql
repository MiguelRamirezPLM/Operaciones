IF OBJECT_ID('dbo.roc_spGetProductsBySeedAndLaboratory') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetProductsBySeedAndLaboratory
go

/* 
	Author:			Daniel Campos / Ramiro Sánchez				 
	Object:			dbo.roc_spGetProductsBySeedAndLaboratory
	
	Description:	Retrieves all Products by Edition, Seed and Laboratory.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetProductsBySeedAndLaboratory @editionId = 4, @seedId = 3, @laboratoryId = 18

 */ 

CREATE PROCEDURE dbo.roc_spGetProductsBySeedAndLaboratory
(
	@editionId			int,
	@seedId				int,
	@laboratoryId		int
)
AS
BEGIN

	-- The parameters shouldn't be null:
	IF (@editionId IS NULL OR  @seedId IS NULL OR @laboratoryId IS NULL)
	
	BEGIN
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
	END

	SELECT P.ProductId, P.ProductName, P.Description, P.Register, P.LaboratoryId, EDP.EditionId
		FROM Products AS P 
		INNER JOIN ProductSeeds AS PS ON P.ProductId=PS.ProductId
		INNER JOIN Seeds AS S ON PS.SeedId=S.SeedId
		INNER JOIN EditionDivisionProducts AS EDP ON EDP.ProductId = P.ProductId
		WHERE P.Active = 1 
			AND S.Active = 1 
			AND PS.SeedId = @seedId 
			AND P.LaboratoryId = @laboratoryId
			AND EDP.EditionId = @editionId
		ORDER BY P.ProductName ASC

END
go