IF OBJECT_ID('dbo.plm_spGetICDProduct') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetICDProduct

GO

/*
       Author:          César Avila	
       		 
	   Object:			dbo.plm_spGetICDProduct
	
	   Description:	    Retrieves ICD by EditionId and ProductId

	   EXECUTE          dbo.plm_spGetICDProduct @editionId, @ProductId
	
	*/
CREATE PROCEDURE dbo.plm_spGetICDProduct

	@editionId  int,
	@productId  int
	
AS
BEGIN

	
      SELECT DISTINCT  I.ICDId,I.ParentId,I.ICDKey,I.SpanishDescription,I.EnglishDescription,I.Active
      
      FROM plm_vwProductsByEdition vw
      
      INNER JOIN  ProductICD  P ON vw.ProductId=P.ProductId 
      
      INNER JOIN ICD as I ON P.ICDId=I.ICDId
      
      WHERE vw.EditionId=@editionId and vw.ProductId=@productId
      
      ORDER BY I.ICDId
      
      RETURN @@ROWCOUNT 
    
END
GO
