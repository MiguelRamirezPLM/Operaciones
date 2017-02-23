IF OBJECT_ID('dbo.plm_spCRUDClientCodesUpdated') IS NOT NULL
	DROP PROCEDURE dbo.plm_spCRUDClientCodesUpdated
go

/* 
	Author:			Juan Ramírez.				 
	Object:			dbo.plm_spCRUDClientCodesUpdated
	
	Description:	Perform the basic operations, or in other words CRUD operations.
					Where CRUD stands for:

						C - Create	{0}
						R - Read	{1}
						U - Update	{2}
						D - Delete	{3}

	Company:		PLM Latina.

	DECLARE
		@CRUDType				tinyint,
		@clientCodeUpdId			int

	SELECT 
		@CRUDType			= 1,
		@divisionId			= 1

	EXECUTE dbo.plm_spCRUDClientCodesUpdated
		@CRUDType,			
		@clientCodeUpdId

 */ 

CREATE PROCEDURE dbo.plm_spCRUDClientCodesUpdated
(
	@CRUDType					tinyint,
	@clientCodeUpdId				int,
	@codeString				varchar(50),
	@packSqlTextId					int,
	@sentDate				   Datetime,
	@updateDate				   Datetime
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
								@sentDate IS NULL OR 
								@updateDate	IS NULL) ) )
	BEGIN
	
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
		
	END
	

	-- C = {(Create, 0)}
	IF (@CRUDType IN (0))
	BEGIN
			
		INSERT INTO [dbo].[ClientCodesUpdated]
           ([CodeString]
           ,[PackSqlTextId]
           ,[SentDate]
           ,[UpdateDate]
           ,[Confirmed])
		VALUES
           (@codeString
           ,@packSqlTextId
           ,@sentDate
           ,@updateDate
           ,1)

		RETURN SCOPE_IDENTITY()


	END
	
END
go