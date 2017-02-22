IF OBJECT_ID('dbo.roc_spGetDivisionsByLetter') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetDivisionsByLetter
go

/* 
	Author:			Daniel Campos / Ramiro Sánchez				 
	Object:			dbo.roc_spGetDivisionsByLetter
	
	Description:	Retrieves Divisions by Letter
					Where searchType stands for:

						L - Letter		{1}
						T - Text		{2}
						FT - FullText	{3}

	Company:		PLM México.

	EXECUTE dbo.roc_spGetDivisionsByLetter @editionId = 1, @countryId = 10, @letter = 'a', @page = 0, @numberByPage = 10
	
 */ 

CREATE PROCEDURE dbo.roc_spGetDivisionsByLetter
(
	@editionId			int,
	@countryId			int,
	@letter				varchar(255),
	@page				int,
	@numberByPage		int

)AS
BEGIN

	IF (@editionId < 1 OR @countryId < 1 OR @letter IS NULL OR @page < 0 OR @numberByPage < 0)
	BEGIN
		
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
	
	END

	BEGIN
	
		SELECT (
			SELECT count(DivisionId) from 
			(
				SELECT DISTINCT(Divisions.DivisionName),Divisions.ShortName, EditionDivisionProducts.DivisionId  FROM Divisions 
					INNER JOIN EditionDivisionProducts ON EditionDivisionProducts.DivisionId = Divisions.DivisionId 
					WHERE EditionDivisionProducts.EditionId = @editionId AND Divisions.CountryId = @countryId  
					AND Divisions.Active = 1 AND  Divisions.DivisionName LIKE @letter+'%'
			) total

		) AS TOTAL,*   

		FROM (
			SELECT Divisions.DivisionName,EditionDivisionProducts.DivisionId,Divisions.ShortName,
				ROW_NUMBER() OVER (ORDER BY Divisions.DivisionName) AS RowNumber 
				FROM Divisions 
				INNER JOIN EditionDivisionProducts ON EditionDivisionProducts.DivisionId = Divisions.DivisionId 
				WHERE EditionDivisionProducts.EditionId = @editionId AND Divisions.CountryId = @CountryId 
				AND Divisions.Active = 1 AND  Divisions.DivisionName LIKE @letter+'%'
				GROUP BY Divisions.DivisionId,Divisions.DivisionName, Divisions.ShortName ,EditionDivisionProducts.DivisionId
		)AS DIVISION

		WHERE RowNumber BETWEEN @numberByPage * @page + 1 AND @numberByPage * (@page + 1)
		
	END
END
go