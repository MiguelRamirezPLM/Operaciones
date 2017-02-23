
use PEV

IF OBJECT_ID('[dbo].[plm_spGetVETProducts]') IS NOT NULL
	DROP PROCEDURE [dbo].[plm_spGetVETProducts]

GO

/*
       Author:          César Avila	
       		 
	   Object:			[dbo].[plm_spGetVETProducts]
	
	   Description:	    

	   EXECUTE          
	
	*/
CREATE PROCEDURE [dbo].[plm_spGetVETProducts]

    @editionId  int,
	@searchText varchar(200)= NULL,
	
	@productId          int = NULL
	
	
AS
BEGIN
  
     -- Get Products All

      IF(@searchText IS NOT NULL)
      
      BEGIN
      
          SELECT  DISTINCT
            PBE.ProductId,
			PBE.ProductName,
			PBE.PharmaFormId,
			PBE.PharmaForm,
			PBE.DivisionId,
			PBE.DivisionName,
			PBE.DivisionShortName,
			PBE.CategoryId,
			PBE.CategoryName
          
          FROM plm_vwProductsByEdition PBE
          
         
          
          
          WHERE PBE.NumberEdition=  @editionId AND
                PBE.ProductActive=1                     AND 
            PBE.ProductName COLLATE SQL_Latin1_General_CP1_CI_AI LIKE CASE WHEN @searchText IS NULL THEN '%'
											   WHEN LEN(@searchText) > 3 THEN '%' + @searchText + '%'
												ELSE @searchText + '%' END
      
      
      
        RETURN @@ROWCOUNT 
      
      
     END	
      
  
  


END



