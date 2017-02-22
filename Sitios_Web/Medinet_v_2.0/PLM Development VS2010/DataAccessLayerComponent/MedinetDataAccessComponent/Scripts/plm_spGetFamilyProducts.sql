IF OBJECT_ID('dbo.plm_spGetFamilyProducts') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetFamilyProducts
go

/* 
	Author:			Ramiro Sánchez
	Object:			dbo.plm_spGetFamilyProducts
	
	Description:	Gets all FamilyProducts by Product
	Company:		PLM Latina.

	EXECUTE dbo.plm_spGetFamilyProducts @editionId = 80, @divisionId = 3, @categoryId = 107, @productId = 40243, @pharmaFormId = 126

 */ 

CREATE PROCEDURE dbo.plm_spGetFamilyProducts
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

	Select	EditionId,
			FamilyId,
			DivisionId,
			CategoryId,
			ProductId,
			PharmaFormId
		From FamilyProducts
		Where EditionId = @editionId
			And DivisionId = @divisionId
			And CategoryId = @categoryId
			And ProductId = @productId
			And PharmaFormId = @pharmaFormId
END
go