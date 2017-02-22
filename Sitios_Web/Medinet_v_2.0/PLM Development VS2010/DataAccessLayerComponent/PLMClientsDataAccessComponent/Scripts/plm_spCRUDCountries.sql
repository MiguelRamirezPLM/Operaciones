IF OBJECT_ID('dbo.plm_spCRUDCountries') IS NOT NULL
	DROP PROCEDURE dbo.plm_spCRUDCountries
go

/* 
	Author:			Juan Ramirez.				 
	Object:			dbo.plm_spCRUDCountries
	
	Description:	Perform the basic operations, or in other words CRUD operations.
					Where CRUD stands for:

						C - Create	{0}
						R - Read	{1}
						U - Update	{2}
						D - Delete	{3}

	Company:		Thomson PLM.

	DECLARE
		@CRUDType		tinyint,
		@countryId			int

	SELECT 
		@CRUDType			= 1,
		@countryId				= 1

	EXECUTE dbo.plm_spCRUDCountries
		@CRUDType,			
		@countryId

 */ 

CREATE PROCEDURE dbo.plm_spCRUDCountries
(
	@CRUDType				tinyint,

	@countryId				tinyint
)
AS
BEGIN

	-- If @CRUDType belongs to {0, 2, 3}:
	IF (@CRUDType IN (0, 2, 3))
	BEGIN
	
		RAISERROR ('The current operation cannot be performed', 16, 1)
		RETURN -1
		
	END

	-- R = {(Read, 1)}:
	IF (@CRUDType IN (1))
	BEGIN

		SELECT [CountryId]
			  ,[CountryName]
			  ,[CountryCode]
			  ,[ID]
			  ,[Active]
		  FROM [dbo].[Countries]


		WHERE	[CountryId] = @countryId

		RETURN @@ROWCOUNT
		
	END

END
go