using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using PLMClientsBusinessEntities;

namespace PLMClientsDataAccessComponent
{
    public sealed class JobPlacesDALC : PLMClientsDataAccessAdapter<JobPlaceInfo>
    {
        #region Constructors

        private JobPlacesDALC() { }

        #endregion

        #region Public Methods

        public override List<JobPlaceInfo> getAll()
        {
            List<JobPlaceInfo> BECollection = new List<JobPlaceInfo>();

            // Get the resultset from the stored procedure:
            using (IDataReader dataReader = JobPlacesDALC.InstanceDatabase.ExecuteReader(
                JobPlacesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetJobPlaces")))
            {
                // Iterates through records:
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            return BECollection;
        }

        #endregion

        #region Protected Methods

        protected override JobPlaceInfo getFromDataReader(IDataReader current)
        {
            JobPlaceInfo record = new JobPlaceInfo();

            record.JobPlaceId = Convert.ToByte(current["JobPlaceId"]);
            record.JobPlaceName = current["JobPlaceName"].ToString();
            record.Active = Convert.ToBoolean(current["Active"]);
            
            return record;
        }

        #endregion

        public static readonly JobPlacesDALC Instance = new JobPlacesDALC();

    }
}
