IF OBJECT_ID('dbo.roc_spGetPhonesByCompany') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetPhonesByCompany
go

/* 
	Author:			Daniel Campos. / Elizabeth Lazo.				 
	Object:			dbo.roc_spGetPhonesByCompany
	
	Description:	Retrieves all telephones  by company.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetPhonesByCompany @companyId = 258


 */ 

CREATE PROCEDURE dbo.roc_spGetPhonesByCompany
(
    @companyId    int  
)

AS
 BEGIN
     --The parameters shouldn't be null:
       IF (@companyId IS NULL) 
  BEGIN

    RAISERROR ('The current operation requires more parameters', 16, 1)
    RETURN -1

  END 

  SELECT PhoneTypes.PhoneTypeId,PhoneTypes.TypeName,CompanyPhones.PhoneValue FROM CompanyPhones 
INNER JOIN PhoneTypes ON PhoneTypes.PhoneTypeId=CompanyPhones.PhoneTypeId
WHERE CompanyId = @companyId AND PhoneTypes.Active='1'

  
  END

 go




