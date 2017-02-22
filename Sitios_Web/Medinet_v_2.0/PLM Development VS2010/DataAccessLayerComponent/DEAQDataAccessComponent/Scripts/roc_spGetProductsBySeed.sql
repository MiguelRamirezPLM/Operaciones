IF OBJECT_ID('dbo.roc_spGetProductsBySeed') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetProductsBySeed
go

/* 
	Author:			Daniel Campos / Ramiro Sánchez				 
	Object:			dbo.roc_spGetProductsBySeed
	
	Description:	Retrieves all Products by Country and Seed.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetProductsBySeed @countryId = 11, @seedId = 3

 */ 

CREATE PROCEDURE dbo.roc_spGetProductsBySeed
(
	@countryId			int,
	@seedId				int
)
AS
BEGIN

	-- The parameters shouldn't be null:
	IF (@countryId IS NULL OR  @seedId IS NULL)
	
	BEGIN
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
	END

	SELECT P.ProductId, P.ProductName, P.Description, P.Register, P.LaboratoryId
		FROM Products AS P 
		INNER JOIN ProductSeeds AS PS ON P.ProductId=PS.ProductId
		INNER JOIN Seeds AS S ON PS.SeedId=S.SeedId
		WHERE P.CountryId = @countryId 
			AND P.Active = 1
			AND S.Active = 1 
			AND PS.SeedId = @seedId   
		ORDER BY P.ProductName ASC

END
go