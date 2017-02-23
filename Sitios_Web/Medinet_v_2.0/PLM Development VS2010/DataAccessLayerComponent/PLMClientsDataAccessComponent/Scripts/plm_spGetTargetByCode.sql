IF OBJECT_ID('dbo.plm_spGetTargetByCode') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetTargetByCode
go


/* 
	Author:			Angel Eduardo Hernández Aguilar			 
	Object:			dbo.plm_spGetTargetByCode
	
	Description:	

	Company:		PLM Latina.

	DECLARE @retValue int

	EXECUTE dbo.[plm_spGetTargetByCode] @codeString = 'jXYfVie4az/iKtuaTlzzeASeGyI='   --3 WEB
	EXECUTE dbo.[plm_spGetTargetByCode] @codeString = 'z+3Q1*FGxr5I2laCHJGcST2Rg2c='	 --1 MOVIL
	EXECUTE dbo.[plm_spGetTargetByCode] @codeString = '6efMyyGRx9AqgOjDuFH4zrTeYY0='	--C/S

 */ 

 CREATE PROCEDURE [dbo].[plm_spGetTargetByCode]
(
	@codeString		varchar(100)
	
)
AS
BEGIN
	SELECT DISTINCT TOT.*
	FROM Codes C
	INNER JOIN CodePrefixes CP ON C.PrefixId = CP.PrefixId
	INNER JOIN DistributionCodePrefixes DCP ON CP.PrefixId = DCP.PrefixId
	INNER JOIN TargetOutputs TOT ON DCP.TargetId = TOT.TargetId
	WHERE C.CodeString = @codeString	
END