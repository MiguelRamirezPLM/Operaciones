
IF OBJECT_ID('dbo.plm_spGetProductsByICD') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetProductsByICD

GO

/*
       Author:          César Avila	
       		 
	   Object:			dbo.plm_spGetProductsByICD
	
	   Description:	    Retrieves Products by ICDId and EditionId

	   EXECUTE          dbo.plm_spGetProductsByICD @editionId, @icdId
	
	*/
CREATE PROCEDURE dbo.plm_spGetProductsByICD

	@editionId  int,
	@icdId  int
	
AS
BEGIN

	
      SELECT DISTINCT 
			
			pbe.ProductId,
			pbe.Brand,
			pbe.ProductActive,
			pbe.DivisionId,
			pbe.DivisionName,
			pbe.DivisionShortName,
			pbe.PharmaFormId,
			pbe.PharmaForm,
			pbe.CategoryId,
			pbe.CategoryName
			

		FROM plm_vwProductsByEdition pbe

		INNER JOIN ProductICD picd ON (pbe.ProductId = picd.ProductId)
		
		Where pbe.EditionId=@editionId AND
		
		picd.ICDId=@icdId
		
		ORDER BY pbe.Brand
    
END
GO
