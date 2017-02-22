using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using PLMClientsBusinessEntities;

namespace PLMClientsDataAccessComponent
{
    public class AgentsDALC : PLMClientsDataAccessAdapter<AgentsInfo>
    {

        #region Constructors

        private AgentsDALC() { }

        #endregion

        #region Public Methods

        public PLMClientsBusinessEntities.AgentsInfo getAgent(int agentId)
        {
            DbCommand dbCmd = AgentsDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetAgents");

            // Add the parameters:
            AgentsDALC.InstanceDatabase.AddParameter(dbCmd, "@agentId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, agentId);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = AgentsDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);
                else
                    return null;
            }
        }

        public List<PLMClientsBusinessEntities.AgentsInfo> getAgentsByBranch(int branchId)
        {
            List<PLMClientsBusinessEntities.AgentsInfo> BECollection = new List<AgentsInfo>();

            DbCommand dbCmd = AgentsDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetAgents");

            // Add the parameters:
            AgentsDALC.InstanceDatabase.AddParameter(dbCmd, "@branchId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, branchId);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = AgentsDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                // Iterates through records:
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }
            return BECollection;
        }

        public PLMClientsBusinessEntities.AgentsInfo getAgentByZone(byte zoneId)
        {
            DbCommand dbCmd = AgentsDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetAgents");

            // Add the parameters:
            AgentsDALC.InstanceDatabase.AddParameter(dbCmd, "@zoneId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, zoneId);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = AgentsDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);
                else
                    return null;
            }
        }

        public List<PLMClientsBusinessEntities.AgentsInfo> getAgentsByPrefix(string prefix, byte targetId)
        {
            List<PLMClientsBusinessEntities.AgentsInfo> BECollection = new List<AgentsInfo>();

            DbCommand dbCmd = AgentsDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetAgentsByPrefix");

            // Add the parameters:
            AgentsDALC.InstanceDatabase.AddParameter(dbCmd, "@prefix", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, prefix);
            AgentsDALC.InstanceDatabase.AddParameter(dbCmd, "@targetId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, targetId);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = AgentsDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                // Iterates through records:
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }
            return BECollection;
        }

        public List<PLMClientsBusinessEntities.AgentsInfo> getAgentsByZoneByLocation(string prefix, byte targetId, byte zoneId, int locationId)
        {
            List<PLMClientsBusinessEntities.AgentsInfo> BECollection = new List<AgentsInfo>();

            DbCommand dbCmd = AgentsDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetAgentsByZoneByLocation");

            // Add the parameters:
            AgentsDALC.InstanceDatabase.AddParameter(dbCmd, "@prefix", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, prefix);
            AgentsDALC.InstanceDatabase.AddParameter(dbCmd, "@targetId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, targetId);
            AgentsDALC.InstanceDatabase.AddParameter(dbCmd, "@zoneId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, zoneId);
            AgentsDALC.InstanceDatabase.AddParameter(dbCmd, "@locationId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, locationId);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = AgentsDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                // Iterates through records:
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }
            return BECollection;
        }

        #endregion

        #region Protected Methods

        protected override PLMClientsBusinessEntities.AgentsInfo getFromDataReader(IDataReader current)
        {
            AgentsInfo record = new AgentsInfo();

            record.AgentId = Convert.ToInt32(current["AgentId"]);
            record.TypeId = Convert.ToByte(current["TypeId"]);
            record.FirstName = current["FirstName"].ToString();
            record.LastName = current["LastName"].ToString();
            record.SecondLastName = current["SecondLastName"].ToString();
            record.Email = current["Email"].ToString();
            record.PhoneOne = current["PhoneOne"].ToString();
            record.PhoneTwo = current["PhoneTwo"].ToString();
            record.Active = Convert.ToBoolean(current["Active"]);

            return record;
        }

        #endregion

        public static readonly AgentsDALC Instance = new AgentsDALC();

    }
}
