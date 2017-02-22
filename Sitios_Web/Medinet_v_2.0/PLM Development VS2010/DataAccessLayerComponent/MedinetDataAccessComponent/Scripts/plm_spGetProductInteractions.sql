IF OBJECT_ID('dbo.plm_spGetProductInteractions') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetProductInteractions
go

/* 
	Author:			Ramiro Sánchez
	Object:			dbo.plm_spGetProductInteractions
	
	Description:	Gets all ProductInteractions given a product and an active substance
	Company:		PLM Latina.

	EXECUTE dbo.plm_spGetProductInteractions @productId = 10361, @activesubstanceId = 849
	EXECUTE dbo.plm_spGetProductInteractions @activesubstanceId = 849 

 */ 

CREATE PROCEDURE dbo.plm_spGetProductInteractions
(
	@productId			int = Null,
	@activesubstanceId	int
)
AS
BEGIN

	IF(@productId Is Not Null)
	BEGIN

		Select Distinct	ProductContentId
					,ProductId
					,ActiveSubstanceId
					,SubstanceInteractId
		From ProductInteractions
		Where ProductId = @productId
			And ActiveSubstanceId = @activesubstanceId

		RETURN @@ROWCOUNT

	END

	IF(@productId Is Null)
	BEGIN

		Select Distinct	ProductContentId
					,ProductId
					,ActiveSubstanceId
					,SubstanceInteractId
		From ProductInteractions
		Where ActiveSubstanceId = @activesubstanceId

		RETURN @@ROWCOUNT

	END

END
go