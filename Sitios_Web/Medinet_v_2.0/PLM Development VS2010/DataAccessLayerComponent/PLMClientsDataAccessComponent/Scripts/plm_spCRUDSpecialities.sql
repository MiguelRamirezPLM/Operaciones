
IF OBJECT_ID('dbo.plm_spCRUDSpecialities') IS NOT NULL
	DROP PROCEDURE dbo.plm_spCRUDSpecialities
go

/* 
	Author:			Erick Morales Silva.				 
	Object:			dbo.plm_spCRUDSpecialities
	
	Description:	Perform the basic operations, or in other words CRUD operations.
					Where CRUD stands for:

						C - Create	{0}
						R - Read	{1}
						U - Update	{2}
						D - Delete	{3}

	Company:		PLM Latina.

	DECLARE
		@CRUDType		tinyint,
		@@SpecialityId	smallint

	SELECT 
		@CRUDType			= 1,
		@@SpecialityId		= 40

	EXECUTE dbo.plm_spCRUDSpecialities
		@CRUDType,			
		@@SpecialityId

 */ 

CREATE PROCEDURE dbo.plm_spCRUDSpecialities
(
	@CRUDType				tinyint,
	@SpecialityId			smallint
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

			SELECT [SpecialityId]
			  ,[SpecialityName]
			  ,[ShortName]
			  ,[Active]
			FROM [dbo].[Specialities]
			
			WHERE SpecialityId = @SpecialityId

		RETURN @@ROWCOUNT
		
	END

END
go