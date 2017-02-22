IF OBJECT_ID('dbo.plm_spGetVersionFile') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetVersionFile
go
/* 
	Author:		    Edibaldo Rosas				 
	Object:			dbo.[plm_spGetVersionFile]
 */ 
CREATE PROCEDURE [dbo].[plm_spGetVersionFile]
(
	@PrefixApplication				varchar(5),
	@EditionId						int
)
AS
BEGIN
		SELECT [VersionFileId]
      ,[ApplicationName]
      ,[PrefixApplication]
      ,[EditionId]
      ,[CurrentValue]
      FROM VersionFiles
		where VersionFiles.PrefixApplication=@PrefixApplication
		and VersionFiles.EditionId=@EditionId

END

GO


