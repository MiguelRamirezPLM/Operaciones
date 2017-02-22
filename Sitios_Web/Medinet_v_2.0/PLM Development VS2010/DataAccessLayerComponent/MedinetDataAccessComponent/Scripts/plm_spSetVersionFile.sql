IF OBJECT_ID('dbo.plm_spSetVersionFile') IS NOT NULL
	DROP PROCEDURE dbo.plm_spSetVersionFile
go


/* 
	Author:		    Edibaldo Rosas				 
	Object:			dbo.[plm_spSetVersionFile]
 */ 
CREATE PROCEDURE [dbo].[plm_spSetVersionFile]
(
	@PrefixApplication				varchar(5)
)
AS
BEGIN
		UPDATE VersionFiles
		   SET CurrentValue = ([CurrentValue]+1)
		   WHERE VersionFiles.PrefixApplication=@PrefixApplication




END

GO

