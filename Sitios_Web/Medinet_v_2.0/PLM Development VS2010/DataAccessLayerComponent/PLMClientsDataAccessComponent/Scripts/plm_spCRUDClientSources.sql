
IF OBJECT_ID('dbo.plm_spCRUDClientSources') IS NOT NULL
	DROP PROCEDURE dbo.plm_spCRUDClientSources
go

/* 
	Author:			Erick Morales				 
	Object:			dbo.plm_spCRUDClientSources
	
	Description:	Perform the basic operations, or in other words CRUD operations.
					Where CRUD stands for:

						C - Create	{0}
						R - Read	{1}
						U - Update	{2} This operation does not apply
						D - Delete	{3}

	Company:		PLM Latina.
		
 */ 

CREATE PROCEDURE dbo.plm_spCRUDClientSources
(
	@CRUDType			tinyint,
	@clientId			int,
	@entrySourceId		int
)
AS
BEGIN

	-- If @CRUDType belongs to {2}:
	IF (@CRUDType IN (2))
	BEGIN
	
		RAISERROR ('The current operation cannot be performed', 16, 1)
		RETURN -1
		
	END

	-- If @CRUDType belongs to {0, 3}, then the parameters shouldn't be null:
	IF (@CRUDType IN (0,3) AND (@clientId IS NULL OR @entrySourceId IS NULL))
	BEGIN

		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1

	END

	-- C = {(CREATE, 0)}:
	IF (@CRUDType IN (0))
	BEGIN

		INSERT INTO [dbo].[ClientSources]
           ([EntrySourceId]
           ,[ClientId]
           ,[AddedDate])
		VALUES
           (@entrySourceId
           ,@clientId
           ,GETDATE())		
	 
		RETURN 0

	END

	-- R = {(Read, 1)}:
	IF (@CRUDType IN (1))

	BEGIN
		
		SELECT [EntrySourceId]
			  ,[ClientId]
			  ,[AddedDate]
		FROM [dbo].[ClientSources]
		WHERE	ClientId		= @clientId AND
				EntrySourceId	= @EntrySourceId
	 
		RETURN @@ROWCOUNT

	END

	-- D = {(Delete, 3)}:
	IF (@CRUDType IN (3))
	BEGIN
		
		DELETE FROM [dbo].[ClientSources]
		WHERE	ClientId		= @clientId AND
				EntrySourceId	= @EntrySourceId	

		RETURN 0

	END

END
go
