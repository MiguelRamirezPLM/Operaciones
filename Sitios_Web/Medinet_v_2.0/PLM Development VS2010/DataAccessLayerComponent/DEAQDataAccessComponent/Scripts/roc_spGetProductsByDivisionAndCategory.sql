IF OBJECT_ID('dbo.roc_spGetProductsByDivisionAndCategory') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetProductsByDivisionAndCategory
go

/* 
	Author:			Daniel Campos / Ramiro Sánchez				 
	Object:			dbo.roc_spGetProductsByDivisionAndCategory
	
	Description:	Retrieves all Products by Edition, Category and Division.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetProductsByDivisionAndCategory @editionId = 4, @categoryId = 1, @divisionId = 1

 */ 

CREATE PROCEDURE dbo.roc_spGetProductsByDivisionAndCategory
(
	@editionId			int,
	@categoryId			int,
	@divisionId			int
)
AS
BEGIN

	-- The parameters shouldn't be null:
	IF (@editionId IS NULL OR @categoryId IS NULL OR @divisionId IS NULL)
	
	BEGIN
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
	END

	SELECT P.ProductId, P.ProductName, P.Description, P.Register,
		ED.PharmaFormId,CASE WHEN PF.PharmaForm = 'Sin Asignar' THEN '' ELSE PF.PharmaForm END AS PharmaForm, P.LaboratoryId, ED.DivisionId, ED.CategoryId
		FROM Products AS P 
		INNER JOIN EditionDivisionProducts AS ED ON ED.ProductId=P.ProductId
		INNER JOIN PharmaForms AS PF ON PF.PharmaFormId=ED.PharmaFormId
		WHERE  PF.Active = 1
			AND P.Active = 1
			AND ED.EditionId = @editionId 
			AND ED.CategoryId = @categoryId 
			AND ED.DivisionId = @divisionId
		ORDER BY P.ProductName
END
go