IF OBJECT_ID('dbo.plm_spCRUDFamilyProducts') IS NOT NULL
	DROP PROCEDURE dbo.plm_spCRUDFamilyProducts
go

/* 
	Author:			Ramiro Sánchez				 
	Object:			dbo.plm_spCRUDFamilyProducts
	
	Description:	Perform the basic operations, or in other words CRUD operations.
					Where CRUD stands for:

						C - Create	{0}
						R - Read	{1}
						U - Update	{2}
						D - Delete	{3}

	Company:		PLM Latina

 */ 

CREATE PROCEDURE dbo.plm_spCRUDFamilyProducts
(
	@CRUDType				tinyint,
	@editionId				int,
	@familyId				int,
	@divisionId				int,
	@categoryId				int,
	@pharmaFormId			int,
	@productId				int
)
AS
BEGIN

	-- If @CRUDType belongs to {1,2}:
	IF (@CRUDType IN (1,2))
	BEGIN	
		RAISERROR ('The current operation cannot be performed', 16, 1)
		RETURN -1
	END

	-- C = {(Create, 0)}
	IF (@CRUDType IN (0))
	BEGIN
	
		INSERT INTO [dbo].[FamilyProducts]
           ([EditionId]
           ,[FamilyId]
           ,[DivisionId]
           ,[CategoryId]
           ,[PharmaFormId]
           ,[ProductId])
		VALUES
           (@editionId
           ,@familyId
           ,@divisionId
           ,@categoryId
           ,@pharmaFormId
           ,@productId)

		RETURN 0
		
	END

	-- D = {(Delete, 3)}
	IF (@CRUDType IN (3))
	BEGIN
	
		DELETE FROM [dbo].[FamilyProducts]
			WHERE [EditionId] = @editionId
				AND [FamilyId] = @familyId
				AND [DivisionId] = @divisionId
				AND [CategoryId] = @categoryId
				AND [PharmaFormId] = @pharmaFormId
				AND [ProductId] = @productId

		RETURN 0
	END

END
go