IF OBJECT_ID('dbo.roc_spGetActiveSubstance') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetActiveSubstance
go

/* 
	Author:			Daniel Campos / Ramiro Sánchez				 
	Object:			dbo.roc_spGetActiveSubstance
	
	Description:	Retrieves all Active Substances by Active Substance Id.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetActiveSubstance @activeSubstanceId = 1


 */ 

CREATE PROCEDURE dbo.roc_spGetActiveSubstance
(
	@activeSubstanceId				int
)
AS
BEGIN

	-- The parameters shouldn't be null:
	IF (@activeSubstanceId IS NULL)
	BEGIN
	
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
		
	END

	SELECT ActiveSubstances.ActiveSubstanceId, ActiveSubstances.ActiveSubstanceName
		FROM ActiveSubstances
		WHERE ActiveSubstances.ActiveSubstanceId = @activeSubstanceId AND ActiveSubstances.Active = 1
	
END
go