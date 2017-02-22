IF OBJECT_ID('dbo.roc_spGetParticipantProduct') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetParticipantProduct
go

/* 
	Author:			Daniel Campos / Ramiro Sánchez				 
	Object:			dbo.roc_spGetParticipantProduct
	
	Description:	Retrieves information about ParticipantProduct by Edition and ProductId.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetParticipantProduct @editionId = 1, @productId = 73

 */ 

CREATE PROCEDURE dbo.roc_spGetParticipantProduct
(
	@editionId			int,
	@productId			int
)
AS
BEGIN

	-- The parameters shouldn't be null:
	IF (@editionId IS NULL OR @productId IS NULL)
	
	BEGIN
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
	END

	SELECT *
		FROM ParticipantProducts
		WHERE EditionId = @editionId 
			AND ProductId = @productId
	
END
go