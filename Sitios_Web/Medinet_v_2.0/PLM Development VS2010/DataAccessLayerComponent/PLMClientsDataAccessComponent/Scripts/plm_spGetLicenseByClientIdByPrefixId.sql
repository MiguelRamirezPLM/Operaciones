IF OBJECT_ID('dbo. plm_spGetLicenseByClientIdByPrefixId ') IS NOT NULL
      DROP PROCEDURE dbo. plm_spGetLicenseByClientIdByPrefixId
go

/****** Object:  StoredProcedure [dbo].[plm_spGetLicenseByClientIdByPrefixId]    Script Date: 05/22/2013 16:35:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/* 
	Author:			ulises Orihuela.				 
	Object:			dbo.[plm_spGetLicenseByClientIdByPrefixId]
	
	Description:	.

	Company:		PLM Latina.

	
	EXECUTE dbo.[plm_spGetLicenseByClientIdByPrefixId] @clientid = 46768 , @prefixid = 356

 */ 


ALTER PROCEDURE [dbo].[plm_spGetLicenseByClientIdByPrefixId]
(
	@clientid			varchar(255) = NULL,
	@prefixid			varchar(255) = NULL
)
AS
BEGIN
	
	DECLARE @codeId	INT = NULL,
	@licensekey int 
	
	IF(@clientid IS NULL)
	BEGIN
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
	END		

	SELECT @codeid = cc.CodeId
	FROM ClientCodes CC
	INNER JOIN CODES C ON CC.CodeId = C.CodeId
	WHERE ClientId = @clientid AND PrefixId = @prefixid

	IF(@codeId IS NOT  null)
	BEGIN
		 SELECT l.*
 		 FROM LicenseTargetCodes LT 
		 INNER JOIN Licenses  L ON LT.LicenseId = L.LicenseId
		 WHERE CodeId = @codeId
	
     	RETURN @@ROWCOUNT
    END
  	
END
