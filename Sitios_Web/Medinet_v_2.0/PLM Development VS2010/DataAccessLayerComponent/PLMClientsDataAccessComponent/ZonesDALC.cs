using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using PLMClientsBusinessEntities;

namespace PLMClientsDataAccessComponent
{
    public class ZonesDALC : PLMClientsDataAccessAdapter<ZonesInfo>
    {

        #region Constructors

        private ZonesDALC() { }

        #endregion

        #region Public Methods

        public List<PLMClientsBusinessEntities.ZonesInfo> getZones()
        {
            List<PLMClientsBusinessEntities.ZonesInfo> BECollection = new List<ZonesInfo>();

            DbCommand dbCmd = ZonesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetZones");

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = ZonesDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                // Iterates through records:
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }
            return BECollection;
        }

        public List<PLMClientsBusinessEntities.ZonesInfo> getZonesByPrefixByTarget(string prefix, byte targetId)
        {
            List<PLMClientsBusinessEntities.ZonesInfo> BECollection = new List<ZonesInfo>();

            DbCommand dbCmd = ZonesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetZones");

            // Add the parameters:
            ZonesDALC.InstanceDatabase.AddParameter(dbCmd, "@prefix", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, prefix);
            ZonesDALC.InstanceDatabase.AddParameter(dbCmd, "@targetId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, targetId);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = ZonesDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                // Iterates through records:
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }
            return BECollection;
        }

        #endregion

        #region Protected Methods

        protected override PLMClientsBusinessEntities.ZonesInfo getFromDataReader(IDataReader current)
        {
            ZonesInfo record = new ZonesInfo();

            record.ZoneId = Convert.ToByte(current["ZoneId"]);
            record.ZoneName = current["ZoneName"].ToString();
            record.Active = Convert.ToBoolean(current["Active"]);

            return record;
        }

        #endregion

        public static readonly ZonesDALC Instance = new ZonesDALC();
    }
}
