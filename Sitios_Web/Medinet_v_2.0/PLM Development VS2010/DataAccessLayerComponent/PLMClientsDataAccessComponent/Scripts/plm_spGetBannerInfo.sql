IF OBJECT_ID('dbo.plm_spGetBannerInfo') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetBannerInfo
go

/* 
	Author:			Beatriz Vázquez / Ramiro Sánchez
	Object:			dbo.plm_spGetBannerInfo
	
	Description:	Get Banners By TargetOutput an Edition.

	Company:		PLM Latina.

	EXECUTE dbo.plm_spGetBannerInfo @isbn = 'DEF 58', @targetid=1
	EXECUTE dbo.plm_spGetBannerInfo @isbn = 'DEF 57', @targetid=4

 */ 

CREATE PROCEDURE dbo.plm_spGetBannerInfo
(
	@isbn			varchar(20),
	@targetId		tinyint
)
AS
BEGIN

	Select  Distinct ei.ElectronicId,
			ei.InfoTypeId,
			ei.ElectronicDescription,
			ei.FileName,
			ei.Active, 
			dt.BaseUrl
	From ElectronicInformation ei
				Inner Join CountryTools ct On ei.ElectronicId = ct.ElectronicId
				Inner Join DistributionTools dt On(ct.ElectronicId = dt.ElectronicId)
					And ct.CountryId = dt.CountryId
				Inner Join Editions e On(ct.CountryId = e.CountryId)
			Where ei.Active = 1 And
				dt.FinalDate > GetDate() And
				e.ISBN = @isbn And
				dt.TargetId = @targetId And
				ei.InfoTypeId	= 4
	
		RETURN @@ROWCOUNT
END
go