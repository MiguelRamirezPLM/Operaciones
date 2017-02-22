using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using PLMClientsBusinessEntities;

namespace PLMClientsDataAccessComponent
{
    public class PharmacyProductsDALC : PLMClientsDataAccessAdapter<PharmacyProductsInfo>
    {

        #region Constructors

        private PharmacyProductsDALC() { }

        #endregion

        #region Public Methods

        public List<PLMClientsBusinessEntities.PharmacyProductsInfo> getByCompanyByText(int companyClientId, string textSearch)
        {
            List<PLMClientsBusinessEntities.PharmacyProductsInfo> BECollection = new List<PharmacyProductsInfo>();

            DbCommand dbCmd = PharmacyProductsDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetPharmacyProducts");

            // Add the parameters:
            PharmacyProductsDALC.InstanceDatabase.AddParameter(dbCmd, "@companyClientId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, companyClientId);
            PharmacyProductsDALC.InstanceDatabase.AddParameter(dbCmd, "@textSearch", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, textSearch);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = PharmacyProductsDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                // Iterates through records:
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }
            return BECollection;
        }

        public List<PLMClientsBusinessEntities.PharmacyProductsInfo> getByBranch(int branchId)
        {
            List<PLMClientsBusinessEntities.PharmacyProductsInfo> BECollection = new List<PharmacyProductsInfo>();

            DbCommand dbCmd = PharmacyProductsDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetPharmacyProducts");

            // Add the parameters:
            PharmacyProductsDALC.InstanceDatabase.AddParameter(dbCmd, "@branchId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, branchId);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = PharmacyProductsDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                // Iterates through records:
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }
            return BECollection;
        }

        #endregion

        #region Protected Methods

        protected override PLMClientsBusinessEntities.PharmacyProductsInfo getFromDataReader(IDataReader current)
        {
            PharmacyProductsInfo record = new PharmacyProductsInfo();

            record.ProductId = Convert.ToInt32(current["ProductId"]);
            record.CompanyClientId = Convert.ToInt32(current["CompanyClientId"]);
            record.ProductName = current["ProductName"].ToString();

            if (current["PharmaceuticalForms"] != System.DBNull.Value)
                record.PharmaceuticalForms = current["PharmaceuticalForms"].ToString();

            if (current["ActiveSubstances"] != System.DBNull.Value)
                record.ActiveSubstances = current["ActiveSubstances"].ToString();

            if (current["Indications"] != System.DBNull.Value)
                record.Indications = current["Indications"].ToString();

            if (current["Presentation"] != System.DBNull.Value)
                record.Presentation = current["Presentation"].ToString();

            if (current["PresentationContent"] != System.DBNull.Value)
                record.PresentationContent = current["PresentationContent"].ToString();

            record.Laboratory = current["Laboratory"].ToString();

            if (current["Logo"] != System.DBNull.Value)
                record.Logo = current["Logo"].ToString();

            if (current["Available"] != System.DBNull.Value)
                record.Available = Convert.ToBoolean(current["Available"]);

            record.Active = Convert.ToBoolean(current["Active"]);

            return record;
        }

        #endregion

        public static readonly PharmacyProductsDALC Instance = new PharmacyProductsDALC();

    }
}
