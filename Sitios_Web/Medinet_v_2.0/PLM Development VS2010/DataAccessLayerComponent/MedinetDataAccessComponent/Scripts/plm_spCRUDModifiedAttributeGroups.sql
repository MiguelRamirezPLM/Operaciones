IF OBJECT_ID('dbo.plm_spCRUDModifiedAttributeGroups') IS NOT NULL
	DROP PROCEDURE dbo.plm_spCRUDModifiedAttributeGroups
go

/* 
	Author:			Ramiro Sánchez				 
	Object:			dbo.plm_spCRUDModifiedAttributeGroups
	
	Description:	Perform the basic operations, or in other words CRUD operations.
					Where CRUD stands for:

						C - Create	{0}
						R - Read	{1}
						U - Update	{2}
						D - Delete	{3}

	Company:		PLM Latina

	EXECUTE dbo.plm_spCRUDModifiedAttributeGroups @CRUDType = 1, @attributeGroupId = 1

 */ 

CREATE PROCEDURE dbo.plm_spCRUDModifiedAttributeGroups
(
	@CRUDType			tinyint,
	@editionId			int,
	@divisionId			int,
	@categoryId			int,
	@pharmaFormId		int,
	@productId			int,
	@attributeGroupId	int
)
AS
BEGIN

	-- If @CRUDType belongs to {2}:
	IF (@CRUDType IN (2))
	BEGIN	
		RAISERROR ('The current operation cannot be performed', 16, 1)
		RETURN -1
	END

	-- C = {(Create, 0)}:
	IF (@CRUDType IN (0))
	BEGIN

		INSERT INTO [dbo].[ModifiedAttributeGroups]
           ([EditionId]
           ,[DivisionId]
           ,[CategoryId]
           ,[PharmaFormId]
           ,[ProductId]
           ,[AttributeGroupId])
		VALUES
           (@editionId
           ,@divisionId
           ,@categoryId
           ,@pharmaFormId
           ,@productId
           ,@attributeGroupId)

		RETURN 0
		
	END

	-- R = {(Read, 1)}:
	IF (@CRUDType IN (1))
	BEGIN

		SELECT	[EditionId]
				,[DivisionId]
				,[CategoryId]
				,[PharmaFormId]
				,[ProductId]
				,[AttributeGroupId]
		FROM [dbo].[ModifiedAttributeGroups]
		WHERE	EditionId = @editionId
				And DivisionId = @divisionId
				And CategoryId = @categoryId
				And PharmaFormId = @pharmaFormId
				And ProductId = @productId
				And AttributeGroupId = @attributeGroupId

		RETURN @@ROWCOUNT
		
	END

	-- D = {(Delete, 3)}:
	IF (@CRUDType IN (3))
	BEGIN

		DELETE FROM [dbo].[ModifiedAttributeGroups]
		WHERE	EditionId = @editionId
				And DivisionId = @divisionId
				And CategoryId = @categoryId
				And PharmaFormId = @pharmaFormId
				And ProductId = @productId
				And AttributeGroupId = @attributeGroupId

		RETURN 0
		
	END

END
go