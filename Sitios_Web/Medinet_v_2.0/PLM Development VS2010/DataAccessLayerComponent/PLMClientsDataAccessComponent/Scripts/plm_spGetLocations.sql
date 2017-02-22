IF OBJECT_ID('dbo.plm_spGetLocations') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetLocations
go

/* 
	Author:			Ramiro Sánchez				 
	Object:			dbo.plm_spGetLocations
	
	Description:	Get Location information.

	Company:		PLM Latina.
	
	EXECUTE dbo.plm_spGetLocations @targetId = 5, @prefix='PLMOBSTCRIMEX', @zoneId = 10
	
 */ 

CREATE PROCEDURE dbo.plm_spGetLocations
(
	@targetId		tinyint = null,
	@prefix			varchar(30) = null,
	@zoneId			tinyint = null
)
AS
BEGIN
	
	IF(@targetId Is Not Null And @prefix Is Not Null And @zoneId Is Not Null)
	BEGIN
	
		SELECT Distinct l.[LocationId]
			,l.[LocationName]
			,l.[Active]
		FROM Locations l
			Inner Join ZoneLocations zl On l.LocationId = zl.LocationId
			Inner join ZoneLocationAgents zla On zl.ZoneId = zla.ZoneId
				And zl.LocationId = zla.LocationId
			Inner Join DistributionAgents da On zla.AgentId = da.AgentId
			Inner Join CodePrefixes cp On da.PrefixId = cp.PrefixId
		WHERE cp.PrefixName = @prefix
			And da.TargetId = @targetId
			And zl.ZoneId = @zoneId
			And l.Active = 1
		ORDER BY l.[LocationName]
	END
	
END
go