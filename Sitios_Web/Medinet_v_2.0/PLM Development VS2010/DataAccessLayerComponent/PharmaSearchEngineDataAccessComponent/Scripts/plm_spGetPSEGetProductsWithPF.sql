IF OBJECT_ID('dbo.plm_spGetPSEGetProductsWithPF') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetPSEGetProductsWithPF

GO
 
CREATE PROCEDURE [dbo].[plm_spGetPSEGetProductsWithPF]

	@countryId			tinyint,
	@editionId			int,
	@typeInEdition		varchar(1) = NULL,
	@brand				varchar(255) = NULL,

	@divisionId			int = NULL,
	@activeSubstanceId	int = NULL,
	@indicationId		int = NULL,
	@therapeuticId		int = NULL,
	@onlyOne			bit = NULL,
	@icdId              int = NULL,
	@numberPage			tinyint = NULL,
	@page				tinyint = NULL
	
AS
BEGIN

	-- Get all products:
	IF (@divisionId IS NULL AND @activeSubstanceId IS NULL AND 
		@indicationId IS NULL AND @therapeuticId IS NULL AND @icdId IS NULL)
	BEGIN
		IF(@brand LIKE 'Ñ%')
		BEGIN
			
			IF(@numberPage IS NOT NULL AND @page IS NOT NULL)
			BEGIN
					SELECT DISTINCT 
						ProductId,
						Brand,
						PharmaFormId,
						PharmaForm,
						DivisionId,
						DivisionName,
						DivisionShortName,
						CategoryId,
						CategoryName
					FROM (SELECT DISTINCT 
							pbe.ProductId,
							pbe.Brand,
							pbe.PharmaFormId,
							pbe.PharmaForm,
							pbe.DivisionId,
							pbe.DivisionName,
							pbe.DivisionShortName,
							pbe.CategoryId,
							pbe.CategoryName,
							ROW_NUMBER() OVER(ORDER BY pbe.Brand,pbe.PharmaForm) AS RowNumber

						FROM plm_vwProductsByEdition pbe

						WHERE	pbe.CountryId				= @countryId AND
								pbe.EditionId	= @editionId AND
								pbe.TypeInEdition LIKE CASE WHEN @typeInEdition IS NULL THEN '%'
										   ELSE	@typeInEdition END AND
								pbe.LabActive				= 1 AND
								pbe.DivisionActive			= 1 AND
								pbe.ProductActive			= 1 AND
								pbe.PharmaActive		    = 1 AND

								pbe.Brand LIKE CASE WHEN @brand IS NULL THEN '%'
													ELSE @brand + '%' END) AS Prods 
					WHERE RowNumber BETWEEN @numberPage * @page + 1 AND @numberPage * (@page + 1)

					ORDER BY Brand
				
			RETURN @@ROWCOUNT
			
			END
			ELSE
			BEGIN
				
				
				SELECT DISTINCT 
					
					pbe.ProductId,
					pbe.Brand,
					pbe.PharmaFormId,
					pbe.PharmaForm,
					pbe.DivisionId,
					pbe.DivisionName,
					pbe.DivisionShortName,
					pbe.CategoryId,
					pbe.CategoryName

				FROM plm_vwProductsByEdition pbe

				WHERE	pbe.CountryId				= @countryId AND
						pbe.EditionId	= @editionId AND
						pbe.TypeInEdition LIKE CASE WHEN @typeInEdition IS NULL THEN '%'
										   ELSE	@typeInEdition END AND
						pbe.LabActive				= 1 AND
						pbe.DivisionActive			= 1 AND
						pbe.ProductActive			= 1 AND
						pbe.PharmaActive		    = 1 AND

						pbe.Brand LIKE CASE WHEN @brand IS NULL THEN '%'
													ELSE @brand + '%' END

				ORDER BY pbe.Brand
				
				RETURN @@ROWCOUNT
			END 	
		END
		ELSE
		BEGIN
			IF(@numberPage IS NOT NULL AND @page IS NOT NULL)
			BEGIN
				
				SELECT DISTINCT 
						ProductId,
						Brand,
						PharmaFormId,
						PharmaForm,
						DivisionId,
						DivisionName,
						DivisionShortName,
						CategoryId,
						CategoryName
					FROM (SELECT DISTINCT 
							pbe.ProductId,
							pbe.Brand,
							pbe.PharmaFormId,
							pbe.PharmaForm,
							pbe.DivisionId,
							pbe.DivisionName,
							pbe.DivisionShortName,
							pbe.CategoryId,
							pbe.CategoryName,
							ROW_NUMBER() OVER(ORDER BY pbe.Brand,pbe.PharmaForm) AS RowNumber

						FROM plm_vwProductsByEdition pbe

						WHERE	pbe.CountryId				= @countryId AND
								pbe.EditionId	= @editionId AND
								pbe.TypeInEdition LIKE CASE WHEN @typeInEdition IS NULL THEN '%'
										   ELSE	@typeInEdition END AND
								pbe.LabActive				= 1 AND
								pbe.DivisionActive			= 1 AND
								pbe.ProductActive			= 1 AND
								pbe.PharmaActive		    = 1 AND

								pbe.Brand COLLATE SQL_Latin1_General_CP1_CI_AI LIKE CASE WHEN @brand IS NULL THEN '%'
												ELSE @brand + '%' END) AS Prods 
					WHERE RowNumber BETWEEN @numberPage * @page + 1 AND @numberPage * (@page + 1)

					ORDER BY Brand

				RETURN @@ROWCOUNT
			END
			ELSE
			BEGIN
				SELECT DISTINCT 
				
				pbe.ProductId,
				pbe.Brand,
				pbe.PharmaFormId,
				pbe.PharmaForm,
				pbe.DivisionId,
				pbe.DivisionName,
				pbe.DivisionShortName,
				pbe.CategoryId,
				pbe.CategoryName

			FROM plm_vwProductsByEdition pbe

			WHERE	pbe.CountryId				= @countryId AND
					pbe.EditionId	= @editionId AND
					pbe.TypeInEdition LIKE CASE WHEN @typeInEdition IS NULL THEN '%'
									   ELSE	@typeInEdition END AND
					pbe.LabActive				= 1 AND
					pbe.DivisionActive			= 1 AND
					pbe.ProductActive			= 1 AND
					pbe.PharmaActive		    = 1 AND

					pbe.Brand COLLATE SQL_Latin1_General_CP1_CI_AI LIKE CASE WHEN @brand IS NULL THEN '%'
												ELSE @brand + '%' END

			ORDER BY pbe.Brand
			
				RETURN @@ROWCOUNT
			END
		END
	END

	-- Get products by division:
	IF (@divisionId IS NOT NULL AND @activeSubstanceId IS NULL AND
		@therapeuticId IS NULL AND @indicationId IS NULL  AND @icdID IS NULL)
	BEGIN

		SELECT DISTINCT 
			
			pbe.ProductId,
			pbe.Brand,
			pbe.PharmaFormId,
			pbe.PharmaForm,
			pbe.DivisionId,
			pbe.DivisionName,
			pbe.DivisionShortName,
			pbe.CategoryId,
			pbe.CategoryName

		FROM plm_vwProductsByEdition pbe

		WHERE	pbe.CountryId				= @countryId AND
				pbe.EditionId	= @editionId  AND
				pbe.TypeInEdition = 'P' AND
				--pbe.TypeInEdition LIKE CASE WHEN @typeInEdition IS NULL THEN '%'
				--					   ELSE	@typeInEdition END AND
				pbe.LabActive				= 1 AND
				pbe.DivisionActive			= 1 AND
				pbe.ProductActive			= 1 AND
				pbe.PharmaActive		    = 1 AND

				pbe.Brand COLLATE SQL_Latin1_General_CP1_CI_AI LIKE CASE WHEN @brand IS NULL THEN '%'
											ELSE @brand + '%' END AND
		
				pbe.DivisionId				= @divisionId

		ORDER BY pbe.Brand
		
		RETURN @@ROWCOUNT		

	END

	-- Get products by active substance:
	IF (@activeSubstanceId IS NOT NULL AND @therapeuticId IS NULL AND
		@indicationId IS NULL AND @divisionId IS NULL AND @onlyOne IS NULL  AND @icdID IS NULL)
	BEGIN

		SELECT DISTINCT 
			
			pbe.ProductId,
			pbe.Brand,
			pbe.PharmaFormId,
			pbe.PharmaForm,
			pbe.DivisionId,
			pbe.DivisionName,
			pbe.DivisionShortName,
			pbe.CategoryId,
			pbe.CategoryName

		FROM plm_vwProductsByEdition pbe

		INNER JOIN ProductSubstances ps ON (pbe.ProductId = ps.ProductId)

		WHERE	pbe.CountryId				= @countryId AND
				pbe.EditionId	= @editionId AND
				pbe.TypeInEdition LIKE CASE WHEN @typeInEdition IS NULL THEN '%'
									   ELSE	@typeInEdition END AND
				pbe.LabActive				= 1 AND
				pbe.DivisionActive			= 1 AND
				pbe.ProductActive			= 1 AND
				pbe.PharmaActive		    = 1 AND

				pbe.Brand COLLATE SQL_Latin1_General_CP1_CI_AI LIKE CASE WHEN @brand IS NULL THEN '%'
											ELSE @brand + '%' END AND
		
				ps.ActiveSubstanceId		= @activeSubstanceId

		ORDER BY pbe.Brand
		
		RETURN @@ROWCOUNT		

	END
	
	-- Get the products alone:
	IF (@activeSubstanceId IS NOT NULL AND @therapeuticId IS NULL AND
		@indicationId IS NULL AND @divisionId IS NULL AND @onlyOne = 1  AND @icdID IS NULL)
	BEGIN

		SELECT DISTINCT 
			
			pbe.ProductId,
			pbe.Brand,
			pbe.PharmaFormId,
			pbe.PharmaForm,
			pbe.DivisionId,
			pbe.DivisionName,
			pbe.DivisionShortName,
			pbe.CategoryId,
			pbe.CategoryName

		FROM plm_vwProductsByEdition pbe

		INNER JOIN ProductSubstances ps ON (pbe.ProductId = ps.ProductId)
		INNER JOIN (Select Count(*) as Num,ProductId 
					From ProductSubstances Group By ProductId) tmp ON(ps.ProductId = tmp.ProductId)

		WHERE	pbe.CountryId				= @countryId AND
				pbe.EditionId	= @editionId AND
				pbe.TypeInEdition LIKE CASE WHEN @typeInEdition IS NULL THEN '%'
									   ELSE	@typeInEdition END AND
				pbe.LabActive				= 1 AND
				pbe.DivisionActive			= 1 AND
				pbe.ProductActive			= 1 AND
				pbe.PharmaActive		    = 1 AND

				pbe.Brand COLLATE SQL_Latin1_General_CP1_CI_AI LIKE CASE WHEN @brand IS NULL THEN '%'
											ELSE @brand + '%' END AND
				tmp.Num						= 1 AND		

				ps.ActiveSubstanceId		= @activeSubstanceId

		ORDER BY pbe.Brand
		
		RETURN @@ROWCOUNT		

	END

		-- Get the combined products:
	IF (@activeSubstanceId IS NOT NULL AND @therapeuticId IS NULL AND
		@indicationId IS NULL AND @divisionId IS NULL AND @onlyOne = 0  AND @icdID IS NULL)
	BEGIN

		SELECT DISTINCT 
			
			pbe.ProductId,
			pbe.Brand,
			pbe.PharmaFormId,
			pbe.PharmaForm,
			pbe.DivisionId,
			pbe.DivisionName,
			pbe.DivisionShortName,
			pbe.CategoryId,
			pbe.CategoryName

		FROM plm_vwProductsByEdition pbe

		INNER JOIN ProductSubstances ps ON (pbe.ProductId = ps.ProductId)
		INNER JOIN (Select Count(*) as Num,ProductId 
					From ProductSubstances Group By ProductId) tmp ON(ps.ProductId = tmp.ProductId)

		WHERE	pbe.CountryId				= @countryId AND
				pbe.EditionId	= @editionId AND
				pbe.TypeInEdition LIKE CASE WHEN @typeInEdition IS NULL THEN '%'
									   ELSE	@typeInEdition END AND
				pbe.LabActive				= 1 AND
				pbe.DivisionActive			= 1 AND
				pbe.ProductActive			= 1 AND
				pbe.PharmaActive		    = 1 AND

				pbe.Brand COLLATE SQL_Latin1_General_CP1_CI_AI LIKE CASE WHEN @brand IS NULL THEN '%'
											ELSE @brand + '%' END AND
				tmp.Num						> 1 AND		

				ps.ActiveSubstanceId		= @activeSubstanceId

		ORDER BY pbe.Brand
		
		RETURN @@ROWCOUNT		

	END

	-- Get products by division by active substance:
	IF (@activeSubstanceId IS NOT NULL AND @divisionId IS NOT NULL AND
		@therapeuticId IS NULL AND @indicationId IS NULL AND @onlyOne IS NULL  AND @icdID IS NULL)
	BEGIN
		
		SELECT DISTINCT 
			
			pbe.ProductId,
			pbe.Brand,
			pbe.PharmaFormId,
			pbe.PharmaForm,
			pbe.DivisionId,
			pbe.DivisionName,
			pbe.DivisionShortName,
			pbe.CategoryId,
			pbe.CategoryName

		FROM plm_vwProductsByEdition pbe

		INNER JOIN ProductSubstances ps ON (pbe.ProductId = ps.ProductId)

		WHERE	pbe.CountryId				= @countryId AND
				pbe.EditionId	= @editionId AND
				pbe.TypeInEdition LIKE CASE WHEN @typeInEdition IS NULL THEN '%'
									   ELSE	@typeInEdition END AND
				pbe.LabActive				= 1 AND
				pbe.DivisionActive			= 1 AND
				pbe.ProductActive			= 1 AND
				pbe.PharmaActive		    = 1 AND

				pbe.Brand COLLATE SQL_Latin1_General_CP1_CI_AI LIKE CASE WHEN @brand IS NULL THEN '%'
											ELSE @brand + '%' END AND

				pbe.DivisionId				= @divisionId AND
				ps.ActiveSubstanceId		= @activeSubstanceId

		ORDER BY pbe.Brand
		
		RETURN @@ROWCOUNT		

	END

	-- Gets the alone products by chemical and division:
	IF (@activeSubstanceId IS NOT NULL AND @divisionId IS NOT NULL AND
		@therapeuticId IS NULL AND @indicationId IS NULL AND @onlyOne = 1  AND @icdID IS NULL)
	BEGIN
		
		SELECT DISTINCT 
			
			pbe.ProductId,
			pbe.Brand,
			pbe.PharmaFormId,
			pbe.PharmaForm,
			pbe.DivisionId,
			pbe.DivisionName,
			pbe.DivisionShortName,
			pbe.CategoryId,
			pbe.CategoryName

		FROM plm_vwProductsByEdition pbe

		INNER JOIN ProductSubstances ps ON (pbe.ProductId = ps.ProductId)
		INNER JOIN (Select Count(*) as Num,ProductId 
					From ProductSubstances Group By ProductId) tmp ON(ps.ProductId = tmp.ProductId)

		WHERE	pbe.CountryId				= @countryId AND
				pbe.EditionId	= @editionId AND
				pbe.TypeInEdition LIKE CASE WHEN @typeInEdition IS NULL THEN '%'
									   ELSE	@typeInEdition END AND
				pbe.LabActive				= 1 AND
				pbe.DivisionActive			= 1 AND
				pbe.ProductActive			= 1 AND
				pbe.PharmaActive		    = 1 AND

				pbe.Brand COLLATE SQL_Latin1_General_CP1_CI_AI LIKE CASE WHEN @brand IS NULL THEN '%'
											ELSE @brand + '%' END AND
				tmp.Num						= 1 AND

				pbe.DivisionId				= @divisionId AND
				ps.ActiveSubstanceId		= @activeSubstanceId

		ORDER BY pbe.Brand
		
		RETURN @@ROWCOUNT		

	END

	-- Gets the alone products by chemical and division:
	IF (@activeSubstanceId IS NOT NULL AND @divisionId IS NOT NULL AND
		@therapeuticId IS NULL AND @indicationId IS NULL AND @onlyOne = 0  AND @icdID IS NULL)
	BEGIN
		
		SELECT DISTINCT 
			
			pbe.ProductId,
			pbe.Brand,
			pbe.PharmaFormId,
			pbe.PharmaForm,
			pbe.DivisionId,
			pbe.DivisionName,
			pbe.DivisionShortName,
			pbe.CategoryId,
			pbe.CategoryName

		FROM plm_vwProductsByEdition pbe

		INNER JOIN ProductSubstances ps ON (pbe.ProductId = ps.ProductId)
		INNER JOIN (Select Count(*) as Num,ProductId 
					From ProductSubstances Group By ProductId) tmp ON(ps.ProductId = tmp.ProductId)

		WHERE	pbe.CountryId				= @countryId AND
				pbe.EditionId	= @editionId AND
				pbe.TypeInEdition LIKE CASE WHEN @typeInEdition IS NULL THEN '%'
									   ELSE	@typeInEdition END AND
				pbe.LabActive				= 1 AND
				pbe.DivisionActive			= 1 AND
				pbe.ProductActive			= 1 AND
				pbe.PharmaActive		    = 1 AND

				pbe.Brand COLLATE SQL_Latin1_General_CP1_CI_AI LIKE CASE WHEN @brand IS NULL THEN '%'
											ELSE @brand + '%' END AND
				tmp.Num						> 1 AND

				pbe.DivisionId				= @divisionId AND
				ps.ActiveSubstanceId		= @activeSubstanceId

		ORDER BY pbe.Brand
		
		RETURN @@ROWCOUNT		

	END

	-- Get products by indication:
	IF (@indicationId IS NOT NULL AND @activeSubstanceId IS NULL AND 
		@divisionId IS NULL AND @therapeuticId IS NULL  AND @icdID IS NULL)
	BEGIN
	
		SELECT DISTINCT 
			
			pbe.ProductId,
			pbe.Brand,
			pbe.PharmaFormId,
			pbe.PharmaForm,
			pbe.DivisionId,
			pbe.DivisionName,
			pbe.DivisionShortName,
			pbe.CategoryId,
			pbe.CategoryName

		FROM plm_vwProductsByEdition pbe

		INNER JOIN ProductIndications pInd ON (pbe.ProductId = pInd.ProductId)

		WHERE	pbe.CountryId				= @countryId AND
				pbe.EditionId	= @editionId AND
				pbe.TypeInEdition LIKE CASE WHEN @typeInEdition IS NULL THEN '%'
									   ELSE	@typeInEdition END AND
				pbe.LabActive				= 1 AND
				pbe.DivisionActive			= 1 AND
				pbe.ProductActive			= 1 AND
				pbe.PharmaActive		    = 1 AND

				pbe.Brand COLLATE SQL_Latin1_General_CP1_CI_AI LIKE CASE WHEN @brand IS NULL THEN '%'
											ELSE @brand + '%' END AND
		
				pInd.IndicationId			= @indicationId

		ORDER BY pbe.Brand
		
		RETURN @@ROWCOUNT			

	END

	-- Get product by division by indication:
	IF (@indicationId IS NOT NULL AND @divisionId IS NOT NULL AND
		@activeSubstanceId IS NULL AND @therapeuticId IS NULL  AND @icdID IS NULL)
	BEGIN
	
		SELECT DISTINCT 
			
			pbe.ProductId,
			pbe.Brand,
			pbe.PharmaFormId,
			pbe.PharmaForm,
			pbe.DivisionId,
			pbe.DivisionName,
			pbe.DivisionShortName,
			pbe.CategoryId,
			pbe.CategoryName

		FROM plm_vwProductsByEdition pbe

		INNER JOIN ProductIndications pInd ON (pbe.ProductId = pInd.ProductId)

		WHERE	pbe.CountryId				= @countryId AND
				pbe.EditionId	= @editionId AND
				pbe.TypeInEdition LIKE CASE WHEN @typeInEdition IS NULL THEN '%'
									   ELSE	@typeInEdition END AND
				pbe.LabActive				= 1 AND
				pbe.DivisionActive			= 1 AND
				pbe.ProductActive			= 1 AND
				pbe.PharmaActive		    = 1 AND

				pbe.Brand COLLATE SQL_Latin1_General_CP1_CI_AI LIKE CASE WHEN @brand IS NULL THEN '%'
											ELSE @brand + '%' END AND
		
				pbe.DivisionId				= @divisionId AND
				pInd.IndicationId			= @indicationId

		ORDER BY pbe.Brand
		
		RETURN @@ROWCOUNT			

	END

	-- Get products by ATC:
	IF (@therapeuticId IS NOT NULL AND @activeSubstanceId IS NULL AND 
		@divisionId IS NULL AND @indicationId IS NULL  AND @icdID IS NULL)
	BEGIN
	
		SELECT DISTINCT 
			
			pbe.ProductId,
			pbe.Brand,
			pbe.PharmaFormId,
			pbe.PharmaForm,
			pbe.DivisionId,
			pbe.DivisionName,
			pbe.DivisionShortName,
			pbe.CategoryId,
			pbe.CategoryName

		FROM plm_vwProductsByEdition pbe

		INNER JOIN ProductTherapeutics pt ON (pbe.ProductId = pt.ProductId)

		WHERE	pbe.CountryId				= @countryId AND
				pbe.EditionId	= @editionId AND
				pbe.TypeInEdition LIKE CASE WHEN @typeInEdition IS NULL THEN '%'
									   ELSE	@typeInEdition END AND
				pbe.LabActive				= 1 AND
				pbe.DivisionActive			= 1 AND
				pbe.ProductActive			= 1 AND
				pbe.PharmaActive		    = 1 AND

				pbe.Brand COLLATE SQL_Latin1_General_CP1_CI_AI LIKE CASE WHEN @brand IS NULL THEN '%'
											ELSE @brand + '%' END AND
		
				pt.TherapeuticId			= @therapeuticId

		ORDER BY pbe.Brand
		
		RETURN @@ROWCOUNT			

	END

	-- Get products by ATC by active substance:
	IF (@therapeuticId IS NOT NULL AND @activeSubstanceId IS NOT NULL AND 
		@divisionId IS NULL AND @indicationId IS NULL AND @onlyOne IS NULL  AND @icdID IS NULL)
	BEGIN
	
		SELECT DISTINCT 
			
			pbe.ProductId,
			pbe.Brand,
			pbe.PharmaFormId,
			pbe.PharmaForm,
			pbe.DivisionId,
			pbe.DivisionName,
			pbe.DivisionShortName,
			pbe.CategoryId,
			pbe.CategoryName

		FROM plm_vwProductsByEdition pbe

		INNER JOIN ProductSubstances ps ON (pbe.ProductId = ps.ProductId)
		INNER JOIN ProductTherapeutics pt ON (pbe.ProductId = pt.ProductId)

		WHERE	pbe.CountryId				= @countryId AND
				pbe.EditionId	= @editionId AND
				pbe.TypeInEdition LIKE CASE WHEN @typeInEdition IS NULL THEN '%'
									   ELSE	@typeInEdition END AND
				pbe.LabActive				= 1 AND
				pbe.DivisionActive			= 1 AND
				pbe.ProductActive			= 1 AND
				pbe.PharmaActive		    = 1 AND

				pbe.Brand COLLATE SQL_Latin1_General_CP1_CI_AI LIKE CASE WHEN @brand IS NULL THEN '%'
											ELSE @brand + '%' END AND
		
				ps.ActiveSubstanceId		= @activeSubstanceId AND
				pt.TherapeuticId			= @therapeuticId

		ORDER BY pbe.Brand
		
		RETURN @@ROWCOUNT			

	END

		-- Gets the alone products by chemical and therapeutic:
	IF (@therapeuticId IS NOT NULL AND @activeSubstanceId IS NOT NULL AND 
		@divisionId IS NULL AND @indicationId IS NULL AND @onlyOne = 1  AND @icdID IS NULL)
	BEGIN
		
		SELECT DISTINCT 
			
			pbe.ProductId,
			pbe.Brand,
			pbe.PharmaFormId,
			pbe.PharmaForm,
			pbe.DivisionId,
			pbe.DivisionName,
			pbe.DivisionShortName,
			pbe.CategoryId,
			pbe.CategoryName

		FROM plm_vwProductsByEdition pbe

		INNER JOIN ProductSubstances ps ON (pbe.ProductId = ps.ProductId)
		INNER JOIN (Select Count(*) as Num,ProductId 
					From ProductSubstances Group By ProductId) tmp ON(ps.ProductId = tmp.ProductId)
		INNER JOIN ProductTherapeutics pt ON (pbe.ProductId = pt.ProductId)

		WHERE	pbe.CountryId				= @countryId AND
				pbe.EditionId	= @editionId AND
				pbe.TypeInEdition LIKE CASE WHEN @typeInEdition IS NULL THEN '%'
									   ELSE	@typeInEdition END AND
				pbe.LabActive				= 1 AND
				pbe.DivisionActive			= 1 AND
				pbe.ProductActive			= 1 AND
				pbe.PharmaActive		    = 1 AND

				pbe.Brand COLLATE SQL_Latin1_General_CP1_CI_AI LIKE CASE WHEN @brand IS NULL THEN '%'
											ELSE @brand + '%' END AND
				tmp.Num						= 1 AND

				pt.TherapeuticId			= @therapeuticId AND
				ps.ActiveSubstanceId		= @activeSubstanceId

		ORDER BY pbe.Brand
		
		RETURN @@ROWCOUNT		

	END

		-- Gets the combined products by chemical and therapeutic:
	IF (@therapeuticId IS NOT NULL AND @activeSubstanceId IS NOT NULL AND 
		@divisionId IS NULL AND @indicationId IS NULL AND @onlyOne = 0  AND @icdID IS NULL)
	BEGIN
		
		SELECT DISTINCT 
			
			pbe.ProductId,
			pbe.Brand,
			pbe.PharmaFormId,
			pbe.PharmaForm,
			pbe.DivisionId,
			pbe.DivisionName,
			pbe.DivisionShortName,
			pbe.CategoryId,
			pbe.CategoryName

		FROM plm_vwProductsByEdition pbe

		INNER JOIN ProductSubstances ps ON (pbe.ProductId = ps.ProductId)
		INNER JOIN (Select Count(*) as Num,ProductId 
					From ProductSubstances Group By ProductId) tmp ON(ps.ProductId = tmp.ProductId)
		INNER JOIN ProductTherapeutics pt ON (pbe.ProductId = pt.ProductId)

		WHERE	pbe.CountryId				= @countryId AND
				pbe.EditionId	= @editionId AND
				pbe.TypeInEdition LIKE CASE WHEN @typeInEdition IS NULL THEN '%'
									   ELSE	@typeInEdition END AND
				pbe.LabActive				= 1 AND
				pbe.DivisionActive			= 1 AND
				pbe.ProductActive			= 1 AND
				pbe.PharmaActive		    = 1 AND

				pbe.Brand COLLATE SQL_Latin1_General_CP1_CI_AI LIKE CASE WHEN @brand IS NULL THEN '%'
											ELSE @brand + '%' END AND
				tmp.Num						> 1 AND

				pt.TherapeuticId			= @therapeuticId AND
				ps.ActiveSubstanceId		= @activeSubstanceId

		ORDER BY pbe.Brand
		
		RETURN @@ROWCOUNT		

	END	
	
	
	
	-- Get products by ICD:
	IF (@icdId IS NOT NULL AND @activeSubstanceId IS NULL AND 
		@divisionId IS NULL AND @therapeuticId IS NULL AND @indicationId IS NULL )
	BEGIN
	
		SELECT DISTINCT 
			
			pbe.ProductId,
			pbe.Brand,
			pbe.PharmaFormId,
			pbe.PharmaForm,
			pbe.DivisionId,
			pbe.DivisionName,
			pbe.DivisionShortName,
			pbe.CategoryId,
			pbe.CategoryName

		FROM plm_vwProductsByEdition pbe

		INNER JOIN ProductICD pInd ON (pbe.ProductId = pInd.ProductId)

		WHERE	pbe.CountryId				= @countryId AND
				pbe.EditionId	= @editionId AND
				pbe.TypeInEdition LIKE CASE WHEN @typeInEdition IS NULL THEN '%'
									   ELSE	@typeInEdition END AND
				pbe.LabActive				= 1 AND
				pbe.DivisionActive			= 1 AND
				pbe.ProductActive			= 1 AND
				pbe.PharmaActive		    = 1 AND

				pbe.Brand COLLATE SQL_Latin1_General_CP1_CI_AI LIKE CASE WHEN @brand IS NULL THEN '%'
											ELSE @brand + '%' END AND
		
				pInd.ICDId			= @icdId

		ORDER BY pbe.Brand
		
		RETURN @@ROWCOUNT			

	END
	
		
END
