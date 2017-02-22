IF OBJECT_ID('dbo.plm_spGetPharmacyProducts') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetPharmacyProducts
go

/* 
	Author:			Ramiro Sánchez				 
	Object:			dbo.plm_spGetPharmacyProducts
	
	Description:	Get Pharmacy Products By Text.

	Company:		PLM Latina.
	
	EXECUTE dbo.plm_spGetPharmacyProducts @companyClientId = 1, @textSearch = 'as'
	
 */ 

CREATE PROCEDURE dbo.plm_spGetPharmacyProducts
(
	@companyClientId		int,
	@textSearch				varchar(50)
)
AS
BEGIN

	-- The parameters shouldn't be null:
	IF (@companyClientId IS NULL OR @textSearch IS NULL)
	BEGIN
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
	END

	Select p.ProductId
		,p.CompanyClientId
		,p.ProductName
		,p.PharmaceuticalForms
		,p.ActiveSubstances
		,p.Indications
		,p.Presentation
		,p.PresentationContent
		,p.Laboratory
		,p.Active
	From PharmacyProducts p
	Where p.CompanyClientId = @companyClientId
		And p.Active = 1
		And p.ProductName LIKE 
			CASE WHEN LEN(@textSearch) <= 3	
			THEN @textSearch + '%' 
			ELSE '%' + @textSearch + '%' END

	RETURN @@ROWCOUNT	
	
END
go