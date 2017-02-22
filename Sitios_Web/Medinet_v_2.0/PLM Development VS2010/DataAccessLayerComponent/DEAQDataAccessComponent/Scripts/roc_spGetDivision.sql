IF OBJECT_ID('dbo.roc_spGetDivision') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetDivision
go

/* 
	Author:			Daniel Campos / Elizabeth Lazo.				 
	Object:			dbo.roc_spGetDivision
	
	Description:	Retrieves all divisions by countryId.
	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetDivision @countryId=11, @divisionId=1


 */ 

CREATE PROCEDURE dbo.roc_spGetDivision
(   
  @countryId     int,   
  @divisionId    int
 
) 
AS
  BEGIN
  --The parameters shouldn't be null:
       IF (@countryId IS NULL OR @divisionId IS NULL) 
             
  BEGIN

    RAISERROR ('The current operation requires more parameters', 16, 1)
    RETURN -1

  END
			
			
	 SELECT   D.DivisionId, D.ParentId, D.DivisionName, D.ShortName, D.LaboratoryId, D.CountryId, D.Active,
			  DI.DivisionInformationId, DI.Image, DI.Address, DI.Suburb, DI.Location, DI.Zipcode, DI.Telephone, DI.Lada, DI.Fax, DI.Web, 
			  DI.City, DI.State, DI.Email
	FROM Divisions AS D
	INNER JOIN DivisionInformation AS DI ON D.DivisionId=DI.DivisionId
	WHERE D.Active='1' AND D.CountryId=@countryId AND D.DivisionId=@divisionId


	END

go


