IF OBJECT_ID('dbo.plm_spGetCountries') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetCountries
go

/* 
	Author:			Juan Ramirez.				 
	Object:			dbo.plm_spGetCountries
	
	Description:	Retrieves all Countries stored in the database.

	Company:		PLM Latina.

	EXECUTE dbo.plm_spGetCountries

 */ 

CREATE PROCEDURE dbo.plm_spGetCountries
AS
BEGIN

	SELECT [CountryId]
		  ,UPPER([CountryName]) As CountryName
		  ,[CountryCode]
		  ,[ID]
		  ,[Active]
	  FROM [dbo].[Countries]
	  WHERE [Active] = 1 
	ORDER BY [CountryName]
	
	RETURN @@ROWCOUNT

END
go