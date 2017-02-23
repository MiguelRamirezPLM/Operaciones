
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using PLMUserBusinessEntries;

namespace PLMUsersDataAccessComponent
{
    public sealed class ProjectActivitiesTimeDALC : PLMUsersDataAccesAdapter<ProjectActivitiesTimeInfo>
    {
        #region Constructors

        private ProjectActivitiesTimeDALC() { }

        #endregion  

        #region Public Methods


        public List<ProjectActivitiesTimeInfo> getProjectActivitiesTimeByUserByBinnacle(int userId, int binnacleId)
        {
            DbCommand dbCmd = ProjectActivitiesTimeDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetProjectActivitiesTimeByUserByBinnacle");

            ProjectActivitiesTimeDALC.InstanceDatabase.AddParameter(dbCmd, "@userId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, userId);
            ProjectActivitiesTimeDALC.InstanceDatabase.AddParameter(dbCmd, "@binnacleId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, binnacleId);

            List<ProjectActivitiesTimeInfo> BECollection = new List<ProjectActivitiesTimeInfo>();

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = ProjectActivitiesTimeDALC.InstanceDatabase.ExecuteReader(dbCmd))
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

        protected override ProjectActivitiesTimeInfo getFromDataReader(IDataReader current)
        {
            ProjectActivitiesTimeInfo record = new ProjectActivitiesTimeInfo();

            record.ProjectName = current["ProjectName"].ToString();
            record.TotalTime = Convert.ToInt32(current["TotalTime"]);
            
            return record;
        }

        #endregion

        public static readonly ProjectActivitiesTimeDALC Instance = new ProjectActivitiesTimeDALC();

    }
}