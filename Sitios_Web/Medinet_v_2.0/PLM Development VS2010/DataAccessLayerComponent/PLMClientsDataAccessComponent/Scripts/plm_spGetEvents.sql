IF OBJECT_ID('dbo.plm_spGetEvents') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetEvents
go

/* 
	Author:			Ramiro Sánchez				 
	Object:			dbo.plm_spGetEvents
	
	Description:	Get Events information By Type and By Date or by Speciality or by Profession.

	Company:		PLM Latina.
	
	EXECUTE dbo.plm_spGetEvents @typeId = 1
	EXECUTE dbo.plm_spGetEvents @typeId = 1, @professionId = 7
	EXECUTE dbo.plm_spGetEvents @typeId = 1, @specialityId = 40
	EXECUTE dbo.plm_spGetEvents @typeId = 1, @month = 7

 */ 

CREATE PROCEDURE dbo.plm_spGetEvents
(
	@typeId				tinyint = null,
	@professionId		tinyint = null,
	@specialityId		tinyint = null,
	@month				tinyint = null
)
AS
BEGIN

	-- The parameters shouldn't be null:
	IF (@typeId IS NULL)
	BEGIN
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
	END

	IF(@professionId IS NULL AND @specialityId IS NULL AND @month IS NULL)
	BEGIN

		SELECT e.[EventId]
			,e.[EventName]
			,e.[TypeId]
			,t.[TypeName]
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
		FROM Events e				
			INNER JOIN EventTypes t ON (e.TypeId = t.TypeId)
			LEFT JOIN ProfessionEvents pe ON (e.EventId = pe.EventId)
			LEFT JOIN Professions p ON (pe.ProfessionId = p.ProfessionId)
			LEFT JOIN SpecialityEvents se ON (e.EventId = se.EventId)
			LEFT JOIN Specialities s ON (se.SpecialityId = s.SpecialityId)
			LEFT JOIN EventAddresses ea ON (e.EventId = ea.EventId)
			LEFT JOIN Addresses a ON (ea.AddressId = a.AddressId)
			LEFT JOIN Countries c ON (a.CountryId = c.CountryId)
			LEFT JOIN States st ON (a.StateId = st.StateId)
		WHERE e.TypeId = @typeId
			AND FinalDate >= GETDATE()
		ORDER BY e.InitialDate

		RETURN @@ROWCOUNT	
	
	END
	
	IF(@professionId IS NOT NULL AND @specialityId IS NULL AND @month IS NULL)
	BEGIN

		SELECT e.[EventId]
			,e.[EventName]
			,e.[TypeId]
			,t.[TypeName]
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
		FROM Events e				
			INNER JOIN EventTypes t ON (e.TypeId = t.TypeId)
			LEFT JOIN ProfessionEvents pe ON (e.EventId = pe.EventId)
			LEFT JOIN Professions p ON (pe.ProfessionId = p.ProfessionId)
			LEFT JOIN SpecialityEvents se ON (e.EventId = se.EventId)
			LEFT JOIN Specialities s ON (se.SpecialityId = s.SpecialityId)
			LEFT JOIN EventAddresses ea ON (e.EventId = ea.EventId)
			LEFT JOIN Addresses a ON (ea.AddressId = a.AddressId)
			LEFT JOIN Countries c ON (a.CountryId = c.CountryId)
			LEFT JOIN States st ON (a.StateId = st.StateId)
		WHERE p.ProfessionId = @professionId 
			AND e.TypeId = @typeId
			AND FinalDate >= GETDATE()
		ORDER BY e.InitialDate

		RETURN @@ROWCOUNT	
	
	END

	IF(@professionId IS NULL AND @specialityId IS NOT NULL AND @month IS NULL)
	BEGIN

		SELECT e.[EventId]
			,e.[EventName]
			,e.[TypeId]
			,t.[TypeName]
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
		FROM Events e				
			INNER JOIN EventTypes t ON (e.TypeId = t.TypeId)
			LEFT JOIN ProfessionEvents pe ON (e.EventId = pe.EventId)
			LEFT JOIN Professions p ON (pe.ProfessionId = p.ProfessionId)
			LEFT JOIN SpecialityEvents se ON (e.EventId = se.EventId)
			LEFT JOIN Specialities s ON (se.SpecialityId = s.SpecialityId)
			LEFT JOIN EventAddresses ea ON (e.EventId = ea.EventId)
			LEFT JOIN Addresses a ON (ea.AddressId = a.AddressId)
			LEFT JOIN Countries c ON (a.CountryId = c.CountryId)
			LEFT JOIN States st ON (a.StateId = st.StateId)
		WHERE s.SpecialityId = @specialityId 
			AND e.TypeId = @typeId
			AND FinalDate >= GETDATE()
		ORDER BY e.InitialDate 

		RETURN @@ROWCOUNT
	
	END

	IF(@professionId IS NULL AND @specialityId IS NULL AND @month IS NOT NULL)
	BEGIN

		SELECT e.[EventId]
			,e.[EventName]
			,e.[TypeId]
			,t.[TypeName]
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
		FROM Events e				
			INNER JOIN EventTypes t ON (e.TypeId = t.TypeId)
			LEFT JOIN ProfessionEvents pe ON (e.EventId = pe.EventId)
			LEFT JOIN Professions p ON (pe.ProfessionId = p.ProfessionId)
			LEFT JOIN SpecialityEvents se ON (e.EventId = se.EventId)
			LEFT JOIN Specialities s ON (se.SpecialityId = s.SpecialityId)
			LEFT JOIN EventAddresses ea ON (e.EventId = ea.EventId)
			LEFT JOIN Addresses a ON (ea.AddressId = a.AddressId)
			LEFT JOIN Countries c ON (a.CountryId = c.CountryId)
			LEFT JOIN States st ON (a.StateId = st.StateId)
		WHERE FinalDate >= GETDATE()
			AND 
			(MONTH(InitialDate) = @month OR MONTH(FinalDate) = @month) 
			AND e.TypeId = @typeId
		ORDER BY e.InitialDate

		RETURN @@ROWCOUNT
	END	
	
END
go