IF OBJECT_ID('dbo.plm_spCRUDAttributeGroup') IS NOT NULL
	DROP PROCEDURE dbo.plm_spCRUDAttributeGroup
go

/* 
	Author:			Ramiro Sánchez				 
	Object:			dbo.plm_spCRUDAttributeGroup
	
	Description:	Perform the basic operations, or in other words CRUD operations.
					Where CRUD stands for:

						C - Create	{0}
						R - Read	{1}
						U - Update	{2}
						D - Delete	{3}

	Company:		PLM Latina

	EXECUTE dbo.plm_spCRUDAttributeGroup @CRUDType = 1, @attributeGroupId = 1

 */ 

CREATE PROCEDURE dbo.plm_spCRUDAttributeGroup
(
	@CRUDType				tinyint,
	@attributeGroupId		int,
	@attributeGroupName		varchar(200) = Null,
	@attributeGroupOrder	int = Null,
	@active					bit = Null
)
AS
BEGIN

	-- If @CRUDType belongs to {0,2,3}:
	IF (@CRUDType IN (0,2,3))
	BEGIN	
		RAISERROR ('The current operation cannot be performed', 16, 1)
		RETURN -1
	END

	-- R = {(Read, 1)}:
	IF (@CRUDType IN (1))
	BEGIN

		SELECT [AttributeGroupId]
			  ,[AttributeGroupName]
			  ,[AttributeGroupOrder]
			  ,[Active]
		  FROM [dbo].[AttributeGroup]
		WHERE	AttributeGroupId = @attributeGroupId

		RETURN @@ROWCOUNT
		
	END
END
go