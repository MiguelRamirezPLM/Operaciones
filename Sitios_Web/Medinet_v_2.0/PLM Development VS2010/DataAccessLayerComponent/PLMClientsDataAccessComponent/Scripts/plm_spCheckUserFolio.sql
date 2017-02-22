IF OBJECT_ID('dbo.plm_spCheckUserFolio') IS NOT NULL
	DROP PROCEDURE dbo.plm_spCheckUserFolio
go

/* 
	Author:			Juan Ramírez.				 
	Object:			dbo.plm_spCheckUserFolio
	
	Description:	Check if a folio has been assigned.

	Company:		PLM Latina.

	
	EXECUTE dbo.plm_spCheckUserFolio @folioId

	

 */ 

CREATE PROCEDURE dbo.plm_spCheckUserFolio
(
	@folioId		int
)
AS
BEGIN
	
		DECLARE @folio int
		
		SELECT @folio = COUNT(*)
		  FROM [dbo].[UserFolioCodes]	
		 WHERE [FolioId] = @folioId

		RETURN @folio

END
go