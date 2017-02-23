
IF OBJECT_ID('dbo.plm_spGetClientCodeUpdates') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetClientCodeUpdates
go

/* 
	Author:			Juan Ramírez.				 
	Object:			dbo.plm_spGetClientCodes
	
	Description:	Gets ClientCodesUpdated By Id.

	Company:		PLM Latina.

	plm_spGetClientCodeUpdates @editionId = 1, @dbId = 1, @code = ''

 */ 

CREATE PROCEDURE dbo.plm_spGetClientCodeUpdates
(
	@ids		varchar(100)
)
AS
BEGIN

	DECLARE @sql varchar(4000)

	SET @sql =	'SELECT  ' + Char(13) + 
				'ClientCodeUpdId,' + Char(13) + 
				'CodeString,' + Char(13) + 
				'PackSqlTextId,' + Char(13) + 
				'SentDate,' + Char(13) + 
				'UpdateDate,' + Char(13) +
				'Confirmed ' + Char(13) +  
				
				'FROM ClientCodesUpdated ' + Char(13) + 
				'WHERE Confirmed = 1 And ' + Char(13) +
				'ClientCodeUpdId In (' + @ids + ') ' + Char(13) +
				'ORDER BY 1'


	PRINT 'SQL: ' + @sql

	EXEC (@sql)

	RETURN @@ROWCOUNT


END
