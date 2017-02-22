IF OBJECT_ID('dbo.plm_spCRUDEditionProductShots') IS NOT NULL
	DROP PROCEDURE dbo.plm_spCRUDEditionProductShots
go

/* 
	Author:			Ramiro Sánchez				 
	Object:			dbo.plm_spCRUDEditionProductShots
	
	Description:	Perform the basic operations, or in other words CRUD operations.
					Where CRUD stands for:

						C - Create	{0}
						R - Read	{1}
						U - Update	{2}
						D - Delete	{3}

	Company:		PLM Latina

 */ 

CREATE PROCEDURE dbo.plm_spCRUDEditionProductShots
(
	@CRUDType				tinyint,
	@editionProductShotId	int,
	@editionId				int = Null,
	@divisionId				int = Null,
	@categoryId				int = Null,
	@productId				int = Null,
	@pharmaFormId			int = Null,
	@psTypeId				tinyint = Null,
	@productShot			varchar(100) = Null,
	@qtyCells				tinyint = Null,
	@active					bit = Null
)
AS
BEGIN

	DECLARE @error int

	-- If @CRUDType belongs to {2}:
	IF (@CRUDType IN (2))
	BEGIN	
		RAISERROR ('The current operation cannot be performed', 16, 1)
		RETURN -1
	END

	-- If @CRUDType belongs to {0}, then the parameters shouldn't be null:
	IF (@CRUDType IN (0) AND((	@editionId IS NULL OR 
								@divisionId IS NULL OR 
								@categoryId IS NULL OR
								@productId IS NULL OR
								@pharmaFormId IS NULL OR
								@psTypeId IS NULL OR 
								@active IS NULL)))
	BEGIN
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
	END

	-- C = {(Create, 0)}:
	IF (@CRUDType IN (0))
	BEGIN
	
		INSERT INTO [dbo].[EditionProductShots]
           ([EditionId]
           ,[DivisionId]
           ,[CategoryId]
           ,[PharmaFormId]
           ,[ProductId]
           ,[PSTypeId]
           ,[ProductShot]
           ,[QtyCells]
           ,[Active])
		VALUES
           (@editionId
           ,@divisionId
           ,@categoryId
           ,@pharmaFormId
           ,@productId
           ,@psTypeId
           ,@productShot
           ,@qtyCells
           ,@active)

		RETURN SCOPE_IDENTITY()
	END

	-- R = {(Read, 1)}:
	IF (@CRUDType IN (1))
	BEGIN
	
		SELECT Distinct	[EditionProductShotId]
				,[EditionId]
				,[DivisionId]
				,[CategoryId]
				,[PharmaFormId]
				,[ProductId]
				,[PSTypeId]
				,[ProductShot]
				,[QtyCells]
				,[Active]
		  FROM [dbo].[EditionProductShots]
		WHERE [EditionProductShotId] = @editionProductShotId

		RETURN @@ROWCOUNT
	END

	-- D = {(Delete, 3)}:
	IF (@CRUDType IN (3))
	BEGIN

		SET @error = 0

		BEGIN TRAN

		-- Delete all the entries in the dbo.FamilyProductShots table:
		DELETE dbo.FamilyProductShots
		WHERE	EditionProductShotId = @editionProductShotId

		-- Get the value of the @@ERROR global variable:
		SET @error = @@ERROR

		IF (@error != 0)
		BEGIN
			ROLLBACK TRAN 
			RAISERROR ('An error ocurred during deleting the Family associated with the ProductShot', 16, 1)
			RETURN -1
		END

		-- Delete the ProductShot:
		DELETE FROM [dbo].[EditionProductShots]
			WHERE [EditionProductShotId] = @editionProductShotId

		-- Get the value of the @@ERROR global variable:
		SET @error = @@ERROR

		IF (@error != 0)
		BEGIN
			ROLLBACK TRAN 
			RAISERROR ('An error ocurred during deleting the ProductShot', 16, 1)
			RETURN -1
		END

		-- There are no errors, so commit the transaction:
		COMMIT TRAN
		RETURN 0
	END
END
go