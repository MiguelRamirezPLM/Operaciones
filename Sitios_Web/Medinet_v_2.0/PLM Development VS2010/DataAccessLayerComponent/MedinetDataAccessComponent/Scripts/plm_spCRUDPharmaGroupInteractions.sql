IF OBJECT_ID('dbo.plm_spCRUDPharmaGroupInteractions') IS NOT NULL
	DROP PROCEDURE dbo.plm_spCRUDPharmaGroupInteractions
go

/* 
	Author:			Ramiro Sánchez				 
	Object:			dbo.plm_spCRUDPharmaGroupInteractions
	
	Description:	Perform the basic operations, or in other words CRUD operations.
					Where CRUD stands for:

						C - Create	{0}
						R - Read	{1}
						U - Update	{2}
						D - Delete	{3}

	Company:		PLM Latina

 */ 

CREATE PROCEDURE dbo.plm_spCRUDPharmaGroupInteractions
(
	@CRUDType				tinyint,
	@productContentId		int,
	@productId				int,
	@activeSubstanceId		int,
	@pharmaGroupId			int
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

		INSERT INTO [dbo].[PharmaGroupInteractions]
           ([ProductContentId]
           ,[ProductId]
           ,[ActiveSubstanceId]
           ,[PharmaGroupId])
		VALUES
           (@productContentId
           ,@productId
           ,@activeSubstanceId
		   ,@pharmaGroupId)

		RETURN 0

	END

	-- D = {(Delete, 3)}
	IF (@CRUDType IN (3))
	BEGIN

		DELETE FROM [dbo].[PharmaGroupInteractions]
			WHERE [ProductContentId] = @productContentId
				AND [ProductId] = @productId
				AND [ActiveSubstanceId] = @activeSubstanceId
				AND [PharmaGroupId] = @pharmaGroupId

	END

END
go