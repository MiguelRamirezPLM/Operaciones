IF OBJECT_ID('dbo.plm_spGetPagedProducts') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetPagedProducts
go


/* 
	Author:			Juan Ramírez.				 
	Object:			dbo.plm_spGetPagedProducts
	
	Description:	Gets all the participant products from database.

	Company:		Thomson PLM.

	DECLARE
		@laboratoryId			int,
		@countryId				int,
		@brand			varchar(100)

	SELECT 
		@laboratoryId			= 182,
		@countryId				= 11,
		@brand					= 'A%'

	EXECUTE dbo.plm_spGetPagedProducts '%',1,'P'
		@laboratoryId,
		@countryId,
		@brand
 */ 

CREATE PROCEDURE [dbo].[plm_spGetPagedProducts]
(
	@brand				varchar(255) = null,
	@editionId			int,
	@typeInEdition		varchar(1) = null
)
AS
BEGIN

	SELECT DISTINCT PRODUCTID
		   ,DIVISIONID
		   ,BRAND
		   ,PRODUCTACTIVE AS ACTIVE
		   ,PHARMAFORM 
		   ,PHARMAFORMID
		   ,DIVISIONSHORTNAME 
		   ,CATEGORYID
		   ,CATEGORYNAME	
		   ,PAGE
		   ,PRODUCTDESCRIPTION
	FROM PLM_VWPRODUCTSBYEDITION
	WHERE EDITIONID = @editionId AND
		  TYPEINEDITION LIKE CASE WHEN @typeInEdition IS NULL THEN '%'
									   ELSE	@typeInEdition END AND
		  BRAND COLLATE SQL_Latin1_General_CP1_CI_AI LIKE CASE WHEN @brand IS NOT NULL THEN @brand
		  ELSE BRAND END

	ORDER BY 3,5						

END

GO

