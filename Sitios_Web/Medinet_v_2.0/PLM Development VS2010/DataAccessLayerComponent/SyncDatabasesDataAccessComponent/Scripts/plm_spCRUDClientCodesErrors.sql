IF OBJECT_ID('dbo.plm_spCRUDClientCodesErrors') IS NOT NULL
	DROP PROCEDURE dbo.plm_spCRUDClientCodesErrors
go

/* 
	Author:			Juan Ramírez.				 
	Object:			dbo.plm_spCRUDClientCodesErrors
	
	Description:	Perform the basic operations, or in other words CRUD operations.
					Where CRUD stands for:

						C - Create	{0}
						R - Read	{1}
						U - Update	{2}
						D - Delete	{3}

	Company:		PLM Latina.

	DECLARE
		@CRUDType				tinyint,
		@clientCodeErrorId			int

	SELECT 
		@CRUDType			= 1,
		@clientCodeErrorId			= 1

	EXECUTE dbo.plm_spCRUDClientCodesErrors
		@CRUDType,			
		@clientCodeErrorId

 */ 

CREATE PROCEDURE dbo.plm_spCRUDClientCodesErrors
(
	@CRUDType					tinyint,
	@clientCodeErrorId				int,
	@codeString				varchar(50),
	@packSqlTextId					int,
	@errorMessage				   text,
	@errorDate				   Datetime
)
AS
BEGIN

	-- If @CRUDType belongs to {2,3,4}:
	IF (@CRUDType IN (2,3,4))
	BEGIN
	
		RAISERROR ('The current operation cannot be performed', 16, 1)
		RETURN -1
		
	END


	-- If @CRUDType belongs to {0}, then the parameters shouldn't be null:
	IF (@CRUDType IN (0) AND ( (@codeString IS NULL OR
								@packSqlTextId IS NULL OR 
								@errorMessage IS NULL OR 
								@errorDate	IS NULL) ) )
	BEGIN
	
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
		
	END
	

	-- C = {(Create, 0)}
	IF (@CRUDType IN (0))
	BEGIN
			
		INSERT INTO [dbo].[ClientCodesErrors]
           ([CodeString]
           ,[ErrorDate]
           ,[PackSqlTextId]
           ,[ErrorMessage]
           ,[Confirmed])
		VALUES
           (@codeString
           ,@errorDate
           ,@packSqlTextId
           ,@errorMessage
           ,1)

		RETURN SCOPE_IDENTITY()


	END
	
END
go