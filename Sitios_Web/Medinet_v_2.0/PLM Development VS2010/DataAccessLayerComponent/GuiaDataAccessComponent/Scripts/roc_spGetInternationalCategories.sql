IF OBJECT_ID('dbo.roc_spGetInternationalCategories') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetInternationalCategories
go

/* 
	Author:			Daniel Campos. / Elizabeth Lazo.
				 
	Object:			dbo.roc_spGetInternationalCategories
	
	Description:	Retrieves all categories.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetInternationalCategories 


 */ 

CREATE PROCEDURE dbo.roc_spGetInternationalCategories

AS
  BEGIN

  		SELECT * FROM Categories AS C 
			WHERE C.ParentId IS NULL AND Active='1'
			ORDER BY Description


END
go