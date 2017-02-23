IF OBJECT_ID('dbo.plm_spGetTargetCountries') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetTargetCountries
go

/* 
	Author:			Juan Ramírez / Ramiro Sánchez
	Object:			dbo.plm_spGetTargetCountries
	
	Description:

	Company:		PLM Latina.

	EXECUTE dbo.plm_spGetTargetCountries @targetId = 1

 */ 

CREATE PROCEDURE dbo.plm_spGetTargetCountries
(
	@targetId		tinyint
)
AS
BEGIN

	Select Distinct c.CountryId,
		   c.CountryName,
		   c.ID,
		   c.CountryCode,
		   mm.BaseUrl,
		   mm.ImageName as [FileName],
		   e.EditionId,
		   e.ISBN
	From Countries c
		Inner Join Editions e On c.CountryId = e.CountryId
		Inner Join CodePrefixEditions cpe On e.EditionId = cpe.EditionId
		Inner Join DistributionCodePrefixes dcp On cpe.PrefixId = dcp.PrefixId
		Inner Join ModuleMenuEditions mme On cpe.PrefixId = mme.PrefixId And
										     cpe.EditionId = mme.EditionId
		Inner Join ModuleMenues mm On mme.ModuleId = mm.ModuleId And
									  mme.MenuId = mm.MenuId
		Inner Join Modules m On mme.ModuleId = m.ModuleId
	Where e.Active = 1 And
		  c.Active = 1 And
		  m.ModuleName = 'REGIONES' And
		  dcp.TargetId = @targetId
	Order By c.CountryId	

	RETURN @@ROWCOUNT

END
