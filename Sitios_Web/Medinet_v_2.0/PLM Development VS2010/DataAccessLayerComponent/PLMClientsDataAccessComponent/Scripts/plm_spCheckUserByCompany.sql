IF OBJECT_ID('dbo.plm_spCheckUserByCompany') IS NOT NULL
	DROP PROCEDURE dbo.plm_spCheckUserByCompany
go

/* 
	Author:			Ramiro Sánchez.				 
	Object:			dbo.plm_spCheckUserByCompany
	
	Description:	Check if a user name is associated with a company.

	Company:		PLM Latina.
	
	EXECUTE dbo.plm_spCheckUserByCompany @companyClientId = 1, @userName = ''

 */ 

CREATE PROCEDURE dbo.plm_spCheckUserByCompany
(
	@companyClientId	int,
	@userName			varchar(60)
)
AS
BEGIN
	
		DECLARE @companyUser int
		SET @companyUser = 0
		
		SELECT @companyUser = COUNT(*)
			FROM [dbo].[CompanyUsers]
		 WHERE CompanyClientId = @companyClientId
			And UserName = @userName

		RETURN @companyUser
	
END
go