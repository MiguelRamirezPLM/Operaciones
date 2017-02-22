IF OBJECT_ID('dbo.plm_spGetProductsInformation') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetProductsInformation

GO

/*
       Author:          César Avila	
       		 
	   Object:			dbo.plm_spGetProductsInformation
	
	   Description:	    Retrieves Product by 

	   EXECUTE          dbo.plm_spGetProductsByICD @EditionId,
	                                               @DivisionId,
	                                               @CategoryId,
	                                               @ProductId,
	                                               @PharmaFormId
	
	*/
CREATE PROCEDURE dbo.plm_spGetProductsInformation

	@editionId  int = NULL,
	@divisionId int = NULL,
	@categoryId int = NULL,
	@pharmaFormId int =NULL,
	@productId int  = NULL
	
	
AS
BEGIN

	
      -- Get products by Information 
	IF (@editionId IS NOT NULL AND @divisionId IS NOT NULL AND  @categoryId IS NOT NULL AND 
	     @productId IS NOT NULL AND @pharmaFormId IS NOT NULL)
	     
	     BEGIN
	     
	        SELECT DISTINCT 
			
			PBE.ProductId,
			PBE.Brand,
			PBE.PharmaFormId,
			PBE.PharmaForm,
			PBE.DivisionId,
			PBE.DivisionName,
			PBE.DivisionShortName,
			PBE.CategoryId,
			PBE.CategoryName

		FROM plm_vwProductsByEdition PBE
	     
	    WHERE
	      
	      PBE.EditionId=@editionId AND
	      PBE.DivisionId=@divisionId AND
	      PBE.CategoryId= @categoryId AND
	      PBE.ProductId =   @productId AND
	      PBE.PharmaFormId = @pharmaFormId AND
	     
	     
	                PBE.LabActive				= 1 AND
					pbe.DivisionActive			= 1 AND
					pbe.ProductActive			= 1 AND
					pbe.PharmaActive		    = 1 
	     
	     RETURN @@ROWCOUNT
	      
	     END
    
END
GO
