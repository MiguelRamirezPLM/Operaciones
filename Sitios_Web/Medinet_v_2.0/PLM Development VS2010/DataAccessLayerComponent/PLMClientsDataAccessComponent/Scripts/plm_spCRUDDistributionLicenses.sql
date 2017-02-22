IF OBJECT_ID('dbo.plm_spCRUDDistributionLicenses') IS NOT NULL
	DROP PROCEDURE dbo.plm_spCRUDDistributionLicenses
go

/* 
	Author:			Ulises Orihuela				 
	Object:			dbo.plm_spCRUDDistributionLicenses
	
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


		EXECUTE dbo.[plm_spCRUDDistributionLicenses]
		@CRUDType = 1,			
		@licenseId = 274626,
		@distributionId = 7,
		@prefixId = 356,
		@targetId = 1
		
 */ 
									
CREATE PROCEDURE [dbo].[plm_spCRUDDistributionLicenses]
(
	@CRUDType			TINYINT,
	@license			INT,
	@distribution		INT,
	@prefix				INT ,
	@target				int ,
	@Return				int = 1
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
	IF (@CRUDType IN (0,2) AND (@distribution IS NULL OR @prefix IS NULL OR @target IS NULL OR @license IS NULL))
	BEGIN
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
	END

	-- C = {(CREATE, 0)}:
	IF (@CRUDType IN (0))
	BEGIN
		
		INSERT INTO [dbo].[DistributionLicenses]
           ([DistributionId]
           ,[PrefixId]
           ,[TargetId]
           ,[LicenseId])
		VALUES
           (@distribution
           ,@prefix
           ,@target
           ,@license)
           
		return @Return 
	END
	
END

