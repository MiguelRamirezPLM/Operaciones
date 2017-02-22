IF OBJECT_ID('dbo.roc_spGetActiveSubstancesByProduct') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetActiveSubstancesByProduct
go

/* 
	Author:			Daniel Campos / Ramiro Sánchez				 
	Object:			dbo.roc_spGetActiveSubstancesByProduct
	
	Description:	Retrieves all ActiveSubstances By Edition and product.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetActiveSubstancesByProduct @editionId = 2, @productId = 252

 */ 

CREATE PROCEDURE dbo.roc_spGetActiveSubstancesByProduct
(
	@editionId					int,
	@productId					int
)
AS
BEGIN

	-- The parameters shouldn't be null:
	IF (@editionId IS NULL OR @productId IS NULL)
	
	BEGIN
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
	END

	SELECT ASU.ActiveSubstanceId,ASU.ActiveSubstanceName,EDP.CategoryId,EDP.PharmaFormId,EDP.DivisionId
		FROM ProductSubstances AS PS
		INNER JOIN ActiveSubstances AS ASU ON ASU.ActiveSubstanceId=PS.ActiveSubstanceId
		INNER JOIN EditionDivisionProducts AS EDP ON EDP.ProductId = PS.ProductId
		WHERE ASU.Active = 1 
			AND EDP.EditionId = @editionId
			AND PS.ProductId = @productId 
		ORDER BY ASU.ActiveSubstanceName
	
END
go