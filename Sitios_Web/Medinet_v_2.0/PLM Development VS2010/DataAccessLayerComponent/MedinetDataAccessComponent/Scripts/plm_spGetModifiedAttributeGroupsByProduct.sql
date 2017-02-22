IF OBJECT_ID('dbo.plm_spGetModifiedAttributeGroupsByProduct') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetModifiedAttributeGroupsByProduct
go

/* 
	Author:			Ramiro Sánchez
	Object:			dbo.plm_spGetModifiedAttributeGroupsByProduct
	
	Description:	Gets all Attribute Groups
	Company:		PLM Latina.

	EXECUTE dbo.plm_spGetModifiedAttributeGroupsByProduct @bookId = 1, @countryId = 11, @editionId = 80, @divisionId = 3, @categoryId = 107,
		@pharmaFormId = 4, @productId = 11006

	EXECUTE dbo.plm_spGetModifiedAttributeGroupsByProduct @editionId = 80, @divisionId = 3, @categoryId = 107,
		@pharmaFormId = 4, @productId = 11006


 */ 

CREATE PROCEDURE dbo.plm_spGetModifiedAttributeGroupsByProduct
(
	@bookId			int = Null,
	@countryId		int = Null,
	@editionId		int = Null,
	@divisionId		int = Null,
	@categoryId		int = Null,
	@pharmaFormId	int = Null,
	@productId		int = Null
)
AS
BEGIN

	IF(	@bookId IS NOT NULL
		AND @countryId IS NOT NULL
		AND @editionId IS NOT NULL
		AND @divisionId IS NOT NULL
		AND @categoryId IS NOT NULL
		AND @pharmaFormId IS NOT NULL
		AND @productId IS NOT NULL)

	BEGIN

		Select Distinct 
				v.AttributeGroupId, 
				v.AttributeGroupName,
				CASE WHEN mag.EditionId Is Not Null Then 1 Else 0 End as ModifiedAttributeGroup

		From (Select Distinct a.AttributeGroupId, a.AttributeGroupName
				From AttributeGroup a
				Inner Join EditionAttributeGroup eag On a.AttributeGroupId = eag.AttributeGroupId
				Inner Join Editions e On eag.EditionId = e.EditionId
				Where e.CountryId = @countryId
					And e.BookId = @bookId
					And a.Active = 1) v

		Left Join (Select EditionId, DivisionId, CategoryId, ProductId, PharmaFormId, AttributeGroupId
					From ModifiedAttributeGroups
					Where	EditionId = @editionId
						And DivisionId = @divisionId
						And CategoryId = @categoryId
						And PharmaFormId = @pharmaFormId
						And ProductId = @productId) mag
			On	v.AttributeGroupId = mag.AttributeGroupId
		Order By 2

		RETURN @@ROWCOUNT
	END

	IF(	@bookId IS NULL
		AND @countryId IS NULL
		AND @editionId IS NOT NULL
		AND @divisionId IS NOT NULL
		AND @categoryId IS NOT NULL
		AND @pharmaFormId IS NOT NULL
		AND @productId IS NOT NULL)

	BEGIN

		Select	EditionId,
				DivisionId,
				CategoryId,
				PharmaFormId,
				ProductId,
				AttributeGroupId
		From ModifiedAttributeGroups
		Where	EditionId = @editionId
				And DivisionId = @divisionId
				And CategoryId = @categoryId
				And PharmaFormId = @pharmaFormId
				And ProductId = @productId

		RETURN @@ROWCOUNT
	
	END
END
go