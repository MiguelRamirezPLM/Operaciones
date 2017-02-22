IF OBJECT_ID('dbo.plm_spGetProductByCode') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetProductByCode
go

/* 
	Author:			Ulises Orihuela		 
	Object:			dbo.plm_spGetProductByCode
	
	Description:	Get ProductId, PharmaFormId, DivisionId and CategoryId associated to BarCode 

	Company:		PLM Latina.

	EXECUTE dbo.plm_spGetProductByCode @barcode =7501871721184

 */ 

CREATE PROCEDURE dbo.plm_spGetProductByCode
(
	@barCode			varchar(20)	
)

AS
BEGIN

	-- The parameters shouldn't be null:
	IF (@barcode IS NULL)
	BEGIN
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
	END

SELECT ProductId, PharmaFormId, DivisionId, CategoryId
FROM ProductCodes 
WHERE BarCode = @barCode

	

END
