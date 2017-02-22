IF OBJECT_ID('dbo.plm_spGetSymptoms ') IS NOT NULL
      DROP PROCEDURE dbo.plm_spGetSymptoms
go

GO
/****** Object:  StoredProcedure [dbo].[plm_spGetSymptomsByEdition]    Script Date: 05/30/2013 12:43:03

  EXECUTE [plm_spGetSymptoms] @symtomid =  10
 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create PROCEDURE [dbo].[plm_spGetSymptoms]
(
	@symtomid		int = null
)
AS
BEGIN

     SELECT * FROM  Symptoms
	  WHERE SymptomId =  @symtomid
END
