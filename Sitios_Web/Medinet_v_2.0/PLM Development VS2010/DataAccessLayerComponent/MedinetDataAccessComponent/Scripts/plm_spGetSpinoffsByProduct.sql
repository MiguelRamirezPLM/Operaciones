IF OBJECT_ID('dbo.plm_spGetSpinoffsByProduct') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetSpinoffsByProduct
go

/* 
	Author:			Ramiro Sánchez
	Object:			dbo.plm_spGetSpinoffsByProduct
	
	Description:	Gets all the editable products from database by division.

	Company:		PLM Latina.

	EXECUTE dbo.plm_spGetSpinoffsByProduct @editionId = 55, @divisionId = 3, @categoryId = 103, @productId = 6660, @pharmaFormId = 210

 */ 

CREATE PROCEDURE dbo.plm_spGetSpinoffsByProduct
(
	@editionId			int,
	@divisionId			int,
	@categoryId			int,
	@productId			int,
	@pharmaFormId		int
)
AS
BEGIN
	
	Select Distinct e.EditionId, 
					b.[Description] As BookName,
					b.ShortName As ShortName,
					(CASE WHEN EXISTS 
							(SELECT EditionId
								FROM plm_vwProductsByEdition
								WHERE EditionId = e.EditionId
									And DivisionId = @divisionId
									And CategoryId = @categoryId
									And ProductId = @productId
									And PharmaFormId = @pharmaFormId
							) THEN 1 ELSE 0 
						END) As Participate
	From Editions e
		Inner Join Books b on e.BookId = b.BookId
	Where e.ParentId = @editionId
	Order By 2

	RETURN @@ROWCOUNT

END
go