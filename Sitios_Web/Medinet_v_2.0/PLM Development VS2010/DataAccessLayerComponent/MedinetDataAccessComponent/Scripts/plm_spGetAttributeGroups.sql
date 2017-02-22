IF OBJECT_ID('dbo.plm_spGetAttributeGroups') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetAttributeGroups
go

/* 
	Author:			Ramiro Sánchez
	Object:			dbo.plm_spGetAttributeGroups
	
	Description:	Gets all Attribute Groups
	Company:		PLM Latina.

	EXECUTE dbo.plm_spGetAttributeGroups
	EXECUTE dbo.plm_spGetAttributeGroups @editionId=80, @divisionId=3, @categoryId=103, @productId=11015, @pharmaFormId=216

 */ 

CREATE PROCEDURE dbo.plm_spGetAttributeGroups
(
	@editionId		int = Null,
	@divisionId		int = Null,
	@categoryId		int = Null,
	@productId		int = Null,
	@pharmaFormId	int = Null
)
AS
BEGIN

	IF(	@editionId Is Null AND
		@divisionId Is Null AND
		@categoryId Is Null AND
		@productId Is Null AND
		@pharmaFormId Is Null)
	BEGIN

		Select Distinct	AttributeGroupId,
				AttributeGroupName,
				AttributeGroupOrder,
				Active
			From AttributeGroup
			Where Active = 1
		Order By 2

		RETURN @@ROWCOUNT

	END

	IF(	@editionId Is Not Null And
		@divisionId Is Not Null And
		@categoryId Is Not Null And
		@productId Is Not Null And
		@pharmaFormId Is Not Null)
	BEGIN

		Select Distinct ag.AttributeGroupId,
				ag.AttributeGroupName,
				ag.AttributeGroupOrder,
				ag.Active
		From AttributeGroup ag
			Inner Join ModifiedAttributeGroups mag On ag.AttributeGroupId = mag.AttributeGroupId
		Where ag.Active = 1
			And mag.EditionId = @editionId
			And mag.DivisionId = @divisionId
			And mag.CategoryId = @categoryId
			And mag.ProductId = @productId
			And mag.PharmaFormId = @pharmaFormId
		Order By 2

		RETURN @@ROWCOUNT

	END

END
go