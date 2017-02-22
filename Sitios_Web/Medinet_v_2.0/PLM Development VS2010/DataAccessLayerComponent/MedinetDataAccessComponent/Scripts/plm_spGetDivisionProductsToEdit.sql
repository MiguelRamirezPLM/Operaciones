IF OBJECT_ID('dbo.plm_spGetDivisionProductsToEdit') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetDivisionProductsToEdit
go

/* 
	Author:			Ramiro Sánchez
	Object:			dbo.plm_spGetDivisionProductsToEdit
	
	Description:	Gets all Products which are not relationated with a Division given
	Company:		PLM Latina.

	EXECUTE dbo.plm_spGetDivisionProductsToEdit @divisionId = 3, @countryId = 11
	EXECUTE dbo.plm_spGetDivisionProductsToEdit @divisionId = 3, @countryId = 11, @divisionSearch = 3
	EXECUTE dbo.plm_spGetDivisionProductsToEdit @divisionId = 3, @countryId = 11, @divisionSearch = 2, @brand = 'dol%'
	
 */ 

CREATE PROCEDURE dbo.plm_spGetDivisionProductsToEdit
(
	@divisionId			int = Null,
	@countryId			int = Null,
	@divisionSearch		int = Null,
	@brand				varchar(100) = Null
)
AS
BEGIN

	-- The parameters shouldn't be null:
	IF (@divisionId IS NULL OR 
		@countryId IS NULL)
	BEGIN
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
	END

	Select Distinct 
			v.DivisionId, 
			v.DivisionName, 
			v.DivisionShortName, 
			v.ProductId, 
			v.Brand, 
			v.PharmaFormId, 
			v.PharmaForm
	From plm_vwProductsByCountry v
		Left join (
			Select Distinct ProductId, PharmaFormId
				From EditionDivisionProducts
				Where DivisionId = @divisionId
		) t On v.ProductId = t.ProductId
			And v.PharmaFormId = t.PharmaFormId
	Where v.DivisionId Is Not Null
		And v.DivisionId <> @divisionId
		And v.CountryId = @countryId
		And t.ProductId Is Null 
		And t.pharmaFormid Is Null
		And v.DivisionId = Case When @divisionSearch Is Not Null Then @divisionSearch Else v.DivisionId End
		And v.Brand COLLATE SQL_Latin1_General_CP1_CI_AI LIKE CASE WHEN @brand IS NOT NULL THEN @brand
										ELSE v.Brand END
	Order By v.DivisionShortName, v.Brand, v.PharmaForm

END
go