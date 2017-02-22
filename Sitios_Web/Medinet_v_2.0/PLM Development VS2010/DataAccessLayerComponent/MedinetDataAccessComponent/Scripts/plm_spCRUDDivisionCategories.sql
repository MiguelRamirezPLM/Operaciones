IF OBJECT_ID('dbo.plm_spCRUDDivisionCategories') IS NOT NULL
	DROP PROCEDURE dbo.plm_spCRUDDivisionCategories
go

/* 
	Author:			Ramiro Sánchez				 
	Object:			dbo.plm_spCRUDDivisionCategories
	
	Description:	Perform the basic operations, or in other words CRUD operations.
					Where CRUD stands for:

						C - Create	{0}
						R - Read	{1}
						U - Update	{2}
						D - Delete	{3}

	Company:		PLM Latina

 */ 

CREATE PROCEDURE dbo.plm_spCRUDDivisionCategories
(
	@CRUDType				tinyint,
	@divisionId				int,
	@categoryId				int
)
AS
BEGIN

	DECLARE @error int

	-- C = {(Create, 0)}
	IF (@CRUDType IN (0))
	BEGIN
	
		INSERT INTO [dbo].[DivisionCategories]
           ([DivisionId]
           ,[CategoryId])
		VALUES
           (@divisionId
           ,@categoryId)

		RETURN 0

	END
END
go