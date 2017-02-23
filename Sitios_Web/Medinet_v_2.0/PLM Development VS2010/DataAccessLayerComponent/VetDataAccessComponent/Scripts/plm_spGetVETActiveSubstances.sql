use PEV

IF OBJECT_ID('[dbo].[plm_spGetVETActiveSubstances]') IS NOT NULL
	DROP PROCEDURE [dbo].[plm_spGetVETActiveSubstances]

GO

/*
       Author:          César Avila	
       		 
	   Object:			[dbo].[plm_spGetVETActiveSubstances]
	
	   Description:	    

	   EXECUTE          
	
	*/
CREATE PROCEDURE [dbo].[plm_spGetVETActiveSubstances]

    @editionId  int,
	@searchText varchar(200)= NULL,
	
	
	@activeSubstanceId	int = NULL
	
	
AS
BEGIN
  
   -- Get ActiveSubstances All     
     IF(@searchText IS NOT NULL )
                                    
      
      BEGIN
      
          SELECT DISTINCT  A.ActiveSubstanceId,A.ActiveSubstanceName,A.Active
          
          FROM plm_vwProductsByEdition PBE
          
          INNER JOIN ProductSubstances PS ON PBE.ProductId=PS.ProductId
          INNER JOIN ActiveSubstances  A  ON PS.ActiveSubstanceId=A.ActiveSubstanceId
          
          
          WHERE PBE.EditionId=  @editionId AND
                A.Active =1                    AND 
            A.ActiveSubstanceName COLLATE SQL_Latin1_General_CP1_CI_AI LIKE CASE WHEN @searchText IS NULL THEN '%'
											   WHEN LEN(@searchText) > 3 THEN '%' + @searchText + '%'
												ELSE @searchText + '%' END
      
      
      
        RETURN @@ROWCOUNT 
      
      
     END	
      
  
  


END



