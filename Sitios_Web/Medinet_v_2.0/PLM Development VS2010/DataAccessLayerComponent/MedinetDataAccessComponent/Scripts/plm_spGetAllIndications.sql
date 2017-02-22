IF OBJECT_ID('dbo.plm_spGetAllIndications') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetAllIndications
go

/* 
	Author:			Erick Silva / Ramiro Sánchez
	Object:			dbo.plm_spGetAllIndications
	
	Description:	Gets all the indications that begin with the given string or identifier.
	Company:		PLM Latina.

	EXECUTE dbo.plm_spGetAllIndications @description = 'dol%'
	EXECUTE dbo.plm_spGetAllIndications @indicationId = 2351, @description = '%'

 */ 

CREATE PROCEDURE dbo.plm_spGetAllIndications
(
	@indicationId		int = Null,
	@description		varchar(100) = Null
)
AS
BEGIN

	IF(@indicationId Is Null And @description Is Not Null)
	BEGIN
	
		SELECT [IndicationId]
			,[ParentId]
			,[Description]
			,[Active]
		FROM [dbo].[Indications]

		WHERE	[Active]	= 1 AND
			UPPER([Description]) COLLATE SQL_Latin1_General_CP1_CI_AI LIKE 
								  CASE WHEN @description IS NULL THEN '%'
									   WHEN LEN(@description) > 3 THEN '%' + UPPER(@description) + '%'
									   ELSE  UPPER(@description) + '%' END 

		ORDER BY [Description]

	RETURN @@ROWCOUNT
	
	END
	
	IF(@indicationId Is Not Null And @description Is Not Null)
	BEGIN
	
		SELECT [IndicationId]
			,[ParentId]
			,[Description]
			,[Active]
		FROM [dbo].[Indications]

		WHERE	[Active]	= 1 AND
			[IndicationId] NOT IN (@indicationId) AND
			UPPER([Description]) COLLATE SQL_Latin1_General_CP1_CI_AI LIKE 
								  CASE WHEN @description IS NULL THEN '%'
									   WHEN LEN(@description) > 3 THEN '%' + UPPER(@description) + '%'
									   ELSE  UPPER(@description) + '%' END 

		ORDER BY [Description]

	RETURN @@ROWCOUNT
	
	END

END
go