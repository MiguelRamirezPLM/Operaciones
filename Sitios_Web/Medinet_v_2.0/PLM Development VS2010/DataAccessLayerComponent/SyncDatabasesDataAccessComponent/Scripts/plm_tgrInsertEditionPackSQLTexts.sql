
IF OBJECT_ID('dbo.plm_tgrInsertEditionPackSQLTexts') IS NOT NULL 
	DROP TRIGGER dbo.plm_tgrInsertEditionPackSQLTexts
go

CREATE TRIGGER dbo.plm_tgrInsertEditionPackSQLTexts
ON dbo.EditionPackSQLTexts
FOR INSERT
AS
BEGIN

	DECLARE
		@EditionId		int,
		@DbId			int,
		@PackCount		int

	-- Take the fields of the inserted record:
	SELECT @EditionID = EditionID, @DbID = [DbId] 
	FROM Inserted

	-- Take PackCount from Versions table:
	SELECT @PackCount = PackCount 
	FROM Versions
	WHERE	EditionId	= @EditionId AND
			[DbId]		= @DbID
			
	-- Add in one PackCount:
	SET @PackCount = @PackCount + 1

	-- Update Versions.PackCount, Versions.VersionNumber and Versions.LastUpdate:
	UPDATE Versions
	SET PackCount = @PackCount, 

		VersionNumber = CONVERT(varchar(MAX), @EditionID) + '.' + 
						CONVERT(varchar, @DbID) + '.' +
						CONVERT(varchar, @PackCount),

		LastUpdate = GETDATE()

END
go