IF OBJECT_ID('dbo.plm_spGetZones') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetZones
go

/* 
	Author:			Ramiro Sánchez				 
	Object:			dbo.plm_spGetZones
	
	Description:	Get Zone information.

	Company:		PLM Latina.
	
	EXECUTE dbo.plm_spGetZones
	EXECUTE dbo.plm_spGetZones @targetId = 5, @prefix='PLMOBSTCRIMEX'
	
 */ 

CREATE PROCEDURE dbo.plm_spGetZones
(
	@targetId		tinyint = null,
	@prefix			varchar(30) = null
)
AS
BEGIN
	
	IF(@targetId Is Null And @prefix Is Null)
	BEGIN
	
		SELECT Distinct z.[ZoneId]
			,z.[ZoneName]
			,z.[Active]
		FROM Zones z
			Inner Join ZoneBranches zb ON (z.ZoneId = zb.ZoneId)
			Inner Join Agents a ON (zb.AgentId = a.AgentId)
		WHERE z.Active = 1
			And a.Active = 1
		ORDER BY z.[ZoneName]

		RETURN @@ROWCOUNT	
	END
	
	IF(@targetId Is Not Null And @prefix Is Not Null)
	BEGIN
	
		SELECT Distinct z.[ZoneId]
			,z.[ZoneName]
			,z.[Active]
		FROM Zones z
			Inner Join ZoneLocations zl On z.ZoneId = zl.ZoneId
			Inner join ZoneLocationAgents zla On zl.ZoneId = zla.ZoneId
				And zl.LocationId = zla.LocationId
			Inner Join DistributionAgents da On zla.AgentId = da.AgentId
			Inner Join CodePrefixes cp On da.PrefixId = cp.PrefixId
		WHERE cp.PrefixName = @prefix
			And da.TargetId = @targetId
			And z.Active = 1
		ORDER BY z.[ZoneName]
	END
	
END
go