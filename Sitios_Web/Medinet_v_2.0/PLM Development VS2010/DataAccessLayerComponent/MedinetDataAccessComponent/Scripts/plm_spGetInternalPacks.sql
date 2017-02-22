IF OBJECT_ID('dbo.plm_spGetInternalPacks') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetInternalPacks
go

/* 
	Author:			Ramiro Sánchez
	Object:			dbo.plm_spGetInternalPacks
	
	Description:	Gets all Internal Packs.
	Company:		PLM Latina.

	EXECUTE dbo.plm_spGetInternalPacks

 */ 

CREATE PROCEDURE dbo.plm_spGetInternalPacks
AS
BEGIN

	Select	InternalPackId,
			InternalPackName,
			Active
		From InternalPacks
		Where Active = 1
	Order By 2

END
go