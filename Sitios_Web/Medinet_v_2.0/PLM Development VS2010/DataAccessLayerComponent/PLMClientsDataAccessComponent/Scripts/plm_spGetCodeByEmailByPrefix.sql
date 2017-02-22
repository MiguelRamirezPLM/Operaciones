IF OBJECT_ID('dbo.plm_spGetCodeByEmailByPrefix ') IS NOT NULL
      DROP PROCEDURE dbo.plm_spGetCodeByEmailByPrefix


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

/* 
	Author:			Ulises Orihuela.				 
	Object:			dbo.plm_spGetCodeByEmailByPrefix
	
	Description:	Get Code Info By email and PrefixName

	Company:		PLM Latina.
	
	EXECUTE dbo.plm_spGetCodeByEmailByPrefix @prefixName = 'PLMPTECGSK' , @email = 'marpa2008@hotmail.com'

 */ 

CREATE PROCEDURE [dbo].[plm_spGetCodeByEmailByPrefix]
(
	@prefixName		varchar(60),
	@email varchar (100)
)
AS
BEGIN
	
		DECLARE @clientCode int
		SET @clientCode = 0
		
	IF (@prefixName IS NULL OR @email IS NULL )
	BEGIN
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
	END	
		
		SELECT *--c.CodeId, c.CodeString ,c.PrefixId 
		  FROM [dbo].[ClientCodes] cc
			Inner Join Codes c On cc.CodeId = c.CodeId
			Inner Join Clients cl On cc.ClientId = cl.ClientId
			inner join CodePrefixes cp on c.PrefixId = cp.PrefixId
		 WHERE prefixName = @prefixName
			And cl.Active = 1
			And c.CodeStatusId = 2
			and Email  = @email

			RETURN @@ROWCOUNT

END
