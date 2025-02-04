USE [Medinet 20130502]
GO
/****** Object:  StoredProcedure [dbo].[plm_spGetPSEIndications]    Script Date: 05/03/2013 17:01:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/* 
	Author:			Erick Morales / Beatriz Vázquez.				 
	Object:			dbo.plm_spGetPSEIndications
	
	Description:	Retrieves all Indications by country by edition.

	Company:		Thomson PLM.

	DECLARE @retValue int

	EXECUTE @retValue = dbo.plm_spGetPSEIndications @CountryId = 11, @EditionId = 1, @Description = 'ABDO'

	SELECT @retValue

 */ 

ALTER PROCEDURE [dbo].[plm_spGetPSEIndications]
(
	@countryId		tinyint,
	@editionId		int,
	@typeInEdition	varchar(1) = NULL,
	@description	varchar(200) = NULL,
	@productId		int = NULL
)
AS
BEGIN
		IF(@productId IS NULL)
		BEGIN
			IF(@description LIKE 'Ñ%')
			BEGIN
				SELECT DISTINCT ind.* 
				FROM plm_vwProductsByEdition pbe

				INNER JOIN ProductIndications pind ON (pbe.ProductId = pind.ProductId)
				INNER JOIN Indications ind ON (pind.IndicationId = ind.IndicationId)

				WHERE	pbe.CountryId				= @countryId AND
						pbe.EditionId	= @editionId AND
						pbe.TypeInEdition LIKE CASE WHEN @typeInEdition IS NULL THEN '%'
									   ELSE	@typeInEdition END AND
						pbe.LabActive				= 1 AND
						pbe.DivisionActive			= 1 AND
						
						ind.Active					= 1 AND

						ind.[Description] LIKE CASE WHEN @description IS NULL THEN '%'
													WHEN LEN(@description) > 3 THEN '%'+ @description + '%'
													ELSE @description + '%' END

				ORDER BY 3
				
				RETURN @@ROWCOUNT
			END
			ELSE
			BEGIN
				SELECT DISTINCT ind.* 
				FROM plm_vwProductsByEdition pbe

				INNER JOIN ProductIndications pind ON (pbe.ProductId = pind.ProductId)
				INNER JOIN Indications ind ON (pind.IndicationId = ind.IndicationId)

				WHERE	pbe.CountryId				= @countryId AND
						pbe.EditionId	= @editionId AND
						pbe.TypeInEdition LIKE CASE WHEN @typeInEdition IS NULL THEN '%'
									   ELSE	@typeInEdition END AND
						pbe.LabActive				= 1 AND
						pbe.DivisionActive			= 1 AND
						
						ind.Active					= 1 AND

						ind.[Description] COLLATE SQL_Latin1_General_CP1_CI_AI LIKE CASE WHEN @description IS NULL THEN '%'
													WHEN LEN(@description) > 3 THEN '%'+ @description + '%'
													ELSE @description + '%' END

				ORDER BY 3
				
				RETURN @@ROWCOUNT

			END
		END
	
		IF(@productId IS NOT NULL)
		BEGIN
			SELECT DISTINCT ind.* 
			FROM plm_vwProductsByEdition pbe

			INNER JOIN ProductIndications pind ON (pbe.ProductId = pind.ProductId)
			INNER JOIN Indications ind ON (pind.IndicationId = ind.IndicationId)

			WHERE	pbe.CountryId				= @countryId AND
					pbe.EditionId	= @editionId AND
					pbe.TypeInEdition LIKE CASE WHEN @typeInEdition IS NULL THEN '%'
									   ELSE	@typeInEdition END AND
					pbe.LabActive				= 1 AND
					pbe.DivisionActive			= 1 AND
					
					ind.Active					= 1 AND
					pbe.productId				= @productId AND

					ind.[Description] COLLATE SQL_Latin1_General_CP1_CI_AI LIKE CASE WHEN @description IS NULL THEN '%'
												ELSE @description + '%' END

			ORDER BY 3
			
			RETURN @@ROWCOUNT
		END
	
END
