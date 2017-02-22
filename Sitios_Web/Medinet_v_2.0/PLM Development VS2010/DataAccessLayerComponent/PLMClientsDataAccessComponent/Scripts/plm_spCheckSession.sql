IF OBJECT_ID('dbo.plm_spCheckSession') IS NOT NULL
	DROP PROCEDURE dbo.plm_spCheckSession
go

/* 
	Author:			Ramiro Sánchez
	Object:			dbo.plm_spCheckSession
	
	Description:	Verify if the User session is active.

	Company:		PLM Latina.

	
 */ 

CREATE PROCEDURE dbo.plm_spCheckSession
(
	@codeString		varchar(60) = Null,
	@sessionTime	int = Null
)
AS
BEGIN

	DECLARE 
		@connectionDate		Datetime,
		@sessionId			tinyint

	SET @connectionDate = Null

	-- The parameters shouldn't be null:
	IF (@codeString IS NULL OR @sessionTime IS NULL)
	BEGIN
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
	END

	SELECT TOP 1 @connectionDate = cuc.DateConnection,
		@sessionId = cuc.SessionId
	FROM CompanyUserConnections cuc
		Inner Join Codes c ON cuc.CodeId = c.CodeId
	WHERE c.CodeString = @codeString
	ORDER BY cuc.DateConnection DESC

	IF(@connectionDate Is Null)
	BEGIN
		RETURN 1
	END
	ELSE
	BEGIN
		IF(@sessionId IN(2))
		BEGIN
			RETURN 1
		END
		ELSE
		BEGIN
			IF(@sessionTime <= DATEDIFF(minute, @connectionDate, GETDATE()))
			BEGIN
				RETURN 1
			END
			ELSE
			BEGIN
				RETURN 0
			END
		END
	END
END
go