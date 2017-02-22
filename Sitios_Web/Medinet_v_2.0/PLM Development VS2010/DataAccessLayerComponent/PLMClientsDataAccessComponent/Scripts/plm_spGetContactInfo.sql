IF OBJECT_ID('dbo.plm_spGetContactInfo') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetContactInfo
go

/* 
	Author:			Beatriz Vázquez.				 
	Object:			dbo.plm_spGetContactInfo
	
	Description:	Retrieves all Countries stored in the database.

	Company:		PLM Latina.

	EXECUTE dbo.plm_spGetContactInfo @id = mex

 */ 
CREATE PROCEDURE dbo.plm_spGetContactInfo 
(
	@id			varchar(3)
)
AS
BEGIN
	IF(@Id IS NOT NULL)
	BEGIN
		SELECT	PB.BRANCHID,
				C.COUNTRYNAME,
				PB.COMPANYNAME,
				A.STREET,
				A.SUBURB,
				A.ZIPCODE,
				A.LADA,	
				A.PHONEONE,
				PB.CONTACTEMAIL
		FROM COUNTRIES C
		INNER JOIN PLMBRANCHES PB ON C.COUNTRYID = PB.COUNTRYID
		INNER JOIN ADDRESSES A ON PB.ADDRESSID = A.ADDRESSID
		WHERE C.ID = @id AND 
			  C.ACTIVE = 1 AND 
			  PB.ACTIVE = 1 AND
			  A.ACTIVE = 1 

		RETURN @@ROWCOUNT
	END

	ELSE
	BEGIN
		RAISERROR ('The @id parameter can''t be null', 16, 1)
		RETURN -1

	END
		
END
GO
