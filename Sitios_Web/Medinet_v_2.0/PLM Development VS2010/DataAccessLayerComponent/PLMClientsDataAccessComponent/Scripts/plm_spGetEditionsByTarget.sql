IF OBJECT_ID('dbo.plm_spGetEditionsByTarget') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetEditionsByTarget
go

/* 
	Author:			Juan Ramírez / Ramiro Sánchez
	Object:			dbo.plm_spGetEditionsByTarget
	
	Description:	Get a edition by TargetOutput id.

	Company:		PLM México.

	DECLARE
		@osMobileId	tinyint,
		@countryId	tinyint

	EXECUTE dbo.plm_spGetEditionsByTarget @targetId = 3,@country = 'mex'

 */ 

CREATE PROCEDURE dbo.plm_spGetEditionsByTarget
(
	@targetId		tinyint,
	@country		varchar(3)
)
AS
BEGIN

	SELECT dcp.TargetId
		,e.[EditionId]
        ,e.[ISBN]
		,mm.[ImageName] As [FileName]
        ,mm.[BaseUrl]
	From Countries c
		Inner Join Editions e On c.CountryId = e.CountryId
		Inner Join CodePrefixEditions cpe On e.EditionId = cpe.EditionId
		Inner Join DistributionCodePrefixes dcp On cpe.PrefixId = dcp.PrefixId
		Inner Join ModuleMenuEditions mme On cpe.PrefixId = mme.PrefixId
			And cpe.EditionId = mme.EditionId
		Inner Join ModuleMenues mm On mme.ModuleId = mm.ModuleId
			And mme.MenuId = mm.MenuId
	Where e.Active = 1 And
		  c.Active = 1 And
		  dcp.TargetId = @targetId And
		  c.ID = @country

	RETURN @@ROWCOUNT

END
go