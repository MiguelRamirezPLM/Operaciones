IF OBJECT_ID('dbo.roc_spGetProductsByActiveSubstance') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetProductsByActiveSubstance
go

/* 
	Author:			Daniel Campos / Ramiro Sánchez				 
	Object:			dbo.roc_spGetProductsByActiveSubstance
	
	Description:	Retrieves all Products by Edition and ActiveSubstance.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetProductsByActiveSubstance @activeSubstanceId = 123, @editionId = 6

 */ 

CREATE PROCEDURE dbo.roc_spGetProductsByActiveSubstance
(
	@activeSubstanceId		int,
	@editionId				int
)
AS
BEGIN

	-- The parameters shouldn't be null:
	IF (@activeSubstanceId IS NULL OR @editionId IS NULL)
	
	BEGIN
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
	END

	SELECT DISTINCT (P.ProductId), P.ProductName, P.Description, P.Register, PF.PharmaFormId, 
		CASE WHEN PF.PharmaForm = 'Sin Asignar' THEN '' ELSE PF.PharmaForm END AS PharmaForm, L.LaboratoryName, D.DivisionName, EDP.CategoryId 
		FROM Products AS P
		INNER JOIN ProductSubstances AS PS ON PS.ProductId = P.ProductId
		INNER JOIN ProductPharmaForms AS PPF ON PPF.ProductId = P.ProductId
		INNER JOIN ActiveSubstances AS ASU ON ASU.ActiveSubstanceId = PS.ActiveSubstanceId
		INNER JOIN Laboratories AS L ON L.LaboratoryId = P.LaboratoryId
		INNER JOIN EditionDivisionProducts AS EDP ON EDP.ProductId = P.ProductId
		INNER JOIN PharmaForms AS PF ON PF.PharmaFormId=EDP.PharmaFormId
		INNER JOIN Divisions AS D ON D.DivisionId = EDP.DivisionId
		WHERE ASU.ActiveSubstanceId = @activeSubstanceId
			AND P.Active = 1
			AND EDP.EditionId = @editionId 
			AND L.Active = 1
			AND D.Active = 1 
			AND D.LaboratoryId = L.LaboratoryId
			AND (SELECT COUNT(*) FROM ProductSubstances 
				INNER JOIN ActiveSubstances ON ActiveSubstances.ActiveSubstanceId = ProductSubstances.ActiveSubstanceId
				WHERE  ProductSubstances.ProductId = P.ProductId  AND ActiveSubstances.Active = 1) < 2
		ORDER BY P.ProductName ASC
END
go