IF OBJECT_ID('dbo.plm_spCRUDGenerateFiles') IS NOT NULL
	DROP PROCEDURE dbo.plm_spCRUDGenerateFiles
go


/* 
	Author:		    Edibaldo Rosas				 
	Object:			dbo.[plm_spCRUDGenerateFiles]
	
	Description:	Perform the basic operations, or in other words CRUD operations.
					Where CRUD stands for:

						C - Create	{0}
						R - Read	{1}
						U - Update	{2}
						D - Delete	{3}
 */ 

CREATE PROCEDURE [dbo].[plm_spCRUDGenerateFiles]
(
	@CRUDType				tinyint,
	@VersionFileId			int,
	@FormatFileId			tinyint,
	@FileName				varchar(255),
	@NickName				varchar(15),
	@FileDate				datetime,
	@BaseUrl				varchar(255),
	@Version				varchar(10),
	@Active					int
)
AS
BEGIN

	

	-- C = {(Create, 0)}
	IF (@CRUDType IN (0))
	BEGIN
			
		INSERT INTO GenerateFiles
           ([VersionFileId]
           ,[FormatFileId]
           ,[FileName]
           ,[NickName]
           ,[FileDate]
           ,[BaseURL]
           ,[Version]
           ,[Active])
     VALUES
           (@VersionFileId
           ,@FormatFileId
           ,@FileName
           ,@NickName
           ,@FileDate
           ,@BaseUrl
           ,@Version
           ,@Active)

		RETURN 0


	END
	
	
END


GO


