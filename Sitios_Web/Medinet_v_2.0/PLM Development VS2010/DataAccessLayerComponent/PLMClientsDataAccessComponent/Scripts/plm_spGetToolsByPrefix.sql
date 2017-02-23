IF OBJECT_ID('dbo.plm_spGetToolsByPrefix') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetToolsByPrefix
go

/* 
	Author:			Juan Ramírez / Ramiro Sánchez				 
	Object:			dbo.plm_spGetToolsByPrefix
	
	Description:	Retrieves all Tools by Country, target output, tool type and code prefix.

	Company:		PLM Latina.
	
	EXECUTE dbo.plm_spGetToolsByPrefix 1,1,1,14

 */ 

CREATE PROCEDURE dbo.plm_spGetToolsByPrefix
(
	@countryId		tinyint,
	@targetId		tinyint,
	@toolTypeId		tinyint,
	@prefixId		int
)
AS
BEGIN

	DECLARE 
		@parentId     int,
		@notes		  int

	Select @notes = Count(*)
	From ElectronicInformation ei
		Inner Join CountryTools ct On ei.ElectronicId = ct.ElectronicId
		Inner Join DistributionTools dt On(ct.ElectronicId = dt.ElectronicId)
			And ct.CountryId = dt.CountryId
	Where ei.Active = 1 And
		  dt.FinalDate > GetDate() And
		  ct.CountryId = @countryId And
		  dt.TargetId = @targetId And
		  ei.InfoTypeId	= @toolTypeId And
		  dt.PrefixId = @prefixId 

	IF(@notes = 0)	
	BEGIN
		
		Select @parentId = ParentId
		From CodePrefixes
		Where PrefixId = @prefixId

		IF(@parentId IS NOT NULL)
		BEGIN

			EXECUTE dbo.plm_spGetToolsByPrefix @countryId,@targetId,@toolTypeId,@parentId

		END
		ELSE
		BEGIN
			
			Select Distinct ct.CountryId, dt.prefixid, ei.infotypeid,
						ei.CompanyClientId,
						ei.ElectronicId As ToolId,
						dt.TargetId,
						ei.ElectronicTitle As ToolTitle,
						ei.ElectronicDescription As ToolDescription,
						ei.FileName,
						ei.PublishedDate,
						ei.Link As Url,
						dt.BaseUrl,
						Null As BannerId,
						Null As BannerName,
						Null As BannerUrl,
						dt.[Order] As ToolOrder
			From ElectronicInformation ei
				Inner Join CountryTools ct On ei.ElectronicId = ct.ElectronicId
				Inner Join DistributionTools dt On(ct.ElectronicId = dt.ElectronicId)
					And ct.CountryId = dt.CountryId
			Where ei.Active = 1 And
				dt.FinalDate > GetDate() And
				ct.CountryId = @countryId And
				dt.TargetId = @targetId And
				ei.InfoTypeId	= @toolTypeId And
				dt.PrefixId = @prefixId
			Order By dt.[Order]

			RETURN @@ROWCOUNT

		END

	END
	ELSE
	BEGIN

		Select Distinct ct.CountryId,
						ei.CompanyClientId,
						ei.ElectronicId As ToolId,
						dt.TargetId,
						ei.ElectronicTitle As ToolTitle,
						ei.ElectronicDescription As ToolDescription,
						ei.FileName,
						ei.PublishedDate,
						ei.Link As Url,
						dt.BaseUrl,
						Null As BannerId,
						Null As BannerName,
						Null As BannerUrl,
						dt.[Order] As ToolOrder
			From ElectronicInformation ei
				Inner Join CountryTools ct On ei.ElectronicId = ct.ElectronicId
				Inner Join DistributionTools dt On(ct.ElectronicId = dt.ElectronicId)
					And ct.CountryId = dt.CountryId
			Where ei.Active = 1 And
				dt.FinalDate > GetDate() And
				ct.CountryId = @countryId And
				dt.TargetId = @targetId And
				ei.InfoTypeId = @toolTypeId And
				dt.PrefixId = @prefixId
			Order By dt.[Order]

		RETURN @@ROWCOUNT
	END
END
go