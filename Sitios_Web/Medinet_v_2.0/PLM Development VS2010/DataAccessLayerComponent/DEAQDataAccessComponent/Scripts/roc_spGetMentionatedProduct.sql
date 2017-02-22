IF OBJECT_ID('dbo.roc_spGetMentionatedProduct') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetMentionatedProduct
go

/* 
	Author:			Daniel Campos / Ramiro Sánchez				 
	Object:			dbo.roc_spGetMentionatedProduct
	
	Description:	Retrieves information about MentionatedProduct by Edition and ProductId.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetMentionatedProduct @editionId = 1, @productId = 4

 */ 

CREATE PROCEDURE dbo.roc_spGetMentionatedProduct
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
		FROM MentionatedProducts
		WHERE EditionId = @editionId 
			AND ProductId = @productId
	
END
go