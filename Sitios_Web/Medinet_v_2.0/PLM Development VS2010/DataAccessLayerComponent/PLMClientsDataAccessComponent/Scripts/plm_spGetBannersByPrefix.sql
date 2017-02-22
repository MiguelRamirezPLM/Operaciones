IF OBJECT_ID('dbo.plm_spGetBannersByPrefix') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetBannersByPrefix
go

/* 
	Author:			Juan Ramírez / Ramiro Sánchez				 
	Object:			dbo.plm_spGetBannersByPrefix
	
	Description:	Retrieves all Banners by Country, target output and code prefix.

	Company:		PLM Latina.
	
	EXECUTE dbo.plm_spGetBannersByPrefix 1,1,14

 */ 

CREATE PROCEDURE dbo.plm_spGetBannersByPrefix
(
	@countryId		tinyint,
	@targetId		tinyint,
	@prefixId		int
)
AS
BEGIN
	
	DECLARE 
		@parentId     int,
		@banners	  int

	Select @banners = Count(*)
	From ElectronicInformation ei
		Inner Join CountryTools ct On ei.ElectronicId = ct.ElectronicId
		Inner Join DistributionTools dt On(ct.ElectronicId = dt.ElectronicId)
			And ct.CountryId = dt.CountryId
	Where ei.Active = 1 And
		  dt.FinalDate > GetDate() And
		  ct.CountryId = @countryId And
		  dt.TargetId = @targetId And
		  ei.InfoTypeId	= 4 And
		  dt.PrefixId = @prefixId

	IF(@banners = 0)
	BEGIN
			
		Select @parentId = ParentId
		From CodePrefixes
		Where PrefixId = @prefixId

		IF(@parentId IS NOT NULL)
		BEGIN

			EXECUTE dbo.plm_spGetBannersByPrefix @countryId,@targetId,@parentId	

		END
		ELSE
		BEGIN

			Select Distinct ct.CountryId,
				ei.CompanyClientId,
				ei.ElectronicId As BannerId,
				dt.TargetId,
				ei.ElectronicDescription As BannerDescription,
				ei.FileName,
				dt.BaseUrl,
				dt.[Order] As BannerOrder,
				ei.Link As Url
				
			From ElectronicInformation ei
				Inner Join CountryTools ct On ei.ElectronicId = ct.ElectronicId
				Inner Join DistributionTools dt On(ct.ElectronicId = dt.ElectronicId)
					And ct.CountryId = dt.CountryId
			Where ei.Active = 1 And
				dt.FinalDate > GetDate() And
				ct.CountryId = @countryId And
				dt.TargetId = @targetId And
				ei.InfoTypeId	= 4 And
				dt.PrefixId = @prefixId
			Order By BannerOrder

			RETURN @@ROWCOUNT				

		END

	END
	ELSE
	BEGIN
			
		Select Distinct ct.CountryId,
			ei.CompanyClientId,
			ei.ElectronicId As BannerId,
			dt.TargetId,
			ei.ElectronicDescription As BannerDescription,
			ei.FileName,
			dt.BaseUrl,
			dt.[Order] As BannerOrder,
			ei.Link As Url
				
		From ElectronicInformation ei
			Inner Join CountryTools ct On ei.ElectronicId = ct.ElectronicId
			Inner Join DistributionTools dt On(ct.ElectronicId = dt.ElectronicId)
				And ct.CountryId = dt.CountryId
		Where ei.Active = 1 And
			dt.FinalDate > GetDate() And
			ct.CountryId = @countryId And
			dt.TargetId = @targetId And
			ei.InfoTypeId	= 4 And
			dt.PrefixId = @prefixId
		Order By BannerOrder

	RETURN @@ROWCOUNT
		
	END

END
go