IF OBJECT_ID('dbo.plm_spGetExternalPacks') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetExternalPacks
go

/* 
	Author:			Ramiro Sánchez
	Object:			dbo.plm_spGetExternalPacks
	
	Description:	Gets all External Packs.
	Company:		PLM Latina.

	EXECUTE dbo.plm_spGetExternalPacks

 */ 

CREATE PROCEDURE dbo.plm_spGetExternalPacks
AS
BEGIN

	Select	ExternalPackId,
			ExternalPackName,
			Active
		From ExternalPacks
		Where Active = 1
	Order By 2

END
go