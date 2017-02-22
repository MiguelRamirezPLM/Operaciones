IF OBJECT_ID('dbo.plm_spCRUDSpecialityClients') IS NOT NULL
	DROP PROCEDURE dbo.plm_spCRUDSpecialityClients
go

/* 
	Author:			Juan Ramírez				 
	Object:			dbo.plm_spCRUDSpecialityClients
	
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
		@specialityId	smallint



	EXECUTE dbo.plm_spCRUDSpecialityClients
		@CRUDType,			
		@clientId
		@specialityId

 */ 

CREATE PROCEDURE dbo.plm_spCRUDSpecialityClients
(
	@CRUDType				tinyint,
	@clientId				int,
	@specialityId			smallint = NULL,
	@otherSpeciality		varchar(100) = NULL
)
AS
BEGIN
	
	IF (@CRUDType IN (0, 2) AND (@specialityId IS NULL))
	BEGIN

		RAISERROR ('Missing parameters to perform current operation', 16, 1)
		RETURN -1

	END

	-- C = {(CREATE, 0)}:
	IF (@CRUDType IN (0))
	BEGIN

		IF (EXISTS (SELECT * FROM [SpecialityClients] WHERE [ClientId] = @clientId))
		BEGIN

			RAISERROR ('User cannot have more than one speciality', 16, 1)
			RETURN -1

		END

		INSERT INTO [dbo].[SpecialityClients]
           ([ClientId]
           ,[SpecialityId]
           ,[OtherSpeciality])
     	VALUES
           (@clientId
			,@specialityId
			,@otherSpeciality)
	 
		RETURN 0

	END

	-- R = {(Read, 1)}:
	IF (@CRUDType IN (1))

	BEGIN
		SELECT [ClientId]
			  ,[SpecialityId]
			  ,[OtherSpeciality]
		  FROM [dbo].[SpecialityClients]
		 WHERE [ClientId] = @clientId
	 
		RETURN @@ROWCOUNT

	END		

	-- U = {(Update, 2)}:
	IF (@CRUDType IN (2))
	BEGIN

		UPDATE [dbo].[SpecialityClients]
		SET	[OtherSpeciality]		= @otherSpeciality
		WHERE 
			[ClientId]	= @clientId AND 
			[SpecialityId]	= @specialityId

		RETURN 0
	END

	-- D = {(Delete, 3)}:
	IF (@CRUDType IN (3) AND @specialityId IS NOT NULL)
	BEGIN
		DELETE FROM [dbo].[SpecialityClients]
		 WHERE 	[ClientId]	= @clientId AND 
			[SpecialityId]	= @specialityId

		RETURN 0

	END
	IF (@CRUDType IN (3) AND @specialityId IS NULL)
	BEGIN
		DELETE FROM [dbo].[SpecialityClients]
		 WHERE 	[ClientId]	= @clientId 

		RETURN 0

	END

END
go
