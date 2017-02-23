using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using PLMUserBusinessEntries;

namespace PLMUsersDataAccessComponent
{
    public sealed class ProjectActivitiesDALC : PLMUsersDataAccesAdapter<ProjectActivitiesInfo>
    {
        #region Constructors

        private ProjectActivitiesDALC() { }

        #endregion  

        #region Public Methods

        public List<ProjectActivitiesInfo> getProjectActivities()
        {
            DbCommand dbCmd = ProjectActivitiesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetProjectActivities");

            List<ProjectActivitiesInfo> BECollection = new List<ProjectActivitiesInfo>();

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = ProjectActivitiesDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                // Iterates through records:
                while (dataReader.Read())
                {
                    BECollection.Add(this.getFromDataReader(dataReader));
                }
            }
            return BECollection;
        }

        public List<ProjectActivitiesInfo> getProjectActivitiesByUserByBinnacle(int userId, int binnacleId)
        {
            DbCommand dbCmd = ProjectActivitiesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetProjectActivitiesByUserByBinnacle");

            ProjectActivitiesDALC.InstanceDatabase.AddParameter(dbCmd, "@userId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, userId);
            ProjectActivitiesDALC.InstanceDatabase.AddParameter(dbCmd, "@binnacleId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, binnacleId);

            List<ProjectActivitiesInfo> BECollection = new List<ProjectActivitiesInfo>();

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = ProjectActivitiesDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                // Iterates through records:
                while (dataReader.Read())
                {
                    BECollection.Add(this.getFromDataReader(dataReader));
                }
            }
            return BECollection;
        }

        public override ProjectActivitiesInfo getOne(int pk)
        {
            DbCommand dbCmd = ProjectActivitiesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDProjectActivities");

            //Add the parameters:
            ProjectActivitiesDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, ProjectActivitiesDALC.CRUD.Read);
            ProjectActivitiesDALC.InstanceDatabase.AddParameter(dbCmd, "@activityId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pk);

            using (IDataReader dataReader = ProjectActivitiesDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);
                else
                    return null;
            }
        }

        public override int insert(ProjectActivitiesInfo businessEntity)
        {
            DbCommand dbCmd = ProjectActivitiesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDProjectActivities");

            // Add the parameters:
            ProjectActivitiesDALC.InstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            ProjectActivitiesDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Create);
            ProjectActivitiesDALC.InstanceDatabase.AddParameter(dbCmd, "@activityId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, -1);
            ProjectActivitiesDALC.InstanceDatabase.AddParameter(dbCmd, "@userId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.UserId);
            ProjectActivitiesDALC.InstanceDatabase.AddParameter(dbCmd, "@projectId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ProjectId);
            ProjectActivitiesDALC.InstanceDatabase.AddParameter(dbCmd, "@roleId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.RoleId);
            ProjectActivitiesDALC.InstanceDatabase.AddParameter(dbCmd, "@binnacleId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.BinnacleId);
            ProjectActivitiesDALC.InstanceDatabase.AddParameter(dbCmd, "@addDate", DbType.DateTime,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.AddDate); 
            ProjectActivitiesDALC.InstanceDatabase.AddParameter(dbCmd, "@activityDate", DbType.DateTime,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ActivityDate);   
            ProjectActivitiesDALC.InstanceDatabase.AddParameter(dbCmd, "@investedTime", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.InvestedTime);
            ProjectActivitiesDALC.InstanceDatabase.AddParameter(dbCmd, "@activityDescription", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ActivityDescription);
            ProjectActivitiesDALC.InstanceDatabase.AddParameter(dbCmd, "@comment", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Comment);

            //Insert record:
            ProjectActivitiesDALC.InstanceDatabase.ExecuteNonQuery(dbCmd);

            businessEntity.ActivityId = Convert.ToInt32(dbCmd.Parameters["@Return"].Value);

            return Convert.ToInt32(dbCmd.Parameters["@Return"].Value);
        }


        public override void update(ProjectActivitiesInfo businessEntity)
        {
            DbCommand dbCmd = ProjectActivitiesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDProjectActivities");

            // Add the parameters:
            ProjectActivitiesDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Update);
            ProjectActivitiesDALC.InstanceDatabase.AddParameter(dbCmd, "@activityId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ActivityId);
            ProjectActivitiesDALC.InstanceDatabase.AddParameter(dbCmd, "@userId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.UserId);
            ProjectActivitiesDALC.InstanceDatabase.AddParameter(dbCmd, "@projectId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ProjectId);
            ProjectActivitiesDALC.InstanceDatabase.AddParameter(dbCmd, "@roleId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.RoleId);
            ProjectActivitiesDALC.InstanceDatabase.AddParameter(dbCmd, "@binnacleId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.BinnacleId);
            ProjectActivitiesDALC.InstanceDatabase.AddParameter(dbCmd, "@addDate", DbType.DateTime,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.AddDate);
            ProjectActivitiesDALC.InstanceDatabase.AddParameter(dbCmd, "@activityDate", DbType.DateTime,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ActivityDate);
            ProjectActivitiesDALC.InstanceDatabase.AddParameter(dbCmd, "@investedTime", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.InvestedTime);
            ProjectActivitiesDALC.InstanceDatabase.AddParameter(dbCmd, "@activityDescription", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ActivityDescription);
            ProjectActivitiesDALC.InstanceDatabase.AddParameter(dbCmd, "@comment", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Comment);

            //Update record:
            ProjectActivitiesDALC.InstanceDatabase.ExecuteNonQuery(dbCmd);
        }

        public override void delete(int pk)
        {
            DbCommand dbCmd = ProjectActivitiesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDProjectActivities");

            // Add the parameters:
            ProjectActivitiesDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Delete );
            ProjectActivitiesDALC.InstanceDatabase.AddParameter(dbCmd, "@activityId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pk);

            //Update record:
            ProjectActivitiesDALC.InstanceDatabase.ExecuteNonQuery(dbCmd);
        }

        #endregion

        #region Protected Methods

        protected override ProjectActivitiesInfo getFromDataReader(IDataReader current)
        {
            ProjectActivitiesInfo record = new ProjectActivitiesInfo();

            record.ActivityId = Convert.ToInt32(current["ActivityId"]);
            record.UserId = Convert.ToInt32(current["UserId"]);          
            record.ProjectId = Convert.ToInt32(current["ProjectId"]);
            record.RoleId = Convert.ToInt32(current["RoleId"]);
            record.BinnacleId = Convert.ToInt32(current["BinnacleId"]);
            record.AddDate = Convert.ToDateTime(current["AddDate"]);
            record.ActivityDate = Convert.ToDateTime(current["ActivityDate"]);
            record.InvestedTime = Convert.ToByte(current["InvestedTime"]);
            record.ActivityDescription = current["ActivityDescription"].ToString();
            record.Comment = current["Comment"].ToString();
            //record.ProjectName = current["ProjectName"].ToString();
            
            return record;
        }

        #endregion

        public static readonly ProjectActivitiesDALC Instance = new ProjectActivitiesDALC();

    }
}