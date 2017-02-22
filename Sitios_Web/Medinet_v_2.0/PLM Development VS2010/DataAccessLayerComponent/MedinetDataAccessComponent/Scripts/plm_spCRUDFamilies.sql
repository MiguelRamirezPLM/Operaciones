IF OBJECT_ID('dbo.plm_spCRUDFamilies') IS NOT NULL
	DROP PROCEDURE dbo.plm_spCRUDFamilies
go

/* 
	Author:			Ramiro Sánchez				 
	Object:			dbo.plm_spCRUDFamilies
	
	Description:	Perform the basic operations, or in other words CRUD operations.
					Where CRUD stands for:

						C - Create	{0}
						R - Read	{1}
						U - Update	{2}
						D - Delete	{3}

	Company:		PLM Latina

 */ 

CREATE PROCEDURE dbo.plm_spCRUDFamilies
(
	@CRUDType				tinyint,
	@familyId				int,
	@prefixId				int = Null,
	@familyString			varchar(50) = Null,
	@active					bit = Null
)
AS
BEGIN

	-- If @CRUDType belongs to {2,3}:
	IF (@CRUDType IN (2,3))
	BEGIN	
		RAISERROR ('The current operation cannot be performed', 16, 1)
		RETURN -1
	END

	-- C = {(Create, 0)}
	IF (@CRUDType IN (0))
	BEGIN
	
		INSERT INTO [dbo].[Families]
           ([PrefixId]
           ,[FamilyString]
           ,[Active])
		VALUES
           (@prefixId
           ,@familyString
           ,@active)
			
		RETURN SCOPE_IDENTITY()

	END

	-- R = {(Read, 1)}
	IF (@CRUDType IN (1))
	BEGIN
	
		SELECT	[FamilyId]
				,[PrefixId]
				,[FamilyString]
				,[Active]
		FROM [dbo].[Families]
		WHERE [FamilyId] = @familyId
			
		RETURN @@ROWCOUNT

	END

END
go