use PEV

IF OBJECT_ID('[dbo].[plm_spGetVETCompleteAttributes]') IS NOT NULL
	DROP PROCEDURE [dbo].[plm_spGetVETCompleteAttributes]

GO

/*
       Author:          César Avila	
       		 
	   Object:			[dbo].[plm_spGetVETActiveSubstances]
	
	   Description:	    

	   EXECUTE          
	
	*/
CREATE PROCEDURE [dbo].[plm_spGetVETCompleteAttributes]

    @editionId		int,
	@divisionId		int,
	@categoryId		int,
	@productId		int,
	@pharmaFormId	int
     
	
	
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
		AttributeGroupOrder		int
		
	)
  

    -- Declare some necessary variables:
	DECLARE
		@attributeGroupId		int,
		@attributeGroupName		varchar(100),
		@attributeGroupOrder	int
		
		
    -- Declare cursor to iterate through AttributeGroups:
    DECLARE curAttributeGroups CURSOR
    FOR
    
    SELECT DISTINCT  AG.AttributeGroupId,AG.AttributeGroupName,AG.[ORDER] AS AttributeGroupOrder  
        FROM AttributeGroup AG 
        
        INNER JOIN EditionAttributeGroup EAG ON AG.AttributeGroupId=EAG.AttributeGroupId
        INNER JOIN EditionAttributes EA      ON EAG.EditionId=EA.EditionId AND
                                                EAG.AttributeId=EA.AttributeId
        INNER JOIN Attributes         A      ON EA.AttributeId=A.AttributeId
        INNER JOIN ProductContents    PC     ON A.AttributeId=PC.AttributeId    
        
        WHERE
        
                PC.EditionId=@editionId     AND
                PC.DivisionId=@divisionId   AND
                PC.CategoryId=@categoryId   AND
                PC.ProductId =@productId    AND
                PC.PharmaFormId =@pharmaFormId AND
                EA.EditionId =@editionId     AND
                EAG.EditionId=@editionId     AND
                Ag.Active   =1               AND
                EAG.Active  =1 
        
        OPEN  curAttributeGroups                   

    -- Get first AttributeGroup:
    
    FETCH NEXT FROM curAttributeGroups 
    INTO @attributeGroupId, @attributeGroupName, @attributeGroupOrder
    
    WHILE @@FETCH_STATUS=0
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
		SELECT PC.PlainContent, PC.HTMLContent
		FROM ProductContents PC
		INNER JOIN Attributes A ON (PC.AttributeId = A.AttributeId)
		INNER JOIN EditionAttributes EA ON (PC.AttributeId = EA.AttributeId AND
												PC.EditionId = EA.EditionId)
		INNER JOIN EditionAttributeGroup EAG ON (EA.AttributeId = EAG.AttributeId AND
													EA.EditionId = EAG.EditionId)
		INNER JOIN AttributeGroup AG ON (EAG.AttributeGroupId = AG.AttributeGroupId)
		WHERE PC.EditionId = @editionId AND
			  PC.Divisionid = @divisionid AND
			  PC.CategoryId = @categoryId AND
			  PC.ProductId = @productId AND
			  PC.PharmaFormId = @pharmaFormId AND
			  AG.AttributeGroupId = @attributeGroupId AND
			  EA.EditionId = @editionId AND
		      EAG.EditionId	= @editionId AND
			  A.Active = 1 AND
			  EAG.Active = 1 AND
			  AG.Active = 1
			  
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

		





