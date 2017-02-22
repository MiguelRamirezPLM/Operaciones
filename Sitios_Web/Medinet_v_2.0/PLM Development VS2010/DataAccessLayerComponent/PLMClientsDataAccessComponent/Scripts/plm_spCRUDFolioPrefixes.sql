IF OBJECT_ID('dbo.plm_spCRUDFolioPrefixes') IS NOT NULL
	DROP PROCEDURE dbo.plm_spCRUDFolioPrefixes
go

/* 
	Author:			Juan Ramírez.				 
	Object:			dbo.plm_spCRUDFolioPrefixes
	
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

	EXECUTE dbo.plm_spCRUDFolioPrefixes
		@CRUDType,			
		@prefixId

 */ 

CREATE PROCEDURE dbo.plm_spCRUDFolioPrefixes
(
	@CRUDType				tinyint,

	@prefixId				int,
	@prefixName			    varchar(15) = null,
	@initialValue			int = null,
	@finalValue				int = null,
	@currentValue			int = null,
	@prefixDescription		varchar(100) = null,
	@active					bit = null
)
AS
BEGIN

	-- If @CRUDType belongs to {0, 3}:
	IF (@CRUDType IN (0, 2, 3))
	BEGIN
	
		RAISERROR ('The current operation cannot be performed', 16, 1)
		RETURN -1
		
	END

	-- If @CRUDType belongs to {2}, then the parameters shouldn't be null:
	IF (@CRUDType IN (2) AND (@prefixName IS NULL OR @initialValue IS NULL OR @finalValue IS NULL OR
							  @currentValue IS NULL OR @prefixDescription IS NULL OR @active IS NULL))
	BEGIN
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
	END

	-- R = {(Read, 1)}:
	IF (@CRUDType IN (1))
	BEGIN

		SELECT [PrefixId]
			  ,[PrefixName]
			  ,[InitialValue]
			  ,[FinalValue]
			  ,[CurrentValue]
			  ,[PrefixDescription]
			  ,[Active]
		  FROM [dbo].[FolioPrefixes]

		WHERE	[PrefixId] = @prefixId

		RETURN @@ROWCOUNT
		
	END

	-- U = {(Update, 2)}
	IF (@CRUDType IN (2))
	BEGIN
		
		UPDATE [dbo].[FolioPrefixes]
		   SET [PrefixName] = @prefixName
			  ,[InitialValue] = @initialValue
			  ,[FinalValue] = @finalValue
			  ,[CurrentValue] = @currentValue
			  ,[PrefixDescription] = @prefixDescription
			  ,[Active] = @active
		 WHERE [PrefixId] = @prefixId

		RETURN 0
		
	END

END
go