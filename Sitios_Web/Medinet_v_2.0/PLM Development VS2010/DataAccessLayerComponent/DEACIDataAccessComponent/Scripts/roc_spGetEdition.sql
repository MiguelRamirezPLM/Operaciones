IF OBJECT_ID('dbo.roc_spGetEdition') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetEdition
go

/* 
	Author:			Daniel Campos. / Elizabeth Lazo.				 
	Object:			dbo.roc_spGetEdition
	
	Description:	Retrieves all edition by country.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetEdition @countryId=4 


 */ 

CREATE PROCEDURE dbo.roc_spGetEdition
(
  @countryId   int
)
AS
  BEGIN
  --The parameters shouldn't be null:
       IF (@countryId IS NULL) 
  BEGIN

    RAISERROR ('The current operation requires more parameters', 16, 1)
    RETURN -1

  END

     SELECT * 
      FROM Editions 
        WHERE NumberEdition=(SELECT MAX(NumberEdition) FROM Editions) AND Active='1' AND CountryId = @countryId

END
 go




