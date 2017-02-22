IF OBJECT_ID('dbo.plm_spGetIndicationsWithoutProduct') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetIndicationsWithoutProduct
go

/* 
	Author:			Ramiro Sánchez
	Object:			dbo.plm_spGetIndicationsWithoutProduct
	
	Description:	Gets all indications which are not relationated with a product
	Company:		PLM Latina.

	EXECUTE dbo.plm_spGetIndicationsWithoutProduct @productId = 6088, @indication = 'a%'

 */ 

CREATE PROCEDURE dbo.plm_spGetIndicationsWithoutProduct
(
	@productId		int = Null,
	@indication		varchar(100) = Null
)
AS
BEGIN

	-- The parameters shouldn't be null:
	IF (@productId IS NULL)
	BEGIN
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
	END

	Select Distinct	i.IndicationId,
			i.ParentId,
			i.[Description],
			i.Active
		From Indications i
		Where i.Active = 1
			And i.IndicationId NOT IN(Select Distinct pin.IndicationId
											From ProductIndications pin
												Inner Join Products p On pin.ProductId = p.ProductId
											Where p.ProductId = @productId
												And p.Active = 1)
			And i.[Description] COLLATE SQL_Latin1_General_CP1_CI_AI LIKE CASE WHEN @indication IS NOT NULL THEN @indication
										ELSE '%' END
	Order by i.[Description]
END
go
