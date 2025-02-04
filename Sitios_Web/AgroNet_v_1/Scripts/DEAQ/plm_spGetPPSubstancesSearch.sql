USE [DEAQ 20141007]
GO
/****** Object:  StoredProcedure [dbo].[plm_spGetPPSubstancesSearch]    Script Date: 09/14/2015 09:38:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*
	Author:			Miguel Ramírez/ Nalleli López			 
	Object:			dbo.[plm_spGetPPSubstancesSearch]
	
	Company:		PLM.
	
	EXECUTE dbo.[plm_spGetPPSubstancesSearch]  @ProductId = 23333, @param=a 

*/

ALTER PROCEDURE [dbo].[plm_spGetPPSubstancesSearch]
(
	@ProductId int,
	@param varchar(200)
)	
AS
BEGIN

	select * from ActiveSubstances a
	left join ProductSubstances ps on a.ActiveSubstanceId = ps.ActiveSubstanceId
								and ps.ProductId = @ProductId
			where ps.ProductId is null AND a.ActiveSubstanceName COLLATE SQL_Latin1_General_CP1_CI_AI LIKE	
																CASE WHEN @param IS NULL THEN '%'
																WHEN LEN(@param) > 3 THEN '%' +  (@param) + '%'
																ELSE   (@param) + '%' END 
END	