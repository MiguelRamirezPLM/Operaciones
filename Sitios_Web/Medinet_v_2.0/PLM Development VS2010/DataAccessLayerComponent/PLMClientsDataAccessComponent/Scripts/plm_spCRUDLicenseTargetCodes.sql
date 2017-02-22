IF OBJECT_ID('dbo.plm_spCRUDLicenseTargetCodes') IS NOT NULL
	DROP PROCEDURE dbo.plm_spCRUDLicenseTargetCodes
go

/* 
	Author:			Ramiro Sánchez				 
	Object:			dbo.plm_spCRUDLicenseTargetCodes
	
	Description:	Perform the basic operations, or in other words CRUD operations.
					Where CRUD stands for:

						C - Create	{0}
						R - Read	{1}
						U - Update	{2}
						D - Delete	{3}

	Company:		PLM Latina.
		
 */ 

CREATE PROCEDURE [dbo].plm_spCRUDLicenseTargetCodes
(
	@CRUDType			TINYINT,
	@clientId			INT,
	@codeId				INT,
	@targetId			TINYINT,
	@deviceId			TINYINT,
	@licenseId			INT,
	@finalDate			datetime = NULL
)
AS
BEGIN

	-- If @CRUDType belongs to {1, 2}:
	IF (@CRUDType IN (1, 2))
	BEGIN
	
		RAISERROR ('The current operation cannot be performed', 16, 1)
		RETURN -1
		
	END

	-- If @CRUDType belongs to {0}, then the parameters shouldn't be null:
	IF (@CRUDType IN (0,3) AND (@licenseId IS NULL OR @codeId IS NULL OR @deviceId IS NULL))
	BEGIN
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
	END
	
	-- C = {(CREATE, 0)}:
	IF (@CRUDType IN (0))
	BEGIN

		DECLARE
		@duration decimal(4,2)

		SELECT @duration = Duration
			FROM Licenses
			WHERE LicenseId = @licenseId
		
		INSERT INTO [dbo].[LicenseTargetCodes]
           ([ClientId]
           ,[CodeId]
           ,[TargetId]
           ,[DeviceId]
           ,[LicenseId]
           ,[InitialDate]
           ,[FinalDate])
     VALUES
           (@clientId
           ,@codeId
           ,@targetId
           ,@deviceId
           ,@licenseId
           ,GETDATE()
           ,CASE WHEN @finalDate Is Null 
				THEN DATEADD(YEAR, @duration, GETDATE()) 
				ELSE @finalDate END)
	 
		RETURN 0

	END
	
	-- D = {(DELETE, 3)}:
	IF (@CRUDType IN (3))
	BEGIN
	
		DELETE FROM [dbo].[LicenseTargetCodes]
		WHERE [ClientId] = @clientId
			AND [CodeId] = @codeId
			AND [TargetId] = @targetId
			AND [DeviceId] = @deviceId
			AND [LicenseId] = @licenseId
				
		RETURN 0
	
	END

END
GO


