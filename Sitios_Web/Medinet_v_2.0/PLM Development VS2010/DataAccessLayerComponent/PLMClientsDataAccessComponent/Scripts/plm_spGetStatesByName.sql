IF OBJECT_ID('dbo.plm_spGetStatesByName') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetStatesByName
go

/* 
	Author:			Juan Ramirez.				 
	Object:			dbo.plm_spGetStatesByName
	
	Description:	Retrieve a State by Name and Country.

	Company:		PLM Latina.

	EXECUTE dbo.plm_spGetStatesByName @countryId = 1, @stateName = 'DISTRITO FEDERAL'
	EXECUTE dbo.plm_spGetStatesByName @shortName = 'DF'
	EXECUTE dbo.plm_spGetStatesByName @countryId = 1, @shortName = 'MS'

 */ 

CREATE PROCEDURE dbo.plm_spGetStatesByName
( 
	@countryId		TINYINT = NULL,
	@stateName		VARCHAR(100) = NULL,
	@shortName		varchar(10) = NULL
)
AS
BEGIN

	IF(@countryId IS NOT NULL AND @stateName IS NOT NULL AND @shortName IS NULL)
	BEGIN
		SELECT [StateId]
		      ,[StateName]
		      ,[CountryId]
		      ,[ShortName]
		      ,[Active]
		FROM [dbo].[States]
		WHERE [CountryId] = @countryId AND
			  [Active] = 1 AND 
			  [StateName] = @stateName 
		RETURN @@ROWCOUNT
	END
	
	IF(@countryId IS NULL AND @shortName IS NOT NULL AND @stateName IS NULL)
	BEGIN
		SELECT [StateId]
		      ,[StateName]
		      ,[CountryId]
		      ,[ShortName]
		      ,[Active]
		FROM [dbo].[States]
		WHERE [Active] = 1 AND 
			  [ShortName] = @shortName 
		RETURN @@ROWCOUNT
	END

	IF(@countryId IS NOT NULL AND @shortName IS NOT NULL AND @stateName IS NULL)
	BEGIN
		SELECT [StateId]
		      ,[StateName]
		      ,[CountryId]
		      ,[ShortName]
		      ,[Active]
		FROM [dbo].[States]
		WHERE [Active] = 1 AND 
			  [CountryId] = @countryId AND
			  [ShortName] = @shortName 
		RETURN @@ROWCOUNT
	END
	
END
go