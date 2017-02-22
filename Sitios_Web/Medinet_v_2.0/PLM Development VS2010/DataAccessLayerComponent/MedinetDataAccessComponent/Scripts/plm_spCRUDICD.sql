IF OBJECT_ID('dbo.plm_spCRUDICD') IS NOT NULL
	DROP PROCEDURE dbo.plm_spCRUDICD


GO

/* 
	Author:			César Avila				 
	Object:			dbo.plm_spCRUDICD
	
	Description:	Perform the basic operations, or in other words CRUD operations.
					Where CRUD stands for:

						C - Create	{0}
						R - Read	{1}
						U - Update	{2}
						D - Delete	{3}
						

	

	EXECUTE dbo.plm_spCRUDICD 

 */ 

CREATE PROCEDURE [dbo].[plm_spCRUDICD]
(
	@CRUDType			tinyint,
    @icdId              int=NULL 
)
AS
BEGIN

    -- If @CRUDType belongs to {3}:
	IF (@CRUDType IN (3))
	BEGIN
		RAISERROR ('The current operation cannot be performed', 16, 1)
		RETURN -1
	END

    --C = {(Create, 0)}
	IF (@CRUDType IN (0))
	BEGIN
		RAISERROR ('The current operation cannot be performed', 16, 1)
		RETURN -1
	END

	
	-- R = {(Read, 1)}:
	IF (@CRUDType IN (1))
	BEGIN

	SELECT  
	        [ICDId]
	       ,[ParentId]
	       ,[ICDKey]
	       ,[SpanishDescription]
	       ,[EnglishDescription]
	       ,[Active]
	       
	FROM ICD
	
	WHERE [ICDId]=@icdId
	
	RETURN @@ROWCOUNT
		
	END
	
	-- U = {(Update, 2)}:
	IF (@CRUDType IN (2))
	BEGIN
		RAISERROR ('The current operation cannot be performed', 16, 1)
		RETURN -1
	END
	
	
	
END



