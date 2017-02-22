IF OBJECT_ID('dbo.plm_spCheckFolio') IS NOT NULL
	DROP PROCEDURE dbo.plm_spCheckFolio
go

/* 
	Author:			Juan Ramírez.				 
	Object:			dbo.plm_spCheckFolio
	
	Description:	Check if folio exist in database.

	Company:		PLM Latina.

	
	EXECUTE dbo.plm_spCheckFolio @folioString

	

 */ 

CREATE PROCEDURE dbo.plm_spCheckFolio
(
	@folioString		varchar(60)
)
AS
BEGIN
	
		DECLARE @folio int
		
		SELECT @folio = COUNT(*)
		  FROM [dbo].[Folios]	
		 WHERE [FolioString] = @folioString

		RETURN @folio

END
go