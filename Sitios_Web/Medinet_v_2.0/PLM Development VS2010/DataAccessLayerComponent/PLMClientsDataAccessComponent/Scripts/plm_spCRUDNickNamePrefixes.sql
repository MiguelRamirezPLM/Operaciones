IF OBJECT_ID('dbo.plm_spCRUDNickNamePrefixes') IS NOT NULL
	DROP PROCEDURE dbo.plm_spCRUDNickNamePrefixes
go

/* 
	Author:			Juan Ramírez.				 
	Object:			dbo.plm_spCRUDNickNamePrefixes
	
	Description:	Perform the basic operations, or in other words CRUD operations.
					Where CRUD stands for:

						C - Create	{0}
						R - Read	{1}
						U - Update	{2}
						D - Delete	{3}

	Company:		Thomson PLM.

	DECLARE
		@CRUDType		tinyint,
		@nickNamePrefixId			int

	SELECT 
		@CRUDType			= 1,
		@nickNamePrefixId			= 24

	EXECUTE dbo.plm_spCRUDNickNamePrefixes
		@CRUDType,			
		@nickNamePrefixId

 */ 

CREATE PROCEDURE dbo.plm_spCRUDNickNamePrefixes
(
	@CRUDType				tinyint,

	@nickPrefixId			int,
	@prefixId				int = null,
	@prefixName			    varchar(15) = null,
	@initialValue			int = null,
	@finalValue				int = null,
	@currentNumber			int = null,
	@prefixDescription		varchar(100) = null,
	@active					bit = null
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
	IF (@CRUDType IN (2) AND (@prefixId IS NULL OR @prefixName IS NULL OR @initialValue IS NULL OR @finalValue IS NULL OR
							  @currentNumber IS NULL OR @prefixDescription IS NULL OR @active IS NULL))
	BEGIN
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
	END


	-- R = {(Read, 1)}:
	IF (@CRUDType IN (1))
	BEGIN

		SELECT [NickPrefixId]
			   [PrefixId]	
			  ,[PrefixName]
			  ,[InitialValue]
			  ,[FinalValue]
			  ,[CurrentNumber]
			  ,[PrefixDescription]
			  ,[Active]
		  FROM [dbo].[NickNamePrefixes]

		WHERE	[NickPrefixId] = @nickPrefixId

		RETURN @@ROWCOUNT
		
	END
	
	-- U = {(Update, 2)}
	IF (@CRUDType IN (2))
	BEGIN
		
		UPDATE [dbo].[NickNamePrefixes]
		   SET [PrefixId] = @prefixId
			  ,[PrefixName] = @prefixName
			  ,[InitialValue] = @initialValue
			  ,[FinalValue] = @finalValue
			  ,[CurrentNumber] = @currentNumber
			  ,[PrefixDescription] = @prefixDescription
			  ,[Active] = @active
		 WHERE [NickPrefixId] = @nickPrefixId

		RETURN 0
		
	END


END
go