IF OBJECT_ID('dbo.roc_spGetActiveSubstancesByProduct') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetActiveSubstancesByProduct
go

/* 
	Author:			Daniel Campos / Ramiro Sánchez				 
	Object:			dbo.roc_spGetActiveSubstancesByProduct
	
	Description:	Retrieves all Active Substances by ProductId.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetActiveSubstancesByProduct @productId = 957


 */ 

CREATE PROCEDURE dbo.roc_spGetActiveSubstancesByProduct
(
	@productId				int
)

AS
BEGIN

	-- The parameters shouldn't be null:
	IF (@productId IS NULL)
	BEGIN
	
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
		
	END

	SELECT ActiveSubstances.* FROM ProductSubstances 
		INNER JOIN ActiveSubstances ON ActiveSubstances.ActiveSubstanceId = ProductSubstances.ActiveSubstanceId
		WHERE ProductSubstances.ProductId = @productId AND ActiveSubstances.Active = 1
	
END
go