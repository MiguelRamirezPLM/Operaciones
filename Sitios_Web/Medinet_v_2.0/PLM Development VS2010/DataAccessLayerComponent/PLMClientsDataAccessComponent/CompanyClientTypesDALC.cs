using System;

using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using PLMClientsBusinessEntities;

namespace PLMClientsDataAccessComponent
{
    public sealed class CompanyClientTypesDALC : PLMClientsDataAccessAdapter<CompanyClientTypesInfo>
    {
        #region Constructors

        private CompanyClientTypesDALC() { }

        #endregion

        #region Public Methods

        public List<CompanyClientTypesInfo> getCompanyClientTypes()
        {
            DbCommand dbCmd = CompanyClientTypesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetAllCompanyClientTypes");

            List<CompanyClientTypesInfo> BECollection = new List<CompanyClientTypesInfo>();

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = CompanyClientTypesDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                // Iterates through records:
                while (dataReader.Read())
                {
                    BECollection.Add(this.getFromDataReader(dataReader));
                }
            }

            return BECollection;
        }

        #endregion

        #region Protected Methods

        protected override CompanyClientTypesInfo getFromDataReader(IDataReader current)
        {
            CompanyClientTypesInfo record = new CompanyClientTypesInfo();

            record.CCTypeId = Convert.ToByte(current["CCTypeId"]);
            record.CCTypeName = current["CCTypeName"].ToString();
            record.Active = Convert.ToBoolean(current["Active"]);

            return record;
        }

        #endregion

        public static readonly CompanyClientTypesDALC Instance = new CompanyClientTypesDALC();
    }
}
