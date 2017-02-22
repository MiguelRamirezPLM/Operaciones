IF OBJECT_ID('dbo.roc_spGetDivisions') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetDivisions
go

/* 
	Author:			Daniel Campos / Ramiro Sánchez				 
	Object:			dbo.roc_spGetDivisions
	
	Description:	Retrieves all Divisions By EditionId And CountryId

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetDivisions @editionId = 1, @countryId = 10, @page = 0, @numberByPage = 10


 */ 

CREATE PROCEDURE dbo.roc_spGetDivisions
(
	@editionId				int,
	@countryId				int,
	@page					int,
	@numberByPage			int
)

AS
BEGIN

	-- The parameters shouldn't be null:
	IF (@editionId IS NULL OR @countryId IS NULL OR @page IS NULL OR @numberByPage IS NULL)
	BEGIN
	
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
		
	END

	SELECT (
		SELECT count(DivisionId) from 
		(
			SELECT DISTINCT(Divisions.DivisionName),EditionDivisionProducts.DivisionId  FROM Divisions 
				INNER JOIN EditionDivisionProducts ON EditionDivisionProducts.DivisionId = Divisions.DivisionId 
				WHERE EditionDivisionProducts.EditionId = @editionId AND Divisions.CountryId = @countryId  AND Divisions.Active = 1 
		) total

	) AS TOTAL,*   

	FROM (
		SELECT Divisions.DivisionName,Divisions.ShortName,EditionDivisionProducts.DivisionId,
			ROW_NUMBER() OVER (ORDER BY Divisions.DivisionName) AS RowNumber 
			FROM Divisions 
			INNER JOIN EditionDivisionProducts ON EditionDivisionProducts.DivisionId = Divisions.DivisionId 
			WHERE EditionDivisionProducts.EditionId = @editionId AND Divisions.CountryId = @countryId AND Divisions.Active = 1
			GROUP BY Divisions.DivisionId,Divisions.DivisionName,Divisions.ShortName ,EditionDivisionProducts.DivisionId
	)AS DIVISION

	WHERE RowNumber BETWEEN @numberByPage * @page + 1 AND @numberByPage * (@page + 1)
	
END
go