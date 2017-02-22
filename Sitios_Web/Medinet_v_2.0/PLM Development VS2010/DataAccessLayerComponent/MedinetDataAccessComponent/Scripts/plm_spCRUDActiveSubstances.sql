IF OBJECT_ID('dbo.plm_spCRUDActiveSubstances') IS NOT NULL
	DROP PROCEDURE dbo.plm_spCRUDActiveSubstances
go

/* 
	Author:			Ramiro Sánchez				 
	Object:			dbo.plm_spCRUDActiveSubstances
	
	Description:	Perform the basic operations, or in other words CRUD operations.
					Where CRUD stands for:

						C - Create	{0}
						R - Read	{1}
						U - Update	{2}
						D - Delete	{3}

	Company:		PLM Latina

	EXECUTE dbo.plm_spCRUDActiveSubstances 

 */ 

CREATE PROCEDURE dbo.plm_spCRUDActiveSubstances
(
	@CRUDType				tinyint,
	@activeSubstanceId		int=NULL,
	@substanceKey			varchar(8) = NULL,
	@description			varchar(200)=NULL,
	@englishDescription		varchar(200) = NULL,
	@enunciative			bit=NULL,
	@active					bit=NULL
)
AS
BEGIN

	-- If @CRUDType belongs to {0, 2, 3}:
	IF (@CRUDType IN (3))
	BEGIN
		RAISERROR ('The current operation cannot be performed', 16, 1)
		RETURN -1
	END

	-- C = {(Create, 0)}
	IF (@CRUDType IN (0))
	BEGIN
		
		INSERT INTO [dbo].[ActiveSubstances]
           ([SubstanceKey]
           ,[Description]
           ,[EnglishDescription]
           ,[Active]
           ,[Enunciative])
		VALUES
           (@substanceKey
           ,@description
           ,@englishDescription
           ,@active
           ,@enunciative)

		RETURN SCOPE_IDENTITY()

	END

	-- R = {(Read, 1)}:
	IF (@CRUDType IN (1))
	BEGIN

		SELECT [ActiveSubstanceId]
			  ,[Description]
			  ,[EnglishDescription]
			  ,[Active]
			  ,[Enunciative]
		FROM [dbo].[ActiveSubstances]
		WHERE [Active] = 1 And [ActiveSubstanceId] = @activeSubstanceId		

		RETURN @@ROWCOUNT
	END

	-- U = {(Update, 2)}:
	IF (@CRUDType IN (2))
	BEGIN

		UPDATE [dbo].[ActiveSubstances]
			SET [SubstanceKey] = @substanceKey
				,[Description] = @description
				,[EnglishDescription] = @englishDescription
				,[Enunciative] = @enunciative
				,[Active] = @active
		WHERE [ActiveSubstanceId] = @activeSubstanceId		

		RETURN 0

	END

END
go