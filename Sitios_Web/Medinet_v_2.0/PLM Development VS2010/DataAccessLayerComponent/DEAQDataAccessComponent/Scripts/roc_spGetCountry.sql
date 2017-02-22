IF OBJECT_ID('dbo.roc_spGetCountry') IS NOT NULL
	DROP PROCEDURE roc_spGetCountry
go

/* 
	Author:			Daniel Campos / Elizabeth Lazo.				 
	Object:			dbo.roc_spGetCountry
	
	Description:	Retrieves all countries by ID.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetCountry @country = 'MEX'


 */ 

CREATE PROCEDURE dbo.roc_spGetCountry
(   
  @country      varchar(3)   
  
) 
AS
  BEGIN
  --The parameters shouldn't be null:
       IF ( @country IS NULL) 
             
  BEGIN

    RAISERROR ('The current operation requires more parameters', 16, 1)
    RETURN -1

  END

   
    SELECT * FROM Countries WHERE ID = @country AND Active = '1'


END
go


