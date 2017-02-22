IF OBJECT_ID('dbo.plm_spGetDrugs') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetDrugs
go

/* 
	Author:			Juan Manuel Ramírez.				 
	Object:			dbo.plm_spGetDrugs
	
	Description:	Retrieves all drugs by text.

	Company:		PLM Latina.

	


    EXECUTE  dbo.plm_spGetDrugs @editionId = 8, @text = null


 */ 

CREATE PROCEDURE [dbo].[plm_spGetDrugs]
(
    @editionId               int = NULL,
    @text					 varchar(255) = NULL
)
AS
BEGIN

	-- The parameters shouldn't be null:
	IF (@editionId  IS NULL)
	BEGIN
	
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
		
	END

	
      SELECT D.DrugId,
			 D.DrugName,
			 D.PharmaceuticForm,
			 D.ActiveSubstance,
			 D.Presentation,
			 C.ClientId,
			 C.CompanyName 
		FROM Drugs D
			 Inner Join Clients C  On D.ClientId = C.ClientId
        WHERE D.Active = 1 And
			  C.Active = 1 And
			  C.EditionId = @editionId And
			  D.DrugName LIKE CASE WHEN @text IS NULL THEN '%'
								   WHEN LEN(@text) > 3 THEN '%' + @text + '%'
								   ELSE @text + '%' END
        ORDER BY D.DrugName


END

