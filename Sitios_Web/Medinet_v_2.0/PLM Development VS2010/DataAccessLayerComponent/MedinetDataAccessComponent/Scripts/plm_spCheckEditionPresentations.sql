IF OBJECT_ID('dbo.plm_spCheckEditionPresentations') IS NOT NULL
	DROP PROCEDURE dbo.plm_spCheckEditionPresentations
go

/* 
	Author:			Ramiro Sánchez
	Object:			dbo.plm_spCheckEditionPresentations
	
	Description:	Checks if a presentation is associated with an edition.
	Company:		PLM Latina.

	EXECUTE dbo.plm_spCheckEditionPresentations @editionId = 80, @presentationId = 8505

 */ 

CREATE PROCEDURE dbo.plm_spCheckEditionPresentations
(
	@editionId			int,
	@presentationId		int
)
AS
BEGIN

	DECLARE @editPres int

	SELECT @editPres = COUNT(*)
	FROM EditionPresentations
	
	WHERE EditionId NOT IN (@editionId) AND
		  PresentationId = @presentationId

	RETURN @editPres

END
go