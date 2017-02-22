IF OBJECT_ID('dbo.plm_spGetOtherEventStates') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetOtherEventStates
go

/* 
	Author:			Ramiro Sánchez			 
	Object:			dbo.plm_spGetOtherEventStates
	
	Description:	

	Company:		PLM Latina.

	EXECUTE dbo.plm_spGetOtherEventStates @prefix = '', @targetId = 3, @stateId = 7

 */ 

CREATE PROCEDURE dbo.plm_spGetOtherEventStates
(
	@prefix			VARCHAR(50) = null,
	@targetId		TINYINT = null,
	@stateId		INT = null
)
AS
BEGIN
	
	IF(@prefix Is Not Null And @targetId Is Not Null And @stateId Is Not Null)
	BEGIN
	
		SELECT Distinct	s.StateId,
			s.StateName,
			s.CountryId,
			s.ShortName,
			s.Active
		FROM States s
			Inner Join Addresses a on s.StateId = a.StateId
			Inner Join EventAddresses ea on a.AddressId = ea.AddressId
			Inner Join [Events] e on ea.EventId = e.EventId
			Inner Join DistributionEvents de on e.EventId = de.EventId
			Inner Join CodePrefixes cp on de.PrefixId = cp.PrefixId
		WHERE e.Active = 1 AND
			  cp.PrefixName = @prefix AND
			  de.TargetId = @targetId AND
			  a.StateId <> @stateId
		Order By s.StateName
	
		RETURN @@ROWCOUNT
	
	END
	
END
go