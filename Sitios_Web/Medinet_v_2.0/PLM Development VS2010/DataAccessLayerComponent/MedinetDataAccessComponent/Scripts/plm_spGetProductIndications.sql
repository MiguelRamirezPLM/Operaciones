IF OBJECT_ID('dbo.plm_spGetProductIndications') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetProductIndications
go

/* 
	Author:			Ramiro Sánchez
	Object:			dbo.plm_spGetProductIndications
	
	Description:	Gets all plm_spGetProductIndications given a product and an indication
	Company:		PLM Latina.

	EXECUTE dbo.plm_spGetProductIndications @productId = 10361, @@indicationId = 849
	EXECUTE dbo.plm_spGetProductIndications @@indicationId = 849

 */ 

CREATE PROCEDURE dbo.plm_spGetProductIndications
(
	@productId		int = Null,
	@indicationId	int
)
AS
BEGIN

	IF(@productId Is Not Null)
	BEGIN

		Select Distinct	ProductId
					,IndicationId
		From ProductIndications
		Where ProductId = @productId
			And IndicationId = @indicationId

		RETURN @@ROWCOUNT

	END

	IF(@productId Is Null)
	BEGIN

		Select Distinct	ProductId
					,IndicationId
		From ProductIndications
		Where IndicationId = @indicationId

		RETURN @@ROWCOUNT

	END

END
go