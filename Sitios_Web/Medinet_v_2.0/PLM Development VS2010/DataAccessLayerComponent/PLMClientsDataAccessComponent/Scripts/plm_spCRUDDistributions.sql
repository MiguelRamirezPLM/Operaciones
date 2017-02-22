USE [PLMClients 20130715]
GO
/****** Object:  StoredProcedure [dbo].[plm_spCRUDDistributions]    Script Date: 07/22/2013 12:06:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

/* 
	Author:			Pilar Nájera				 
	Object:			dbo.plm_spCRUDDistributions
	
	Description:	Perform the basic operations, or in other words CRUD operations.
					Where CRUD stands for:

						C - Create	{0}
						R - Read	{1}
						U - Update	{2}
						D - Delete	{3}

	Company:		PLM Latina.

	DECLARE
		@CRUDType			tinyint,
		@distributionId			int



	EXECUTE dbo.plm_spCRUDDistributions
		@CRUDType,			
		@distributionId
		
 */ 

ALTER PROCEDURE [dbo].[plm_spCRUDDistributions]
(
	@CRUDType				TINYINT,
	@distributionId			INT,
	@distributionName		VARCHAR(150) = NULL,
	@description			VARCHAR(150) = NULL,
	@version				VARCHAR(25) = NULL,
	@lastUpdate				DATETIME = NULL,
	@active					BIT = NULL
)
AS
BEGIN

	-- If @CRUDType belongs to {0,2}, then the parameters shouldn't be null:
	IF (@CRUDType IN (0,2) AND (@distributionName IS NULL OR @description IS NULL OR @version IS NULL 
								OR @lastUpdate IS NULL OR @active IS NULL))
	BEGIN
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
	END

	-- C = {(CREATE, 0)}:
	IF (@CRUDType IN (0))
	BEGIN


		INSERT INTO [dbo].[Distributions]
			([DistributionName] 
			,[Description] 
			,[Version] 
			,[LastUpdate]
			,[Active])                  
		 VALUES
			   (@distributionName
			   ,@description
			   ,@version
			   ,@lastUpdate
			   ,@active)
	 
		RETURN SCOPE_IDENTITY()

	END

	-- R = {(Read, 1)}
	IF (@CRUDType IN (1))
	BEGIN
		
		SELECT [DistributionId]
			,[DistributionName] 
			,[Description] 
			,[Version] 
			,[LastUpdate]
			,[Active]
		  FROM [dbo].[Distributions]
		 WHERE [DistributionId] = @distributionId			

		RETURN @@ROWCOUNT
	END
	
	-- U = {(Update, 2)}
	IF (@CRUDType IN (2))
	BEGIN

		UPDATE [dbo].[Distributions]
		   SET[DistributionName] = @distributionName
			,[Description] = @description
			,[Version] = @version
			,[LastUpdate] = @lastUpdate
			,[Active] = @active
		 WHERE [DistributionId] = @distributionId	

		RETURN 0

	END	
END
