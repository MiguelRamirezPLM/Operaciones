IF OBJECT_ID('dbo.plm_spGetProductSubstances') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetProductSubstances
go

/* 
	Author:			Ramiro Sánchez
	Object:			dbo.plm_spGetProductSubstances
	
	Description:	Gets all PharmaGroupInteractions given a product and an active substance
	Company:		PLM Latina.

	EXECUTE dbo.plm_spGetProductSubstances @productId = 10361, @activesubstanceId = 849
	EXECUTE dbo.plm_spGetProductSubstances @activesubstanceId = 849

 */ 

CREATE PROCEDURE dbo.plm_spGetProductSubstances
(
	@productId			int = Null,
	@activesubstanceId	int
)
AS
BEGIN

	IF(@productId Is Not Null)
	BEGIN

		Select Distinct	ProductId
					,ActiveSubstanceId
		From ProductSubstances
		Where ProductId = @productId
			And ActiveSubstanceId = @activesubstanceId

		RETURN @@ROWCOUNT

	END

	IF(@productId Is Null)
	BEGIN

		Select Distinct	ProductId
					,ActiveSubstanceId
		From ProductSubstances
		Where ActiveSubstanceId = @activesubstanceId

		RETURN @@ROWCOUNT

	END

END
go