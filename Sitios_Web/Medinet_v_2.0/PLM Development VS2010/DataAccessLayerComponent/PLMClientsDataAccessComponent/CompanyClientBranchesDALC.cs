using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using PLMClientsBusinessEntities;

namespace PLMClientsDataAccessComponent
{
    public class CompanyClientBranchesDALC : PLMClientsDataAccessAdapter<BranchDetailInfo>
    {

        #region Constructors

        private CompanyClientBranchesDALC() { }

        #endregion

        #region Public Methods

        public PLMClientsBusinessEntities.BranchDetailInfo getBranch(int branchId)
        {
            DbCommand dbCmd = CompanyClientBranchesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetBranch");

            // Add the parameters:
            CompanyClientBranchesDALC.InstanceDatabase.AddParameter(dbCmd, "@branchId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, branchId);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = CompanyClientBranchesDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);
                else
                    return null;
            }
        }

        public List<PLMClientsBusinessEntities.BranchDetailInfo> getBranches()
        {
            List<PLMClientsBusinessEntities.BranchDetailInfo> BECollection = new List<BranchDetailInfo>();

            DbCommand dbCmd = CompanyClientBranchesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetBranches");

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = CompanyClientBranchesDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                // Iterates through records:
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }
            return BECollection;
        }

        public List<PLMClientsBusinessEntities.BranchDetailInfo> getBranchesByCompanyClient(int companyClientId)
        {
            List<PLMClientsBusinessEntities.BranchDetailInfo> BECollection = new List<BranchDetailInfo>();

            DbCommand dbCmd = CompanyClientBranchesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetBranches");

            // Add the parameters:
            CompanyClientBranchesDALC.InstanceDatabase.AddParameter(dbCmd, "@companyClientId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, companyClientId);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = CompanyClientBranchesDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                // Iterates through records:
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }
            return BECollection;
        }

        public List<PLMClientsBusinessEntities.BranchDetailInfo> getBranchesByType(byte typeId)
        {
            List<PLMClientsBusinessEntities.BranchDetailInfo> BECollection = new List<BranchDetailInfo>();

            DbCommand dbCmd = CompanyClientBranchesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetBranches");

            // Add the parameters:
            CompanyClientBranchesDALC.InstanceDatabase.AddParameter(dbCmd, "@CCtypeId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, typeId);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = CompanyClientBranchesDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                // Iterates through records:
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }
            return BECollection;
        }

        public List<PLMClientsBusinessEntities.BranchDetailInfo> getBranchesByState(int stateId)
        {
            List<PLMClientsBusinessEntities.BranchDetailInfo> BECollection = new List<BranchDetailInfo>();

            DbCommand dbCmd = CompanyClientBranchesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetBranches");

            // Add the parameters:
            CompanyClientBranchesDALC.InstanceDatabase.AddParameter(dbCmd, "@stateId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, stateId);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = CompanyClientBranchesDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                // Iterates through records:
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }
            return BECollection;
        }

        public List<PLMClientsBusinessEntities.BranchDetailInfo> getBranchesByAgent(int agentId)
        {
            List<PLMClientsBusinessEntities.BranchDetailInfo> BECollection = new List<BranchDetailInfo>();

            DbCommand dbCmd = CompanyClientBranchesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetBranches");

            // Add the parameters:
            CompanyClientBranchesDALC.InstanceDatabase.AddParameter(dbCmd, "@agentId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, agentId);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = CompanyClientBranchesDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                // Iterates through records:
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }
            return BECollection;
        }

        public List<PLMClientsBusinessEntities.BranchDetailInfo> getBranchesByZone(byte zoneId)
        {
            List<PLMClientsBusinessEntities.BranchDetailInfo> BECollection = new List<BranchDetailInfo>();

            DbCommand dbCmd = CompanyClientBranchesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetBranches");

            // Add the parameters:
            CompanyClientBranchesDALC.InstanceDatabase.AddParameter(dbCmd, "@zoneId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, zoneId);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = CompanyClientBranchesDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                // Iterates through records:
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }
            return BECollection;
        }

