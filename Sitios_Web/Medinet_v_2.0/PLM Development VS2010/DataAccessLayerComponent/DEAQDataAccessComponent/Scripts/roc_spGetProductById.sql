IF OBJECT_ID('dbo.roc_spGetProductById') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetProductById
go

/* 
	Author:			Daniel Campos / Ramiro Sánchez				 
	Object:			dbo.roc_spGetProductById
	
	Description:	Retrieves information about Product by Edition and ProductId.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetProductById @editionId = 2, @productId = 103

 */ 

CREATE PROCEDURE dbo.roc_spGetProductById
(
	@editionId			int,
	@productId			int
)
AS
BEGIN

	-- The parameters shouldn't be null:
	IF (@editionId IS NULL OR @productId IS NULL)
	
	BEGIN
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
	END

	SELECT P.ProductId, P.ProductName, P.Description, P.Register, ED.PharmaFormId, 
		P.LaboratoryId, L.LaboratoryName, ED.DivisionId, D.DivisionName, ED.CategoryId
		FROM Products AS P 
		INNER JOIN EditionDivisionProducts AS ED ON ED.ProductId=P.ProductId
		INNER JOIN Divisions AS D ON D.DivisionId=ED.DivisionId
		INNER JOIN Laboratories AS L ON L.LaboratoryId=P.LaboratoryId
		WHERE P.Active = 1 
			AND ED.EditionId = @editionId 
			AND P.ProductId = @productId
	
END
go