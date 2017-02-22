IF OBJECT_ID('dbo.roc_spGetEdition') IS NOT NULL
    DROP PROCEDURE dbo.roc_spGetEdition

go

/* 
	Author:			Daniel Campos. / Elizabeth Lazo.				 
	Object:			dbo.roc_spGetEdition
	
	Description:	Retrieves all editions.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetEdition 


 */ 

CREATE PROCEDURE dbo.roc_spGetEdition

AS
BEGIN

 SELECT * 
    FROM Editions 
    WHERE NumberEdition=(SELECT MAX(NumberEdition) FROM Editions) AND 
          Active='1'


 END

go
