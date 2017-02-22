IF OBJECT_ID('dbo.plm_spGetClientByEmailByPasswordByPrefix') IS NOT NULL
DROP PROCEDURE dbo.plm_spGetClientByEmailByPasswordByPrefix
GO
/****** Object:  StoredProcedure [dbo].[plm_spGetClientByEmailByPasswordByPrefix]    Script Date: 08/21/2013 09:09:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

/*
Author:			Ulises Orihuela
Object:			dbo.plm_spGetClientByEmailByPasswordByPrefix

Description:	Get code Information by prefixname.

Company:		PLM Latina.


EXECUTE dbo.[plm_spGetClientByEmailByPasswordByPrefix] @email = 'ulises.orihuela@plmlatina.com', @password = 'ha/f9EF7q4TvLObHrMkB1Q==' , @prefix = 'MEDPLMCOM'

*/

create PROCEDURE [dbo].[plm_spGetClientByEmailByPasswordByPrefix]
(
@email			varchar(60),
@password		varchar(100),
@prefix varchar(100)

)
AS
BEGIN

declare @parent varchar (100) = null
SELECT @parent = prefixId
FROM CodePrefixes
WHERE PrefixName = @prefix

IF (@parent is not NULL)
begin
SELECT top 1 c.[ClientId]
,c.[Email]
,cd.[CodeId]
,cd.[CodeString],
cp.PrefixId,cp.PrefixName
FROM [dbo].[Clients] c
Inner Join ClientCodes cc On c.ClientId = cc.ClientId
Inner Join Codes cd On cc.CodeId = cd.CodeId
Inner Join CodePrefixes cp On cp.PrefixId = cd.PrefixId
WHERE	c.[Email] = @email
And c.[Password] =@password
And cp.ParentId  = @parent
And c.Active = 1
ORDER BY CD.CodeId DESC

RETURN @@ROWCOUNT
END
END
