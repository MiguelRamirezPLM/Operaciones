IF OBJECT_ID('dbo.plm_spCRUDCodePrefixes') IS NOT NULL
	DROP PROCEDURE dbo.plm_spCRUDCodePrefixes
go

/* 
	Author:			Juan Ramírez / Ramiro Sanchez
	Object:			dbo.plm_spCRUDCodePrefixes
	
	Description:	Perform the basic operations, or in other words CRUD operations.
					Where CRUD stands for:

						C - Create	{0}
						R - Read	{1}
						U - Update	{2}
						D - Delete	{3}

	Company:		Thomson PLM.

	DECLARE
		@CRUDType		tinyint,
		@prefixId			int

	SELECT 
		@CRUDType			= 1,
		@prefixId			= 24

	EXECUTE dbo.plm_spCRUDCodePrefixes
		@CRUDType,			
		@prefixId

 */ 

CREATE PROCEDURE dbo.plm_spCRUDCodePrefixes
(
	@CRUDType			tinyint,

	@prefixId			int,
	@parentId			int = null,
	@prefixTypeId		tinyint = null,
	@companyClientId	int = null,
	@prefixName			varchar(15) = null,
	@prefixValue		varchar(15) = null,
	@currentValue		int = null,
	@finalValue			int = null,
	@prefixDescription	varchar(100) = null,
	@active				bit = null
)
AS
BEGIN

	-- If @CRUDType belongs to {0, 3}:
	IF (@CRUDType IN (0, 3))
	BEGIN
	
		RAISERROR ('The current operation cannot be performed', 16, 1)
		RETURN -1
		
	END

	-- If @CRUDType belongs to {2}, then the parameters shouldn't be null:
	IF (@CRUDType IN (2) AND (@prefixTypeId IS NULL OR @companyClientId	IS NULL OR @prefixName IS NULL 
							OR @prefixValue IS NULL OR @currentValue IS NULL 
							OR @prefixDescription IS NULL OR @active IS NULL))
	BEGIN
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
	END

	-- R = {(Read, 1)}:
	IF (@CRUDType IN (1))
	BEGIN

		SELECT [PrefixId]
			  ,[ParentId]
			  ,[PrefixTypeId]
			  ,[CompanyClientId]
			  ,[PrefixName]
			  ,[PrefixValue]
			  ,[CurrentValue]
			  ,[FinalValue]
			  ,[PrefixDescription]
			  ,[AddedDate]
			  ,[Active]
		 FROM [dbo].[CodePrefixes]
		WHERE	[PrefixId] = @prefixId

		RETURN @@ROWCOUNT
		
	END

	-- U = {(Update, 2)}
	IF (@CRUDType IN (2))
	BEGIN
		
		UPDATE [dbo].[CodePrefixes]
		   SET [ParentId] = @parentId
			  ,[PrefixTypeId] = @prefixTypeId
			  ,[CompanyClientId] = @companyClientId
			  ,[PrefixName] = @prefixName
			  ,[PrefixValue] = @prefixValue
			  ,[CurrentValue] = @currentValue
			  ,[FinalValue] = @finalValue
			  ,[PrefixDescription] = @prefixDescription
			  ,[Active] = @active
		 WHERE [PrefixId] = @prefixId

		RETURN 0
		
	END

END
go