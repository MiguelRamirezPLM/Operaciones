IF OBJECT_ID('dbo.plm_spCRUDProductCategories') IS NOT NULL
	DROP PROCEDURE dbo.plm_spCRUDProductCategories
go

/* 
	Author:			Juan Ramírez / Ramiro Sánchez				 
	Object:			dbo.plm_spCRUDProductCategories
	
	Description:	Perform the basic operations, or in other words CRUD operations.
					Where CRUD stands for:

						C - Create	{0}
						R - Read	{1}
						U - Update	{2}
						D - Delete	{3}

	Company:		PLM Latina

 */ 

CREATE PROCEDURE dbo.plm_spCRUDProductCategories
(
	@CRUDType				tinyint,
	@divisionId				int,
	@categoryId				int,
	@pharmaFormId			int,
	@productId				int,
	@technicalSheet			bit = null,
	@productDescription	    varchar(max) = null
)
AS
BEGIN

	DECLARE @error int

	-- C = {(Create, 0)}
	IF (@CRUDType IN (0))
	BEGIN
	
		INSERT INTO [dbo].[ProductCategories]
           ([DivisionId]
           ,[CategoryId]
           ,[PharmaFormId]
           ,[ProductId]
           ,[TechnicalSheet]
		   ,[ProductDescription])
		VALUES
           (@divisionId
           ,@categoryId
           ,@pharmaFormId
           ,@productId
           ,@technicalSheet
		   ,@productDescription)

		RETURN 0

	END
		
	-- R = {(Read, 1)}
	IF (@CRUDType IN (1))
	BEGIN

		SELECT [DivisionId]
			   ,[CategoryId]
			   ,[PharmaFormId]
			   ,[ProductId]
			   ,[TechnicalSheet]
		FROM ProductCategories
		WHERE [DivisionId] = @divisionId AND
			  [CategoryId] = @categoryId AND
			  [PharmaFormId] = @pharmaFormId AND
			  [ProductId] = @productId

		RETURN @@ROWCOUNT	
	
	END

	-- U = {(Update, 2)}
	IF (@CRUDType IN(2))
	BEGIN
	
		UPDATE ProductCategories
		SET [ProductDescription] = @productDescription
		WHERE [DivisionId] = @divisionId AND
			  [CategoryId] = @categoryId AND
			  [PharmaFormId] = @pharmaFormId AND
			  [ProductId] = @productId
		
		RETURN 0
	
	END

	-- D = {(Delete, 3)}
	IF (@CRUDType IN (3))
	BEGIN
		
		SET @error = 0

		BEGIN TRAN

		-- Delete all the entries in the dbo.ParticipantProducs table:
		DELETE dbo.ParticipantProducts
		WHERE [DivisionId] = @divisionId AND
			  [CategoryId] = @categoryId AND
			  [PharmaFormId] = @pharmaFormId AND
			  [ProductId] = @productId

		-- Get the value of the @@ERROR global variable:
		SET @error = @@ERROR

		IF (@error != 0)
		BEGIN
			
			ROLLBACK TRAN 
			RAISERROR ('An error ocurred during deleting the edition associated with the product', 16, 1)
			RETURN -1

		END

		-- Delete all the entries in the dbo.MentionatedProducts table:
		DELETE dbo.MentionatedProducts
		WHERE [DivisionId] = @divisionId AND
			  [CategoryId] = @categoryId AND
			  [PharmaFormId] = @pharmaFormId AND
			  [ProductId] = @productId

		-- Get the value of the @@ERROR global variable:
		SET @error = @@ERROR

		IF (@error != 0)
		BEGIN
			
			ROLLBACK TRAN 
			RAISERROR ('An error ocurred during deleting the edition associated with the product', 16, 1)
			RETURN -1

		END

		-- Delete all the entries in the dbo.NewProducts table:
		DELETE dbo.NewProducts
		WHERE [DivisionId] = @divisionId AND
			  [CategoryId] = @categoryId AND
			  [PharmaFormId] = @pharmaFormId AND
			  [ProductId] = @productId

		-- Get the value of the @@ERROR global variable:
		SET @error = @@ERROR

		IF (@error != 0)
		BEGIN
			
			ROLLBACK TRAN 
			RAISERROR ('An error ocurred during deleting the edition associated with the product', 16, 1)
			RETURN -1

		END

		-- Delete all the entries in the dbo.EditionDivisionProducts table:
		DELETE dbo.EditionDivisionProducts
		WHERE [DivisionId] = @divisionId AND
			  [CategoryId] = @categoryId AND
			  [PharmaFormId] = @pharmaFormId AND
			  [ProductId] = @productId

		-- Get the value of the @@ERROR global variable:
		SET @error = @@ERROR

		IF (@error != 0)
		BEGIN
			
			ROLLBACK TRAN 
			RAISERROR ('An error ocurred during deleting the edition associated with the product', 16, 1)
			RETURN -1

		END

		-- Delete all the entries in the dbo.ProductCategories table:
		DELETE FROM [dbo].[ProductCategories]
		WHERE [DivisionId] = @divisionId AND
			  [CategoryId] = @categoryId AND
			  [PharmaFormId] = @pharmaFormId AND
			  [ProductId] = @productId
		
		-- Get the value of the @@ERROR global variable:
		SET @error = @@ERROR

		IF (@error != 0)
		BEGIN
			
			ROLLBACK TRAN 
			RAISERROR ('An error ocurred during deleting the product', 16, 1)
			RETURN -1

		END

		-- There are no errors, so commit the transaction:
		COMMIT TRAN
		RETURN 0
	

	END
END
go