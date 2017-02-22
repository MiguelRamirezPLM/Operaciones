IF OBJECT_ID('dbo.plm_spGetAgentsByZoneByLocation') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetAgentsByZoneByLocation
go

/* 
	Author:			Ramiro Sánchez				 
	Object:			dbo.plm_spGetAgentsByZoneByLocation
	
	Description:	Get Agent information By Prefix.

	Company:		PLM Latina.
	
	EXECUTE dbo.plm_spGetAgentsByZoneByLocation @prefix= 'PLMOBSTCRIMEX', @targetId = 5, @zoneId = 10, @locationId = 5

 */ 

CREATE PROCEDURE dbo.plm_spGetAgentsByZoneByLocation
(
	@targetId		tinyint = null,
	@prefix			varchar(30) = null,
	@zoneId			tinyint = null,
	@locationId		int = null
)
AS
BEGIN

	IF(@prefix IS NOT NULL AND @targetId IS NOT NULL AND @zoneId IS NOT NULL AND @locationId IS NOT NULL)
	BEGIN

		SELECT Distinct a.[AgentId]
			,a.[TypeId]
			,a.[FirstName]
			,a.[LastName]
			,a.[SecondLastName]
			,a.[Email]
			,a.[PhoneOne]
			,a.[PhoneTwo]
			,a.[Active]
		FROM [dbo].[Agents] a
		INNER JOIN DistributionAgents da on a.AgentId = da.AgentId
		INNER JOIN DistributionCodePrefixes dcp on da.DistributionId = dcp.DistributionId
			and da.PrefixId = dcp.PrefixId
			and da.TargetId = dcp.TargetId
		INNER JOIN CodePrefixes cp on dcp.PrefixId = cp.PrefixId
		INNER JOIN ZoneLocationAgents za On a.AgentId = za.AgentId
		WHERE cp.PrefixName = @prefix
			And dcp.TargetId = @targetId
			And za.ZoneId = @zoneId
			And za.LocationId = @locationId 
			And a.[Active] = 1

		RETURN @@ROWCOUNT	
	
	END
	
END
go