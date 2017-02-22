using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using PLMClientsBusinessEntities;

namespace PLMClientsDataAccessComponent
{
    public sealed class StatesDALC : PLMClientsDataAccessAdapter<StateInfo>
    {
        #region Constructors

        private StatesDALC() { }

        #endregion

        #region Public Methods

        public override List<StateInfo> getAll()
        {
            List<StateInfo> BECollection = new List<StateInfo>();

            // Get the resultset from the stored procedure:
            using (IDataReader dataReader = StatesDALC.InstanceDatabase.ExecuteReader(
                StatesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetStates")))
            {
                // Iterates through records:
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            return BECollection;
        }

        public List<StateInfo> getByCountry(byte countryId)
        {
            DbCommand dbCmd = StatesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetStates");

            // Add the parameters:
            StatesDALC.InstanceDatabase.AddParameter(dbCmd, "@countryId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, countryId);
            
            List<StateInfo> BECollection = new List<StateInfo>();

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = StatesDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            return BECollection;
        }

        public List<StateInfo> getByCountryByName(byte countryId, string prefix)
        {
            DbCommand dbCmd = StatesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetStates");

            // Add the parameters:
            StatesDALC.InstanceDatabase.AddParameter(dbCmd, "@countryId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, countryId);
            StatesDALC.InstanceDatabase.AddParameter(dbCmd, "@prefix", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, prefix);

            List<StateInfo> BECollection = new List<StateInfo>();

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = StatesDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            return BECollection;
        }

        public StateInfo getByName(byte countryId, string stateName)
        {
            DbCommand dbCmd = StatesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetStatesByName");

            // Add the parameters:
            StatesDALC.InstanceDatabase.AddParameter(dbCmd, "@countryId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, countryId);
            StatesDALC.InstanceDatabase.AddParameter(dbCmd, "@stateName", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, stateName);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = StatesDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);
                else
                    return null;
            }

        }

        public StateInfo getByShortName(string shortName)
        {
            DbCommand dbCmd = StatesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetStatesByName");

            // Add the parameters:
            StatesDALC.InstanceDatabase.AddParameter(dbCmd, "@shortName", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, shortName);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = StatesDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);
                else
                    return null;
            }
        }

        public List<StateInfo> getByClientAdvisor(int clientId)
        {
            DbCommand dbCmd = StatesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetInformation_By_AdvisorId");

            // Add the parameters:
            StatesDALC.InstanceDatabase.AddParameter(dbCmd, "@clientId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, clientId);
            List<StateInfo> listState = new List<StateInfo>();
            // Get the result set from the stored procedure:
            using (IDataReader dataReader = StatesDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                {
                   listState.Add(this.getFromDataReader(dataReader));
                }
            }
            return listState;
        }

        public StateInfo getByCountryByShortName(int countryId, string shortName)
        {
            DbCommand dbCmd = StatesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetStatesByName");

            // Add the parameters:
            StatesDALC.InstanceDatabase.AddParameter(dbCmd, "@countryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, countryId);
            StatesDALC.InstanceDatabase.AddParameter(dbCmd, "@shortName", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, shortName);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = StatesDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);
                else
                    return null;
            }
        }

        public override StateInfo getOne(int pk)
        {
            DbCommand dbCmd = ClientsDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDStates");

            // Add the parameters:
            StatesDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Read);
            StatesDALC.InstanceDatabase.AddParameter(dbCmd, "@stateId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pk);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = StatesDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);

                else
                    return null;
            }
        }

        #endregion

        #region Protected Methods

        protected override StateInfo getFromDataReader(IDataReader current)
        {
            StateInfo record = new StateInfo();

            record.StateId = Convert.ToInt32(current["StateId"]);
            record.StateName = current["StateName"].ToString();
            record.CountryId = Convert.ToByte(current["CountryId"]);
            record.ShortName = current["ShortName"].ToString();
            record.Active = Convert.ToBoolean(current["Active"]);

            return record;

        }

        #endregion

        public static readonly StatesDALC Instance = new StatesDALC();
    }
}
