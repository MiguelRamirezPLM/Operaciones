IF OBJECT_ID('dbo.plm_spGetResidenceLevels') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetResidenceLevels
go

/* 
	Author:			Ramiro Sánchez				 
	Object:			dbo.plm_spGetResidenceLevels
	
	Description:	Get Residence level information.

	Company:		PLM Latina.
	
	EXECUTE dbo.plm_spGetResidenceLevels @residenceId = 1
	EXECUTE dbo.plm_spGetResidenceLevels @residenceKey = 'R1'
	EXECUTE dbo.plm_spGetResidenceLevels @specialityId = 40
	EXECUTE dbo.plm_spGetResidenceLevels @clientId = 40

 */ 

CREATE PROCEDURE dbo.plm_spGetResidenceLevels
(
	@residenceId		int = null,
	@residenceKey		varchar(5) = null,
	@specialityId		smallint = null,
	@clientId			int = null
)
AS
BEGIN

	IF(@residenceId IS NOT NULL AND @residenceKey IS NULL AND @specialityId IS NULL AND @clientId IS NULL)
	BEGIN
		SELECT Distinct [ResidenceId]
			,[ResidenceKey]
			,[ResidenceName]
			,[Active]
		FROM [dbo].[ResidenceLevels]
		WHERE [ResidenceId] = @residenceId

		RETURN @@ROWCOUNT	
	END
	
	IF(@residenceId IS NULL AND @residenceKey IS NOT NULL AND @specialityId IS NULL AND @clientId IS NULL)
	BEGIN
		SELECT Distinct [ResidenceId]
			,[ResidenceKey]
			,[ResidenceName]
			,[Active]
		FROM [dbo].[ResidenceLevels]
		WHERE [ResidenceKey] = @residenceKey

		RETURN @@ROWCOUNT
	END

	IF(@residenceId IS NULL AND @residenceKey IS NULL AND @specialityId IS NOT NULL AND @clientId IS NULL)
	BEGIN
		SELECT Distinct rl.[ResidenceId]
			,rl.[ResidenceKey]
			,rl.[ResidenceName]
			,rl.[Active]
		FROM [dbo].[ResidenceLevels] rl
			Inner Join SpecialityResidences sr On rl.ResidenceId = sr.ResidenceId
			Inner Join ProfessionSpecialities ps On sr.SpecialityId = ps.SpecialityId
			Inner Join Specialities s On ps.SpecialityId = s.SpecialityId
		WHERE s.SpecialityId = @specialityId

		RETURN @@ROWCOUNT
	END

	IF(@residenceId IS NULL AND @residenceKey IS NULL AND @specialityId IS NULL AND @clientId IS NOT NULL)
	BEGIN
		SELECT Distinct rl.[ResidenceId]
			,rl.[ResidenceKey]
			,rl.[ResidenceName]
			,rl.[Active]
		FROM [dbo].[ResidenceLevels] rl
			Inner Join ResidenceClients rc On rl.ResidenceId = rc.ResidenceId
		WHERE rc.ClientId = @clientId

		RETURN @@ROWCOUNT
	END
END
go