IF OBJECT_ID('dbo.plm_spGetPSEProductAttributes') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetPSEProductAttributes
go

/* 
	Author:			Juan Manuel Ramírez / Erick Silva / Elizabeth Lazo.				 
	Object:			dbo.plm_spGetPSEProductAttributes
	
	Description:	Gets a product from the database verifing if it has content.

	Company:		PLM Latina.

	DECLARE @retValue int

	EXECUTE @retValue = dbo.[plm_spGetPSEProductAttributes] 
			@editionId = 82, 
			@divisionId = 134, 
			@categoryId = 101, 
			@productId = 44829 , 
			@pharmaFormId = 162

	SELECT @retValue

 */ 

CREATE PROCEDURE [dbo].[plm_spGetPSEProductAttributes]

	@editionId		int,
	@divisionId		int,
	@categoryId		int,
	@productId		int,
	@pharmaFormId	int
AS
BEGIN

	SELECT DISTINCT
	 
		v.ProductId,
		v.Brand,
		v.PharmaFormId,
		v.PharmaForm,
		v.DivisionName,
		eps.ProductShot, 
		NULL AS BaseUrl,
		di.ImageName As DivisionImage,
		NULL AS DivisionBaseUrl,
		ppf.HTMLFileName As ReferenceUrl
		
	FROM dbo.plm_vwProductsByEdition v
	Left Join ProductContents pc On	(v.EditionId = pc.EditionId And 
										v.DivisionId = pc.DivisionId And 
											v.CategoryId = pc.CategoryId And 
												v.ProductId = pc.ProductId And 
													v.PharmaFormId = pc.PharmaFormId)
									 
	LEFT JOIN (Select *
			   From EditionProductShots
			   Where Active	= 1) eps ON(v.EditionId = eps.EditionId And 
											v.DivisionId = eps.DivisionId And 
												v.CategoryId = eps.CategoryId And 
													v.ProductId = eps.ProductId And 
														v.PharmaFormId = eps.PharmaFormId)

	LEFT JOIN DivisionImages di ON (v.DivisionId = di.DivisionId)
	INNER JOIN ProductCategories ppf ON (v.DivisionId = ppf.DivisionId And
											v.CategoryId = ppf.CategoryId And	
												v.ProductId = ppf.ProductId And
													v.PharmaFormId = ppf.PharmaFormId)

	WHERE v.EditionId		= @editionId And
		  v.Divisionid		= @divisionId And
		  v.CategoryId		= @categoryId And
		  v.ProductId		= @productId And
		  v.PharmaFormId	= @pharmaFormId 
		  
		  

	ORDER BY 2
			
	RETURN @@ROWCOUNT

END
