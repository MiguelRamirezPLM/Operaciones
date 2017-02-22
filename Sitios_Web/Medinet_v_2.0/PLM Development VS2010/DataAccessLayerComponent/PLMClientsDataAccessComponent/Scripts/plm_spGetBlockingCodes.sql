IF OBJECT_ID('dbo.plm_spGetBlockingCodes') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetBlockingCodes
go

/* 
	Author:			Juan Manuel Ramírez.				 
	Object:			dbo.plm_spGetBlockingCodes
	
	Description:	Check if a Blockcode is valid.

	Company:		PLM Latina.
	
	EXECUTE dbo.plm_spGetBlockingCodes @blockString = 'a'
	EXECUTE dbo.plm_spGetBlockingCodes @blockString = 'a'

 */ 

CREATE PROCEDURE dbo.plm_spGetBlockingCodes
(
	@prefixName		varchar(15) =  null,
	@targetId		tinyint = null,
	@blockString	varchar(10)
)
AS
BEGIN
	
		If(@blockString Is Not Null And @prefixName Is Null And @targetId Is Null)
		Begin
		
			SELECT [BlockingCodeId]
				  ,[BlockString]
				  ,[Active]
			  FROM [dbo].[BlockingCodes] 
			WHERE [BlockString] = @blockString And
				  [Active] = 1

			RETURN @@ROWCOUNT

		End
		If(@blockString Is Not Null And @prefixName Is Not Null And @targetId Is Not Null)
		Begin
		
			DECLARE @blockingCode int
			SET @blockingCode = 0
			
			SELECT @blockingCode = COUNT(*)
			  FROM [dbo].[BlockingCodes] BC
				Inner Join [dbo].[BlockingCodePrefixes] BCP On BC.[BlockingCodeId] = BCP.[BlockingCodeId]
				Inner Join [dbo].[CodePrefixes] CP On BCP.PrefixId = CP.PrefixId
			 WHERE BC.[BlockString] = @blockString And
				   BC.[Active] = 1 And 
				   CP.[PrefixName] = @prefixName And
				   BCP.[TargetId] = @targetId
			 
			RETURN @blockingCode
		End
END
