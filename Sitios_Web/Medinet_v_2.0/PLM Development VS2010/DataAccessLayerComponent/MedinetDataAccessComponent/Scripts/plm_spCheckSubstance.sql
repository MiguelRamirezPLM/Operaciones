IF OBJECT_ID('dbo.plm_spCheckSubstance') IS NOT NULL
	DROP PROCEDURE dbo.plm_spCheckSubstance
go

/* 
	Author:			Juan Ramírez / Ramiro Sánchez
	Object:			dbo.plm_spCheckSubstance
	
	Description:	Checks if a substance is associated with a product.
	Company:		PLM Latina.

	EXECUTE dbo.plm_spCheckSubstance @activeSubstanceId = 8505
	EXECUTE dbo.plm_spCheckSubstance @productId = 80, @activeSubstanceId = 8505

 */ 

CREATE PROCEDURE dbo.plm_spCheckSubstance
(
	@productId			int = Null,
	@activeSubstanceId	int
)
AS
BEGIN

	DECLARE @productSubs int

	IF(@productId Is Not Null)
	BEGIN

		SELECT @productSubs = Count(*)
		FROM ProductSubstances
		
		WHERE ProductId = @productId AND
			  ActiveSubstanceId = @activeSubstanceId

		RETURN @productSubs
	
	END
	
	IF(@productId Is Null)
	BEGIN

		SELECT @productSubs = Count(*)
		FROM ProductSubstances
		
		WHERE ActiveSubstanceId = @activeSubstanceId

		RETURN @productSubs
	
	END


END