IF OBJECT_ID('dbo.plm_spGetEventsByPrefix') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetEventsByPrefix
go

/* 
	Author:			Ramiro Sánchez				 
	Object:			dbo.plm_spGetEventsByPrefix
	
	Description:	

	Company:		PLM Latina.
	
	EXECUTE dbo.plm_spGetEventsByPrefix @prefix = 'plmpevmex', @targetId = 3

 */ 

CREATE PROCEDURE dbo.plm_spGetEventsByPrefix
(
	@prefix				varchar(50) = null,
	@targetId			tinyint = null
)
AS
BEGIN

	-- The parameters shouldn't be null:
	IF (@prefix IS NULL OR @targetId IS NULL)
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
	FROM [Events] e
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
		WHERE e.Active = 1 AND
			  cp.PrefixName = @prefix AND
			  de.TargetId = @targetId AND
			  e.FinalDate >= GETDATE()

	RETURN @@ROWCOUNT
	
END
go