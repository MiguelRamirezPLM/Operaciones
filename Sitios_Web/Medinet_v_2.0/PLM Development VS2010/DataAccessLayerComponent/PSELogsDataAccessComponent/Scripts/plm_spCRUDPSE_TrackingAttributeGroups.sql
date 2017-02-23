IF OBJECT_ID('dbo.plm_spCRUDPSE_TrackingAttributeGroups') IS NOT NULL
	DROP PROCEDURE dbo.plm_spCRUDPSE_TrackingAttributeGroups
go

/* 
	Author:			Ramiro Sánchez.				 
	Object:			dbo.plm_spCRUDPSE_TrackingAttributeGroups
	
	Description:	Perform the basic operations, or in other words CRUD operations.
					Where CRUD stands for:

						C - Create	{0}
						R - Read	{1}
						U - Update	{2}
						D - Delete	{3}

	Company:		PLM Latina.

 */ 

CREATE PROCEDURE dbo.plm_spCRUDPSE_TrackingAttributeGroups
(
	@CRUDType				tinyint,
	@trackId				int = null,
	@attributeGroupId		int = null
)
AS
BEGIN

	-- If @CRUDType belongs to {0, 2, 3}:
	IF (@CRUDType IN (1, 2, 3))
	BEGIN
	
		RAISERROR ('The current operation cannot be performed', 16, 1)
		RETURN -1
		
	END

	-- If @CRUDType belongs to {0}, then the parameters shouldn't be null:
	IF (@CRUDType IN (0) AND ( (@trackId IS NULL OR
								@attributeGroupId IS NULL)))
	BEGIN
	
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
		
	END

	-- R = {(Create, 0)}:
	IF (@CRUDType IN (0))
	BEGIN

		INSERT INTO [dbo].[PSE_TrackingAttributeGroups]
           ([TrackId]
           ,[AttributeGroupId])
		VALUES
           (@trackId
           ,@attributeGroupId)

		RETURN 0
		
	END

END
go