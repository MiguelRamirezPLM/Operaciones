IF OBJECT_ID('dbo.roc_spGetDivisionsByParentId') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetDivisionsByParentId
go

/* 
	Author:			Daniel Campos / Elizabeth Lazo.				 
	Object:			dbo.roc_spGetDivisionsByParentId
	
	Description:	Retrieves all divisions by parentId.
	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetDivisionsByParentId @parentId= 27


 */ 

CREATE PROCEDURE dbo.roc_spGetDivisionsByParentId
(   
  
  @parentId     int   
 
) 
AS
  BEGIN
  --The parameters shouldn't be null:
 
       IF (@parentId IS NULL) 
             
  BEGIN

    RAISERROR ('The current operation requires more parameters', 16, 1)
    RETURN -1

  END
			
			
		SELECT D.DivisionId, D.ParentId, D.DivisionName, D.ShortName, D.LaboratoryId, D.CountryId, D.Active,
			   DI.DivisionInformationId, DI.Image, DI.Address, DI.Suburb, DI.Location, DI.Zipcode, DI.Telephone, DI.Lada, DI.Fax, DI.Web, 
			   DI.City, DI.State, DI.Email, DI.Active
			FROM Divisions AS D
			INNER JOIN DivisionInformation AS DI ON D.DivisionId=DI.DivisionId
			WHERE D.Active='1' AND ParentId =@parentId
			ORDER BY DivisionName


	END

go


