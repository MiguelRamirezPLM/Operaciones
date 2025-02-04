USE [DEAQ 20141007]
GO
/****** Object:  StoredProcedure [dbo].[plm_spGetPPCropsSearch]    Script Date: 09/14/2015 09:38:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*
	Author:			Miguel Ramírez/ Nalleli López			 
	Object:			dbo.[plm_spGetPPCropsSearch]
	
	Company:		PLM.
	
	EXECUTE dbo.[plm_spGetPPCropsSearch]  @ProductId = 23333, @param=en general

*/

ALTER PROCEDURE [dbo].[plm_spGetPPCropsSearch]
(
	@ProductId int,
	@param varchar(200)
)	
AS
BEGIN
 
	select * from Crops c
	left join ProductCrops pc on c.CropId = pc.CropId
								and pc.ProductId= @ProductId
	where pc.ProductId is null AND c.CropName COLLATE SQL_Latin1_General_CP1_CI_AI LIKE 
												CASE WHEN @param IS NULL THEN '%'
												WHEN LEN(@param) > 3 THEN '%' +  (@param) + '%'
												ELSE   (@param) + '%' END 

	order by 2
	
END	