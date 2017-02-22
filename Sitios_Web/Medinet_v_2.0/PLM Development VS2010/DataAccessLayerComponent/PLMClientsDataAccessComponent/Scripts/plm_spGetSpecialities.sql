
IF OBJECT_ID('dbo.plm_spGetSpecialities') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetSpecialities
go

/* 
	Author:			Juan Ramirez.				 
	Object:			dbo.plm_spGetSpecialities
	
	Description:	Retrieves all Specialities stored in the database.

	Company:		PLM Latina.

	EXECUTE dbo.plm_spGetSpecialities

 */ 

CREATE PROCEDURE dbo.plm_spGetSpecialities
(
	@professionId     smallint = null
)
AS
BEGIN

	IF(@professionId IS NULL)
	BEGIN
		SELECT [SpecialityId]
			  ,[SpecialityName]
			  ,[ShortName]
			  ,[Active]
		FROM [dbo].[Specialities]

		RETURN @@ROWCOUNT
	END
	IF(@professionId IS NOT NULL)
	BEGIN
		SELECT S.[SpecialityId]
			  ,S.[SpecialityName]
			  ,S.[ShortName]
			  ,S.[Active]
		FROM [dbo].[Specialities] S
			 INNER JOIN [dbo].[ProfessionSpecialities] SP
			 ON(S.SpecialityId = SP.SpecialityId)
		WHERE SP.ProfessionId = @professionId
		
		RETURN @@ROWCOUNT
	END


END
go

