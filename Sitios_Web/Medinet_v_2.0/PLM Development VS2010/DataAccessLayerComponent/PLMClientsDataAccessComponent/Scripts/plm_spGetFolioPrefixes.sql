IF OBJECT_ID('dbo.plm_spGetFolioPrefixes') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetFolioPrefixes
go

/* 
	Author:			Juan Ramirez.				 
	Object:			dbo.plm_spGetFolioPrefixes
	
	Description:	Retrieves the prefix by name.

	Company:		PLM Latina.

	EXECUTE dbo.plm_spGetFolioPrefixes

 */ 

CREATE PROCEDURE dbo.plm_spGetFolioPrefixes
(
	@prefixName		VARCHAR(15)
)
AS
BEGIN

	SELECT [PrefixId]
		  ,[PrefixName]
		  ,[InitialValue]
		  ,[FinalValue]
		  ,[CurrentValue]
		  ,[PrefixDescription]
		  ,[Active]
	  FROM [dbo].[FolioPrefixes]

	 WHERE [PrefixName] = @prefixName AND 
		   [Active] = 1
	
	RETURN @@ROWCOUNT

END
go