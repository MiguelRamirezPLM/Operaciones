using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using PLMUserBusinessEntries;

namespace PLMUsersDataAccessComponent
{
    public sealed class UserProjectsDALC : PLMUsersDataAccesAdapter<UserProjectInfo>
    {
        #region Constructors

        private UserProjectsDALC() { }

        #endregion

        #region Public Methods

        //public override UserProjectInfo getOne(int pk)
        //{
        //    DbCommand dbCmd = UserProjectsDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDProjects");

        //    //Add the parameters:
        //    UserProjectsDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Int32,
        //        ParameterDirection.Input, string.Empty, DataRowVersion.Current, UserProjectsDALC.CRUD.Read);
        //    UserProjectsDALC.InstanceDatabase.AddParameter(dbCmd, "@projectId", DbType.Int32,
        //        ParameterDirection.Input, string.Empty, DataRowVersion.Current, pk);

        //    using (IDataReader dataReader = UserProjectsDALC.InstanceDatabase.ExecuteReader(dbCmd))
        //    {
        //        if (dataReader.Read())
        //            return this.getFromDataReader(dataReader);
        //        else
        //            return null;
        //    }
        //}


        public override int insert(UserProjectInfo businessEntity)
        {
            DbCommand dbCmd = UserProjectsDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDUserProjects");

            // Add the parameters:
            UserProjectsDALC.InstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            UserProjectsDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Create);
            UserProjectsDALC.InstanceDatabase.AddParameter(dbCmd, "@userId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.UserId);
            UserProjectsDALC.InstanceDatabase.AddParameter(dbCmd, "@projectId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ProjectId);
            UserProjectsDALC.InstanceDatabase.AddParameter(dbCmd, "@roleId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.RoleId);

            //Insert record:
            UserProjectsDALC.InstanceDatabase.ExecuteNonQuery(dbCmd);

            businessEntity.UserId = Convert.ToInt32(dbCmd.Parameters["@Return"].Value);

            return Convert.ToInt32(dbCmd.Parameters["@Return"].Value);
        }

        public List<UserProjectInfo> getUserProjects()
        {
            DbCommand dbCmd = UserProjectsDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetUserProjects");

            List<UserProjectInfo> BECollection = new List<UserProjectInfo>();

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = UserProjectsDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                // Iterates through records:
                while (dataReader.Read())
                {
                    BECollection.Add(this.getFromDataReader(dataReader));
                }
            }
            return BECollection;
        }   
        
        #endregion

        #region Protected

        protected override UserProjectInfo getFromDataReader(IDataReader current)
        {
            UserProjectInfo record = new UserProjectInfo();

            record.UserId = Convert.ToInt32(current["UserId"]);
            record.ProjectId = Convert.ToInt32(current["ProjectId"]);
            record.RoleId = Convert.ToInt32(current["RoleId"]);

            return record;
        }

        #endregion

        public static readonly UserProjectsDALC Instance = new UserProjectsDALC();
    }
}
