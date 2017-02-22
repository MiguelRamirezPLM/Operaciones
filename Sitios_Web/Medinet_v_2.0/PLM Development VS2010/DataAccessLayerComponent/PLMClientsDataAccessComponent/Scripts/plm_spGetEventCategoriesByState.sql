IF OBJECT_ID('dbo.plm_spGetEventCategoriesByState') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetEventCategoriesByState
go

/* 
	Author:			Ramiro Sánchez			 
	Object:			dbo.plm_spGetEventCategoriesByState
	
	Description:	

	Company:		PLM Latina.

	EXECUTE dbo.plm_spGetEventCategoriesByState @prefix = '', @targetId = 3, @stateId = 7

 */ 

CREATE PROCEDURE dbo.plm_spGetEventCategoriesByState
(
	@prefix			VARCHAR(50) = null,
	@targetId		TINYINT = null,
	@stateId		INT = null
)
AS
BEGIN
	
	IF(@prefix Is Not Null And @targetId Is Not Null And @stateId Is Not Null)
	BEGIN
	
		SELECT Distinct	ec.CategoryId,
				ec.CategoryName,
				ec.Active
		FROM EventCategories ec
			Inner Join [Events] e on ec.CategoryId = e.CategoryId
			Inner Join DistributionEvents de on e.EventId = de.EventId
			Inner Join EventAddresses ea on e.EventId = ea.EventId
			Inner Join Addresses a on ea.AddressId = a.AddressId
			Inner Join CodePrefixes cp on de.PrefixId = cp.PrefixId
		WHERE ec.Active = 1 AND 
			  e.Active = 1 AND
			  cp.PrefixName = @prefix AND
			  de.TargetId = @targetId AND
			  a.StateId = @stateId AND
			  FinalDate >= GETDATE()
		Order By ec.CategoryName
	
		RETURN @@ROWCOUNT
	
	END
	
END
go