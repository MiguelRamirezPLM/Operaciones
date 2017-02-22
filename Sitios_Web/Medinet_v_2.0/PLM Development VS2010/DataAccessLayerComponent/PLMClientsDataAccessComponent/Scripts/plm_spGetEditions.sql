IF OBJECT_ID('dbo.plm_spGetEditions') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetEditions
go

/* 
	Author:			Juan Ramírez.				 
	Object:			dbo.plm_spGetEditions
	
	Description:	Get a edition by User.

	Company:		PLM México.

	DECLARE
		@userId		int

	SELECT 
		@userId	

	EXECUTE dbo.plm_spGetEditions @codeString = 'a'

 */ 

CREATE PROCEDURE dbo.plm_spGetEditions
(
	@codeString     varchar(100) = null
)
AS
BEGIN
	
	IF(@codeString Is Not Null)
	BEGIN
	
		SELECT E.[EditionId]
			  ,E.[ParentId]
			  ,E.[CountryId]
			  ,E.[BookId]
			  ,E.[NumberEdition]
			  ,E.[ISBN]
			  ,E.[BarCode]
			  ,E.[Active]
		FROM [dbo].[Editions] e
			 INNER JOIN [dbo].[CodePrefixEditions] cpe ON(e.[EditionId] = cpe.[EditionId])
			 INNER JOIN [dbo].[Codes] c ON(cpe.[PrefixId] = c.[PrefixId])
		WHERE E.[Active] = 1 AND 
			  C.[Assign] = 1 AND
			  C.[CodeStatusId] = 2 AND
			  C.[CodeString] = @codeString
	
		RETURN @@ROWCOUNT
	
	END
	
END
go