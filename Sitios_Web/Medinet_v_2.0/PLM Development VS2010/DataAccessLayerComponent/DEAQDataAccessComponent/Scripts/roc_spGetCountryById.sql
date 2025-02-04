USE [DEAQ]
GO
/****** Objeto:  StoredProcedure [dbo].[roc_spGetCountryById]    Fecha de la secuencia de comandos: 01/03/2012 17:14:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/* 
	Author:		 Elizabeth Lazo.				 
	Object:			dbo.roc_spGetCountryById
	
	Description:	Retrieves all countries by ID.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetCountryById @countryId = 1


 */ 

CREATE PROCEDURE [dbo].[roc_spGetCountryById]
(   
  @countryId      int   
  
) 
AS
  BEGIN
  --The parameters shouldn't be null:
       IF ( @countryId IS NULL) 
             
  BEGIN

    RAISERROR ('The current operation requires more parameters', 16, 1)
    RETURN -1

  END

   
    SELECT * FROM Countries WHERE CountryId = @countryId AND Active = '1'


END


