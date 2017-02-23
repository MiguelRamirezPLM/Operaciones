IF OBJECT_ID ('dbo.plm_tgrUpdateNumberInstallation') IS NOT NULL
   DROP TRIGGER dbo.plm_tgrUpdateNumberInstallation
GO

/* 
	Author:			Ramiro Sánchez.				 
	Object:			dbo.plm_tgrUpdateNumberInstallation
	
	Description:	This trigger updates the number of installations made with a license

	Company:		PLM Latina.
	
 */ 

CREATE TRIGGER plm_tgrUpdateNumberInstallation 
ON dbo.LicenseTargetCodes
AFTER INSERT, DELETE AS

BEGIN

IF UPDATE(LICENSEID)
BEGIN
	Update Licenses
		SET CurrentInstallation = (CurrentInstallation + 1)
	Where LicenseId = (Select i.LicenseId FROM INSERTED i);
END
ELSE
BEGIN
Update Licenses
		SET CurrentInstallation = (CurrentInstallation - 1)
	Where LicenseId = (Select d.LicenseId FROM DELETED d);
END
END

GO
