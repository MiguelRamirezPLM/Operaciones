IF OBJECT_ID('dbo.plm_spCRUDDistributionCompanyUsers') IS NOT NULL
	DROP PROCEDURE dbo.plm_spCRUDDistributionCompanyUsers
go

/* 
	Author:			Ramiro Sánchez				 
	Object:			dbo.plm_spCRUDDistributionCompanyUsers
	
	Description:	Perform the basic operations, or in other words CRUD operations.
					Where CRUD stands for:

						C - Create	{0}
						R - Read	{1}
						U - Update	{2}
						D - Delete	{3}

	Company:		PLM Latina.
		
 */ 

CREATE PROCEDURE dbo.plm_spCRUDDistributionCompanyUsers
(
	@CRUDType				TINYINT,
	@distributionId			INT,
	@companyUserId			INT,
	@codeId					INT,
	@initialDate			DATETIME,
	@finalDate				DATETIME,
	@active					BIT
)
AS
BEGIN

	-- If @CRUDType belongs to {1, 2, 3}:
	IF (@CRUDType IN (1, 2, 3))
	BEGIN
	
		RAISERROR ('The current operation cannot be performed', 16, 1)
		RETURN -1
		
	END

	-- C = {(CREATE, 0)}:
	IF (@CRUDType IN (0))
	BEGIN
	
		INSERT INTO [dbo].[DistributionCompanyUsers]
           ([DistributionId]
           ,[CompanyUserId]
           ,[CodeId]
           ,[InitialDate]
           ,[FinalDate]
           ,[Active])
     VALUES
           (@distributionId
           ,@companyUserId
           ,@codeId
           ,@initialDate
           ,@finalDate
           ,@active)
	
	RETURN -1

	END	

END
go
