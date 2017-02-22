IF OBJECT_ID('dbo.plm_spGetActiveSubstancesWithoutProduct') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetActiveSubstancesWithoutProduct
go

/* 
	Author:			Ramiro Sánchez
	Object:			dbo.plm_spGetActiveSubstancesWithoutProduct
	
	Description:	Gets all active substances which are not relationated with a product
	Company:		PLM Latina.

	EXECUTE dbo.plm_spGetActiveSubstancesWithoutProduct @productId = 6088, @substance = 'a%'

 */ 

CREATE PROCEDURE dbo.plm_spGetActiveSubstancesWithoutProduct
(
	@productId		int = Null,
	@substance		varchar(100) = Null
)
AS
BEGIN

	-- The parameters shouldn't be null:
	IF (@productId IS NULL)
	BEGIN
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
	END

	Select Distinct	acs.ActiveSubstanceId,
			acs.[Description],
			acs.EnglishDescription,
			acs.Active,
			acs.Enunciative
		From ActiveSubstances acs
		Where acs.Active = 1
			And acs.ActiveSubstanceId NOT IN(Select Distinct ps.ActiveSubstanceId
											From ProductSubstances ps
												Inner Join Products p On ps.ProductId = p.ProductId
											Where p.ProductId = @productId
												And p.Active = 1)
			And acs.[Description] COLLATE SQL_Latin1_General_CP1_CI_AI LIKE CASE WHEN @substance IS NOT NULL THEN @substance
										ELSE '%' END
	Order by acs.[Description]

END
go
