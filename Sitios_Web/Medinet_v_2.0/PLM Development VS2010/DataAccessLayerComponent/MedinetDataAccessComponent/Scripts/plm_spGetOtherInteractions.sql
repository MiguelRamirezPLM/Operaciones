IF OBJECT_ID('dbo.plm_spGetOtherInteractions') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetOtherInteractions
go

/* 
	Author:			Ramiro Sánchez
	Object:			dbo.plm_spGetOtherInteractions
	
	Description:	Gets all OtherInteractions given a product and an active substance
	Company:		PLM Latina.

	EXECUTE dbo.plm_spGetOtherInteractions @productId = 10361, @activesubstanceId = 849 
	EXECUTE dbo.plm_spGetOtherInteractions @activesubstanceId = 849 

 */ 

CREATE PROCEDURE dbo.plm_spGetOtherInteractions
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
					,ElementId
		From OtherInteractions
		Where ProductId = @productId
			And ActiveSubstanceId = @activesubstanceId

		RETURN @@ROWCOUNT

	END

	IF(@productId Is Null)
	BEGIN

		Select Distinct	ProductContentId
					,ProductId
					,ActiveSubstanceId
					,ElementId
		From OtherInteractions
		Where ActiveSubstanceId = @activesubstanceId

		RETURN @@ROWCOUNT

	END

END
go