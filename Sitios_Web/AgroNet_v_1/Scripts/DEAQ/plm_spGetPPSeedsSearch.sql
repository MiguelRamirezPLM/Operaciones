USE [DEAQ 20141007]
GO
/****** Object:  StoredProcedure [dbo].[plm_spGetPPSeedsSearch]    Script Date: 09/14/2015 09:38:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
	Author:			Miguel Ramírez/ Nalleli López			 
	Object:			dbo.[plm_spGetPPSeedsSearch]
	
	Company:		PLM.
	
	EXECUTE dbo.[plm_spGetPPSeedsSearch]  @ProductId = 23333, @param='hort'

*/

ALTER PROCEDURE [dbo].[plm_spGetPPSeedsSearch]
		@ProductId int,
		@param varchar(200)
AS
BEGIN
 
	select * from Seeds s
	left join ProductSeeds ps on s.SeedId = ps.SeedId
								and ps.ProductId = @ProductId
	where ps.ProductId is null AND S.SeedName COLLATE SQL_Latin1_General_CP1_CI_AI LIKE 
												CASE WHEN @param IS NULL THEN '%'
												WHEN LEN(@param) > 3 THEN '%' +  (@param) + '%'
												ELSE   (@param) + '%' END 
 
 
 END