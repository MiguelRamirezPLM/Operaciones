IF OBJECT_ID('dbo.roc_spGetDivisionsByText') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetDivisionsByText
go

/* 
	Author:			Daniel Campos / Ramiro Sánchez				 
	Object:			dbo.roc_spGetDivisionsByText
	
	Description:	Retrieves Divisions by Text
					
	Company:		PLM México.

	EXECUTE dbo.roc_spGetDivisionsByText @editionId = 1, @countryId = 10, @text = 'agr', @page = 0, @numberByPage = 10
	
 */ 

CREATE PROCEDURE dbo.roc_spGetDivisionsByText
(
	@editionId			int,
	@countryId			int,
	@text				varchar(255),
	@page				int,
	@numberByPage		int

)AS
BEGIN

	IF (@editionId < 1 OR @countryId < 1 OR @text IS NULL OR @page < 0 OR @numberByPage < 0)
	BEGIN
		
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
	
	END

	BEGIN
	
		SELECT (
			SELECT count(DivisionId) from 
			(
				SELECT DISTINCT(Divisions.DivisionName),Divisions.ShortName,EditionDivisionProducts.DivisionId  FROM Divisions 
					INNER JOIN EditionDivisionProducts ON EditionDivisionProducts.DivisionId = Divisions.DivisionId 
					WHERE EditionDivisionProducts.EditionId = @editionId AND Divisions.CountryId = @countryId  
					AND Divisions.Active = 1 AND  Divisions.DivisionName  LIKE '%'+@text+'%' COLLATE SQL_LATIN1_GENERAL_CP1_CI_AI 
			) total

		) AS TOTAL,*   

		FROM (
			SELECT Divisions.DivisionName,EditionDivisionProducts.DivisionId,Divisions.ShortName,
				ROW_NUMBER() OVER (ORDER BY Divisions.DivisionName) AS RowNumber 
				FROM Divisions 
				INNER JOIN EditionDivisionProducts ON EditionDivisionProducts.DivisionId = Divisions.DivisionId 
				WHERE EditionDivisionProducts.EditionId = @editionId AND Divisions.CountryId = @countryId 
				AND Divisions.Active = 1 AND  Divisions.DivisionName LIKE '%'+@text+'%' COLLATE SQL_LATIN1_GENERAL_CP1_CI_AI 
				GROUP BY Divisions.DivisionId,Divisions.DivisionName ,Divisions.ShortName,EditionDivisionProducts.DivisionId
		)AS DIVISION

		WHERE RowNumber BETWEEN @numberByPage * @page + 1 AND @numberByPage * (@page + 1)
		
	END
END
go