IF OBJECT_ID('dbo.plm_spGetStateOsMobileUserCodes') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetStateOsMobileUserCodes
go

/* 
	Author:			Juan Ramirez.				 
	Object:			dbo.plm_spGetStateOsMobileUserCodes
	
	Description:	Retrieve a State by Name and Country.

	Company:		PLM Latina.

	EXECUTE dbo.plm_spGetStates

 */ 

CREATE PROCEDURE dbo.plm_spGetStateOsMobileUserCodes
( 
	@osMobileId		TINYINT,
	@codeId			INT,
	@userId			INT
)
AS
BEGIN

	IF(@osMobileId IS NULL OR @codeId IS NULL OR @userId IS NULL)
	BEGIN
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
	END
	
	SELECT [StateId]
		  ,[OSMobileId]
		  ,[UserId]
		  ,[CodeId]
	FROM [dbo].[StateOSMobileUserCodes]

	WHERE [OSMobileId]	= @osMobileId AND
		  [CodeId]		= @codeId  AND 
		  [UserId]		= @userId  
		  

	RETURN @@ROWCOUNT

	
END
go