IF OBJECT_ID('dbo.plm_spGetProfessions') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetProfessions
go

/* 
	Author:			Juan Ramirez.				 
	Object:			dbo.plm_spGetProfessions
	
	Description:	Retrieves all Professions stored in the database.

	Company:		PLM Latina.

	EXECUTE dbo.plm_spGetProfessions

 */ 

CREATE PROCEDURE dbo.plm_spGetProfessions
(
	@parentId		smallint = NULL
)
AS
BEGIN

	IF(@parentId Is Null)
	BEGIN

		SELECT [ProfessionId]
			,[ParentId]
			,[ProfessionName]
			,[ReqProfessionalLicense]
			,[Active]
		FROM [dbo].[Professions]
		WHERE [ParentId] Is Null
		ORDER BY [ProfessionName]

		RETURN @@ROWCOUNT
	END
	
	IF(@parentId Is Not Null)
	BEGIN

		SELECT [ProfessionId]
			,[ParentId]
			,[ProfessionName]
			,[ReqProfessionalLicense]
			,[Active]
		FROM [dbo].[Professions]
		WHERE [ParentId] = @parentId
		ORDER BY [ProfessionName]

		RETURN @@ROWCOUNT
	END
	
END
go