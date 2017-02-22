IF OBJECT_ID('dbo.plm_spGetPSEAttibuteDetails') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetPSEAttibuteDetails
go

/* 
	Author:			Juan Manuel Ramírez / Erick Silva.
	Modified:		Ramiro Sánchez 20120810
	Object:			dbo.plm_spGetPSEAttibuteDetails
	
	Description:	Get attributes:

	Company:		PLM Latina.

	DECLARE @retValue int

	EXECUTE @retValue = dbo.plm_spGetPSEAttibuteDetails 
								@editionId		= 55, 
								@divisionId		= 171,	
								@categoryId		= 101, 
								@productId		= 9343, 
								@pharmaFormId	= 210,
								@targetId		= 1
	SELECT @retValue

 */ 

CREATE PROCEDURE [dbo].[plm_spGetPSEAttibuteDetails]
(
	@editionId		int,
	@divisionId		int,
	@categoryId		int,
	@productId		int,
	@pharmaFormId	int,
	@targetId		tinyint
)
AS
BEGIN

	DECLARE
		@countryId	tinyint
		
	-- Get countryId given by EditionId:
	SELECT @countryId = countryId FROM Editions WHERE EditionId = @editionId
	
	-- Create the final container:
	CREATE TABLE #tmpProductContents
	(
		EditionId				int NOT NULL,
		DivisionId				int NOT NULL,
		CategoryId				int NOT NULL,
		ProductId				int NOT NULL,
		PharmaFormId			int NOT NULL,
		AttributeId				int NOT NULL,
		
		AttributeName			varchar(100) NOT NULL,
		AttributeContent		text NULL,
		AttributeHTMLContent	text NULL,
		
		AtributeTargetOrder	int
	)
	
	-- Declare some necessary variables:
	DECLARE
		@targetAttributeId		int,
		@targetAttributeName	varchar(100),
		@targetAttributeOrder	int
	
	-- Declare cursor to iterate through AttributeGroups:
	DECLARE curTargetAttr CURSOR
	FOR	
	SELECT 
		ma.TargetAttributeId, ma.TargetAttributeName, ma.[Order] AS TargetOrder
		
	FROM TargetAttributes ma

	WHERE	ma.CountryId	= @countryId AND
			ma.TargetId		= @targetId
			
	OPEN curTargetAttr
	
	-- Get first AttributeGroup:
	FETCH NEXT FROM curTargetAttr
	INTO @targetAttributeId, @targetAttributeName, @targetAttributeOrder

	WHILE @@FETCH_STATUS = 0
	BEGIN
	
		-- Variable to hold plain content:
		DECLARE 
			@content			varchar(8000),
			@finalContent		varchar(8000),
			@HTMLContent		varchar(8000),
			@finalHTMLContent	varchar(8000)
	
		-- Declare cursor to iterate through contents:
		DECLARE curContent CURSOR
		FOR
		SELECT pc.PlainContent, pc.HTMLContent
		FROM ProductContents pc

		INNER JOIN Attributes a ON (pc.AttributeId = a.AttributeId)

		INNER JOIN EditionAttributes ea ON (pc.AttributeId = ea.AttributeId AND
												pc.EditionId = ea.EditionId)
												
		INNER JOIN EditionAttributeGroup eag ON (ea.AttributeId = eag.AttributeId AND
													ea.EditionId = eag.EditionId)
													
		INNER JOIN AttributeGroup ag ON (eag.AttributeGroupId = ag.AttributeGroupId)
		
		INNER JOIN MasterTargetAttributes mta ON (ag.AttributeGroupId = mta.AttributeGroupId)

		WHERE pc.EditionId			= @editionId AND
			  pc.Divisionid			= @divisionid AND
			  pc.CategoryId			= @categoryId AND
			  pc.ProductId			= @productId AND
			  pc.PharmaFormId		= @pharmaFormId AND
			  mta.TargetAttributeId	= @targetAttributeId
			  
		OPEN curContent
	
		-- Get first content:
		FETCH NEXT FROM curContent
		INTO @content, @HTMLContent

		-- Clean @finalContent variable:
		SET @finalContent = ''
		SET @finalHTMLContent = ''

		WHILE @@FETCH_STATUS = 0
		BEGIN
			
			IF (LEN(@finalContent) > 0)
				SET @finalContent = @finalContent + '\n'

			IF (LEN(@finalHTMLContent) > 0)
				SET @finalHTMLContent = @finalHTMLContent + '<br />'
			
			-- Concat Plain Content:
			SET @finalContent = @finalContent +	@content

			-- Concat HTML Content:
			SET @finalHTMLContent = @finalHTMLContent +	@HTMLContent
			
			-- Get the next content:
			FETCH NEXT FROM curContent
			INTO @content, @HTMLContent
			
		END
	
		CLOSE curContent
		DEALLOCATE curContent
	
		-- Insert entry into #tmpProductContents final container:
		INSERT INTO #tmpProductContents (EditionId, DivisionId, CategoryId, ProductId, PharmaFormId, 
			AttributeId, AttributeName, AttributeContent, AttributeHTMLContent, AtributeTargetOrder)
			
		VALUES 
		(	
			@editionId, @divisionid, @categoryId, @productId, @pharmaFormId,
			
			CASE @targetAttributeName 
				WHEN 'DOSIS Y VÍA DE ADMINISTRACIÓN'	THEN 3420
				WHEN 'FORMA FARMACEÚTICA Y FORMULACIÓN' THEN 3585
				WHEN 'INDICACIONES TERAPÉUTICAS'		THEN 3438
				WHEN 'CONTRAINDICACIONES'				THEN 3407
				WHEN 'PRESENTACIONES'					THEN 3473
				
			ELSE 0 END,
			
			@targetAttributeName,
			@finalContent,
			@finalHTMLContent,
			@targetAttributeOrder
		)
		
		-- Get next AttributeGroup:
		FETCH NEXT FROM curTargetAttr
		INTO @targetAttributeId, @targetAttributeName, @targetAttributeOrder
		
	END
	
	CLOSE curTargetAttr
	DEALLOCATE curTargetAttr

	-- Return datarows:
	SELECT EditionId, DivisionId, CategoryId, ProductId, PharmaFormId, AttributeId, AttributeName, AttributeContent, AttributeHTMLContent
	FROM #tmpProductContents
	ORDER BY AtributeTargetOrder

	RETURN @@ROWCOUNT

	DROP TABLE #tmpProductContents

END
