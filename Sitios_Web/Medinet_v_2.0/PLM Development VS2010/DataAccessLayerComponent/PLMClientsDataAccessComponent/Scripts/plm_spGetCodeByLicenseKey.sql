IF OBJECT_ID('dbo.plm_spGetCodeByLicenseKey') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetCodeByLicenseKey
go

/* 
	Author:			Ulises Orihuela		 
	Object:			dbo.plm_spGetCodeByLicenseKey
	
	Description:	Update TargetClientCodes
					
	Company:		PLM Latina.

	DECLARE
		@CRUDType		tinyint,
		@addressId		int

	EXECUTE dbo.plm_spGetCodeByLicenseKey @licensekey = EU3621H2J6181BR5
		
 */ 

CREATE PROCEDURE dbo.plm_spGetCodeByLicenseKey
(
		 
		 @licensekey   varchar( 100)
)
AS
BEGIN

	DECLARE 
		@codeId			INT
		,@codeString int 
		,@prefixid int
		,@codestatusid int 
		,@assing int
	
	IF (  @licensekey IS NULL )
	BEGIN
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
	END
	
	
	select  c.CodeId ,c.CodeString, c.PrefixId, c.CodeStatusId, c.Assign
	from codes c 
	inner join DistributionCodePrefixes d on c.PrefixId = d.PrefixId
	inner join DistributionLicenses dl on d.PrefixId = dl.PrefixId
	inner join Licenses l on dl.LicenseId = l.LicenseId
	where l.LicenseKey =  @licensekey

RETURN @@ROWCOUNT

END