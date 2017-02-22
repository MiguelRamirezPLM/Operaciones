IF OBJECT_ID('dbo.plm_spCRUDClientUsers') IS NOT NULL
	DROP PROCEDURE dbo.plm_spCRUDClientUsers
go

/* 
	Author:			Juan Ramírez				 
	Object:			dbo.plm_spCRUDClientUsers
	
	Description:	Perform the basic operations, or in other words CRUD operations.
					Where CRUD stands for:

						C - Create	{0}
						R - Read	{1}
						U - Update	{2}
						D - Delete	{3}

	Company:		PLM Latina.

	DECLARE
		@CRUDType		tinyint,
		@userId			int,
		@codeId			int



	EXECUTE dbo.plm_spCRUDClientUsers
		@CRUDType,			
		@userId
		@codeId

 */ 

CREATE PROCEDURE dbo.plm_spCRUDClientUsers
(
	@CRUDType			tinyint,
	@clientId			int,
	@userId				int
)
AS
BEGIN

	-- If @CRUDType belongs to {2}:
	IF (@CRUDType IN (1,2))
	BEGIN
	
		RAISERROR ('The current operation cannot be performed', 16, 1)
		RETURN -1
		
	END

	-- C = {(CREATE, 0)}:
	IF (@CRUDType IN (0))
	BEGIN

		INSERT INTO [dbo].[ClientUsers]
           ([ClientId]
           ,[UserId])
		VALUES
           (@clientId
           ,@userId)
	 
		RETURN 0

	END

	-- D = {(Delete, 3)}:
	IF (@CRUDType IN (3))
	BEGIN
		
		DELETE FROM [dbo].[ClientUsers]
		WHERE [ClientId] = @clientId AND 
			  [UserId] = @userId	

		RETURN 0

	END


END
go
