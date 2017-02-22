IF OBJECT_ID('dbo.plm_spCRUDOtherInteractions') IS NOT NULL
	DROP PROCEDURE dbo.plm_spCRUDOtherInteractions
go

/* 
	Author:			Ramiro Sánchez				 
	Object:			dbo.plm_spCRUDOtherInteractions
	
	Description:	Perform the basic operations, or in other words CRUD operations.
					Where CRUD stands for:

						C - Create	{0}
						R - Read	{1}
						U - Update	{2}
						D - Delete	{3}

	Company:		PLM Latina

 */ 

CREATE PROCEDURE dbo.plm_spCRUDOtherInteractions
(
	@CRUDType				tinyint,
	@productContentId		int,
	@productId				int,
	@activeSubstanceId		int,
	@elementId				int
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

		INSERT INTO [dbo].[OtherInteractions]
           ([ProductContentId]
           ,[ProductId]
           ,[ActiveSubstanceId]
           ,[ElementId])
		VALUES
           (@productContentId
           ,@productId
           ,@activeSubstanceId
		   ,@elementId)

		RETURN 0

	END

	-- D = {(Delete, 3)}
	IF (@CRUDType IN (3))
	BEGIN

		DELETE FROM [dbo].[OtherInteractions]
			WHERE [ProductContentId] = @productContentId
				AND [ProductId] = @productId
				AND [ActiveSubstanceId] = @activeSubstanceId
				AND [ElementId] = @elementId

	END

END
go