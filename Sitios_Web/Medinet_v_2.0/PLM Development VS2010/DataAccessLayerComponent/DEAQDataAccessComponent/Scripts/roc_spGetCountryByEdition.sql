USE [DEAQ]
GO
/****** Objeto:  StoredProcedure [dbo].[roc_spGetCountryById]    Fecha de la secuencia de comandos: 01/04/2012 10:10:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/* 
	Author:		    Elizabeth Lazo.				 
	Object:			dbo.roc_spGetCountryByEdition
	
	Description:	Retrieves all countries by Edition.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetCountryByEdition @editionId  = 1


 */ 

CREATE PROCEDURE [dbo].[roc_spGetCountryByEdition]
(   
  @editionId      int   
  
) 
AS
  BEGIN
  --The parameters shouldn't be null:
       IF (@editionId  IS NULL) 
             
  BEGIN

    RAISERROR ('The current operation requires more parameters', 16, 1)
    RETURN -1

  END

   
    SELECT e.EditionId, e.CountryId, c.CountryName
      FROM Editions e 
       inner join Countries c ON(e.CountryId = c.CountryId)
WHERE e.EditionId = @editionId  AND e.Active = '1' and c.Active = '1'


END


