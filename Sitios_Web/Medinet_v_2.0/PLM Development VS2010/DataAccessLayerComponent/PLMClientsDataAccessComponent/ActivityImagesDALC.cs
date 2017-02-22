using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using PLMClientsBusinessEntities;

namespace PLMClientsDataAccessComponent
{
    public class ActivityImagesDALC : PLMClientsDataAccessAdapter<ActivityImagesInfo>
    {

        #region Constructors

        private ActivityImagesDALC() { }

        #endregion

        #region Public Methods

        public List<PLMClientsBusinessEntities.ActivityImagesInfo> getImagesByPhysicalActivity(byte activityId)
        {
            List<PLMClientsBusinessEntities.ActivityImagesInfo> BECollection = new List<PLMClientsBusinessEntities.ActivityImagesInfo>();

            DbCommand dbCmd = ActivityImagesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetImagesByPhysicalActivity");

            // Add the parameters:
            ActivityImagesDALC.InstanceDatabase.AddParameter(dbCmd, "@activityId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, activityId);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = ActivityImagesDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                // Iterates through records:
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }
            return BECollection;
        }

        #endregion

        #region Protected Methods

        protected override PLMClientsBusinessEntities.ActivityImagesInfo getFromDataReader(IDataReader current)
        {
            ActivityImagesInfo record = new ActivityImagesInfo();

            record.ImageId = Convert.ToInt32(current["ImageId"]);
            record.ActivityId = Convert.ToByte(current["ActivityId"]);
            record.ImageName = current["ImageName"].ToString();
            record.BaseUrl = current["BaseUrl"].ToString();
            record.Active = Convert.ToBoolean(current["Active"]);

            return record;
        }

        #endregion

        public static readonly ActivityImagesDALC Instance = new ActivityImagesDALC();

    }
}
