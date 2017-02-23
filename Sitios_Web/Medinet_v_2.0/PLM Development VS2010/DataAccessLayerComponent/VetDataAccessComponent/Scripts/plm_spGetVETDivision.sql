use PEV

IF OBJECT_ID('[dbo].[plm_spGetVETDivision]') IS NOT NULL
	DROP PROCEDURE [dbo].[plm_spGetVETDivision]

GO

/*
       Author:          César Avila	
       		 
	   Object:			[dbo].[plm_spGetVETDivision]
	
	   Description:	    

	   EXECUTE          
	
	*/
CREATE PROCEDURE [dbo].[plm_spGetVETDivision]

    @editionId  int,
	@searchText varchar(200)= NULL,
	
	
	@divisionId		int = NULL
	
	
AS
BEGIN
  
   -- Get Division All     
     IF(@divisionId IS NOT NULL )
                                    
      BEGIN
      
          SELECT  D.DivisionId,D.DivisionName,D.ShortName,D.CountryId,D.Active
          
          FROM plm_vwProductsByEdition PBE
          
          INNER JOIN Divisions D ON PBE.DivisionId=D.DivisionId
          
          WHERE PBE.NumberEdition=  @editionId AND
                D.Active =1                    AND 
            D.DivisionName COLLATE SQL_Latin1_General_CP1_CI_AI LIKE CASE WHEN @searchText IS NULL THEN '%'
											   WHEN LEN(@searchText) > 3 THEN '%' + @searchText + '%'
												ELSE @searchText + '%' END
      
      
      
        RETURN @@ROWCOUNT 
      
      
     END	
       
      
      
      
  END	