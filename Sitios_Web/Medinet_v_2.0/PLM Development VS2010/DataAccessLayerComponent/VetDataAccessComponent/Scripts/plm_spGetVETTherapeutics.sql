use PEV

IF OBJECT_ID('[dbo].[plm_spGetVETTherapeutics]') IS NOT NULL
	DROP PROCEDURE [dbo].[plm_spGetVETTherapeutics]

GO

/*
       Author:          César Avila	
       		 
	   Object:			[dbo].[plm_spGetVETTherapeutics]
	
	   Description:	    

	   EXECUTE          
	
	*/
CREATE PROCEDURE [dbo].[plm_spGetVETTherapeutics]

    @editionId  int,
	@searchText varchar(200)= NULL,
	
	
	@therapeuticId		int = NULL
	
	
AS
BEGIN
  
   -- Get Therapeutics All     
     IF(@therapeuticId IS NOT NULL )
                                    
      
       
      BEGIN
      
          SELECT  T.TherapeuticId,T.TherapeuticName,T.ParentId,T.Active
          
          FROM plm_vwProductsByEdition PBE
          
          INNER JOIN ProductTherapeuticUses PTU ON PBE.ProductId=PTU.ProductId
          INNER JOIN TherapeuticUses  T  ON PTU.TherapeuticId=T.TherapeuticId
          
          
          WHERE PBE.NumberEdition=  @editionId AND
                T.Active =1                    AND 
            T.TherapeuticName COLLATE SQL_Latin1_General_CP1_CI_AI LIKE CASE WHEN @searchText IS NULL THEN '%'
											   WHEN LEN(@searchText) > 3 THEN '%' + @searchText + '%'
												ELSE @searchText + '%' END
      
      
      
        RETURN @@ROWCOUNT 
      
      
     END	
      
      
     END	
      
  
 
