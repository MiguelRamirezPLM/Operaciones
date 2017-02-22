IF OBJECT_ID('dbo.plm_spGetAgents') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetAgents
go

/* 
	Author:			Ramiro Sánchez				 
	Object:			dbo.plm_spGetAgents
	
	Description:	Get Agent information By AgentId or by Branch.

	Company:		PLM Latina.
	
	EXECUTE dbo.plm_spGetAgents @agentId
	EXECUTE dbo.plm_spGetAgents @branchId
	EXECUTE dbo.plm_spGetAgents @zoneId = 3

 */ 

CREATE PROCEDURE dbo.plm_spGetAgents
(
	@agentId		int = null,
	@branchId		int = null,
	@zoneId			int = null
)
AS
BEGIN

	IF(@agentId IS NOT NULL AND @branchId IS NULL AND @zoneId IS NULL)
	BEGIN

		SELECT Distinct [AgentId]
			,[TypeId]
			,[FirstName]
			,[LastName]
			,[SecondLastName]
			,[Email]
			,[PhoneOne]
			,[PhoneTwo]
			,[Active]
		FROM [dbo].[Agents]
		WHERE [AgentId] = @agentId
			And [Active] = 1

		RETURN @@ROWCOUNT	
	
	END
	
	IF(@agentId IS NULL AND @branchId IS NOT NULL AND @zoneId IS NULL)
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
			INNER JOIN [ZoneBranches] zb ON a.[AgentId] = zb.[AgentId]
			INNER JOIN [CompanyClientBranches] ccb ON zb.[BranchId] = ccb.[BranchId]
		WHERE ccb.[BranchId] = @branchId
			And a.[Active] = 1

		RETURN @@ROWCOUNT	
	
	END
	IF(@agentId IS NULL AND @branchId IS NULL AND @zoneId IS NOT NULL)
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
			INNER JOIN [ZoneBranches] zb ON a.[AgentId] = zb.[AgentId]
		WHERE zb.[ZoneId] = @zoneId
			And a.[Active] = 1


		RETURN @@ROWCOUNT	
	
	END	
END
go