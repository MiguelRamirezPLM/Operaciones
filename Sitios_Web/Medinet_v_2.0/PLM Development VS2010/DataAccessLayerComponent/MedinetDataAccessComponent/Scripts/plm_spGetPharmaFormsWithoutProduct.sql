IF OBJECT_ID('dbo.plm_spGetPharmaFormsWithoutProduct') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetPharmaFormsWithoutProduct
go

/* 
	Author:			Ramiro Sánchez
	Object:			dbo.plm_spGetPharmaFormsWithoutProduct
	
	Description:	Gets all pharmaceutical forms which are not relationated with a product
	Company:		PLM Latina.

	EXECUTE dbo.plm_spGetPharmaFormsWithoutProduct @productId = 6088

 */ 

CREATE PROCEDURE dbo.plm_spGetPharmaFormsWithoutProduct
(
	@productId		int = Null
)
AS
BEGIN

	-- The parameters shouldn't be null:
	IF (@productId IS NULL)
	BEGIN
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
	END

	Select Distinct	pf.PharmaFormId,
			pf.[Description],
			pf.EnglishDescription,
			pf.Active
		From PharmaceuticalForms pf
		Where pf.Active = 1
			And pf.PharmaFormId NOT IN(Select Distinct PharmaFormId
											From plm_vwProductsByEdition
											Where ProductId = @productId
												And Active = 1)
	Order by pf.[Description]
END
go
