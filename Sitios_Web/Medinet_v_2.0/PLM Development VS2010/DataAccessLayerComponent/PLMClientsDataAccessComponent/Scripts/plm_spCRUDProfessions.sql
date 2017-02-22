IF OBJECT_ID('dbo.plm_spCRUDProfessions') IS NOT NULL
	DROP PROCEDURE dbo.plm_spCRUDProfessions
go

/* 
	Author:			Juan Ramírez.				 
	Object:			dbo.plm_spCRUDProfessions
	
	Description:	Perform the basic operations, or in other words CRUD operations.
					Where CRUD stands for:

						C - Create	{0}
						R - Read	{1}
						U - Update	{2}
						D - Delete	{3}

	Company:		Thomson PLM.

	DECLARE
		@CRUDType		tinyint,
		@professionId	smallint

	SELECT 
		@CRUDType			= 1,
		@professionId		= 24

	EXECUTE dbo.plm_spCRUDProfessions
		@CRUDType,			
		@professionId

 */ 

CREATE PROCEDURE dbo.plm_spCRUDProfessions
(
	@CRUDType				tinyint,

	@professionId			smallint
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

		SELECT [ProfessionId]
			,[ParentId]
			,[ProfessionName]
			,[ReqProfessionalLicense]
			,[Active]
		  FROM [dbo].[Professions]

		WHERE	[ProfessionId] = @professionId

		RETURN @@ROWCOUNT
		
	END

END
go