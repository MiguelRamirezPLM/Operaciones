IF OBJECT_ID('dbo.plm_spCRUDFamilyPrefixes') IS NOT NULL
	DROP PROCEDURE dbo.plm_spCRUDFamilyPrefixes
go

/* 
	Author:			Ramiro Sánchez				 
	Object:			dbo.plm_spCRUDFamilyPrefixes
	
	Description:	Perform the basic operations, or in other words CRUD operations.
					Where CRUD stands for:

						C - Create	{0}
						R - Read	{1}
						U - Update	{2}
						D - Delete	{3}

	Company:		PLM Latina

 */ 

CREATE PROCEDURE dbo.plm_spCRUDFamilyPrefixes
(
	@CRUDType				tinyint,
	@prefixId				int,
	@editionId				int = Null,
	@prefixName				varchar(15) = Null,
	@currentValue			int = Null,
	@prefixDescription		varchar(100) = Null,
	@active					bit = Null
)
AS
BEGIN

	-- If @CRUDType belongs to {0,3}:
	IF (@CRUDType IN (0,3))
	BEGIN	
		RAISERROR ('The current operation cannot be performed', 16, 1)
		RETURN -1
	END

	-- R = {(Read, 1)}
	IF (@CRUDType IN (1))
	BEGIN
	
		SELECT	[PrefixId]
				,[EditionId]
				,[PrefixName]
				,[CurrentValue]
				,[PrefixDescription]
				,[Active]
		FROM [dbo].[FamilyPrefixes]
		WHERE [PrefixId] = @prefixId

		RETURN @@ROWCOUNT
	END

	-- U = {(Update, 2)}
	IF (@CRUDType IN (2))
	BEGIN
	
		UPDATE [dbo].[FamilyPrefixes]
			SET [EditionId] = @editionId
				,[PrefixName] = @prefixName
				,[CurrentValue] = @currentValue
				,[PrefixDescription] = @prefixDescription
				,[Active] = @active
		WHERE [PrefixId] = @prefixId

		RETURN @@ROWCOUNT
	END

END
go