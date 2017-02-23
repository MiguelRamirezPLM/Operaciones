use PEV

IF OBJECT_ID('[dbo].[plm_spGetVETSpecies]') IS NOT NULL
	DROP PROCEDURE [dbo].[plm_spGetVETSpecies]

GO

/*
       Author:          César Avila	
       		 
	   Object:			[dbo].[plm_spGetVETSpecies
	
	   Description:	    

	   EXECUTE          
	
	*/
CREATE PROCEDURE [dbo].[plm_spGetVETSpecies]

    @editionId  int,
	@searchText varchar(200)= NULL,
	
	@specieId           int = NULL
	

	
	
AS
BEGIN
  
   -- Get Division All     
     IF(@specieId IS NOT NULL )
                                    
       BEGIN
      
          SELECT  S.SpecieId,S.SpecieName,S.Active
          
          FROM plm_vwProductsByEdition PBE
          
          INNER JOIN ProductSpecies PS ON PBE.ProductId=PS.ProductId
          INNER JOIN Species S         ON Ps.SpecieId=S.SpecieId   
          
          WHERE PBE.NumberEdition=  @editionId AND
                S.Active =1                    AND 
            S.SpecieName COLLATE SQL_Latin1_General_CP1_CI_AI LIKE CASE WHEN @searchText IS NULL THEN '%'
											   WHEN LEN(@searchText) > 3 THEN '%' + @searchText + '%'
												ELSE @searchText + '%' END
      
      
      
        RETURN @@ROWCOUNT 
      
      
     END	
       
      
      
      
  END	