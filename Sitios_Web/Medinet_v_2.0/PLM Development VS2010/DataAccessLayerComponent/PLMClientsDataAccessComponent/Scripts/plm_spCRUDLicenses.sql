
IF OBJECT_ID('dbo.plm_spCRUDLicenses') IS NOT NULL
	DROP PROCEDURE dbo.plm_spCRUDLicenses
go

/* 
	Author:			Ramiro Sánchez				 
	Object:			dbo.plm_spCRUDLicenses
	
	Description:	Perform the basic operations, or in other words CRUD operations.
					Where CRUD stands for:

						C - Create	{0}
						R - Read	{1}
						U - Update	{2}
						D - Delete	{3}

	Company:		PLM Latina.

	DECLARE
		@CRUDType		tinyint,
		@licenseId		int



	EXECUTE dbo.plm_spCRUDLicenses
		@CRUDType,			
		@licenseId
		
 */ 

CREATE PROCEDURE dbo.plm_spCRUDLicenses
(
	@CRUDType				TINYINT,
	@licenseId				INT,
--	@distributionId			INT = NULL,
--	@prefixId				INT = NULL,
--	@targetId				TINYINT = NULL,
	@licenseKey				VARCHAR(16) = NULL,
	@currentInstallation	SMALLINT = 0,
	@duration				DECIMAL(4,2) = NULL,
	@active					BIT = Null
)
AS
BEGIN

	-- If @CRUDType belongs to {3}:
	IF (@CRUDType IN (3))
	BEGIN
	
		RAISERROR ('The current operation cannot be performed', 16, 1)
		RETURN -1
		
	END

	-- If @CRUDType belongs to {0,2}, then the parameters shouldn't be null:
	IF (@CRUDType IN (0,2) AND (@licenseKey IS NULL OR @currentInstallation IS NULL OR @duration IS NULL OR @active IS NULL))
	BEGIN
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
	END

	-- C = {(CREATE, 0)}:
	IF (@CRUDType IN (0))
	BEGIN
		
		INSERT INTO [dbo].[Licenses]
           ([LicenseKey]
           ,[CurrentInstallation]
		   ,[Duration]
           ,[Active])
		VALUES
           (@licenseKey
           ,@currentInstallation
		   ,@duration
           ,@active)

	 RETURN SCOPE_IDENTITY()

	END

	-- R = {(Read, 1)}
	IF (@CRUDType IN (1))
	BEGIN
		SELECT [LicenseId]
				,[LicenseKey]
				,[CurrentInstallation]
			    ,[Duration]
				,[Active]
		  FROM [dbo].[Licenses]
		 WHERE [LicenseId] = @licenseId			

		RETURN @@ROWCOUNT
	END
	
	-- U = {(Update, 2)}
	IF (@CRUDType IN (2))
	BEGIN

		UPDATE [dbo].[Licenses]
			SET [LicenseKey] = @licenseKey
				,[CurrentInstallation] = @currentInstallation
				,[Duration] = @duration
				,[Active] = @active
		WHERE [LicenseId] = @licenseId
		
		RETURN 0

	END	
END
go
