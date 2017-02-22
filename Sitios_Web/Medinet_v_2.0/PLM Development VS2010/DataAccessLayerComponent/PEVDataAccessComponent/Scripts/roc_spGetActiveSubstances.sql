IF OBJECT_ID('dbo.roc_spGetActiveSubstances') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetActiveSubstances
go

/* 
	Author:			Daniel Campos / Ramiro Sánchez				 
	Object:			dbo.roc_spGetActiveSubstances
	
	Description:	Retrieves all Active Substances by EditionId.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetActiveSubstances @editionId = 1, @numberByPage = 10, @page = 0


 */ 

CREATE PROCEDURE dbo.roc_spGetActiveSubstances
(
	@editionId				int,
	@page					int,
	@numberByPage			int
)
AS
BEGIN

	-- The parameters shouldn't be null:
	IF (@editionId IS NULL OR @page IS NULL OR @numberByPage IS NULL)
	BEGIN
	
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
		
	END

	SELECT (
		SELECT count(*) from 
		(
			SELECT DISTINCT (ActiveSubstances.ActiveSubstanceName),ActiveSubstances.ActiveSubstanceId FROM Products 
				INNER JOIN ProductSubstances ON ProductSubstances.ProductId = Products.ProductId
				INNER JOIN ActiveSubstances ON ActiveSubstances.ActiveSubstanceId = ProductSubstances.ActiveSubstanceId
				INNER JOIN EditionDivisionProducts ON EditionDivisionProducts.ProductId = Products.ProductId
				WHERE  EditionDivisionProducts.EditionId = @editionId AND ActiveSubstances.Active = 1 
		) total

	) AS TOTAL,*   

	FROM (

		SELECT DISTINCT (ActiveSubstances.ActiveSubstanceName),ActiveSubstances.ActiveSubstanceId, 
			ROW_NUMBER() OVER (ORDER BY ActiveSubstances.ActiveSubstanceName ASC) AS RowNumber  FROM Products 
			INNER JOIN ProductSubstances ON ProductSubstances.ProductId = Products.ProductId
			INNER JOIN ActiveSubstances ON ActiveSubstances.ActiveSubstanceId = ProductSubstances.ActiveSubstanceId
			INNER JOIN EditionDivisionProducts ON EditionDivisionProducts.ProductId = Products.ProductId
			WHERE  EditionDivisionProducts.EditionId = @editionId AND ActiveSubstances.Active = 1 
			GROUP BY ActiveSubstances.ActiveSubstanceName, ActiveSubstances.ActiveSubstanceId
	)AS ACTIVE_SUBSTANCES

	WHERE RowNumber BETWEEN @numberByPage * @page + 1 AND @numberByPage * (@page + 1)
	
END
go