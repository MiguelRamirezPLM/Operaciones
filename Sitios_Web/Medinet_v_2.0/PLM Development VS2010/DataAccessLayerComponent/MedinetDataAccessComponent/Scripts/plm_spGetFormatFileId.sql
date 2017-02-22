IF OBJECT_ID('dbo.plm_spGetFormatFileId') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetFormatFileId
go

/* 
	Author:		    Edibaldo Rosas				 
	Object:			dbo.[plm_spGetFormatFileId]
 */ 
CREATE PROCEDURE [dbo].[plm_spGetFormatFileId]
(
	@FormatFile					varchar(10)
)
AS
BEGIN
		SELECT FormatFileId
		FROM FormatFiles
		WHERE FormatFiles.FormatFile=@FormatFile
END

GO


