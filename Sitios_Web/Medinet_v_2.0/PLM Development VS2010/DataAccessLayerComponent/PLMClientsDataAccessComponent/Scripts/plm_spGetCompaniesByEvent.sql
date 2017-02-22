IF OBJECT_ID('dbo.plm_spGetCompaniesByEvent') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetCompaniesByEvent
go

/* 
	Author:			Ramiro Sánchez				 
	Object:			dbo.plm_spGetCompaniesByEvent
	
	Description:	Get Physical Activities By Calculator Result.

	Company:		PLM Latina.
	
	EXECUTE dbo.plm_spGetCompaniesByEvent @eventId = 30

 */ 

CREATE PROCEDURE dbo.plm_spGetCompaniesByEvent
(
	@eventId		int = Null
)
AS
BEGIN

	SELECT Distinct c.CompanyClientId, c.CompanyName, d.CompanyImage, d.BaseUrl
	FROM CompanyClients c
		Inner Join CompanyEvents d on c.CompanyClientId = d.CompanyClientId
	WHERE c.Active = 1
		And d.EventId = @eventId
	Order By c.CompanyName

	RETURN @@ROWCOUNT

	
END
go