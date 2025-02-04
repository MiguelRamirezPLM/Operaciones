
IF OBJECT_ID('dbo.plm_spGetICDByText') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetICDByText

GO

/*
       Author:          César Avila	
       		 
	   Object:			dbo.plm_spGetICDProduct
	
	   Description:	    Retrieves ICD by EditionId and Text.

	   EXECUTE          dbo.plm_spGetICDProduct @editionId, @Seach
	
	*/
CREATE PROCEDURE [dbo].[plm_spGetICDByText]

	@editionId  int,
	@search varchar(200) 
	
AS
BEGIN

 -- Get ICD by EditionId and Search

 
      SELECT DISTINCT  I.ICDId,I.ParentId,I.ICDKey,I.SpanishDescription,I.EnglishDescription,I.Active
      
      FROM plm_vwProductsByEdition vw
      
      INNER JOIN  ProductICD  P ON vw.ProductId=P.ProductId 
      
      INNER JOIN ICD as I ON P.ICDId=I.ICDId
      
      WHERE vw.EditionId=  @editionId AND
            I.SpanishDescription COLLATE SQL_Latin1_General_CP1_CI_AI LIKE CASE WHEN @search IS NULL THEN '%'
											   WHEN LEN(@search) > 3 THEN '%' + @search + '%'
												ELSE @search + '%' END
      
      RETURN @@ROWCOUNT 
         
  END
  
