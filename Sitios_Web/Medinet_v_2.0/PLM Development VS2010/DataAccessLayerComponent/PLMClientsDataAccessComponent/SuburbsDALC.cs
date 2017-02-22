using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using PLMClientsBusinessEntities;

namespace PLMClientsDataAccessComponent
{
    public class SuburbsDALC : PLMClientsDataAccessAdapter<SuburbsInfo>
    {
        #region Constructors

        private SuburbsDALC() { }

        #endregion

        #region Public Methods

        public List<PLMClientsBusinessEntities.SuburbsInfo> getSuburbsByLocations(int locationId)
        {
            List<PLMClientsBusinessEntities.SuburbsInfo> BECollection = new List<SuburbsInfo>();

            DbCommand dbCmd = SuburbsDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetSuburbsByLocation");

            // Add the parameters:
            SuburbsDALC.InstanceDatabase.AddParameter(dbCmd, "@locationId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, locationId);
            SuburbsInfo record;
            // Get the result set from the stored procedure:
            using (IDataReader dataReader = SuburbsDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                // Iterates through records:
                while (dataReader.Read())
                {
                    record = new SuburbsInfo();
                    record.SuburbId = Convert.ToInt32(dataReader["SuburbId"]);
                    record.SuburbName = dataReader["SuburbName"].ToString();
                    record.Active = Convert.ToBoolean(dataReader["Active"]);

                    BECollection.Add(record);
                }
            }
            return BECollection;
        }

        public List<PLMClientsBusinessEntities.SuburbZipCodeInfo> getSuburbsZipCodeByLocation(int locationId,int suburbId)
        {
            List<PLMClientsBusinessEntities.SuburbZipCodeInfo> BECollection = new List<SuburbZipCodeInfo>();

            DbCommand dbCmd = SuburbsDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetZipCodeBySuburb");

            // Add the parameters:

            SuburbsDALC.InstanceDatabase.AddParameter(dbCmd, "@locationId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, locationId);
            SuburbsDALC.InstanceDatabase.AddParameter(dbCmd, "@suburbId", DbType.Int32,
            ParameterDirection.Input, string.Empty, DataRowVersion.Current, suburbId);

            SuburbZipCodeInfo record;
            // Get the result set from the stored procedure:
            using (IDataReader dataReader = SuburbsDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                // Iterates through records:
                while (dataReader.Read())
                {
                    record = new SuburbZipCodeInfo();

                    record.SuburbName = dataReader["SuburbName"].ToString();
                    record.SuburbId = Convert.ToInt32(dataReader["SuburbId"]);
                    record.ZipCodeId = Convert.ToInt32(dataReader["ZipCodeId"]);
                    record.Zipcode = dataReader["Zipcode"].ToString();

                    BECollection.Add(record);
                }
            }
            return BECollection;
        }

        #endregion
        public static readonly SuburbsDALC Instance = new SuburbsDALC();
    }
}
