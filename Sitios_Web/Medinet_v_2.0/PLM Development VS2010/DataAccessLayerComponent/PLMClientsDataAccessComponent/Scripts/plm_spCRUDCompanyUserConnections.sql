IF OBJECT_ID('dbo.plm_spCRUDCompanyUserConnections') IS NOT NULL
	DROP PROCEDURE dbo.plm_spCRUDCompanyUserConnections
go

/* 
	Author:			Ramiro Sánchez				 
	Object:			dbo.plm_spCRUDCompanyUserConnections
	
	Description:	Perform the basic operations, or in other words CRUD operations.
					Where CRUD stands for:

						C - Create	{0}
						R - Read	{1}
						U - Update	{2}
						D - Delete	{3}

	Company:		PLM Latina.
		
 */ 

CREATE PROCEDURE dbo.plm_spCRUDCompanyUserConnections
(
	@CRUDType				TINYINT,
	@userConnectionId		INT,
	@distributionId			INT = Null,
	@companyUserId			INT = Null,
	@codeId					INT = Null,
	@sessionId				TINYINT = Null,
	@ip						VARCHAR(30) = Null
)
AS
BEGIN

	-- If @CRUDType belongs to {2,3}:
	IF (@CRUDType IN (2,3))
	BEGIN
		RAISERROR ('The current operation cannot be performed', 16, 1)
		RETURN -1
	END

	-- If @CRUDType belongs to {0}, then the parameters shouldn't be null:
	IF (@CRUDType IN (0) AND (@distributionId IS NULL OR @companyUserId IS NULL OR 
			@codeId IS NULL OR @sessionId IS NULL))
	BEGIN
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
	END

	-- C = {(CREATE, 0)}:
	IF (@CRUDType IN (0))
	BEGIN

		INSERT INTO [dbo].[CompanyUserConnections]
           ([DistributionId]
           ,[CompanyUserId]
           ,[CodeId]
           ,[SessionId]
           ,[DateConnection]
           ,[IP])
		VALUES
           (@distributionId
           ,@companyUserId
           ,@codeId
           ,@sessionId
           ,GETDATE()
           ,@ip)

		RETURN SCOPE_IDENTITY()

	END

	-- R = {(Read, 1)}
	IF (@CRUDType IN (1))
	BEGIN

		SELECT [UserConnectionId]
			,[DistributionId]
			,[CompanyUserId]
			,[CodeId]
			,[SessionId]
			,[DateConnection]
			,[IP]
		FROM [dbo].[CompanyUserConnections]
		WHERE [UserConnectionId] = @userConnectionId

		RETURN @@ROWCOUNT

	END
END
go
