using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using PLMClientsBusinessEntities;

namespace PLMClientsDataAccessComponent
{
    public class ResolutionScreensDALC : PLMClientsDataAccessAdapter<ResolutionScreensInfo>
    {

        #region Constructors

        private ResolutionScreensDALC() { }

        #endregion

        #region Public Methods

        public PLMClientsBusinessEntities.ResolutionScreensInfo getByResolutionKey(string resolutionKey)
        {
            DbCommand dbCmd = ResolutionScreensDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetResolutionScreens");

            // Add the parameters:
            ResolutionScreensDALC.InstanceDatabase.AddParameter(dbCmd, "@resolutionKey", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, resolutionKey);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = ResolutionScreensDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);
                else
                    return null;
            }
        }

        #endregion

        #region Protected Methods

        protected override PLMClientsBusinessEntities.ResolutionScreensInfo getFromDataReader(IDataReader current)
        {
            PLMClientsBusinessEntities.ResolutionScreensInfo record = new PLMClientsBusinessEntities.ResolutionScreensInfo();

            record.ScreenId = Convert.ToByte(current["ScreenId"]);
            record.ResolutionId = Convert.ToByte(current["ResolutionId"]);
            record.BaseUrl = current["BaseUrl"].ToString();

            return record;
        }

        #endregion

        public static readonly ResolutionScreensDALC Instance = new ResolutionScreensDALC();

    }
}
