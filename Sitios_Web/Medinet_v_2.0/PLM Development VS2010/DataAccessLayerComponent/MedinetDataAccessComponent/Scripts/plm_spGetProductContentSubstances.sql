IF OBJECT_ID('dbo.plm_spGetProductContentSubstances') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetProductContentSubstances
go

/* 
	Author:			Ramiro Sánchez
	Object:			dbo.plm_spGetProductContentSubstances
	
	Description:	Gets all ProductContentSubstances given a product and an active substance
	Company:		PLM Latina.

	EXECUTE dbo.plm_spGetProductContentSubstances @activesubstanceId = 849 
	EXECUTE dbo.plm_spGetProductContentSubstances @productId = 10361, @activesubstanceId = 849 

 */ 

CREATE PROCEDURE dbo.plm_spGetProductContentSubstances
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
		From ProductContentSubstances
		Where ProductId = @productId
			And ActiveSubstanceId = @activesubstanceId
	
	END

	IF(@productId Is Null)
	BEGIN

		Select Distinct	ProductContentId
					,ProductId
					,ActiveSubstanceId
		From ProductContentSubstances
		Where ActiveSubstanceId = @activesubstanceId
	
	END

END
go