IF OBJECT_ID('dbo.plm_spCRUDResidenceClients') IS NOT NULL
	DROP PROCEDURE dbo.plm_spCRUDResidenceClients
go

/* 
	Author:			Ramiro Sánchez				 
	Object:			dbo.plm_spCRUDResidenceClients
	
	Description:	Perform the basic operations, or in other words CRUD operations.
					Where CRUD stands for:

						C - Create	{0}
						R - Read	{1}
						U - Update	{2}
						D - Delete	{3}

	Company:		PLM Latina.
		
 */ 

CREATE PROCEDURE dbo.plm_spCRUDResidenceClients
(
	@CRUDType			TINYINT,
	@clientId			INT,
	@specialityId		SMALLINT = NULL,
	@residenceId		TINYINT = NULL
)
AS
BEGIN

	-- If @CRUDType belongs to {2}:
	IF (@CRUDType IN (2))
	BEGIN
		RAISERROR ('The current operation cannot be performed', 16, 1)
		RETURN -1
	END

	-- The parameters shouldn't be null:
	IF (@clientId IS NULL)
	BEGIN
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
	END

	-- C = {(CREATE, 0)}:
	IF (@CRUDType IN (0))
	BEGIN

		INSERT INTO [dbo].[ResidenceClients]
           ([ClientId]
           ,[SpecialityId]
           ,[ResidenceId])
		VALUES
           (@clientId
           ,@specialityId
           ,@residenceId)

		RETURN 1

	END

	-- R = {(Read, 1)}
	IF (@CRUDType IN (1))
	BEGIN
		
		SELECT [ClientId]
				,[SpecialityId]
				,[ResidenceId]
		  FROM [dbo].[ResidenceClients]
		 WHERE [ClientId] = @clientId 
			AND [SpecialityId] = @specialityId
			AND [ResidenceId] = @residenceId

		RETURN @@ROWCOUNT
	END
	
	-- D = {(Delete, 3)}:
	IF (@CRUDType IN (3))
	BEGIN
		
		IF(@clientId IS NOT NULL AND @specialityId IS NULL AND @residenceId IS NULL)
		BEGIN
			--Delete all entries in the [dbo].[ClientCodes] table:
			DELETE FROM [dbo].[ResidenceClients]
			WHERE [ClientId] = @clientId

			RETURN 0
		END

		IF(@clientId IS NOT NULL AND @specialityId IS NOT NULL AND @residenceId IS NOT NULL)
		BEGIN
			--Delete all entries in the [dbo].[ClientCodes] table:
			DELETE FROM [dbo].[ResidenceClients]
			WHERE [ClientId] = @clientId 
				AND [SpecialityId] = @specialityId
				AND [ResidenceId] = @residenceId
		
			RETURN 0
		END		
	END
END
go
