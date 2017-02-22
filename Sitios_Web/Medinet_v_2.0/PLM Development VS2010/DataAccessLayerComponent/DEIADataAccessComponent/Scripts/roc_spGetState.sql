IF OBJECT_ID('dbo.dbo.roc_spGetState') IS NOT NULL
	DROP PROCEDURE dbo.roc_spGetState
go

/* 
	Author:			Daniel Campos. / Elizabeth Lazo.				 
	Object:			dbo.roc_spGetState
	
	Description:	Retrieves all state by Id.

	Company:		PLM Latina.

	EXECUTE dbo.roc_spGetState @stateId = 5


 */ 

CREATE PROCEDURE dbo.roc_spGetState
(
   @stateId int
)

AS
 BEGIN
     --The parameters shouldn't be null:
       IF (@stateId IS NULL) 
  BEGIN

    RAISERROR ('The current operation requires more parameters', 16, 1)
    RETURN -1

  END 

SELECT * FROM States
WHERE States.Active = '1' AND States.StateId = @stateId


  END

 go



