
IF OBJECT_ID('dbo.plm_spGetUpdatePacks') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetUpdatePacks
go

/* 
	Author:			Juan Ramírez.				 
	Object:			dbo.plm_spGetUpdatePacks
	
	Description:	.

	Company:		PLM Latina.

	plm_spGetUpdatePacks @editionId = 1, @dbId = 1, @code = ''

 */ 

CREATE PROCEDURE dbo.plm_spGetUpdatePacks
(
	@packUpdId		int,
	@code			varchar(50) 
)
AS
BEGIN

	  SELECT e.[PackSqlTextId]
			,e.[PackUpdId]
			,e.[SqlText]
			,e.[AddedDate]
			,e.[PackOrder]
			,e.[Active]

	  FROM [dbo].[EditionPackSQLTexts] e
		   Left Join (SELECT * 
					  FROM [dbo].[ClientCodesUpdated]
					  WHERE CodeString = @code) cc On(e.[PackSqlTextId] = cc.[PackSqlTextId])
		   
	WHERE e.Active		= 1 And 
		  e.PackUpdId   = @packUpdId
		  
	ORDER BY [PackOrder]

	RETURN @@ROWCOUNT


END
