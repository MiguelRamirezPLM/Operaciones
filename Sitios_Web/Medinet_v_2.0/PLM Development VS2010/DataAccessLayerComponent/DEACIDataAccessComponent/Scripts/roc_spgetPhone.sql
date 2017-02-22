
IF OBJECT_ID('dbo.roc_spgetPhone') IS NOT NULL
	DROP PROCEDURE dbo.roc_spgetPhone
go

/* 
	Author:			Daniel Campos. / Elizabeth Lazo.
				 
	Object:			dbo.roc_spgetPhone
	
	Description:	Retrieves all phones by company and phone type.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spgetPhone  @companyId=435, @phoneTypeId= 4


 */ 

CREATE PROCEDURE dbo.roc_spgetPhone
(
       
  @companyId      int,
  @phoneTypeId    tinyint
  
)
AS
  BEGIN
  --The parameters shouldn't be null:
       IF (@companyId IS NULL OR @phoneTypeId IS NULL)
  BEGIN

    RAISERROR ('The current operation requires more parameters', 16, 1)
    RETURN -1

  END



SELECT PhoneTypes.Description, CompanyPhones.PhoneValue FROM CompanyPhones
INNER JOIN PhoneTypes ON PhoneTypes.PhoneTypeId = CompanyPhones.PhoneTypeId
 WHERE CompanyId = @companyId AND PhoneTypes.PhoneTypeId = @phoneTypeId

END
go