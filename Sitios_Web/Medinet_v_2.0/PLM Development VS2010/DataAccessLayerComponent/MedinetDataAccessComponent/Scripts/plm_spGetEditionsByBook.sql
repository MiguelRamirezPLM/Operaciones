IF OBJECT_ID('dbo.plm_spGetEditionsByBook') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetEditionsByBook
go

/* 
	Author:			Ramiro Sánchez
	Object:			dbo.plm_spGetEditionsByBook
	
	Description:	Gets all the editions given a book
	Company:		PLM Latina.


 */ 

CREATE PROCEDURE dbo.plm_spGetEditionsByBook
(
	@bookId				int,
	@countryId			int
)
AS
BEGIN

	SELECT [EditionId]
		  ,[ParentId]
		  ,[CountryId]
		  ,[BookId]
		  ,[NumberEdition]
		  ,[ISBN]
		  ,[BarCode]
		  ,[Active]
	FROM [dbo].[Editions]
	WHERE [Active] = 1 AND [BookId] = @bookId AND
		  [CountryId] = @countryId
	Order BY [NumberEdition]

END
go