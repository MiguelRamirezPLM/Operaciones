IF OBJECT_ID('dbo.plm_spGetCommentTypesByPrefix') IS NOT NULL
	DROP PROCEDURE dbo.plm_spGetCommentTypesByPrefix
go

/* 
	Author:			Ramiro Sánchez				 
	Object:			dbo.plm_spGetCommentTypesByPrefix
	
	Description:	Get Comment types By Prefix and Target.

	Company:		PLM Latina.
	
	EXECUTE dbo.plm_spGetCommentTypesByPrefix @targetId = 3, @prefix = 'imCardio_mex'
	EXECUTE dbo.plm_spGetCommentTypesByPrefix @commentTypeId = 4, @branchId = 1, @businessUnitId = 1,
		@distributionId = 14, @targetId = 3, @prefixId = 282

 */ 

CREATE PROCEDURE dbo.plm_spGetCommentTypesByPrefix
(
	@commentTypeId		tinyint = Null,
	@branchId			tinyint = Null,
	@businessUnitId		tinyint = Null,
	@distributionId		int = Null,
	@targetId			tinyint = Null,
	@prefixId			int = Null,
	@prefix				varchar(20) = Null
)
AS
BEGIN

	-- The parameters shouldn't be null:
	IF (@targetId IS NOT NULL AND @prefix IS NOT NULL)
	BEGIN

		Select p.BranchId, 
		p.BranchName,
		bu.BusinessUnitId,
		bu.BunitName,
		ct.CommentTypeId, 
		ct.TypeName,
		ct.TypeDescription,
		dc.DistributionId,
		d.DistributionName,
		dc.PrefixId,
		dc.TargetId,
		tao.TargetName,
		dc.Email
		From CommentTypes ct
		Inner Join CommentTypeBranches ctb On ct.CommentTypeId = ctb.CommentTypeId
		Inner Join PLMBranches p On ctb.BranchId = p.BranchId
		Inner Join BusinessUnits bu On ctb.BusinessUnitId = bu.BusinessUnitId
		Inner Join DistributionComments dc On ctb.BranchId = dc.BranchId
			And ctb.BusinessUnitId = dc.BusinessUnitId
			And ctb.CommentTypeId = dc.CommentTypeId
		Inner Join CodePrefixes cp On dc.PrefixId = cp.PrefixId
		Inner Join Distributions d On dc.DistributionId = d.DistributionId
		Inner Join TargetOutputs tao On dc.TargetId = tao.TargetId
		Where dc.TargetId = @targetId
		And cp.PrefixName = @prefix
		And ctb.Active = 1

	RETURN @@ROWCOUNT
		
	END

	-- The parameters shouldn't be null:
	IF (@commentTypeId IS NOT NULL
			AND @branchId IS NOT NULL
			AND @businessUnitId IS NOT NULL
			AND @distributionId IS NOT NULL
			AND @targetId IS NOT NULL
			AND @prefixId IS NOT NULL)
	BEGIN

		Select p.BranchId, 
		p.BranchName,
		bu.BusinessUnitId,
		bu.BunitName,
		ct.CommentTypeId, 
		ct.TypeName,
		ct.TypeDescription,
		dc.DistributionId,
		d.DistributionName,
		dc.PrefixId,
		dc.TargetId,
		tao.TargetName,
		dc.Email
		From CommentTypes ct
		Inner Join CommentTypeBranches ctb On ct.CommentTypeId = ctb.CommentTypeId
		Inner Join PLMBranches p On ctb.BranchId = p.BranchId
		Inner Join BusinessUnits bu On ctb.BusinessUnitId = bu.BusinessUnitId
		Inner Join DistributionComments dc On ctb.BranchId = dc.BranchId
			And ctb.BusinessUnitId = dc.BusinessUnitId
			And ctb.CommentTypeId = dc.CommentTypeId
		Inner Join CodePrefixes cp On dc.PrefixId = cp.PrefixId
		Inner Join Distributions d On dc.DistributionId = d.DistributionId
		Inner Join TargetOutputs tao On dc.TargetId = tao.TargetId
		Where dc.CommentTypeId = @commentTypeId
			And dc.BranchId = @branchId
			And dc.BusinessUnitId = @businessUnitId
			And dc.DistributionId = @distributionId
			And dc.PrefixId = @prefixId
			And dc.TargetId = @targetId
			And ctb.Active = 1

	RETURN @@ROWCOUNT
		
	END
	
	
END
go