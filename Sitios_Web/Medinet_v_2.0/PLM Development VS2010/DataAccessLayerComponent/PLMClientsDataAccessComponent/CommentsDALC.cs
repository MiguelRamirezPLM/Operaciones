using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using PLMClientsBusinessEntities;

namespace PLMClientsDataAccessComponent
{
    public class CommentsDALC : PLMClientsDataAccessAdapter<CommentsInfo>
    {

        #region Constructors

        private CommentsDALC() { }

        #endregion

        #region Public Methods

        public override int insert(PLMClientsBusinessEntities.CommentsInfo BEntity)
        {
            DbCommand dbCmd = CommentsDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDComments");

            // Add the parameters:
            CommentsDALC.InstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            CommentsDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Create);
            CommentsDALC.InstanceDatabase.AddParameter(dbCmd, "@commentId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, -1);
            CommentsDALC.InstanceDatabase.AddParameter(dbCmd, "@commentTypeId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, BEntity.CommentTypeId);
            CommentsDALC.InstanceDatabase.AddParameter(dbCmd, "@branchId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, BEntity.BranchId);
            CommentsDALC.InstanceDatabase.AddParameter(dbCmd, "@businessUnitId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, BEntity.BusinessUnitId);
            CommentsDALC.InstanceDatabase.AddParameter(dbCmd, "@distributionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, BEntity.DistributionId);
            CommentsDALC.InstanceDatabase.AddParameter(dbCmd, "@prefixId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, BEntity.PrefixId);
            CommentsDALC.InstanceDatabase.AddParameter(dbCmd, "@targetId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, BEntity.TargetId);

            if (BEntity.Content != null)
                CommentsDALC.InstanceDatabase.AddParameter(dbCmd, "@content", DbType.AnsiString,
                    ParameterDirection.Input, string.Empty, DataRowVersion.Current, BEntity.Content);

            CommentsDALC.InstanceDatabase.ExecuteNonQuery(dbCmd);

            return Convert.ToInt32(dbCmd.Parameters["@Return"].Value);
        }

        public override void update(CommentsInfo BEntity)
        {
            DbCommand dbCmd = CommentsDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDComments");

            // Add the parameters:
            CommentsDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Update);
            CommentsDALC.InstanceDatabase.AddParameter(dbCmd, "@commentId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, BEntity.CommentId);
            if (BEntity.SentDate != null)
                CommentsDALC.InstanceDatabase.AddParameter(dbCmd, "@sentDate", DbType.DateTime,
                    ParameterDirection.Input, string.Empty, DataRowVersion.Current, BEntity.SentDate);

            CommentsDALC.InstanceDatabase.ExecuteNonQuery(dbCmd);
        }

        public override CommentsInfo getOne(int commentId)
        {
            DbCommand dbCmd = CommentsDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDComments");

            // Add the parameters:
            CommentsDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Read);
            CommentsDALC.InstanceDatabase.AddParameter(dbCmd, "@commentId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, commentId);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = CommentsDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);

                else
                    return null;
            }
        }

        public List<PLMClientsBusinessEntities.CommentTypeDetailInfo> getCommentTypesByPrefix(byte targetId, string prefix)
        {
            List<PLMClientsBusinessEntities.CommentTypeDetailInfo> BECollection = new List<CommentTypeDetailInfo>();

            DbCommand dbCmd = CommentsDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetCommentTypesByPrefix");

            // Add the parameters:
            CommentsDALC.InstanceDatabase.AddParameter(dbCmd, "@targetId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, targetId);
            CommentsDALC.InstanceDatabase.AddParameter(dbCmd, "@prefix", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, prefix);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = CommentsDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                {
                    PLMClientsBusinessEntities.CommentTypeDetailInfo record = new CommentTypeDetailInfo();
                    record.BranchId = Convert.ToByte(dataReader["BranchId"]);
                    record.BranchName = dataReader["BranchName"].ToString();
                    record.BusinessUnitId = Convert.ToByte(dataReader["BusinessUnitId"]);
                    record.BUnitName = dataReader["BUnitName"].ToString();
                    record.CommentTypeId = Convert.ToByte(dataReader["CommentTypeId"]);
                    record.TypeName = dataReader["TypeName"].ToString();
                    record.TypeDescription = dataReader["TypeDescription"].ToString();
                    record.DistributionId = Convert.ToInt32(dataReader["DistributionId"]);
                    record.DistributionName = dataReader["DistributionName"].ToString();
                    record.PrefixId = Convert.ToInt32(dataReader["PrefixId"]);
                    record.TargetId = Convert.ToByte(dataReader["TargetId"]);
                    record.TargetName = dataReader["TargetName"].ToString();
                    record.Email = dataReader["Email"].ToString();

                    BECollection.Add(record);
                }
            }
            return BECollection;
        }

        public List<PLMClientsBusinessEntities.CommentTypeDetailInfo> getCommentTypesByPrefixByBranch(byte targetId, string prefix,int branchId)
        {
            List<PLMClientsBusinessEntities.CommentTypeDetailInfo> BECollection = new List<CommentTypeDetailInfo>();

            DbCommand dbCmd = CommentsDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetCommentTypesByPrefixByBranch");

            // Add the parameters:
            CommentsDALC.InstanceDatabase.AddParameter(dbCmd, "@branchId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, branchId);
            CommentsDALC.InstanceDatabase.AddParameter(dbCmd, "@targetId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, targetId);
            CommentsDALC.InstanceDatabase.AddParameter(dbCmd, "@prefix", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, prefix);
            


            // Get the result set from the stored procedure:
            using (IDataReader dataReader = CommentsDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                {
                    PLMClientsBusinessEntities.CommentTypeDetailInfo record = new CommentTypeDetailInfo();
                    record.BranchId = Convert.ToByte(dataReader["BranchId"]);
                    record.BranchName = dataReader["BranchName"].ToString();
                    record.BusinessUnitId = Convert.ToByte(dataReader["BusinessUnitId"]);
                    record.BUnitName = dataReader["BUnitName"].ToString();
                    record.CommentTypeId = Convert.ToByte(dataReader["CommentTypeId"]);
                    record.TypeName = dataReader["TypeName"].ToString();
                    record.TypeDescription = dataReader["TypeDescription"].ToString();
                    record.DistributionId = Convert.ToInt32(dataReader["DistributionId"]);
                    record.DistributionName = dataReader["DistributionName"].ToString();
                    record.PrefixId = Convert.ToInt32(dataReader["PrefixId"]);
                    record.TargetId = Convert.ToByte(dataReader["TargetId"]);
                    record.TargetName = dataReader["TargetName"].ToString();
                    record.Email = dataReader["Email"].ToString();

                    BECollection.Add(record);
                }
            }
            return BECollection;
        }

        public PLMClientsBusinessEntities.CommentTypeDetailInfo getCommentType(byte commentTypeId, byte branchId, byte businessUnitId, int distributionId, int prefixId, byte targetId)
        {
            PLMClientsBusinessEntities.CommentTypeDetailInfo record = new CommentTypeDetailInfo();

            DbCommand dbCmd = CommentsDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetCommentTypesByPrefix");

            // Add the parameters:
            CommentsDALC.InstanceDatabase.AddParameter(dbCmd, "@commentTypeId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, commentTypeId);
            CommentsDALC.InstanceDatabase.AddParameter(dbCmd, "@branchId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, branchId);
            CommentsDALC.InstanceDatabase.AddParameter(dbCmd, "@businessUnitId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessUnitId);
            CommentsDALC.InstanceDatabase.AddParameter(dbCmd, "@distributionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, distributionId);
            CommentsDALC.InstanceDatabase.AddParameter(dbCmd, "@prefixId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, prefixId);
            CommentsDALC.InstanceDatabase.AddParameter(dbCmd, "@targetId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, targetId);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = CommentsDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                {
                    record.BranchId = Convert.ToByte(dataReader["BranchId"]);
                    record.BranchName = dataReader["BranchName"].ToString();
                    record.BusinessUnitId = Convert.ToByte(dataReader["BusinessUnitId"]);
                    record.BUnitName = dataReader["BUnitName"].ToString();
                    record.CommentTypeId = Convert.ToByte(dataReader["CommentTypeId"]);
                    record.TypeName = dataReader["TypeName"].ToString();
                    record.TypeDescription = dataReader["TypeDescription"].ToString();
                    record.DistributionId = Convert.ToInt32(dataReader["DistributionId"]);
                    record.DistributionName = dataReader["DistributionName"].ToString();
                    record.PrefixId = Convert.ToInt32(dataReader["PrefixId"]);
                    record.TargetId = Convert.ToByte(dataReader["TargetId"]);
                    record.TargetName = dataReader["TargetName"].ToString();
                    record.Email = dataReader["Email"].ToString();

                    return record;
                }
                else
                    return null;
            }
        }

        #endregion

        #region Protected Methods

        protected override CommentsInfo getFromDataReader(IDataReader current)
        {
            CommentsInfo record = new CommentsInfo();

            record.CommentId = Convert.ToInt32(current["CommentId"]);
            record.CommentTypeId = Convert.ToByte(current["CommentTypeId"]);
            record.BranchId = Convert.ToByte(current["BranchId"]);
            record.BusinessUnitId = Convert.ToByte(current["BusinessUnitId"]);
            record.DistributionId = Convert.ToInt32(current["DistributionId"]);
            record.PrefixId = Convert.ToInt32(current["PrefixId"]);
            record.TargetId = Convert.ToByte(current["TargetId"]);

            record.Content = current["Content"].ToString();

            if (current["CommentDate"] != System.DBNull.Value)
                record.CommentDate = Convert.ToDateTime(current["CommentDate"]);

            if (current["SentDate"] != System.DBNull.Value)
                record.SentDate = Convert.ToDateTime(current["SentDate"]);

            return record;
        }

        #endregion

        public static readonly CommentsDALC Instance = new CommentsDALC();

    }
}
