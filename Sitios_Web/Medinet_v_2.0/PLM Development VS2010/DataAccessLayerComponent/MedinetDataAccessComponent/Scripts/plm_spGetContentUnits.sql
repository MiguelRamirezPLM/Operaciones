IF OBJECT_ID('dbo.plm_spGetContentUnits') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetContentUnits
go

/* 
	Author:			Ramiro Sánchez
	Object:			dbo.plm_spGetContentUnits
	
	Description:	Gets all Content Units.
	Company:		PLM Latina.

	EXECUTE dbo.plm_spGetContentUnits

 */ 

CREATE PROCEDURE dbo.plm_spGetContentUnits
AS
BEGIN

	Select	ContentUnitId,
			UnitName,
			Active
		From ContentUnits
		Where Active = 1
	Order By 2

END
go