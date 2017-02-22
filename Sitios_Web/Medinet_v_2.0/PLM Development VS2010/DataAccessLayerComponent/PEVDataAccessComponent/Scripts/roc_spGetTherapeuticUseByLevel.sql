IF OBJECT_ID('dbo.roc_spGetTherapeuticUseByLevel') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetTherapeuticUseByLevel
go

/* 
	Author:			Daniel Campos / Ramiro Sánchez				 
	Object:			dbo.roc_spGetTherapeuticUseByLevel
	
	Description:	Retrieves all Therapeutic Uses by Level.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetTherapeuticUseByLevel @level = 2, @parentId = 230


 */ 

CREATE PROCEDURE dbo.roc_spGetTherapeuticUseByLevel
(
	@level				int,
	@parentId			int
)

AS
BEGIN

	-- The parameters shouldn't be null:
	IF (@level IS NULL OR @parentId IS NULL)
	BEGIN
	
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
		
	END

	SELECT * 
		FROM TherapeuticUses 
		WHERE ParentId = @parentId AND nivel = @level 
		ORDER BY TherapeuticName ASC

	
END
go