IF OBJECT_ID('dbo.plm_spGetClients') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetClients
go

/* 
	Author:			Juan Ramirez / Ramiro Sánchez
	Object:			dbo.plm_spGetClients
	
	Description:	Retrieve Client by Name.

	Company:		PLM Latina.

	EXECUTE dbo.plm_spGetClients

 */ 

CREATE PROCEDURE dbo.plm_spGetClients
(
	@completeName		VARCHAR(100)
)
AS
BEGIN
	
		DECLARE
			@clients      INT,
			@clientId	  INT
			

		IF(@completeName LIKE '%Ñ%')
		BEGIN

			SELECT @clients = COUNT(*)
			  FROM [dbo].[Clients]
				   
			 WHERE [Active] = 1 AND 
				   [CompleteName] = @completeName 
					

			IF(@clients > 1)
			BEGIN
				
				SELECT TOP 1 @clientid = ClientId
				FROM [dbo].[Clients]
				WHERE [Active] = 1 AND 
				   [CompleteName] = @completeName 
				ORDER BY ClientId DESC
				
			
				--Inactive all users:
				UPDATE [dbo].[Clients]
				SET [Active] = 0
				WHERE [Active] = 1 AND 
				   [CompleteName] = @completeName AND
				   [ClientId] <> @clientId

				--RetriveUser		
				SELECT [ClientId]
					  ,[FirstName]
					  ,[LastName]
					  ,[SecondLastName]
					  ,[CompleteName]
					  ,[Gender]
					  ,[Birthday]
					  ,[Email]
					  ,[Password]
					  ,[AddedDate]
					  ,[LastUpdate]
					  ,[CountryId]
					  ,[StateId]
					  ,[Age]
					  ,[ZipCode]
					  ,[Active]
				  FROM [dbo].[Clients]
				  WHERE [Active] = 1 AND 
				        [CompleteName] = @completeName 
				
				RETURN @@ROWCOUNT

			END
			ELSE
			BEGIN
				SELECT [ClientId]
					  ,[FirstName]
					  ,[LastName]
					  ,[SecondLastName]
					  ,[CompleteName]
					  ,[Gender]
					  ,[Birthday]
					  ,[Email]
					  ,[Password]
					  ,[AddedDate]
					  ,[LastUpdate]
					  ,[CountryId]
					  ,[StateId]
					  ,[Age]
					  ,[ZipCode]
					  ,[Active]
				  FROM [dbo].[Clients]
				  WHERE [Active] = 1 AND 
				        [CompleteName] = @completeName 
				
				RETURN @@ROWCOUNT
			END

		END
		ELSE
		BEGIN
			SELECT @clients = COUNT(*)
			  FROM [dbo].[Clients]
				   
			 WHERE [Active] = 1 AND 
				   [CompleteName] COLLATE SQL_Latin1_General_CP1_CI_AI LIKE @completeName 
					

			IF(@clients > 1)
			BEGIN
				
				SELECT TOP 1 @clientid = ClientId
				FROM [dbo].[Clients]
				WHERE [Active] = 1 AND 
				   [CompleteName] COLLATE SQL_Latin1_General_CP1_CI_AI LIKE @completeName 
				ORDER BY ClientId DESC
				
			
				--Inactive all users:
				UPDATE [dbo].[Clients]
				SET [Active] = 0
				WHERE [Active] = 1 AND 
				   [CompleteName] COLLATE SQL_Latin1_General_CP1_CI_AI LIKE @completeName AND
				   [ClientId] <> @clientId

				--RetriveUser		
				SELECT [ClientId]
					  ,[FirstName]
					  ,[LastName]
					  ,[SecondLastName]
					  ,[CompleteName]
					  ,[Gender]
					  ,[Birthday]
					  ,[Email]
					  ,[Password]
					  ,[AddedDate]
					  ,[LastUpdate]
					  ,[CountryId]
					  ,[StateId]
					  ,[Age]
					  ,[ZipCode]
					  ,[Active]
				  FROM [dbo].[Clients]
				  WHERE [Active] = 1 AND 
				        [CompleteName] COLLATE SQL_Latin1_General_CP1_CI_AI LIKE @completeName 
				
				RETURN @@ROWCOUNT

			END
			ELSE
			BEGIN
				SELECT [ClientId]
					  ,[FirstName]
					  ,[LastName]
					  ,[SecondLastName]
					  ,[CompleteName]
					  ,[Gender]
					  ,[Birthday]
					  ,[Email]
					  ,[Password]
					  ,[AddedDate]
					  ,[LastUpdate]
					  ,[CountryId]
					  ,[StateId]
					  ,[Age]
					  ,[ZipCode]
					  ,[Active]
				  FROM [dbo].[Clients]
				  WHERE [Active] = 1 AND 
				        [CompleteName] COLLATE SQL_Latin1_General_CP1_CI_AI LIKE @completeName 
				
				RETURN @@ROWCOUNT
			END		


		END
END
go