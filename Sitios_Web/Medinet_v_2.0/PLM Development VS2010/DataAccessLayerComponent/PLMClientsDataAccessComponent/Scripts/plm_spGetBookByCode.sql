IF OBJECT_ID('dbo.plm_spGetBookByCode') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetBookByCode
go

/* 
	Author:			Elizabeth Lazo.				 
	Object:			dbo.plm_spGetBookByCode
	
	Description:	Retrieves all information the  book by Code.

	Company:		PLM Latina.

	EXECUTE dbo.plm_spGetBookByCode @codeString = '9RL5ZN4AW6'


 */ 

CREATE PROCEDURE [dbo].[plm_spGetBookByCode]
(   
  @codeString      varchar(50) 
  
) 
AS
  BEGIN
  --The parameters shouldn't be null:
       IF (@codeString IS NULL) 
             
  BEGIN

    RAISERROR ('The current operation requires more parameters', 16, 1)
    RETURN -1

  END
 
  select b.*
        
   from Codes c 
    inner join  EditionCodes ec ON(c.CodeId = ec.CodeId) 
    inner join Editions e ON(ec.EditionId = e.EditionId)
    inner join Books b ON(e.BookId = b.BookId)
   where c.codeString = @codeString 
    

END




