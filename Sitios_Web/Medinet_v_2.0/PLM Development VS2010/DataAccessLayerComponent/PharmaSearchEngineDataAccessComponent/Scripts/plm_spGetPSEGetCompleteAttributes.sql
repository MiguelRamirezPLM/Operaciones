IF OBJECT_ID('dbo.plm_spGetPSEGetCompleteAttributes') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetPSEGetCompleteAttributes
go

/* 
	Author:			Ramiro Sánchez
	Object:			dbo.plm_spGetPSEGetCompleteAttributes
	
	Description:	Get attributes:

	Company:		PLM Latina.

	EXECUTE dbo.plm_spGetPSEGetCompleteAttributes 
				@editionId		= 58, 
				@divisionId		= 1107,	
				@categoryId		= 101, 
				@productId		= 17342, 
				@pharmaFormId	= 210

 */ 

CREATE PROCEDURE [dbo].[plm_spGetPSEGetCompleteAttributes]
(
	@editionId		int,
	@divisionId		int,
	@categoryId		int,
	@productId		int,
	@pharmaFormId	int,
	@prefixName		varchar(15)
)
AS
BEGIN

	-- Create the final container:
	CREATE TABLE #tmpProductContents
	(
		EditionId				int NOT NULL,
		DivisionId				int NOT NULL,
		CategoryId				int NOT NULL,
		ProductId				int NOT NULL,
		PharmaFormId			int NOT NULL,
		AttributeGroupId		int NOT NULL,
		AttributeGroupName		varchar(100) NOT NULL,
		AttributePlainContent	text NULL,
		AttributeHTMLContent	text NULL,
		AttributeGroupOrder		int,
		PrefixName				varchar
	)
	
	-- Declare some necessary variables:
	DECLARE
		@attributeGroupId		int,
		@attributeGroupName		varchar(100),
		@attributeGroupOrder	int
		
	
	-- Declare cursor to iterate through AttributeGroups:
	DECLARE curAttributeGroups CURSOR
	FOR	
	
	SELECT DISTINCT
		ag.AttributeGroupId, ag.AttributeGroupName, ag.[AttributeGroupOrder] AS AttributeGroupOrder	
	FROM AttributeGroup ag
		INNER JOIN EditionAttributeGroup eag ON ag.AttributeGroupId = eag.AttributeGroupId
		INNER JOIN EditionAttributes ea ON eag.AttributeId = ea.AttributeId
			And eag.EditionId = ea.EditionId
		INNER JOIN Attributes a ON ea.AttributeId = a.AttributeId
		INNER JOIN ProductContents pc ON a.AttributeId = pc.AttributeId
		
		INNER JOIN DistributionAttributeGroup dag ON eag.AttributeGroupId = dag.AttributeGroupId And
														eag.EditionId = dag.EditionId And
															eag.AttributeId = dag.AttributeId
															
		INNER JOIN DistributionCodePrefixes dcp ON dag.DistributionId = dcp.DistributionId And			
														dag.TargetId = dcp.TargetId And
															dag.PrefixId = dcp.PrefixId	
																
		INNER JOIN CodePrefixes cp ON (cp.PrefixId = dcp.PrefixId)									
														
		
	WHERE	pc.EditionId	= @editionId AND
			pc.Divisionid	= @divisionid AND
			pc.CategoryId	= @categoryId AND
			pc.ProductId	= @productId AND
			pc.PharmaFormId	= @pharmaFormId AND
			ea.EditionId	= @editionId AND
			eag.EditionId	= @editionId AND
			cp.PrefixName	= @PrefixName AND
			ag.Active = 1 AND
			eag.Active = 1
			
	OPEN curAttributeGroups
	
	-- Get first AttributeGroup:
	FETCH NEXT FROM curAttributeGroups
	INTO @attributeGroupId, @attributeGroupName, @attributeGroupOrder

	WHILE @@FETCH_STATUS = 0
	BEGIN
	
		-- Variable to hold Attribute contain:
		DECLARE 
			@plainContent			varchar(8000),
			@finalPlainContent		varchar(8000),
			@HTMLContent			varchar(8000),
			@finalHTMLContent		varchar(8000)
	
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
		WHERE pc.EditionId = @editionId AND
			  pc.Divisionid = @divisionid AND
			  pc.CategoryId = @categoryId AND
			  pc.ProductId = @productId AND
			  pc.PharmaFormId = @pharmaFormId AND
			  ag.AttributeGroupId = @attributeGroupId AND
			  ea.EditionId = @editionId AND
		      eag.EditionId	= @editionId AND
			  a.Active = 1 AND
			  eag.Active = 1 AND
			  ag.Active = 1
			  
		OPEN curContent
	
		-- Get first content:
		FETCH NEXT FROM curContent
		INTO @plainContent, @HTMLContent

		-- Clean variables:
		SET @finalPlainContent = ''
		SET @finalHTMLContent = ''

		WHILE @@FETCH_STATUS = 0
		BEGIN
			
			IF (LEN(@finalPlainContent) > 0)
				SET @finalPlainContent = @finalPlainContent + '\n'

			IF (LEN(@finalHTMLContent) > 0)
				SET @finalHTMLContent = @finalHTMLContent + '<br />'
			
			-- Concat Plain Content:
			SET @finalPlainContent = @finalPlainContent + @plainContent

			-- Concat HTML Content:
			SET @finalHTMLContent = @finalHTMLContent +	@HTMLContent
			
			-- Get the next content:
			FETCH NEXT FROM curContent
			INTO @plainContent, @HTMLContent
		END
	
		CLOSE curContent
		DEALLOCATE curContent
	
		-- Insert entry into #tmpProductContents final container:
		INSERT INTO #tmpProductContents (EditionId, DivisionId, CategoryId, ProductId, PharmaFormId, 
			AttributeGroupId, AttributeGroupName, AttributePlainContent, AttributeHTMLContent, AttributeGroupOrder)
		VALUES 
		(	
			@editionId,
			@divisionid,
			@categoryId,
			@productId,
			@pharmaFormId,
			@attributeGroupId,
			@attributeGroupName,
			@finalPlainContent,
			@finalHTMLContent,
			@attributeGroupOrder
		)
		
		-- Get next AttributeGroup:
		FETCH NEXT FROM curAttributeGroups
		INTO @attributeGroupId, @attributeGroupName, @attributeGroupOrder
		
	END
	
	CLOSE curAttributeGroups
	DEALLOCATE curAttributeGroups

	-- Return datarows:
	SELECT EditionId, DivisionId, CategoryId, ProductId, PharmaFormId, AttributeGroupId, AttributeGroupName, 
		AttributePlainContent, AttributeHTMLContent
	FROM #tmpProductContents
	ORDER BY AttributeGroupOrder

	RETURN @@ROWCOUNT

	DROP TABLE #tmpProductContents

END
