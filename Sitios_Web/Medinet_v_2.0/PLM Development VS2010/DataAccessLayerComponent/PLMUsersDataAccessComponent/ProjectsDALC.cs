using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using PLMUserBusinessEntries;

namespace PLMUsersDataAccessComponent
{
    public sealed class ProjectsDALC : PLMUsersDataAccesAdapter<ProjectInfo>
    {
        #region Constructors

        private ProjectsDALC() { }

        #endregion

        #region Public Methods

        public List<ProjectInfo> getProjects()
        {
            DbCommand dbCmd = ProjectsDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetProjects");

            List<ProjectInfo> BECollection = new List<ProjectInfo>();

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = ProjectsDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                // Iterates through records:
                while (dataReader.Read())
                {
                    BECollection.Add(this.getFromDataReader(dataReader));
                }
            }
            return BECollection;
        }

        public List<ProjectInfo> getProjectsByUser(int userId)
        {
            DbCommand dbCmd = ProjectsDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetProjectsByUser");

            ProjectsDALC.InstanceDatabase.AddParameter(dbCmd, "@userId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, userId);

            List<ProjectInfo> BECollection = new List<ProjectInfo>();

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = ProjectsDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                // Iterates through records:
                while (dataReader.Read())
                {
                    BECollection.Add(this.getFromDataReader(dataReader));
                }
            }
            return BECollection;
        }

        public override int insert(ProjectInfo businessEntity)
        {
            DbCommand dbCmd = ProjectsDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDProjects");

            // Add the parameters:
            ProjectsDALC.InstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            ProjectsDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Create);
            ProjectsDALC.InstanceDatabase.AddParameter(dbCmd, "@projectId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, -1);
            ProjectsDALC.InstanceDatabase.AddParameter(dbCmd, "@projectName", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ProjectName);
            ProjectsDALC.InstanceDatabase.AddParameter(dbCmd, "@projectDescription", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ProjectDescription);
            ProjectsDALC.InstanceDatabase.AddParameter(dbCmd, "@initialDate", DbType.DateTime,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.InitialDate);
            ProjectsDALC.InstanceDatabase.AddParameter(dbCmd, "@finalDate", DbType.DateTime,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.FinalDate);
            ProjectsDALC.InstanceDatabase.AddParameter(dbCmd, "@active", DbType.Boolean,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Active);
            ProjectsDALC.InstanceDatabase.AddParameter(dbCmd, "@investedTime", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.InvestedTime);

            //Insert record:
            ProjectsDALC.InstanceDatabase.ExecuteNonQuery(dbCmd);

            businessEntity.ProjectId = Convert.ToInt32(dbCmd.Parameters["@Return"].Value);

            return Convert.ToInt32(dbCmd.Parameters["@Return"].Value);
        }

        public override ProjectInfo getOne(int pk)
        {
            DbCommand dbCmd = ProjectsDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDProjects");

            //Add the parameters:
            UsersDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, UsersDALC.CRUD.Read);
            UsersDALC.InstanceDatabase.AddParameter(dbCmd, "@projectId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pk);

            using (IDataReader dataReader = UsersDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);
                else
                    return null;
            }
        }

        #endregion

        #region Protected

        protected override ProjectInfo getFromDataReader(IDataReader current)
        {
            ProjectInfo record = new ProjectInfo();

            record.ProjectId = Convert.ToInt32(current["ProjectId"]);
            record.ProjectName = current["ProjectName"].ToString();
            record.ProjectDescription = current["ProjectDescription"].ToString();
            record.InitialDate = Convert.ToDateTime(current["InitialDate"]);
            record.FinalDate = Convert.ToDateTime(current["FinalDate"]);
            record.Active = Convert.ToBoolean(current["Active"]);
            record.InvestedTime = Convert.ToInt32(current["InvestedTime"]);

            return record;
        }

        #endregion

        public static readonly ProjectsDALC Instance = new ProjectsDALC();
    }
}
