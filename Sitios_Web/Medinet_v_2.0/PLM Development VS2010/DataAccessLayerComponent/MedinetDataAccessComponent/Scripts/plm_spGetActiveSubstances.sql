IF OBJECT_ID('dbo.plm_spGetActiveSubstances') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetActiveSubstances
go

/* 
	Author:			Ramiro Sánchez
	Object:			dbo.plm_spGetActiveSubstances
	
	Description:	Gets all Content Units.
	Company:		PLM Latina.

	EXECUTE dbo.plm_spGetActiveSubstances @activeSubstanceId = 2723
	EXECUTE dbo.plm_spGetActiveSubstances @description = 'APIGEINA'
	EXECUTE dbo.plm_spGetActiveSubstances @activeSubstanceId = 2723, @description = 'Ap%'

 */ 

CREATE PROCEDURE dbo.plm_spGetActiveSubstances
(
	@activeSubstanceId		int = Null,
	@description			varchar(210) = Null
)
AS
BEGIN

	IF(@activeSubstanceId Is Null And @description Is Not Null)
	BEGIN
	
		SELECT DISTINCT [ActiveSubstanceId]
		  ,[Description]
		  ,[EnglishDescription]
		  ,[Active]
		FROM [dbo].[ActiveSubstances]
		WHERE UPPER([Description]) COLLATE SQL_Latin1_General_CP1_CI_AI LIKE @description
			And [Active] = 1

		ORDER BY [Description]

		RETURN @@ROWCOUNT

	END

	IF(@activeSubstanceId Is Not Null And @description Is Null)
	BEGIN
	
		SELECT DISTINCT [ActiveSubstanceId]
		  ,[Description]
		  ,[EnglishDescription]
		  ,[Active]
		FROM [dbo].[ActiveSubstances]
		WHERE [ActiveSubstanceId] Not In (@activeSubstanceId)
			And [Active] = 1

		ORDER BY [Description]

		RETURN @@ROWCOUNT

	END

	IF(@activeSubstanceId Is Not Null And @description Is Not Null)
	BEGIN
	
		SELECT DISTINCT [ActiveSubstanceId]
		  ,[Description]
		  ,[EnglishDescription]
		  ,[Active]
		FROM [dbo].[ActiveSubstances]
		WHERE [ActiveSubstanceId] Not In (@activeSubstanceId)
			And UPPER([Description]) COLLATE SQL_Latin1_General_CP1_CI_AI LIKE @description
			And [Active] = 1

		ORDER BY [Description]

		RETURN @@ROWCOUNT

	END

END
go