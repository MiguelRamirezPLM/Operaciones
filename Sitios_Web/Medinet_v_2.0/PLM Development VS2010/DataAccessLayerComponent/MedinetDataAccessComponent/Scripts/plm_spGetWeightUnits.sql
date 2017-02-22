IF OBJECT_ID('dbo.plm_spGetWeightUnits') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetWeightUnits
go

/* 
	Author:			Ramiro Sánchez
	Object:			dbo.plm_spGetWeightUnits
	
	Description:	Gets all Weight Units.
	Company:		PLM Latina.

	EXECUTE dbo.plm_spGetWeightUnits

 */ 

CREATE PROCEDURE dbo.plm_spGetWeightUnits
AS
BEGIN

	Select	WeightUnitId,
			UnitName,
			ShortName,
			Active
		From WeightUnits
		Where Active = 1
	Order By 2

END
go