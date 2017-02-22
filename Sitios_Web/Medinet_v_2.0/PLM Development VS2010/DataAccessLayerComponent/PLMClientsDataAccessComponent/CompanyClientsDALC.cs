using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using PLMClientsBusinessEntities;

namespace PLMClientsDataAccessComponent
{
    public class CompanyClientsDALC : PLMClientsDataAccessAdapter<CompanyClientsInfo>
    {

        #region Constructors

        private CompanyClientsDALC() { }

        #endregion

        #region Public Methods

        public List<CompanyClientsInfo> getCompanyClientsByPrefix(string prefix)
        {
            List<PLMClientsBusinessEntities.CompanyClientsInfo> BECollection = new List<CompanyClientsInfo>();

            DbCommand dbCmd = CompanyClientsDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetCompanyClients");

            // Add the parameters:
            CompanyClientsDALC.InstanceDatabase.AddParameter(dbCmd, "@prefix", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, prefix);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = CompanyClientsDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                // Iterates through records:
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }
            return BECollection;
        }

        public List<CompanyClientsInfo> getAllCompanyClients()
        {
            DbCommand dbCmd = CompanyClientsDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetAllCompanyClients");

            List<CompanyClientsInfo> BECollection = new List<CompanyClientsInfo>();

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = CompanyClientsDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                // Iterates through records:
                while (dataReader.Read())
                {
                    BECollection.Add(this.getFromDataReader(dataReader));
                }
            }
            return BECollection;
        }

        public override int insert(CompanyClientsInfo businessEntity)
        {
            DbCommand dbCmd = CompanyClientsDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDCompanyClients");

            // Add the parameters:
            CompanyClientsDALC.InstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            CompanyClientsDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Create);
            CompanyClientsDALC.InstanceDatabase.AddParameter(dbCmd, "@companyClientId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, -1);
            CompanyClientsDALC.InstanceDatabase.AddParameter(dbCmd, "@cCTypeId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.CCTypeId);
            CompanyClientsDALC.InstanceDatabase.AddParameter(dbCmd, "@companyName", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.CompanyName);
            CompanyClientsDALC.InstanceDatabase.AddParameter(dbCmd, "@active", DbType.Boolean,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Active);

            //Insert record:
            CompanyClientsDALC.InstanceDatabase.ExecuteNonQuery(dbCmd);

            businessEntity.CompanyClientId = Convert.ToInt32(dbCmd.Parameters["@Return"].Value);

            return Convert.ToInt32(dbCmd.Parameters["@Return"].Value);
        }

        #endregion

        #region Protected Methods

        protected override PLMClientsBusinessEntities.CompanyClientsInfo getFromDataReader(IDataReader current)
        {
            CompanyClientsInfo record = new CompanyClientsInfo();

            record.CompanyClientId = Convert.ToInt32(current["CompanyClientId"]);
            record.CCTypeId = Convert.ToByte(current["CCTypeId"]);
            record.CompanyName = current["CompanyName"].ToString();
            record.Active = Convert.ToBoolean(current["Active"]);

            return record;
        }

        #endregion

        public static readonly CompanyClientsDALC Instance = new CompanyClientsDALC();

    }
}
