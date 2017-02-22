
IF OBJECT_ID('dbo.plm_spGetSpecialityByName') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetSpecialityByName
go

-- =============================================
-- Author:		<JADJ>
-- Create date: <17/03/2011>
-- Description:	<Find Speciality by Name>

-- Modified by Erick Morales Silva.

-- =============================================

CREATE PROCEDURE [dbo].[plm_spGetSpecialityByName]
(
	@specialityName VARCHAR(100)
)
AS
BEGIN

	IF (@specialityName IS NULL)
	BEGIN
		RAISERROR ('@specialityName variable could not be null', 16, 1)
		RETURN -1
	END

	SELECT 
		[SpecialityId]
      ,[SpecialityName]
      ,[ShortName]
      ,[Active]
	
	FROM [dbo].[Specialities]
	WHERE [SpecialityName] COLLATE SQL_Latin1_General_CP1_CI_AI = @specialityName
	 
	RETURN @@ROWCOUNT
			
END
