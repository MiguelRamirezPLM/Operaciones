IF OBJECT_ID('dbo.plm_spGetBrandsByClient') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetBrandsByClient
go

/* 
	Author:			Ulises Orihuela.
				 
	Object:			dbo.plm_spGetBrandsByClient
	
	Description:	Retrieves all the brands by client.

	Company:		PLM Latina.

	EXECUTE dbo.getBrandsByClient @editionId = 8  @clientId = 1


 */ 

CREATE PROCEDURE [dbo].[plm_spGetBrandsByClient]
(
	@editionId				int,
	@clientId				int
)
AS
BEGIN

	-- The parameters shouldn't be null:
	IF (@editionId IS NULL OR @clientId IS NULL)
	BEGIN
	
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
		
	END

	SELECT Brands.BrandId, 
		   Brands.BrandName, 
		   Brands.Logo, 
		   Brands.Active,
		   ClientBrands.BaseUrl,
		   ClientBrands.ClientId,
		   Clients.EditionId 
		FROM  Brands
			INNER JOIN ClientBrands ON(Brands.BrandId = ClientBrands.BrandId)
			INNER JOIN Clients ON(ClientBrands.ClientId = Clients.ClientId) 
		WHERE ClientBrands.ClientId = @clientId AND 
			  ClientBrands.Active = 1 AND 
			  Brands.Active = 1 AND
			  Clients.EditionId = @editionId

	
	RETURN @@ROWCOUNT
	
		
END
