IF OBJECT_ID('dbo.roc_spGetProductsByUseId') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetProductsByUseId
go

/* 
	Author:			Daniel Campos / Elizabeth Lazo.				 
	Object:			dbo.roc_spGetProductsByUseId
	
	Description:	Retrieves all information the products by uses.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetProductsByUseId @useId=85, @countryId=11, @editionId=4


 */ 

CREATE PROCEDURE dbo.roc_spGetProductsByUseId
(   
  @useId      int,
  @countryId  int,
  @editionId  int   
  
) 
AS
  BEGIN
  --The parameters shouldn't be null:
       IF ( @useId IS NULL OR @countryId IS NULL OR @editionId IS NULL ) 
             
  BEGIN

    RAISERROR ('The current operation requires more parameters', 16, 1)
    RETURN -1

  END
  
   SELECT DISTINCT AU.AgrochemicalUseId, AU.AgrochemicalUseName,PAU.ProductId,P.ProductName,P.Description,P.Register,P.LaboratoryId,EDP.DivisionId,EDP.CategoryId
	FROM AgrochemicalUses AS AU
	INNER JOIN ProductAgrochemicalUses AS PAU ON PAU.AgrochemicalUseId=AU.AgrochemicalUseId
	INNER JOIN EditionDivisionProducts AS EDP ON EDP.ProductId=PAU.ProductId
	INNER JOIN Products AS P ON P.ProductId=EDP.ProductId
	 WHERE AU.Active='1' AND EDP.EditionId=@editionId AND AU.AgrochemicalUseId=@useId AND P.CountryId=@countryId
	order by P.ProductName

END
go

