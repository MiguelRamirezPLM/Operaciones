IF OBJECT_ID('dbo.plm_spGetSymptomsByEdition') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetSymptomsByEdition
go

/* 
	Author:			Ramiro Sánchez				 
	Object:			dbo.plm_spGetSymptomsByEdition
	
	Description:	Get all Symptoms by EditionId.

	Company:		PLM Latina.

	EXECUTE dbo.plm_spGetSymptomsByEdition @editionId = 52

 */

CREATE PROCEDURE dbo.plm_spGetSymptomsByEdition
(
	@editionId			int,
	@parentId			int = NULL
)
AS
BEGIN

	DECLARE @symptomId			int,
			@symptomParent		int,
			@symptomName		varchar(30),
			@symptomColorIn		varchar(30),
			@symptomColorOut	varchar(30)

	-- The parameters shouldn't be null:
	IF (@editionId IS NULL)
	BEGIN
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
	END

	SET @symptomId = NULL
	SET @symptomParent = NULL
	SET @symptomName = NULL
	SET @symptomColorIn = NULL 
	SET @symptomColorOut = NULL

	IF(@parentId Is Null)
	BEGIN
		--Create a temporary table to store search results
		IF OBJECT_ID('dbo.tmpSymptomDetail') IS NOT NULL
		BEGIN
			Delete From dbo.tmpSymptomDetail
		END
		ELSE
		BEGIN
			CREATE TABLE tmpSymptomDetail
				(SymptomId int,
				ParentId int,
				SymptomName varchar(30),
				SymptomColorIn varchar(30), 
				SymptomColorOut varchar(30))
		END

		--A query that gets all the symptoms of an Edition
		DECLARE curSYMPTOMS CURSOR
		FOR Select Distinct
			s.SymptomId, 
			s.ParentId, 
			s.SymptomName, 
			es.SymptomColorIn, 
			es.SymptomColorOut
		From Symptoms s
			Inner Join EditionSymptoms es On s.SymptomId = es.SymptomId
		Where es.EditionId = @editionId
		Order By s.SymptomName

		--Explore each of the symptoms obtained		
		OPEN curSYMPTOMS
		FETCH NEXT FROM curSYMPTOMS
		INTO @symptomId, @symptomParent, @symptomName, @symptomColorIn, @symptomColorOut

		WHILE @@FETCH_STATUS = 0
		BEGIN
			--Insert the record in the temporary table
			INSERT INTO tmpSymptomDetail
				(SymptomId, 
				ParentId, 
				SymptomName, 
				SymptomColorIn, 
				SymptomColorOut)
			Values(@symptomId, @symptomParent, @symptomName, @symptomColorIn, @symptomColorOut)

			--If the symptom is a symptom child
			IF(@symptomParent Is Not Null)
			BEGIN
				EXECUTE plm_spGetSymptomsByEdition @editionId, @symptomParent
			END

			SET @symptomId = NULL
			SET @symptomParent = NULL
			SET @symptomName = NULL
			SET @symptomColorIn = NULL 
			SET @symptomColorOut = NULL

			FETCH NEXT FROM curSYMPTOMS
			INTO @symptomId, @symptomParent, @symptomName, @symptomColorIn, @symptomColorOut
		END
		CLOSE curSYMPTOMS
		DEALLOCATE curSYMPTOMS
		
		--Final query the temporary table to get all the symptoms.
		Select Distinct 
			SymptomId, 
			ParentId, 
			SymptomName, 
			SymptomColorIn, 
			SymptomColorOut
		From tmpSymptomDetail
		Order by ParentId, SymptomName

		--Delete the temporary table.
		DROP TABLE tmpSymptomDetail
		
	END
	ELSE
	BEGIN
		--Get the information of the Parent Symptom
		Select	@symptomId = SymptomId,
				@symptomParent = ParentId,
				@symptomName = SymptomName,
				@symptomColorIn = Null,
				@symptomColorOut = Null
		From Symptoms
		Where SymptomId = @parentId
		Order By SymptomName

		--If the symptom does not exist in the temporary table, insert it.
		IF NOT EXISTS(Select * 
						From tmpSymptomDetail 
						Where SymptomId = @SymptomId)
		BEGIN
			INSERT INTO tmpSymptomDetail
				(SymptomId, 
				ParentId, 
				SymptomName, 
				SymptomColorIn, 
				SymptomColorOut)
			Values(@symptomId, @symptomParent, @symptomName, @symptomColorIn, @symptomColorOut)
		END

		--If the symptom is a symptom child
		IF(@symptomParent Is Not Null)
		BEGIN
			EXECUTE plm_spGetSymptomsByEdition @editionId, @symptomParent
		END
			SET @symptomId = NULL
			SET @symptomParent = NULL
			SET @symptomName = NULL
			SET @symptomColorIn = NULL 
			SET @symptomColorOut = NULL
	END
	RETURN @@ROWCOUNT
END
go