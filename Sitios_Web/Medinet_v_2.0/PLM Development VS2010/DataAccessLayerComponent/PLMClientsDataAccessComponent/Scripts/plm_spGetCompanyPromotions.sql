IF OBJECT_ID('dbo.plm_spGetCompanyPromotions') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetCompanyPromotions
go

/* 
	Author:			Ramiro Sánchez				 
	Object:			dbo.plm_spGetCompanyPromotions
	
	Description:	Get Company Promotions.

	Company:		PLM Latina.
	
	EXECUTE dbo.plm_spGetCompanyPromotions @companyClientId = 126

 */ 

CREATE PROCEDURE dbo.plm_spGetCompanyPromotions
(
	@companyClientId		int = null
)
AS
BEGIN

	IF(@companyClientId IS NOT NULL)
	BEGIN

		SELECT Distinct p.[PromotionId]
			,p.[PromotionName]
			,p.[Active]
		FROM [dbo].[Promotions] p
			INNER JOIN [CompanyPromotions] c ON c.[PromotionId] = p.[PromotionId]
		WHERE c.[CompanyClientId] = @companyClientId
			And p.[Active] = 1
		ORDER BY p.[PromotionName]

		RETURN @@ROWCOUNT	
	
	END
	
END
go