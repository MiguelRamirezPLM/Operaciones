IF OBJECT_ID('dbo.plm_spGetTargetMenues') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetTargetMenues
go

/* 
	Author:			Juan Ramírez / Ramiro Sánchez
	Object:			dbo.plm_spGetTargetMenues
	
	Description:	Get a all menues by Target Id.

	Company:		PLM México.

	EXECUTE dbo.plm_spGetTargetMenues @isbn = 'DEF 58',@targetId = 3

 */ 

CREATE PROCEDURE dbo.plm_spGetTargetMenues
(
	@isbn			varchar(20),
	@targetId		tinyint
)
AS
BEGIN

	Select Distinct dcp.TargetId,
		   e.EditionId,
		   m.MenuId,
		   m.MenuName,
		   m.ShortName,
		   mm.ImageName,
		   mm.BaseUrl,
		   mm.[Order]
	From Editions e
		Inner Join CodePrefixEditions cpe On e.EditionId = cpe.EditionId
		Inner Join DistributionCodePrefixes dcp On cpe.PrefixId = dcp.PrefixId
		Inner Join ModuleMenuEditions mme On cpe.PrefixId = mme.PrefixId
			And cpe.EditionId = mme.EditionId
		Inner Join ModuleMenues mm On mme.ModuleId = mm.ModuleId
			And mme.MenuId = mm.MenuId
		Inner Join Menues m On mm.MenuId = m.MenuId
		Inner Join Modules md On mm.ModuleId = md.ModuleId
	Where e.ISBN = @isbn And
		  dcp.TargetId = @targetId And
		  md.ModuleName = 'BÚSQUEDAS' And
		  mm.Active = 1 And
		  m.Active = 1
	Order By mm.[Order]			

	RETURN @@ROWCOUNT

END
