IF OBJECT_ID('dbo.plm_spGetSpecialitiesByGuies') IS NOT NULL
DROP PROCEDURE dbo.plm_spGetSpecialitiesByGuies
GO
/****** Object:  StoredProcedure [dbo].[plm_spGetSpecialities]    Script Date: 08/26/2013 09:48:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

/*
Author:			Ulises Orihuela.
Object:			dbo.plm_spGetSpecialities[plm_spGetSpecialitiesByGuies]

Description:	Retrieves all Specialities stored in the database.

Company:		PLM Latina.

EXECUTE dbo.[plm_spGetSpecialitiesByGuies]

*/

CREATE PROCEDURE [dbo].[plm_spGetSpecialitiesByGuies]
(
	 @prefix			varchar(15) = null
	) 
AS
BEGIN


SELECT DISTINCT S.[SpecialityId]
,S.[SpecialityName]
,S.[ShortName]
,S.[Active]
,cp.PrefixId,
cp.PrefixName
FROM [dbo].[Specialities] S
INNER JOIN SpecialityTools ST ON S.SpecialityId = ST.SpecialityId
INNER JOIN ElectronicInformation E ON ST.ElectronicId = E.ElectronicId
INNER JOIN CountryTools CT ON E.ElectronicId = CT.ElectronicId
INNER JOIN DistributionTools DT ON CT.ElectronicId = DT.ElectronicId
											and CT.CountryId = DT.CountryId
INNER JOIN CodePrefixes CP ON DT.PrefixId = CP.PrefixId
where CP.PrefixName = @prefix

RETURN @@ROWCOUNT


END
