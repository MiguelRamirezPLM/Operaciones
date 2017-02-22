IF OBJECT_ID('dbo.plm_spCheckMail') IS NOT NULL
	DROP PROCEDURE dbo.plm_spCheckMail
go

/* 
	Author:			Juan Ramírez.				 
	Object:			dbo.plm_spCheckMail
	
	Description:	Check if email exist in database.

	Company:		PLM Latina.

	
	EXECUTE dbo.plm_spCheckMail @email

	

 */ 

CREATE PROCEDURE dbo.plm_spCheckMail
(
	@email			varchar(60)
)
AS
BEGIN
	
		DECLARE @clientEmail int
		
		SELECT @clientEmail = COUNT(*)
		  FROM [dbo].[Clients]	
		 WHERE [Email] = @email

		RETURN @clientEmail

END
go