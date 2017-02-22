IF OBJECT_ID('dbo.plm_spGetFolio') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetFolio
go

/* 
	Author:			Juan Ramírez.				 
	Object:			dbo.plm_spGetFolio
	
	Description:	Get the folio by name.

	Company:		PLM Latina.

	
	EXECUTE dbo.plm_spGetFolio @folioString

	

 */ 

CREATE PROCEDURE dbo.plm_spGetFolio
(
	@folioString		varchar(60)
)
AS
BEGIN
	
		SELECT *
		  FROM [dbo].[Folios]	
		 WHERE [FolioString] = @folioString

		RETURN @@ROWCOUNT

END
go