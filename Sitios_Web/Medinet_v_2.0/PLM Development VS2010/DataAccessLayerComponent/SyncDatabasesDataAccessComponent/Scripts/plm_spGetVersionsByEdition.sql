
IF OBJECT_ID('dbo.plm_spGetVersionsByEdition') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetVersionsByEdition
go

/* 
	Author:			Juan Ramírez.				 
	Object:			dbo.plm_spGetVersionsByEdition
	
	Description:	Get all the versions filtered by edition and database.

	Company:		PLM Latina.

	plm_spGetVersionsByEdition @editionId = 1, @dbId = 1

 */ 

CREATE PROCEDURE dbo.plm_spGetVersionsByEdition
(
	@editionId		int,
	@dbId			int 
)
AS
BEGIN

	SELECT [VersionId]
		  ,[EditionId]
		  ,[DbId]
		  ,[PackCount]
		  ,[VersionNumber]
		  ,[LastUpdate]
		  ,[Active]
	FROM [dbo].[Versions]
	WHERE [EditionId] = @editionId AND
		  [DbId]	  = @dbId



END
