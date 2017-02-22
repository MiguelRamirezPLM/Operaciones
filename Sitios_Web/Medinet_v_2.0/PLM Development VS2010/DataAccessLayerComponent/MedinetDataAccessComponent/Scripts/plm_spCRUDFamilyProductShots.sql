IF OBJECT_ID('dbo.plm_spCRUDFamilyProductShots') IS NOT NULL
	DROP PROCEDURE dbo.plm_spCRUDFamilyProductShots
go

/* 
	Author:			Ramiro Sánchez				 
	Object:			dbo.plm_spCRUDFamilyProductShots
	
	Description:	Perform the basic operations, or in other words CRUD operations.
					Where CRUD stands for:

						C - Create	{0}
						R - Read	{1}
						U - Update	{2}
						D - Delete	{3}

	Company:		PLM Latina

 */ 

CREATE PROCEDURE dbo.plm_spCRUDFamilyProductShots
(
	@CRUDType				tinyint,
	@familyId				int,
	@editionProductShotId	int
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
	
		INSERT INTO [dbo].[FamilyProductShots]
           ([FamilyId]
           ,[EditionProductShotId])
		VALUES
           (@familyId
           ,@editionProductShotId)

		RETURN 0
	END

	-- D = {(Delete, 3)}
	IF (@CRUDType IN (3))
	BEGIN
	
		DELETE FROM [dbo].[FamilyProductShots]
			WHERE [FamilyId] = @familyId
				AND [EditionProductShotId] = @editionProductShotId

		RETURN 0
	END

END
go