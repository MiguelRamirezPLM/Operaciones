USE [PLMClients 20130715]
GO
/****** Object:  StoredProcedure [dbo].[plm_spCRUDTargetOutputs]    Script Date: 07/22/2013 12:06:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

/* 
	Author:			Pilar Nájera				 
	Object:			dbo.plm_spCRUDTargetOutputs
	
	Description:	Perform the basic operations, or in other words CRUD operations.
					Where CRUD stands for:

						C - Create	{0}
						R - Read	{1}
						U - Update	{2}
						D - Delete	{3}

	Company:		PLM Latina.

	DECLARE
		@CRUDType		tinyint,
		@targetId		tinyint



	EXECUTE dbo.plm_spCRUDTargetOutputs
		@CRUDType,			
		@targetId
		
 */ 

ALTER PROCEDURE [dbo].[plm_spCRUDTargetOutputs]
(
	@CRUDType				TINYINT,
	@targetId				TINYINT,
	@targetName				VARCHAR(40) = NULL,
	@active					BIT = NULL
)
AS
BEGIN

	-- If @CRUDType belongs to {0,2}, then the parameters shouldn't be null:
	IF (@CRUDType IN (0,2) AND (@targetName IS NULL OR @active IS NULL))
	BEGIN
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
	END

	-- C = {(CREATE, 0)}:
	IF (@CRUDType IN (0))
	BEGIN


		INSERT INTO [dbo].[TargetOutputs]
			([TargetName] 
			,[Active])                  
		 VALUES
			   (@targetName
			   ,@active)
	 
		RETURN SCOPE_IDENTITY()

	END

	-- R = {(Read, 1)}
	IF (@CRUDType IN (1))
	BEGIN
		
		SELECT [TargetId]
			,[TargetName] 
			,[Active]
		  FROM [dbo].[TargetOutputs]
		 WHERE [TargetId] = @targetId	

		RETURN @@ROWCOUNT
	END
	
	-- U = {(Update, 2)}
	IF (@CRUDType IN (2))
	BEGIN

		UPDATE [dbo].[TargetOutputs]
		   SET[TargetName] = @targetName
			,[Active] = @active
		 WHERE [TargetId] = @targetId	

		RETURN 0

	END	
END
