
IF OBJECT_ID('dbo.plm_spGetClientByCode') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetClientByCode
go

/*

	Author:		Erick Morales Silva / Ramiro Sánchez
	Date:		August 23, 2011.
	Company:	PLM Latina.
	
	EXECUTE dbo.plm_spGetClientByCode @CodeString = '7dKBOFUYfPvKpi0FyUgdV1Ys0q4='

*/

CREATE PROCEDURE dbo.plm_spGetClientByCode
(
	@CodeString varchar(50)
)
AS
BEGIN

	SELECT 

		c.ClientId,
		c.FirstName,
		c.LastName,
		c.SecondLastName,
		c.CompleteName,
		c.Gender,
		c.Email,
		c.Password,
		c.Birthday,
		c.AddedDate,
		c.LastUpdate,
		c.CountryId,
		c.StateId,
		c.Age,
		c.ZipCode,
		c.Active
		
	FROM Clients c

	INNER JOIN ClientCodes cc ON (c.ClientId = cc.ClientId)
	INNER JOIN Codes cod ON (cc.CodeId = cod.CodeId)

	WHERE cod.CodeString = @CodeString

	RETURN @@ROWCOUNT

END
go