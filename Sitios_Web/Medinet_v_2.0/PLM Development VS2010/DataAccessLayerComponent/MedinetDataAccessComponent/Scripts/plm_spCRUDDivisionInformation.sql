IF OBJECT_ID('dbo.plm_spCRUDDivisionInformation') IS NOT NULL
	DROP PROCEDURE dbo.plm_spCRUDDivisionInformation
go

/* 
	Author:			Ramiro Sánchez				 
	Object:			dbo.plm_spCRUDDivisionInformation
	
	Description:	Perform the basic operations, or in other words CRUD operations.
					Where CRUD stands for:

						C - Create	{0}
						R - Read	{1}
						U - Update	{2}
						D - Delete	{3}

	Company:		PLM Latina

 */ 

CREATE PROCEDURE dbo.plm_spCRUDDivisionInformation
(
	@CRUDType		tinyint,
	@divisionInfId	int,
	@divisionId		int = Null,
	@address		text = Null,
	@image			varchar(200) = Null,
	@suburb			varchar(200) = Null,
	@zipCode		varchar(50) = Null,
	@telephone		varchar(100) = Null,
	@fax			varchar(100) = Null,
	@web			varchar(100) = Null,
	@email			varchar(100) = Null,
	@city			varchar(100) = Null,
	@state			varchar(100) = Null,
	@contact		varchar(200) = Null,
	@active			bit = Null
)
AS
BEGIN

	-- If @CRUDType belongs to {3}:
	IF (@CRUDType IN (3))
	BEGIN	
		DELETE FROM DivisionInformation 
		where DivisionInfId =@divisionInfId
	END

	-- R = {(Read, 1)}:
	IF (@CRUDType IN (1))
	BEGIN

		SELECT Top 1 [DivisionInfId]
				,[DivisionId]
				,[Address]
				,[Image]
				,[Suburb]
				,[ZipCode]
				,[Telephone]
				,[Fax]
				,[Web]
				,[Email]
				,[City]
				,[State]
				,[Contact]
				,[Active]
		FROM [dbo].[DivisionInformation]
		WHERE [DivisionId] = @divisionId

		RETURN @@ROWCOUNT
		
	END

	-- U = {(Update, 2)}
	IF (@CRUDType IN (2))
	BEGIN

		UPDATE [dbo].[DivisionInformation]
			SET [DivisionId] = @divisionId
				,[Address] = @address
				,[Image] = @image
				,[Suburb] = @suburb
				,[ZipCode] = @zipCode
				,[Telephone] = @telephone
				,[Fax] = @fax
				,[Web] = @web
				,[Email] = @email
				,[City] = @city
				,[State] = @state
				,[Contact] = @contact
				,[Active] = @active
		WHERE [DivisionInfId] = @divisionInfId

		RETURN 0

	END
	IF (@CRUDType IN (0))
	BEGIN

		
		
		
	INSERT INTO [dbo].[DivisionInformation]
           ([DivisionId]
           ,[Address]
           ,[Image]
           ,[Suburb]
           ,[ZipCode]
           ,[Telephone]
           ,[Fax]
           ,[Web]
           ,[Email]
           ,[City]
           ,[State]
           ,[Contact]
           ,[Active])
     VALUES
           (@divisionId
           ,@address
           ,@image
           ,@suburb
           ,@zipCode
           ,@telephone
           ,@fax
           ,@web
           ,@email
           ,@city
           ,@state
           ,@contact
           ,@active)
           
		RETURN SCOPE_IDENTITY()

	END
END
go
