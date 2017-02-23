
IF OBJECT_ID('dbo.plm_spGetClientCodesErrors') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetClientCodesErrors
go

/* 
	Author:			Juan Ramírez.				 
	Object:			dbo.plm_spGetClientCodesErrors
	
	Description:	Gets ClientCodesErrors By Id.

	Company:		PLM Latina.

	plm_spGetClientCodesErrors @editionId = 1, @dbId = 1, @code = ''

 */ 

CREATE PROCEDURE dbo.plm_spGetClientCodesErrors
(
	@ids		varchar(100)
)
AS
BEGIN

	DECLARE @sql varchar(4000)

	SET @sql =	'SELECT ' + Char(13) + 
				'ClientCodeErrorId,' + Char(13) + 
				'CodeString,' + Char(13) + 
				'PackSqlTextId,' + Char(13) + 
				'ErrorMessage,' + Char(13) + 
				'ErrorDate,' + Char(13) +
				'Confirmed ' + Char(13) +  
				
				'FROM ClientCodesErrors ' + Char(13) + 
				'WHERE Confirmed = 1 And ' + Char(13) +
				'ClientCodeErrorId In (' + @ids + ') ' + Char(13) +
				'ORDER BY 1'


	PRINT 'SQL: ' + @sql

	EXEC (@sql)

	RETURN @@ROWCOUNT


END
