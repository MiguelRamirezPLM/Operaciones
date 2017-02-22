IF OBJECT_ID('dbo.roc_spGetClientsByBrand') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetClientsByBrand
go

/* 
	Author:			Benjamín Castillo Arriaga.				 
	Object:			dbo.roc_spGetClientsByBrand
	
	Description:	Retrieves all clients by  brand.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetClientsByBrand @editionId = 5, @brandId = 1492,@top=10


 */ 

CREATE PROCEDURE dbo.roc_spGetClientsByBrand
(
	@editionId				int,
	@brandId				int,
	@top					int
)
AS
BEGIN

	-- The parameters shouldn't be null:
	IF (@editionId IS NULL OR @brandId IS NULL)
	BEGIN
	
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
		
	END

	IF(@top IS NULL)
	BEGIN
		SELECT Clients.ClientId, 
			   Clients.CompanyName, 
			   Clients.ShortName, 
			   Clients.ClientTypeId 
		FROM ClientBrands 
			INNER JOIN Clients ON(Clients.ClientId = ClientBrands.ClientId)
		WHERE ClientBrands.BrandId = @brandId AND 
			  ClientBrands.Active = 1 AND 
			  Clients.Active = 1 AND 
			  Clients.EditionId = @editionId
	

	END
	ELSE
	BEGIN
		DECLARE @sql varchar(4000)

		SET @sql = '	SELECT TOP ' + CONVERT(VARCHAR(10),@top) + ' Clients.ClientId, ' + Char(13) +  
				   '		   Clients.CompanyName,		'  + Char(13) + 
				   '	       Clients.ShortName,		' + Char(13) + 
				   '	       Clients.ClientTypeId		' + Char(13) + 
				   '	FROM ClientBrands				' + Char(13) + 
				   '		INNER JOIN Clients ON(Clients.ClientId = ClientBrands.ClientId)' + Char(13) + 
				   '	WHERE	ClientBrands.BrandId = ' + CONVERT(VARCHAR(10),@brandId) + ' AND ' + Char(13) + 
				   '			ClientBrands.Active = 1 AND ' + Char(13) + 
				   '			Clients.Active = 1 AND ' + Char(13) + 
				   '			Clients.EditionId = ' + CONVERT(VARCHAR(10),@editionId) + ' ' + Char(13)  
		
		PRINT 'SQL: ' + @sql

		EXEC (@sql)

		RETURN @@ROWCOUNT
	END
		
END
go