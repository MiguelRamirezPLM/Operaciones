IF OBJECT_ID('dbo.plm_spCRUDComments') IS NOT NULL
	DROP PROCEDURE dbo.plm_spCRUDComments
go

/* 
	Author:			Ramiro Sánchez				 
	Object:			dbo.plm_spCRUDComments
	
	Description:	Perform the basic operations, or in other words CRUD operations.
					Where CRUD stands for:

						C - Create	{0}
						R - Read	{1}
						U - Update	{2}
						D - Delete	{3}

	Company:		PLM Latina.
		
 */ 

CREATE PROCEDURE dbo.plm_spCRUDComments
(
	@CRUDType				TINYINT,
	@commentId				INT,
	@commentTypeId			TINYINT = NULL,
	@branchId				TINYINT = NULL,
	@businessUnitId			TINYINT = NULL,
	@distributionId			INT = NULL,
	@prefixId				INT = NULL,
	@targetId				TINYINT = NULL,
	@content				VARCHAR(MAX) = NULL,
	@commentDate			DATETIME = NULL,
	@sentDate				DATETIME = NULL
)
AS
BEGIN

	-- If @CRUDType belongs to {0,2}, then the parameters shouldn't be null:
	IF (@CRUDType IN (0) AND (@commentTypeId IS NULL OR 
			@branchId IS NULL OR @businessUnitId IS NULL OR @content IS NULL))
	BEGIN
		RAISERROR ('The current operation requires more parameters', 16, 1)
		RETURN -1
	END

	-- C = {(CREATE, 0)}:
	IF (@CRUDType IN (0))
	BEGIN
	INSERT INTO [dbo].[Comments]
           ([CommentTypeId]
           ,[BranchId]
		   ,[BusinessUnitId]
		   ,[DistributionId]
		   ,[PrefixId]
		   ,[TargetId]
           ,[Content]
           ,[CommentDate]
           ,[SentDate])
     VALUES
           (@commentTypeId
           ,@branchId
		   ,@businessUnitId
		   ,@distributionId
		   ,@prefixId
		   ,@targetId
           ,@content
           ,GETDATE()
           ,@sentDate)

	RETURN SCOPE_IDENTITY()

	END
	-- R = {(Read, 1)}
	IF (@CRUDType IN (1))
	BEGIN

		SELECT [CommentId]
			,[CommentTypeId]
			,[BranchId]
			,[BusinessUnitId]
			,[DistributionId]
			,[PrefixId]
			,[TargetId]
			,[Content]
			,[CommentDate]
			,[SentDate]
		FROM [dbo].[Comments]
		WHERE [CommentId] = @commentId

		RETURN @@ROWCOUNT
	END
	
	-- U = {(Update, 2)}
	IF (@CRUDType IN (2))
	BEGIN

		UPDATE [dbo].[Comments]
		SET [SentDate] = @sentDate
		WHERE [CommentId] = @commentId

		RETURN 0

	END
END
go
