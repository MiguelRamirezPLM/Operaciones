IF OBJECT_ID('dbo.plm_spGetAgentsByPrefix') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetAgentsByPrefix
go

/* 
	Author:			Ramiro S�nchez				 
	Object:			dbo.plm_spGetAgentsByPrefix
	
	Description:	Get Agent information By Prefix.

	Company:		PLM Latina.
	
	EXECUTE dbo.plm_spGetAgentsByPrefix @prefix= '', @targetId = 3

 */ 

CREATE PROCEDURE dbo.plm_spGetAgentsByPrefix
(
	@targetId		tinyint = null,
	@prefix			varchar(30) = null
)
AS
BEGIN

	IF(@prefix IS NOT NULL AND @targetId IS NOT NULL)
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
		WHERE cp.PrefixName = @prefix
			and dcp.TargetId = @targetId
			And a.[Active] = 1

		RETURN @@ROWCOUNT	
	
	END
	
END
go