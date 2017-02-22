IF OBJECT_ID('dbo.plm_spCRUDStates') IS NOT NULL
	DROP PROCEDURE dbo.plm_spCRUDStates
go

/* 
	Author:			Juan Ramírez.				 
	Object:			dbo.plm_spCRUDStates
	
	Description:	Perform the basic operations, or in other words CRUD operations.
					Where CRUD stands for:

						C - Create	{0}
						R - Read	{1}
						U - Update	{2}
						D - Delete	{3}

	Company:		Thomson PLM.

	DECLARE
		@CRUDType		tinyint,
		@stateId	smallint

	SELECT 
		@CRUDType			= 1,
		@stateId		= 24

	EXECUTE dbo.plm_spCRUDStates
		@CRUDType,			
		@stateId

 */ 

CREATE PROCEDURE dbo.plm_spCRUDStates
(
	@CRUDType				tinyint,
	@stateId				int
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

		SELECT [StateId]
			  ,[CountryId]
			  ,[StateName]
			  ,[ShortName]
			  ,[Active]
		  FROM [dbo].[States]

		WHERE	[StateId] = @stateId

		RETURN @@ROWCOUNT
		
	END

END
