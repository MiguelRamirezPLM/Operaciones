IF OBJECT_ID('dbo.roc_spGetAdvertisementsByCompanyId') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetAdvertisementsByCompanyId
go

/* 
	Author:			Daniel Campos. / Elizabeth Lazo.				 
	Object:			dbo.roc_spGetAdvertisementsByCompanyId
	
	Description:	Retrieves all advertisements by companyId.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetAdvertisementsByCompanyId @companyId = 17


 */ 

CREATE PROCEDURE dbo.roc_spGetAdvertisementsByCompanyId 
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

   SELECT Advertisements.* FROM Advertisements 
     WHERE CompanyId = @companyId AND Active='1'


  
  END

 go



