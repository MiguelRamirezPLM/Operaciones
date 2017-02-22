IF OBJECT_ID('dbo.plm_spGetFamilyProductShots') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetFamilyProductShots
go

/* 
	Author:			Ramiro Sánchez
	Object:			dbo.plm_spGetFamilyProductShots
	
	Description:	Gets all FamilyProducts by Product
	Company:		PLM Latina.

	EXECUTE dbo.plm_spGetFamilyProductShots @editionId = 80, @divisionId = 3, @categoryId = 107, @productId = 40243, @pharmaFormId = 126

 */ 

CREATE PROCEDURE dbo.plm_spGetFamilyProductShots
(
	@editionId		int = Null,
	@divisionId		int = Null,
	@categoryId		int = Null,
	@productId		int = Null,
	@pharmaFormId	int = Null
)
AS
BEGIN

	-- The parameters shouldn't be null:
	IF (@editionId IS NULL OR 
		@divisionId IS NULL OR 
		@categoryId IS NULL OR
		@productId IS NULL OR
		@pharmaFormId IS NULL)
	BEGIN
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
	END

	Select	fps.FamilyId,
			fps.EditionProductShotId
		From FamilyProductShots fps
			Inner Join EditionProductShots eps On fps.EditionProductShotId = eps.EditionProductShotId
		Where eps.EditionId = @editionId
			And eps.DivisionId = @divisionId
			And eps.CategoryId = @categoryId
			And eps.ProductId = @productId
			And eps.PharmaFormId = @pharmaFormId
END
go