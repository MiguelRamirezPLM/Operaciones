IF OBJECT_ID('dbo.roc_spGetDivisionByLaboratoryId') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetDivisionByLaboratoryId
go

/* 
	Author:			Daniel Campos / Elizabeth Lazo.				 
	Object:			dbo.roc_spGetDivisionByLaboratoryId
	
	Description:	Retrieves all divisions by laboratoryId.
	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetDivisionByLaboratoryId @laboratoryId = 34


 */ 

CREATE PROCEDURE dbo.roc_spGetDivisionByLaboratoryId
(   
  
  @laboratoryId     int   
  
) 
AS
  BEGIN
  --The parameters shouldn't be null:
       IF (@laboratoryId IS NULL) 
             
  BEGIN

    RAISERROR ('The current operation requires more parameters', 16, 1)
    RETURN -1

  END
			
			
	SELECT D.DivisionId, D.ParentId, D.DivisionName, D.ShortName, D.LaboratoryId, D.CountryId, D.Active,
       DI.DivisionInformationId, DI.Image, DI.Address, DI.Suburb, DI.Location, DI.Zipcode, DI.Telephone, DI.Lada, DI.Fax, DI.Web, 
       DI.City, DI.State, DI.Email
	
    FROM Divisions AS D
	INNER JOIN DivisionInformation AS DI ON D.DivisionId=DI.DivisionId
	WHERE D.Active='1' AND D.LaboratoryId=@laboratoryId AND ParentId IS NULL


	END

go


