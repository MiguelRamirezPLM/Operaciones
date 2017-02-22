IF OBJECT_ID('dbo.plm_spGetPSEIndicationsByDivision') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetPSEIndicationsByDivision
go

/* 
	Author:			Ramiro Sánchez
	Object:			dbo.plm_spGetPSEIndicationsByDivision
	
	Description:	Get all indications By Division:

	Company:		PLM Latina.

	EXECUTE dbo.plm_spGetPSEIndicationsByDivision @CountryId = 11, @editionId = 55, @divisionId = 131, @typeInEdition = 'P'

 */ 

CREATE PROCEDURE [dbo].[plm_spGetPSEIndicationsByDivision]
(
	@countryId		tinyint,
	@editionId		int,
	@divisionId		int,
	@typeInEdition	varchar(1) = NULL
)
AS
BEGIN

	SELECT DISTINCT ind.*
		FROM plm_vwProductsByEdition pbe
			INNER JOIN ProductIndications pind ON (pbe.ProductId = pind.ProductId)
			INNER JOIN Indications ind ON (pind.IndicationId = ind.IndicationId)
		WHERE	pbe.CountryId = @countryId AND
				pbe.EditionId	= @editionId AND
				pbe.DivisionId	= @divisionId AND
				pbe.TypeInEdition LIKE CASE WHEN @typeInEdition IS NULL THEN '%'
					ELSE @typeInEdition END AND
				pbe.LabActive				= 1 AND
				pbe.DivisionActive			= 1 AND		
				ind.Active					= 1
		ORDER BY 4
				
		RETURN @@ROWCOUNT
END
