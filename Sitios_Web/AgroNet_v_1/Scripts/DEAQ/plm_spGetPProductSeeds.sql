USE [DEAQ 20141007]
GO
/****** Object:  StoredProcedure [dbo].[plm_spGetPProductSeeds]    Script Date: 09/09/2015 18:54:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*
	Author:			Miguel Ramírez/ Nalleli López			 
	Object:			dbo.[plm_spGetPProductSeeds]
	
	Company:		PLM.
	
	EXECUTE dbo.[plm_spGetPProductSeeds]  @ProductId = 67

*/

CREATE PROCEDURE [dbo].[plm_spGetPProductSeeds]
(
	@ProductId int
)	
AS
BEGIN

	select * from Seeds s
	left join ProductSeeds ps on s.SeedId = ps.SeedId
							and ps.ProductId = @ProductId
	where ps.ProductId is null order by 2
	
END	