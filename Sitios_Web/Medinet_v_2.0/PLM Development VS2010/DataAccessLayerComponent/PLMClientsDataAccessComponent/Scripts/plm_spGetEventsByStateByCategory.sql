IF OBJECT_ID('dbo.plm_spGetEventsByStateByCategory') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetEventsByStateByCategory
go

/* 
	Author:			Ramiro Sánchez				 
	Object:			dbo.plm_spGetEventsByStateByCategory
	
	Description:	

	Company:		PLM Latina.
	
	EXECUTE dbo.plm_spGetEventsByStateByCategory @prefix = '', @targetId = 3, @stateId = 7, @categoryId = 1

 */ 

CREATE PROCEDURE dbo.plm_spGetEventsByStateByCategory
(
	@prefix				varchar(50) = null,
	@targetId			tinyint = null,
	@stateId			int = null,
	@categoryId			tinyint = null
)
AS
BEGIN

	-- The parameters shouldn't be null:
	IF (@prefix IS NULL OR @targetId IS NULL OR @stateId IS NULL OR @categoryId IS NULL)
	BEGIN
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
	END

	
	SELECT distinct e.[EventId]
		,e.[EventName]
		,e.[TypeId]
		,et.[TypeName]
		,e.[Site]
		,e.[InitialDate]
		,e.[FinalDate]
		,e.[Organizer]
		,e.[WebPage]
		,e.[Active]
		,p.[ProfessionName]
		,s.[SpecialityName]
		,a.[AddressId]
		,a.[Street]
		,a.[InternalNumber]
		,a.[Suburb]
		,a.[ZipCode]
		,a.[Location]
		,a.[CountryId]
		,c.[CountryName]
		,a.[StateId]
		,st.[StateName]
		,a.[Lada]
		,a.[PhoneOne]
		,a.[PhoneTwo]
		,a.[Ext]
		,a.[Latitude]
		,a.[Longitude]
	FROM EventCategories ec
			Inner Join [Events] e on ec.CategoryId = e.CategoryId
			Inner Join EventTypes et on e.TypeId = et.TypeId
			Inner Join DistributionEvents de on e.EventId = de.EventId
			Inner Join EventAddresses ea on e.EventId = ea.EventId
			Inner Join Addresses a on ea.AddressId = a.AddressId
			Inner Join CodePrefixes cp on de.PrefixId = cp.PrefixId
			Inner Join Countries c on a.CountryId = c.CountryId
			Inner Join States st on a.StateId = st.StateId
			Left Join ProfessionEvents pe on e.EventId = pe.EventId
			Left Join Professions p on pe.ProfessionId = p.ProfessionId
			Left Join SpecialityEvents se on e.EventId = se.EventId
			Left Join Specialities s on se.SpecialityId = s.SpecialityId
		WHERE ec.Active = 1 AND 
			  e.Active = 1 AND
			  cp.PrefixName = @prefix AND
			  de.TargetId = @targetId AND
			  a.StateId = @stateId AND
			  ec.CategoryId = @categoryId AND
			  e.FinalDate >= GETDATE()

	RETURN @@ROWCOUNT
	
END
go