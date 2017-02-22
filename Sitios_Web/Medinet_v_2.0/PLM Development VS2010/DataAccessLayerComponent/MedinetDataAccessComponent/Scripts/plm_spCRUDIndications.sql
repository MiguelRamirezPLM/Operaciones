IF OBJECT_ID('dbo.plm_spCRUDIndications') IS NOT NULL
	DROP PROCEDURE dbo.plm_spCRUDIndications
go

/* 
	Author:			Ramiro Sánchez				 
	Object:			dbo.plm_spCRUDIndications
	
	Description:	Perform the basic operations, or in other words CRUD operations.
					Where CRUD stands for:

						C - Create	{0}
						R - Read	{1}
						U - Update	{2}
						D - Delete	{3}

	Company:		PLM Latina

	EXECUTE dbo.plm_spCRUDIndications 

 */ 

CREATE PROCEDURE dbo.plm_spCRUDIndications
(
	@CRUDType			tinyint,
	@indicationId		int = Null,
	@parentId			int = Null,
	@indicationKey		varchar(8) = Null,
	@description		varchar(200) = Null,
	@active				bit = Null
)
AS
BEGIN

	-- If @CRUDType belongs to {3}:
	IF (@CRUDType IN (3))
	BEGIN
		RAISERROR ('The current operation cannot be performed', 16, 1)
		RETURN -1
	END

	--C = {(Create, 0)}
	IF (@CRUDType IN (0))
	BEGIN
		
		INSERT INTO [dbo].[Indications]
           ([ParentId]
           ,[IndicationKey]
           ,[Description]
           ,[Active])
		VALUES
           (@parentId
           ,@indicationKey
           ,@description
           ,@active)

		RETURN SCOPE_IDENTITY()

	END
	
	-- R = {(Read, 1)}:
	IF (@CRUDType IN (1))
	BEGIN

		SELECT	[IndicationId]
				,[ParentId]
				,[Description]
				,[Active]
		FROM [dbo].[Indications]
		WHERE [IndicationId] = @indicationId

		RETURN @@ROWCOUNT
		
	END
	
	-- U = {(Update, 2)}:
	IF (@CRUDType IN (2))
	BEGIN
	
		UPDATE [dbo].[Indications]
			SET [ParentId] = @parentId
				,[IndicationKey] = @indicationKey
				,[Description] = @description
				,[Active] = @active
		WHERE [IndicationId] = @indicationId
		
		RETURN 0
		
	END
	
END
go