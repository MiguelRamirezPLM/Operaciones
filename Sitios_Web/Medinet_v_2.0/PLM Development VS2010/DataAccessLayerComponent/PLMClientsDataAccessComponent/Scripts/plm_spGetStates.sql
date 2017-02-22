IF OBJECT_ID('dbo.plm_spGetStates') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetStates
go

/* 
	Author:			Juan Ramirez.				 
	Object:			dbo.plm_spGetStates
	
	Description:	Retrieves all States stored in the database.

	Company:		PLM Latina.

	EXECUTE dbo.plm_spGetStates

 */ 

CREATE PROCEDURE dbo.plm_spGetStates
( 
	@countryId		TINYINT = NULL,
	@prefix			VARCHAR(100) = NULL
)
AS
BEGIN

	IF(@countryId IS NULL AND @prefix IS NULL)
	BEGIN
		SELECT [StateId]
		  ,[StateName]
		  ,[CountryId]
		  ,[ShortName]
		  ,[Active]
		FROM [dbo].[States]
		WHERE [Active] = 1
		RETURN @@ROWCOUNT
	END
	
	IF(@countryId IS NOT NULL)
	BEGIN
		SELECT [StateId]
		  ,[StateName]
		  ,[CountryId]
		  ,[ShortName]
		  ,[Active]
		FROM [dbo].[States]
		WHERE [CountryId] = @countryId AND
			  [Active] = 1 AND 
			  [StateName] COLLATE SQL_Latin1_General_CP1_CI_AI LIKE CASE WHEN @prefix IS NULL THEN '%'
												ELSE @prefix + '%' END
		RETURN @@ROWCOUNT
	END
END
go