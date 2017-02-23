using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using PLMUserBusinessEntries;

namespace PLMUsersDataAccessComponent
{
    public sealed class ProjectActivitiesNameDALC : PLMUsersDataAccesAdapter<ProjectActivitiesNameInfo>
    {
        #region Constructors

        private ProjectActivitiesNameDALC() { }

        #endregion  

        #region Public Methods


        public List<ProjectActivitiesNameInfo> getProjectActivitiesByUserByBinnacle(int userId, int binnacleId)
        {
            DbCommand dbCmd = ProjectActivitiesNameDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetProjectActivitiesNameByUserByBinnacle");

            ProjectActivitiesNameDALC.InstanceDatabase.AddParameter(dbCmd, "@userId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, userId);
            ProjectActivitiesNameDALC.InstanceDatabase.AddParameter(dbCmd, "@binnacleId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, binnacleId);

            List<ProjectActivitiesNameInfo> BECollection = new List<ProjectActivitiesNameInfo>();

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = ProjectActivitiesNameDALC.InstanceDatabase.ExecuteReader(dbCmd))
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

        #region Protected Methods

        protected override ProjectActivitiesNameInfo getFromDataReader(IDataReader current)
        {
            ProjectActivitiesNameInfo record = new ProjectActivitiesNameInfo();

            record.ActivityId = Convert.ToInt32(current["ActivityId"]);
            record.UserId = Convert.ToInt32(current["UserId"]);          
            record.ProjectId = Convert.ToInt32(current["ProjectId"]);
            record.ProjectName = current["ProjectName"].ToString();
            record.ActivityDate = Convert.ToDateTime(current["ActivityDate"]);
            record.InvestedTime = Convert.ToByte(current["InvestedTime"]);
            record.ActivityDescription = current["ActivityDescription"].ToString();
            record.Comment = current["Comment"].ToString();
                    
            return record;
        }

        #endregion

        public static readonly ProjectActivitiesNameDALC Instance = new ProjectActivitiesNameDALC();

    }
}