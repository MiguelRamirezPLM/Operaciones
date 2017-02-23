IF OBJECT_ID('dbo.plm_tgrInsertTargetClientCode') IS NOT NULL
	DROP TRIGGER dbo.plm_tgrInsertTargetClientCode
GO
-- ======================================================================
/*
	Author:			Angel Eduardo Hernández Aguilar.
	Object:			dbo.plm_tgrInsertTargetClientCode
	
	Description:	This trigger inserted in MailingDistributionClients, 
					once that was inserted in TargetClientCodes
					
	Company:		Thomson PLM.

*/
-- ======================================================================
ALTER TRIGGER [dbo].[plm_tgrInsertTargetClientCode]
ON [dbo].[TargetClientCodes] 
FOR INSERT
AS
BEGIN

	DECLARE 
		@clientid			INT,
		@codeid				INT,
		@deviceid			INT,
		@targetid			INT,
		@mailingid			INT,
		@prefixid			INT,
		@distributionid		INT
	
	-- Take the pk of the inserted record:
	SELECT @clientid = clientid, @codeid = codeid, @deviceid = deviceid, @targetid = targetid FROM Inserted
	
	SELECT @mailingid =MDCO.MailingId, @distributionid =MDCO.DistributionId, @prefixid= MDCO.PrefixId
	FROM TargetClientCodes TCC
	INNER JOIN ClientCodes CC ON TCC.ClientId = CC.ClientId AND TCC.CodeId = CC.CodeId
	INNER JOIN Codes C ON CC.CodeId = C.CodeId
	INNER JOIN CodePrefixes CP ON C.PrefixId = CP.PrefixId
	INNER JOIN DistributionCodePrefixes DCP ON CP.PrefixId = DCP.PrefixId
	INNER JOIN MailingDistributionCodes MDCO ON DCP.PrefixId= MDCO.PrefixId
	WHERE TCC.ClientId = @clientid 
	
	INSERT INTO [dbo].[MailingDistributionClients]
           ([MailingId]
           ,[DistributionId]
           ,[PrefixId]
           ,[TargetId]
           ,[ClientId]
           ,[CodeId]
           ,[DeviceId]
           ,[Dispatched]
           ,[SentDate])
			VALUES
			(@mailingid,
			 @distributionid,
			 @prefixid,
			 @targetid,
			 @clientid,
			 @codeid,
			 @deviceid,
			 0,
			 null)
END