        public List<PLMClientsBusinessEntities.BranchDetailInfo> getBranchesByAgentByType(byte zoneId, int agentId, byte typeId)
        {
            List<PLMClientsBusinessEntities.BranchDetailInfo> BECollection = new List<BranchDetailInfo>();

            DbCommand dbCmd = CompanyClientBranchesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetBranches");

            // Add the parameters:
            CompanyClientBranchesDALC.InstanceDatabase.AddParameter(dbCmd, "@zoneId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, zoneId);
            CompanyClientBranchesDALC.InstanceDatabase.AddParameter(dbCmd, "@agentId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, agentId);
            CompanyClientBranchesDALC.InstanceDatabase.AddParameter(dbCmd, "@CCtypeId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, typeId);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = CompanyClientBranchesDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                // Iterates through records:
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }
            return BECollection;
        }

        public List<PLMClientsBusinessEntities.BranchDetailInfo> getBranchesByPrefix(string codePrefix)
        {
            List<PLMClientsBusinessEntities.BranchDetailInfo> BECollection = new List<BranchDetailInfo>();

            DbCommand dbCmd = CompanyClientBranchesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetBranchesByPrefix");

            // Add the parameters:
            CompanyClientBranchesDALC.InstanceDatabase.AddParameter(dbCmd, "@prefix", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, codePrefix);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = CompanyClientBranchesDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                // Iterates through records:
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }
            return BECollection;
        }

        public List<PLMClientsBusinessEntities.BranchDetailInfo> getBranchesByPrefixByText(string prefix, int? stateId, string companyClients, string searchText)
        {
            List<PLMClientsBusinessEntities.BranchDetailInfo> BECollection = new List<BranchDetailInfo>();

            DbCommand dbCmd = CompanyClientBranchesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetBranchesByPrefixByText");

            // Add the parameters:
            CompanyClientBranchesDALC.InstanceDatabase.AddParameter(dbCmd, "@prefix", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, prefix);

            if(stateId != null)
                CompanyClientBranchesDALC.InstanceDatabase.AddParameter(dbCmd, "@stateId", DbType.Int32,
                    ParameterDirection.Input, string.Empty, DataRowVersion.Current, stateId);

            if (companyClients != null)
                CompanyClientBranchesDALC.InstanceDatabase.AddParameter(dbCmd, "@companyClients", DbType.String,
                    ParameterDirection.Input, string.Empty, DataRowVersion.Current, companyClients);

            if (searchText != null)
                CompanyClientBranchesDALC.InstanceDatabase.AddParameter(dbCmd, "@searchText", DbType.String,
                    ParameterDirection.Input, string.Empty, DataRowVersion.Current, searchText);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = CompanyClientBranchesDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                // Iterates through records:
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }
            return BECollection;
        }

        public List<PLMClientsBusinessEntities.BranchDetailInfo> getBranchesByPrefixByCompanyType(string prefix, byte companyTypeId)
        {
            List<PLMClientsBusinessEntities.BranchDetailInfo> BECollection = new List<BranchDetailInfo>();

            DbCommand dbCmd = CompanyClientBranchesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetBranchesByPrefixByCompanyType");

            // Add the parameters:
            CompanyClientBranchesDALC.InstanceDatabase.AddParameter(dbCmd, "@prefix", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, prefix);
            CompanyClientBranchesDALC.InstanceDatabase.AddParameter(dbCmd, "@ccTypeId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, companyTypeId);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = CompanyClientBranchesDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                // Iterates through records:
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }
            return BECollection;
        }

        #endregion

        #region Protected Methods

        protected override PLMClientsBusinessEntities.BranchDetailInfo getFromDataReader(IDataReader current)
        {
            BranchDetailInfo record = new BranchDetailInfo();

            record.BranchId = Convert.ToInt32(current["BranchId"]);
            record.CompanyClientId = Convert.ToInt32(current["CompanyClientId"]);
            record.CompanyName = current["CompanyName"].ToString();
            record.BranchKey = current["BranchKey"].ToString();
            record.BranchName = current["BranchName"].ToString();
            record.WebPage = current["WebPage"].ToString();
            record.Email = current["Email"].ToString();
            record.BranchActive = Convert.ToBoolean(current["BranchActive"]);

            if (current["BaseUrl"] != System.DBNull.Value)
                record.BaseUrl = current["BaseUrl"].ToString();

            if (current["Logo"] != System.DBNull.Value)
                record.Logo = current["Logo"].ToString();

            if (current["AddressId"] != System.DBNull.Value)
                record.AddressId = Convert.ToInt32(current["AddressId"]);

            if (current["AttentionSchedules"] != System.DBNull.Value)
                record.AttentionSchedules = current["AttentionSchedules"].ToString();

            if (current["HomeService"] != System.DBNull.Value)
                record.HomeService = current["HomeService"].ToString();

            record.Street = current["Street"].ToString();
            record.InternalNumber = current["InternalNumber"].ToString();
            record.Suburb = current["Suburb"].ToString();
            record.ZipCode = current["ZipCode"].ToString();
            record.Location = current["Location"].ToString();

            if (current["CountryId"] != System.DBNull.Value)
                record.CountryId = Convert.ToByte(current["CountryId"]);

            if (current["StateId"] != System.DBNull.Value)
                record.StateId = Convert.ToInt32(current["StateId"]);
            
            record.StateName = current["StateName"].ToString();
            record.Lada = current["Lada"].ToString();
            record.PhoneOne = current["PhoneOne"].ToString();
            record.PhoneTwo = current["PhoneTwo"].ToString();
            record.Ext = current["Ext"].ToString();

            if (current["Latitude"] != System.DBNull.Value)
                record.Latitude = Convert.ToDecimal(current["Latitude"]);

            if (current["Longitude"] != System.DBNull.Value)
                record.Longitude = Convert.ToDecimal(current["Longitude"]);

            if (current["DisplayPharmacies"] != System.DBNull.Value)
                record.DisplayPharmacies = Convert.ToInt32(current["DisplayPharmacies"]);

            if (current["ServiceType"] != System.DBNull.Value)
                record.ServiceType = current["ServiceType"].ToString();

            return record;
        }

        #endregion

        public static readonly CompanyClientBranchesDALC Instance = new CompanyClientBranchesDALC();

    }
}
