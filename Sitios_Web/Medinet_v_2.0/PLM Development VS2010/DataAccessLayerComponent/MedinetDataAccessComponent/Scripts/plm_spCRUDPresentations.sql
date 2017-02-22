IF OBJECT_ID('dbo.plm_spCRUDPresentations') IS NOT NULL
	DROP PROCEDURE dbo.plm_spCRUDPresentations
go

/* 
	Author:			Ramiro Sánchez				 
	Object:			dbo.plm_spCRUDPresentations
	
	Description:	Perform the basic operations, or in other words CRUD operations.
					Where CRUD stands for:

						C - Create	{0}
						R - Read	{1}
						U - Update	{2}
						D - Delete	{3}

	Company:		PLM Latina

 */ 

CREATE PROCEDURE dbo.plm_spCRUDPresentations
(
	@CRUDType				tinyint,
	@presentationId			int,
	@divisionId				int = Null,
	@categoryId				int = Null,
	@productId				int = Null,
	@pharmaFormId			int = Null,
	@qtyExternalPack		int = Null,
	@externalPackId			int = Null,
	@qtyInternalPack		int = Null,
	@internalPackId			int = Null,
	@qtyContentUnit			varchar(20) = Null,
	@contentUnitId			int = Null,
	@qtyWeightUnit			varchar(20) = Null,
	@weightUnitId			int = Null,
	@presentation			varchar(255) = Null,
	@active					bit = Null
)
AS
BEGIN

	-- If @CRUDType belongs to {3}:
	IF (@CRUDType IN (3))
	BEGIN	
		RAISERROR ('The current operation cannot be performed', 16, 1)
		RETURN -1
	END

	-- C = {(Create, 0)}
	IF (@CRUDType IN (0))
	BEGIN
	
		INSERT INTO [dbo].[Presentations]
           ([DivisionId]
           ,[CategoryId]
           ,[ProductId]
           ,[PharmaFormId]
           ,[QtyExternalPack]
           ,[ExternalPackId]
           ,[QtyInternalPack]
           ,[InternalPackId]
           ,[QtyContentUnit]
           ,[ContentUnitId]
           ,[QtyWeightUnit]
           ,[WeightUnitId]
           ,[Presentation]
           ,[Active])
     VALUES
           (@divisionId
           ,@categoryId
           ,@productId
           ,@pharmaFormId
           ,@qtyExternalPack
           ,@externalPackId
           ,@qtyInternalPack
           ,@internalPackId
           ,@qtyContentUnit
           ,@contentUnitId
           ,@qtyWeightUnit
           ,@weightUnitId
           ,@presentation
           ,@active)
			
		RETURN SCOPE_IDENTITY()

	END

	-- R = {(Read, 1)}
	IF (@CRUDType IN (1))
	BEGIN
	
		SELECT	[PresentationId]
				,[DivisionId]
				,[CategoryId]
				,[ProductId]
				,[PharmaFormId]
				,[QtyExternalPack]
				,[ExternalPackId]
				,[QtyInternalPack]
				,[InternalPackId]
				,[QtyContentUnit]
				,[ContentUnitId]
				,[QtyWeightUnit]
				,[WeightUnitId]
				,[Presentation]
				,[Active]
		FROM [dbo].[Presentations]
		WHERE [PresentationId] = @presentationId
			
		RETURN @@ROWCOUNT

	END

	-- U = {(Update, 2)}
	IF (@CRUDType IN (2))
	BEGIN

		UPDATE [dbo].[Presentations]
		SET	[DivisionId] = @divisionId
			,[CategoryId] = @categoryId
			,[ProductId] = @productId
			,[PharmaFormId] = @pharmaFormId
			,[QtyExternalPack] = @qtyExternalPack
			,[ExternalPackId] = @externalPackId
			,[QtyInternalPack] = @qtyInternalPack
			,[InternalPackId] = @internalPackId
			,[QtyContentUnit] = @qtyContentUnit
			,[ContentUnitId] = @contentUnitId
			,[QtyWeightUnit] = @qtyWeightUnit
			,[WeightUnitId] = @weightUnitId
			,[Presentation] = @presentation
			,[Active] = @active
		WHERE [PresentationId] = @presentationId
			
		RETURN 0

	END

END
go