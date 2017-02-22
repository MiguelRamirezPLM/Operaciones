IF OBJECT_ID('dbo.roc_spGetCountry') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetCountry
go

/* 
	Author:			Daniel Campos / Ramiro Sánchez				 
	Object:			dbo.roc_spGetCountry
	
	Description:	Retrieves information from a country by ID.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetCountry @ID = 'MEX'


 */ 

CREATE PROCEDURE dbo.roc_spGetCountry
(
	@ID					varchar(10)
)
AS
BEGIN

	-- The parameters shouldn't be null:
	IF (@ID IS NULL)
	BEGIN
	
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
		
	END

		SELECT * FROM Countries 
		WHERE ID = @ID AND Active = 1
	
END
go