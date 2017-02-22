IF OBJECT_ID('dbo.plm_spGetProfessionalPractices') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetProfessionalPractices
go

/* 
	Author:			Juan Ramirez.				 
	Object:			dbo.plm_spGetProfessionalPractices
	
	Description:	Retrieves all Professional Practices stored in the database.

	Company:		PLM Latina.

	EXECUTE dbo.plm_spGetProfessionalPractices

 */ 

CREATE PROCEDURE dbo.plm_spGetProfessionalPractices
AS
BEGIN

		SELECT [PracticeId]
			  ,[Description]
			  ,[Active]
		FROM [dbo].[ProfessionalPractices]

	RETURN @@ROWCOUNT

END
go