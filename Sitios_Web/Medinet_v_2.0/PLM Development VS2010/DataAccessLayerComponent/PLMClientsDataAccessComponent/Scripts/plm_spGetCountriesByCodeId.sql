IF OBJECT_ID('dbo.plm_spGetCountriesByCodeId') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetCountriesByCodeId
go

/* 
	Author:			Juan Manuel Ramírez.				 
	Object:			dbo.plm_spGetCountriesByCodeId
	
	Description:	Gets all the editions associated to the given book.

	Company:		Thomson PLM.

	DECLARE
		@bookId		int

	SELECT 
		@bookId	= 1

	EXECUTE dbo.plm_spGetCountriesByCodeId 'MEX'

 */ 

CREATE PROCEDURE dbo.plm_spGetCountriesByCodeId
(
	@countries		varchar(100)
)
AS
BEGIN

	DECLARE @sql varchar(4000)

	SET @sql = 'SELECT DISTINCT [CountryId],[CountryName],[CountryCode]' + CHAR(13) +
			   ',[ID],[Active]' + CHAR(13) +
			   'FROM [dbo].[Countries] ' + CHAR(13) +
			   'WHERE [Active] = 1 AND [ID] IN (''' + @countries + ''')' + CHAR(13) +
			   'ORDER BY [CountryName] '

	PRINT 'SQL: ' + @sql

	EXEC (@sql)

	RETURN @@ROWCOUNT
END
