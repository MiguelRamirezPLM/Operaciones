IF OBJECT_ID('dbo.plm_spCRUDPSE_TrackingAttributes') IS NOT NULL
	DROP PROCEDURE dbo.plm_spCRUDPSE_TrackingAttributes
go

/* 
	Author:			Juan Ramírez.				 
	Object:			dbo.plm_spCRUDPSE_TrackingAttributes
	
	Description:	Perform the basic operations, or in other words CRUD operations.
					Where CRUD stands for:

						C - Create	{0}
						R - Read	{1}
						U - Update	{2}
						D - Delete	{3}

	Company:		Thomson PLM.

	DECLARE
		@CRUDType		tinyint,
		@crossIndexId			int

	SELECT 
		@CRUDType			= 1,
		@crossIndexId				= 1

	EXECUTE dbo.plm_spCRUDPSE_TrackingAttributes
		@CRUDType,			
		@trackId

 */ 

CREATE PROCEDURE dbo.plm_spCRUDPSE_TrackingAttributes
(
	@CRUDType				tinyint,
	@trackId				int = null,
	@attributeId			int = null
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
								@attributeId IS NULL)))
	BEGIN
	
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
		
	END


	-- R = {(Create, 0)}:
	IF (@CRUDType IN (0))
	BEGIN

		INSERT INTO [dbo].[PSE_TrackingAttributes]
           ([TrackId]
           ,[AttributeId])
		VALUES
           (@trackId
           ,@attributeId)

		RETURN 0
		
	END

END
go