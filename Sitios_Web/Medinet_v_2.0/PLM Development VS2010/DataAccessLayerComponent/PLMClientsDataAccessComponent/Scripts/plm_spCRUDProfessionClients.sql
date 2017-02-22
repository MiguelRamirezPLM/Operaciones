
IF OBJECT_ID('dbo.plm_spCRUDProfessionClients') IS NOT NULL
	DROP PROCEDURE dbo.plm_spCRUDProfessionClients
go

/* 
	Author:			Juan Ramírez				 
	Object:			dbo.plm_spCRUDProfessionClients
	
	Description:	Perform the basic operations, or in other words CRUD operations.
					Where CRUD stands for:

						C - Create	{0}
						R - Read	{1}
						U - Update	{2}
						D - Delete	{3}

	Company:		PLM Latina.

	DECLARE
		@CRUDType		tinyint,
		@clientId		int,
		@professionId	smallint



	EXECUTE dbo.plm_spCRUDProfessionClients
		@CRUDType,			
		@clientId
		@professionId

 */ 

CREATE PROCEDURE dbo.plm_spCRUDProfessionClients
(
	@CRUDType				tinyint,
	@clientId				int,
	@professionId			smallint = NULL,
	@otherProfession		varchar(100) = NULL,
	@professionalLicense     varchar(20) = NULL
	
)
AS
BEGIN

	IF (@CRUDType IN (0, 2, 3) AND (@professionId IS NULL))
	BEGIN

		RAISERROR ('Missing parameter @professionId to perform current operation', 16, 1)
		RETURN -1

	END
	
	IF (@CRUDType IN (0, 2) AND (@professionalLicense IS NULL) AND 
			@professionId IN (SELECT ProfessionId FROM Professions WHERE ReqProfessionalLicense = 1))
	BEGIN

		RAISERROR ('Missing parameter @professionalLicense to perform current operation', 16, 1)
		RETURN -1

	END
	
	-- C = {(CREATE, 0)}:
	IF (@CRUDType IN (0))
	BEGIN

		IF (EXISTS (SELECT * FROM [ProfessionClients] WHERE [ClientId] = @clientId))
		BEGIN

			RAISERROR ('User cannot have more than one profession', 16, 1)
			RETURN -1

		END

		INSERT INTO [dbo].[ProfessionClients]
           ([ClientId]
           ,[ProfessionId]
           ,[OtherProfession]
           ,[ProfessionalLicense])
      	VALUES
           (@clientId
			,@professionId
			,@otherProfession
			,@professionalLicense)
	 
		RETURN 0

	END

	-- R = {(Read, 1)}:
	IF (@CRUDType IN (1))

	BEGIN
		SELECT [ClientId]
			  ,[ProfessionId]
			  ,[OtherProfession]
			  ,[ProfessionalLicense]
		  FROM [dbo].[ProfessionClients]
		 WHERE [ClientId] = @clientId
	 
		RETURN 0

	END		

	-- U = {(Update, 2)}:
	IF (@CRUDType IN (2))
	BEGIN

		UPDATE [dbo].[ProfessionClients]
		SET	 [OtherProfession]	= @otherProfession,
			 [ProfessionalLicense] = @professionalLicense
		WHERE 
			[ClientId]	= @clientId AND 
			[ProfessionId]	= @professionId

		RETURN 0
	END

	-- D = {(Delete, 3)}:
	IF (@CRUDType IN (3))
	BEGIN
		DELETE FROM [dbo].[ProfessionClients]
		 WHERE 	[ClientId]	= @clientId AND 
			[ProfessionId]	= @professionId

		RETURN 0

	END


END
go
