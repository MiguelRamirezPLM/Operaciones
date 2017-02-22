IF OBJECT_ID('dbo.plm_spCRUDProductContents') IS NOT NULL
	DROP PROCEDURE dbo.plm_spCRUDProductContents
go

/* 
	Author:			Juan Ramírez / Ramiro Sánchez				 
	Object:			dbo.plm_spCRUDProductContents
	
	Description:	Perform the basic operations, or in other words CRUD operations.
					Where CRUD stands for:

						C - Create	{0}
						R - Read	{1}
						U - Update	{2}
						D - Delete	{3}

	Company:		PLM Latina
	
	EXECUTE dbo.plm_spCRUDProductContents @CRUDType = 1, @editionId = 55, @divisionId = 171, @categoryId = 101, @productId = 9343,
		@pharmaFormId = 210, @attributeId = 3449
	EXECUTE dbo.plm_spCRUDProductContents @CRUDType = 1, @editionId = 55, @divisionId = 171, @categoryId = 101, @productId = 9343,
		@pharmaFormId = 210

 */ 

CREATE PROCEDURE dbo.plm_spCRUDProductContents
(
	@CRUDType		tinyint,
	@editionId			int,
	@divisionId			int,
	@categoryId			int,
	@productId			int,
	@pharmaFormId		int,
	@attributeId		int = Null
)
AS
BEGIN

	-- If @CRUDType belongs to {0, 2, 3}:
	IF (@CRUDType IN (0, 2, 3))
	BEGIN
		RAISERROR ('The current operation cannot be performed', 16, 1)
		RETURN -1
	END

	-- R = {(Read, 1)}:
	IF (@CRUDType IN (1))
	BEGIN

		IF(@attributeId IS NOT NULL)
		BEGIN
		
			SELECT [ProductContentId]
				  ,[ProductId]
				  ,[PharmaFormId]
				  ,[AttributeId]
				  ,[EditionId]
				  ,[DivisionId]
				  ,[CategoryId]
				  ,[Content]
				  ,[PlainContent]
				  ,[HTMLContent]
			  FROM [dbo].[ProductContents]

			WHERE	[EditionId] = @editionId AND
					[DivisionId] = @divisionId AND
					[CategoryId] = @categoryId AND
					[ProductId] = @productId AND
					[PharmaFormId] = @pharmaFormId AND
					[AttributeId] = @attributeId 

			RETURN @@ROWCOUNT
		END

		IF(@attributeId IS NULL)
		BEGIN
			SELECT [ProductContentId]
				  ,[ProductId]
				  ,[PharmaFormId]
				  ,[AttributeId]
				  ,[EditionId]
				  ,[DivisionId]
				  ,[CategoryId]
				  ,[Content]
				  ,[PlainContent]
				  ,[HTMLContent]
			FROM [dbo].[ProductContents]

			WHERE	[EditionId] = @editionId AND
					[DivisionId] = @divisionId AND
					[CategoryId] = @categoryId AND
					[ProductId] = @productId AND
					[PharmaFormId] = @pharmaFormId

			RETURN @@ROWCOUNT
		END
	END

END
go