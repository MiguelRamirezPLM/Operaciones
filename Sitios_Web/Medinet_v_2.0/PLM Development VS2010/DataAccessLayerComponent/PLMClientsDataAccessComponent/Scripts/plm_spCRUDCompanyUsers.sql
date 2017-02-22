IF OBJECT_ID('dbo.plm_spCRUDCompanyUsers') IS NOT NULL
	DROP PROCEDURE dbo.plm_spCRUDCompanyUsers
go

/* 
	Author:			Ramiro Sánchez				 
	Object:			dbo.plm_spCRUDCompanyUsers
	
	Description:	Perform the basic operations, or in other words CRUD operations.
					Where CRUD stands for:

						C - Create	{0}
						R - Read	{1}
						U - Update	{2}
						D - Delete	{3}

	Company:		PLM Latina.
		
 */ 

CREATE PROCEDURE dbo.plm_spCRUDCompanyUsers
(
	@CRUDType				TINYINT,
	@companyUserId			INT,
	@companyClientId		INT = NULL,
	@firstName				VARCHAR(60) = NULL,
	@lastName				VARCHAR(60) = NULL,
	@userName				VARCHAR(50) = NULL,
	@userPassword			VARCHAR(100) = NULL,
	@active					BIT = NULL
)
AS
BEGIN

	-- If @CRUDType belongs to {2, 3}:
	IF (@CRUDType IN (2, 3))
	BEGIN
	
		RAISERROR ('The current operation cannot be performed', 16, 1)
		RETURN -1
		
	END

	-- If @CRUDType belongs to {0}, then the parameters shouldn't be null:
	IF (@CRUDType IN (0) AND (@companyClientId IS NULL OR @firstName IS NULL OR @lastName IS NULL OR 
		@userName IS NULL OR @userPassword IS NULL))
	BEGIN
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
	END

	-- C = {(CREATE, 0)}:
	IF (@CRUDType IN (0))
	BEGIN
		
		INSERT INTO [dbo].[CompanyUsers]
           ([CompanyClientId]
           ,[FirstName]
           ,[LastName]
           ,[UserName]
           ,[UserPassword]
           ,[Active])
		VALUES
           (@companyClientId
           ,@firstName
           ,@lastName
           ,@userName
           ,@userPassword
           ,@active)		

		RETURN SCOPE_IDENTITY()

	END
	
	-- R = {(Read, 1)}
	IF (@CRUDType IN (1))
	BEGIN

		SELECT [CompanyUserId]
			,[CompanyClientId]
			,[FirstName]
			,[LastName]
			,[UserName]
			,[UserPassword]
			,[Active]
		FROM [dbo].[CompanyUsers]
			Where CompanyUserId = @companyUserId

		RETURN @@ROWCOUNT
	END

END
go
