IF OBJECT_ID('dbo.roc_spGetUse') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetUse
go

/* 
	Author:			Daniel Campos / Ramiro Sánchez				 
	Object:			dbo.roc_spGetUse
	
	Description:	Retrieves information about AgrochemicalUse by UseId.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetUse @useId = 30

 */ 

CREATE PROCEDURE dbo.roc_spGetUse
(
	@useId				int
)
AS
BEGIN

	-- The parameters shouldn't be null:
	IF (@useId IS NULL)
	
	BEGIN
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
	END

	SELECT *
		FROM AgrochemicalUses
		WHERE Active = 1 
			AND AgrochemicalUseId = @useId

END
go