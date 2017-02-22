IF OBJECT_ID('dbo.plm_spGetEditionProductShots') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetEditionProductShots
go

/* 
	Author:			Ramiro Sánchez
	Object:			dbo.plm_spGetEditionProductShots
	
	Description:	Gets all Product Shots given a product
	Company:		PLM Latina.

	EXECUTE dbo.plm_spGetEditionProductShots @divisionId = 3, @categoryId = 101 

 */ 

CREATE PROCEDURE dbo.plm_spGetEditionProductShots
(
	@editionId		int,
	@divisionId		int,
	@categoryId		int,
	@productId		int,
	@pharmaFormId	int
)
AS
BEGIN

	Select Distinct	[EditionProductShotId]
				,[EditionId]
				,[DivisionId]
				,[CategoryId]
				,[PharmaFormId]
				,[ProductId]
				,[PSTypeId]
				,[ProductShot]
				,[QtyCells]
				,[Active]
	From EditionProductShots
	Where EditionId = @editionId
		And DivisionId = @divisionId
		And CategoryId = @categoryId
		And ProductId = @productId
		And PharmaFormId = @pharmaFormId

END
go