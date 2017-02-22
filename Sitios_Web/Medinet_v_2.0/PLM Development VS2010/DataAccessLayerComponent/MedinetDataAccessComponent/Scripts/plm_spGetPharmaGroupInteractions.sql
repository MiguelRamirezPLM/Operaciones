IF OBJECT_ID('dbo.plm_spGetPharmaGroupInteractions') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetPharmaGroupInteractions
go

/* 
	Author:			Ramiro Sánchez
	Object:			dbo.plm_spGetPharmaGroupInteractions
	
	Description:	Gets all PharmaGroupInteractions given a product and an active substance
	Company:		PLM Latina.

	EXECUTE dbo.plm_spGetPharmaGroupInteractions @productId = 10361, @activesubstanceId = 849
	EXECUTE dbo.plm_spGetPharmaGroupInteractions @activesubstanceId = 849

 */ 

CREATE PROCEDURE dbo.plm_spGetPharmaGroupInteractions
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
					,PharmaGroupId
		From PharmaGroupInteractions
		Where ProductId = @productId
			And ActiveSubstanceId = @activesubstanceId

		RETURN @@ROWCOUNT

	END

	IF(@productId Is Null)
	BEGIN

		Select Distinct	ProductContentId
					,ProductId
					,ActiveSubstanceId
					,PharmaGroupId
		From PharmaGroupInteractions
		Where ActiveSubstanceId = @activesubstanceId

		RETURN @@ROWCOUNT

	END

END
go