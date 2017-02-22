IF OBJECT_ID('dbo.plm_spGetDivisionCategories') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetDivisionCategories
go

/* 
	Author:			Ramiro Sánchez
	Object:			dbo.plm_spGetDivisionCategories
	
	Description:	Gets rhe relationship between divisions and categories
	Company:		PLM Latina.

	EXECUTE dbo.plm_spGetDivisionCategories @divisionId = 3, @categoryId = 101 

 */ 

CREATE PROCEDURE dbo.plm_spGetDivisionCategories
(
	@divisionId		int,
	@categoryId		int
)
AS
BEGIN

	Select Distinct DivisionId,
					CategoryId

	FROM DivisionCategories
	Where DivisionId = @divisionId
		And CategoryId = @categoryId

END
go