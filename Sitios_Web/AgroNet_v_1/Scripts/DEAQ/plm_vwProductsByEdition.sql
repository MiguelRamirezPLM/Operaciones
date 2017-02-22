USE [DEAQ 20141007]
GO

/****** Object:  View [dbo].[plm_vwProductsByEdition]    Script Date: 09/14/2015 09:40:56 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*
	Author:			Erick Morales Silva.
 	Object:			plm_vwProductsByEdition.
	
	Description:	Gets all the Participant Products by Edition.

	Company:		Thomson PLM.

	SELECT * FROM dbo.plm_vwProductsByEdition ORDER BY CountryName, LaboratoryName, DivisionName, Brand

 */


ALTER VIEW [dbo].[plm_vwProductsByEdition]
AS

	SELECT 

		c.CountryId,
		c.CountryName,

		lab.LaboratoryId,
		lab.LaboratoryName,
		lab.Active AS LabActive,
		
		d.DivisionId,
		d.DivisionName,
		d.ShortName AS DivisionShortName,
		d.Active as DivisionActive,

		ct.CategoryId,
		ct.CategoryName,
		ct.Active AS CategoryActive,

		p.ProductId,
		p.ProductName,
		p.Description AS ProductDescription,
		p.Register,
		p.Active AS ProductActive,

		ppf.PharmaFormId,
		pf.PharmaForm,
		ppf.Active AS PharmaActive,

		e.EditionId,
		e.NumberEdition,
		e.ISBN,
		e.BarCode,
		e.Active AS EditionActive,

		tmp.TypeInEdition,
		tmp.Page AS Page,
		
		b.BookId,
		b.BookName,
		b.Active AS BookActive,
		
		CASE WHEN newp.ProductId IS NULL THEN null ELSE 'N' END AS NewProduct,
		CASE WHEN tmp.ContentTypeId = 2 THEN 'C/C'
			WHEN tmp.ContentTypeId IS NULL THEN NULL END AS ContentType,
		
		 (SELECT COUNT(*) FROM ProductSubstances WHERE ProductId = p.ProductId) AS NumberOfActiveSubstances,
		 (SELECT COUNT(*) FROM ProductCrops WHERE ProductId = p.ProductId) AS NumberOfCrops ,
		 (SELECT COUNT(*) FROM ProductSeeds WHERE ProductId = p.ProductId) AS NumberOfSeeds ,
		 (SELECT COUNT(*) FROM ProductAgrochemicalUses WHERE ProductId = p.ProductId) AS NumberOfAgrochemicalUses  

	FROM Products p

	INNER JOIN Countries c ON (p.CountryId = c.CountryId)
	INNER JOIN Laboratories lab ON (p.LaboratoryId = lab.LaboratoryId)

	INNER JOIN ProductPharmaForms ppf ON (p.ProductId = ppf.ProductId)
	INNER JOIN PharmaForms pf ON (ppf.PharmaFormId = pf.PharmaFormId)

	LEFT JOIN ProductCategories pc ON (ppf.ProductId = pc.ProductId AND 
									   ppf.PharmaFormId = pc.PharmaFormId)

	LEFT JOIN EditionDivisionProducts dp ON (pc.ProductId = dp.ProductId AND
										pc.PharmaFormId = dp.PharmaFormId AND
										pc.DivisionId = dp.DivisionId AND 
										pc.CategoryId = dp.CategoryId)

	INNER JOIN Divisions d ON (dp.DivisionId = d.DivisionId)

	INNER JOIN Editions e ON (dp.EditionId = e.EditionId)
	INNER JOIN Books b ON (e.BookId = b.BookId)

	INNER JOIN Categories ct ON(dp.CategoryId = ct.CategoryId) 
	
	LEFT JOIN NewProducts newp on (pc.ProductId = newp.ProductId AND
										pc.PharmaFormId = newp.PharmaFormId AND
										pc.DivisionId = newp.DivisionId AND 
										pc.CategoryId = newp.CategoryId)  

	LEFT JOIN (
		
			SELECT ProductId, PharmaFormId, DivisionId, CategoryId, 'P' AS TypeInEdition, e.*, pp.page, pp.ContentTypeId
			FROM ParticipantProducts pp 
			INNER JOIN Editions e ON (pp.EditionId = e.EditionId)

			UNION 
			SELECT ProductId, PharmaFormId, DivisionId, CategoryId, 'M' AS TypeInEdition, e.*, NULL, null
			FROM MentionatedProducts mp
			INNER JOIN Editions e ON (mp.EditionId = e.EditionId)

		) tmp ON (dp.ProductId = tmp.ProductId AND 
					dp.PharmaFormId = tmp.PharmaFormId AND
						dp.DivisionId = tmp.DivisionId AND 
							dp.CategoryId = tmp.CategoryId AND
								dp.EditionId = tmp.EditionId)
GO


