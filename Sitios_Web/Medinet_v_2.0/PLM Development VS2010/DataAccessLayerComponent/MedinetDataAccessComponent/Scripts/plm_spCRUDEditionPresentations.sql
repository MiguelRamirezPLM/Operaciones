IF OBJECT_ID('dbo.plm_spCRUDEditionPresentations') IS NOT NULL
	DROP PROCEDURE dbo.plm_spCRUDEditionPresentations
go

/* 
	Author:			Ramiro Sánchez				 
	Object:			dbo.plm_spCRUDEditionPresentations
	
	Description:	Perform the basic operations, or in other words CRUD operations.
					Where CRUD stands for:

						C - Create	{0}
						R - Read	{1}
						U - Update	{2}
						D - Delete	{3}

	Company:		PLM Latina

 */ 

CREATE PROCEDURE dbo.plm_spCRUDEditionPresentations
(
	@CRUDType			tinyint,
	@editionId			int,
	@presentationId		int
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

		INSERT INTO [dbo].[EditionPresentations]
           ([EditionId]
           ,[PresentationId])
		VALUES
           (@editionId
           ,@presentationId)

		RETURN 0

	END

	-- D = {(Delete, 3)}
	IF (@CRUDType IN (3))
	BEGIN

		DELETE FROM [dbo].[EditionPresentations]
			WHERE [EditionId] = @editionId
				AND [PresentationId] = @presentationId

	END

END
go