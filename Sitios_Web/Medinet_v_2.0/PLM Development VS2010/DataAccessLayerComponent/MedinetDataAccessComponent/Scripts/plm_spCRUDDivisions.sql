IF OBJECT_ID('dbo.plm_spCRUDDivisions') IS NOT NULL
	DROP PROCEDURE dbo.plm_spCRUDDivisions
go

/* 
	Author:			Ramiro Sánchez				 
	Object:			dbo.plm_spCRUDDivisions
	
	Description:	Perform the basic operations, or in other words CRUD operations.
					Where CRUD stands for:

						C - Create	{0}
						R - Read	{1}
						U - Update	{2}
						D - Delete	{3}

	Company:		PLM Latina

 */ 

CREATE PROCEDURE dbo.plm_spCRUDDivisions
(
	@CRUDType		tinyint,
	@divisionId		int,
	@parentId		int = Null,
	@laboratoryId	int = Null,
	@countryId		int = Null,
	@description	varchar(300) = Null,
	@shortName		varchar(100) = Null,
	@active			bit = Null
)
AS
BEGIN

	-- If @CRUDType belongs to {0,3}:
	IF (@CRUDType IN (0,3))
	BEGIN	
		RAISERROR ('The current operation cannot be performed', 16, 1)
		RETURN -1
	END

	-- R = {(Read, 1)}:
	IF (@CRUDType IN (1))
	BEGIN

		SELECT [DivisionId]
			  ,[LaboratoryId]
			  ,[CountryId]
			  ,[Description]
			  ,[ShortName]
			  ,[Active]
		  FROM [dbo].[Divisions]

		WHERE	DivisionId = @divisionId

		RETURN @@ROWCOUNT
		
	END

	-- U = {(Update, 2)}
	IF (@CRUDType IN (2))
	BEGIN
	
		UPDATE Divisions
			SET ParentId = @parentId,
				LaboratoryId = @laboratoryId,
				CountryId = @countryId,
				[Description] = @description,
				ShortName = @shortName,
				Active = @active
		Where DivisionId = @divisionId

		RETURN 0

	END
END
go