using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using PLMClientsBusinessEntities;

namespace PLMClientsDataAccessComponent
{
    public class ResidenceLevelsDALC : PLMClientsDataAccessAdapter<ResidenceLevelsInfo>
    {

        #region Constructors

        private ResidenceLevelsDALC() { }

        #endregion

        #region Public Methods

        public PLMClientsBusinessEntities.ResidenceLevelsInfo getResidenceLevel(byte residenceId)
        {
            DbCommand dbCmd = ResidenceLevelsDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetResidenceLevels");

            // Add the parameters:
            ResidenceLevelsDALC.InstanceDatabase.AddParameter(dbCmd, "@residenceId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, residenceId);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = ResidenceLevelsDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);
                else
                    return null;
            }
        }

        public PLMClientsBusinessEntities.ResidenceLevelsInfo getResidenceLevel(string residenceKey)
        {
            DbCommand dbCmd = ResidenceLevelsDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetResidenceLevels");

            // Add the parameters:
            ResidenceLevelsDALC.InstanceDatabase.AddParameter(dbCmd, "@residenceKey", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, residenceKey);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = ResidenceLevelsDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);
                else
                    return null;
            }
        }

        public List<PLMClientsBusinessEntities.ResidenceLevelsInfo> getResidenceLevelsBySpeciality(short specialityId)
        {
            List<PLMClientsBusinessEntities.ResidenceLevelsInfo> BECollection = new List<ResidenceLevelsInfo>();
            DbCommand dbCmd = ResidenceLevelsDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetResidenceLevels");

            // Add the parameters:
            ResidenceLevelsDALC.InstanceDatabase.AddParameter(dbCmd, "@specialityId", DbType.Int16,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, specialityId);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = ResidenceLevelsDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                // Iterates through records:
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }
            return BECollection;
        }

        public PLMClientsBusinessEntities.ResidenceLevelsInfo getResidenceLevelByClient(int clientId)
        {
            DbCommand dbCmd = ResidenceLevelsDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetResidenceLevels");

            // Add the parameters:
            ResidenceLevelsDALC.InstanceDatabase.AddParameter(dbCmd, "@clientId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, clientId);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = ResidenceLevelsDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);
                else
                    return null;
            }
        }


        #endregion

        #region Protected Methods

        protected override PLMClientsBusinessEntities.ResidenceLevelsInfo getFromDataReader(IDataReader current)
        {
            ResidenceLevelsInfo record = new ResidenceLevelsInfo();

            if (current["ResidenceId"] != System.DBNull.Value)
                record.ResidenceId = Convert.ToByte(current["ResidenceId"]);

            if (current["ResidenceKey"] != System.DBNull.Value)
                record.ResidenceKey = current["ResidenceKey"].ToString();

            if (current["ResidenceName"] != System.DBNull.Value)
                record.ResidenceName = current["ResidenceName"].ToString();

            if (current["Active"] != System.DBNull.Value)
                record.Active = Convert.ToBoolean(current["Active"]);

            return record;
        }

        #endregion

        public static readonly ResidenceLevelsDALC Instance = new ResidenceLevelsDALC();

    }
}
