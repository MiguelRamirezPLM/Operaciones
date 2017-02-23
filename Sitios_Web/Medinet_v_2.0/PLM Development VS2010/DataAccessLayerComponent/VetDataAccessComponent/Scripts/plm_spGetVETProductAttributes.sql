use PEV

IF OBJECT_ID('[dbo].[plm_spGetVETProductAttributes]') IS NOT NULL
	DROP PROCEDURE [dbo].[plm_spGetVETProductAttributes]

GO

/*
       Author:          César Avila	
       		 
	   Object:			[dbo].[plm_spGetVETProductAttributes]
	
	   Description:	    

	   EXECUTE          
	
	*/
CREATE PROCEDURE [dbo].[plm_spGetVETProductAttributes]

    @editionId		int,
	@divisionId		int,
	@categoryId		int,
	@productId		int,
	@pharmaFormId	int
	
	
AS
BEGIN
  
  
     SELECT DISTINCT  PBE.ProductId,PBE.ProductName,PBE.PharmaFormId,PBE.PharmaForm, PBE.DivisionName
       FROM plm_vwProductsByEdition PBE
         INNER JOIN ProductContents PC ON
                                        PBE.EditionId=PC.EditionId AND
                                        PBE.DivisionId=PC.DivisionId AND
                                        PBE.CategoryId=PC.CategoryId AND
                                        PBE.ProductId=PC.ProductId AND
                                        PBE.PharmaFormId=PC.PharmaFormId
              
      WHERE PBE.EditionId=@editionId AND
            PBE.DivisionId=@divisionId AND
            PBE.CategoryId=@categoryId AND
            PBE.PharmaFormId=@pharmaFormId
      
      ORDER BY 2
      
      RETURN @@ROWCOUNT
      
      
  END	