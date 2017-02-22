IF OBJECT_ID('dbo.plm_spCRUDProductContentSubstances') IS NOT NULL
	DROP PROCEDURE dbo.plm_spCRUDProductContentSubstances
go

/* 
	Author:			Ramiro Sánchez				 
	Object:			dbo.plm_spCRUDProductContentSubstances
	
	Description:	Perform the basic operations, or in other words CRUD operations.
					Where CRUD stands for:

						C - Create	{0}
						R - Read	{1}
						U - Update	{2}
						D - Delete	{3}

	Company:		PLM Latina

 */ 

CREATE PROCEDURE dbo.plm_spCRUDProductContentSubstances
(
	@CRUDType				tinyint,
	@productContentId		int,
	@productId				int,
	@activeSubstanceId		int
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
	
		INSERT INTO [dbo].[ProductContentSubstances]
           ([ProductContentId]
           ,[ProductId]
           ,[ActiveSubstanceId])
		VALUES
           (@productContentId
           ,@productId
           ,@activeSubstanceId)

		RETURN 0

	END

	-- D = {(Delete, 3)}
	IF (@CRUDType IN (3))
	BEGIN
	
		DELETE FROM [dbo].[ProductContentSubstances]
		WHERE [ProductContentId] = @productContentId
			AND [ProductId] = @productId
			AND [ActiveSubstanceId] = @activeSubstanceId

	END

END
go