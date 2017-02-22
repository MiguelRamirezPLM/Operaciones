IF OBJECT_ID('dbo.roc_spGetDivision') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetDivision
go

/* 
	Author:			Daniel Campos / Ramiro Sánchez				 
	Object:			dbo.roc_spGetDivision
	
	Description:	Retrieves information from Divisions By DivisionId

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetDivision @divisionId = 30


 */ 

CREATE PROCEDURE dbo.roc_spGetDivision
(
	@divisionId				int
)

AS
BEGIN

	-- The parameters shouldn't be null:
	IF (@divisionId IS NULL)
	BEGIN
	
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
		
	END

	SELECT Divisions.DivisionName, Divisions.ShortName, DivisionInformation.* FROM DivisionInformation
		INNER JOIN Divisions ON Divisions.DivisionId = DivisionInformation.DivisionId
		WHERE DivisionInformation.DivisionId = @divisionId and DivisionInformation.Active=1
	
END
go