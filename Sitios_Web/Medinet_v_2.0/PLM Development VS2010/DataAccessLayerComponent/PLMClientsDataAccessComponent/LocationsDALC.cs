using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using PLMClientsBusinessEntities;

namespace PLMClientsDataAccessComponent
{
    public class LocationsDALC : PLMClientsDataAccessAdapter<LocationsInfo>
    {

        #region Constructors

        private LocationsDALC() { }

        #endregion

        #region Public Methods

        public List<PLMClientsBusinessEntities.LocationsInfo> getLocationsByPrefixByZone(string prefix, byte targetId, byte zoneId)
        {
            List<PLMClientsBusinessEntities.LocationsInfo> BECollection = new List<LocationsInfo>();

            DbCommand dbCmd = LocationsDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetLocations");

            // Add the parameters:
            LocationsDALC.InstanceDatabase.AddParameter(dbCmd, "@prefix", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, prefix);
            LocationsDALC.InstanceDatabase.AddParameter(dbCmd, "@targetId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, targetId);
            LocationsDALC.InstanceDatabase.AddParameter(dbCmd, "@zoneId", DbType.Byte,
               ParameterDirection.Input, string.Empty, DataRowVersion.Current, zoneId);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = LocationsDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                // Iterates through records:
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }
            return BECollection;
        }

        public List<PLMClientsBusinessEntities.LocationsInfo> getLocationsByState(int stateId)
        {
            List<PLMClientsBusinessEntities.LocationsInfo> BECollection = new List<LocationsInfo>();

            DbCommand dbCmd = LocationsDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetLocationsByState");

            // Add the parameters:
            LocationsDALC.InstanceDatabase.AddParameter(dbCmd, "@stateId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, stateId);
            
            // Get the result set from the stored procedure:
            using (IDataReader dataReader = LocationsDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                // Iterates through records:
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }
            return BECollection;
        }

        #endregion

        #region Protected Methods

        protected override PLMClientsBusinessEntities.LocationsInfo getFromDataReader(IDataReader current)
        {
            LocationsInfo record = new LocationsInfo();

            record.LocationId = Convert.ToInt32(current["LocationId"]);
            record.LocationName = current["LocationName"].ToString();
            record.Active = Convert.ToBoolean(current["Active"]);

            return record;
        }

        #endregion

        public static readonly LocationsDALC Instance = new LocationsDALC();

    }
}
