IF OBJECT_ID('dbo.roc_spGetCity') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetCity
go

/* 
	Author:			Daniel Campos. / Elizabeth Lazo.				 
	Object:			dbo.roc_spGetCity
	
	Description:	Retrieves all city by cityId.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetCity @cityId = 52


 */ 

CREATE PROCEDURE dbo.roc_spGetCity 
(
    @cityId    int  
)

AS
 BEGIN
     --The parameters shouldn't be null:
       IF (@cityId IS NULL) 
  BEGIN

    RAISERROR ('The current operation requires more parameters', 16, 1)
    RETURN -1

  END 

  SELECT * FROM Cities WHERE CityId=@cityId AND Active='1'


  
  END

 go



