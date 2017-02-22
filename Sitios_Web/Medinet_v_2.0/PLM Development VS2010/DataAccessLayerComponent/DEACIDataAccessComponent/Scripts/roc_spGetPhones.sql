IF OBJECT_ID('dbo.roc_spGetPhones') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetPhones
go

/* 
	Author:			Daniel Campos. / Elizabeth Lazo.
				 
	Object:			dbo.roc_spGetPhones
	
	Description:	Retrieves all phones by company.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetPhones  @companyId=435


 */ 

CREATE PROCEDURE dbo.roc_spGetPhones
(
       
  @companyId      int
  
)
AS
  BEGIN
  --The parameters shouldn't be null:
       IF (@companyId IS NULL)
  BEGIN

    RAISERROR ('The current operation requires more parameters', 16, 1)
    RETURN -1

  END



SELECT PhoneTypes.Description, CompanyPhones.PhoneValue FROM CompanyPhones
INNER JOIN PhoneTypes ON PhoneTypes.PhoneTypeId = CompanyPhones.PhoneTypeId
WHERE PhoneTypes.Active='1' AND CompanyId = @companyId
ORDER BY PhoneTypes.PhoneTypeId 

END
go