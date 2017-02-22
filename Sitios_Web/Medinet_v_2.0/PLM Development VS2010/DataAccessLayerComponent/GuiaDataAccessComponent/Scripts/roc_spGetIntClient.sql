IF OBJECT_ID('dbo.roc_spGetIntClient') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetIntClient
go

/* 
	Author:			Benjamín Castillo Arriaga.				 
	Object:			dbo.roc_spGetIntClient

	
	Description:	Retrieves a specific International Client.

	Company:		PLM Latina.

	

	EXECUTE dbo.roc_spGetIntClient @intClientId = 1

	

 */ 

CREATE PROCEDURE [dbo].[roc_spGetIntClient]
(
	@intClientId				int
)
AS
BEGIN

	-- The parameters shouldn't be null:
	IF (@intClientId IS NULL)
	BEGIN
	
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
		
	END
	
	Select IntClientId,
		   CompanyName,
		   ShortName,
		   Active
	From IntClients
	Where IntClientId = @intClientId
	
	

END