using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using MedinetBusinessEntries;
using PharmaSearchEngineBusinessEntries;

namespace PharmaSearchEngineDataAccessComponent
{
    public sealed class ReportsDALC : PharmaSearchEngineDataAccessAdapter<VendorDetailInfo>
    {
        #region Constructors

        private ReportsDALC() { }

        #endregion

        #region Public Methods

        public List<VendorDetailInfo> getProductsByVendor(int editionId, int moduleId, int divisionId, string vendor)
        {
            DbCommand dbCmd = ReportsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.[plm_spGetReportsByModule]");

            // Add the parameters:
            ReportsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            ReportsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@moduleId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, moduleId);
            ReportsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, divisionId);
            ReportsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@vendor", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, vendor);

            List<VendorDetailInfo> BECollection = new List<VendorDetailInfo>();

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = ReportsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            return BECollection;
        }

        public List<VendorDetailInfo> getAllProductsByVendor(int editionId)
        {
            DbCommand dbCmd = ReportsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.[plm_spGetAllReportsByModule]");

            // Add the parameters:
            ReportsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            ReportsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@moduleId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, 4);

            List<VendorDetailInfo> BECollection = new List<VendorDetailInfo>();

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = ReportsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            return BECollection;
        }

        public List<MedicDetailInfo> getProductsByMedic(int editionId, int moduleId, int divisionId, string vendor)
        {
            DbCommand dbCmd = ReportsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.[plm_spGetReportsByModule]");

            // Add the parameters:
            ReportsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            ReportsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@moduleId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, moduleId);
            ReportsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, divisionId);
            ReportsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@vendor", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, vendor);

            List<MedicDetailInfo> BECollection = new List<MedicDetailInfo>();

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = ReportsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReaderMedic(dataReader));
            }

            return BECollection;
        }

        public List<DesignerDetailInfo> getDesignerByVendor(int editionId, int moduleId, int divisionId, string vendor)
        {
            DbCommand dbCmd = ReportsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.[plm_spGetReportsByModule]");

            // Add the parameters:
            ReportsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            ReportsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@moduleId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, moduleId);
            ReportsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, divisionId);
            ReportsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@vendor", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, vendor);

            List<DesignerDetailInfo> BECollection = new List<DesignerDetailInfo>();

            // Get the resultset from the stored procedure:
            using (IDataReader dataReader = ReportsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                PharmaSearchEngineBusinessEntries.DesignerDetailInfo record;

                // Iterates through records:
                while (dataReader.Read())
                {
                    record = new DesignerDetailInfo();

                    record.Vendor = dataReader["Vendor"].ToString();
                    record.EditionId = Convert.ToInt32(dataReader["EditionId"]);
                    record.DivisionId = Convert.ToInt32(dataReader["DivisionId"]);
                    record.DivisionName = dataReader["DivisionName"].ToString();
                    record.DivisionShortName = dataReader["DivisionShortName"].ToString();
                    record.ProductId = Convert.ToInt32(dataReader["ProductId"]);
                    record.Brand = dataReader["Brand"].ToString();
                    record.PharmaFormId = Convert.ToInt32(dataReader["PharmaFormId"]);
                    record.PharmaForm = dataReader["PharmaForm"].ToString();
                    record.NewProduct = dataReader["NewProduct"].ToString();
                    record.ModifiedContent = dataReader["ModifiedContent"].ToString();
                    record.ModifiedAttributes = dataReader["ModifiedAttributes"].ToString();
                    record.Presentations = dataReader["Presentations"].ToString();
                    if (dataReader["ModuleId"] != DBNull.Value)
                    {
                        record.ModuleId = Convert.ToInt32(dataReader["ModuleId"]);
                    }
                    record.CategoryId = Convert.ToInt32(dataReader["CategoryId"]);
                    record.NumberEdition = Convert.ToInt32(dataReader["NumberEdition"]);
                    BECollection.Add(record);

                }
            }

            return BECollection;
        }

        #endregion

        #region Protected Methods

        protected VendorDetailInfo getFromDataReader(IDataReader current)
        {
            VendorDetailInfo record = new VendorDetailInfo();

            record.Vendor = current["Vendor"].ToString();
            record.EditionId = Convert.ToInt32(current["EditionId"]);
            record.DivisionId = Convert.ToInt32(current["DivisionId"]);
            record.DivisionName = current["DivisionName"].ToString();

            if (current["DivisionShortName"] != DBNull.Value)
                record.DivisionShortName = current["DivisionShortName"].ToString();

            else
                record.DivisionShortName = null;

            record.ProductId = Convert.ToInt32(current["ProductId"]);
            record.Brand = current["Brand"].ToString();
            record.PharmaFormId = Convert.ToInt32(current["PharmaFormId"]);
            record.PharmaForm = current["PharmaForm"].ToString();
            record.NewProduct = current["NewProduct"].ToString();
            record.ModifiedContent = current["ModifiedContent"].ToString();
            record.ProductShot = current["ProductShot"].ToString();
            record.NumberEdition = Convert.ToInt32(current["NumberEdition"]);
            record.ModuleId = Convert.ToInt32(current["ModuleId"]);
            record.ShortName = current["ShortName"].ToString();

            return record;
        }

        protected MedicDetailInfo getFromDataReaderMedic(IDataReader current)
        {
            MedicDetailInfo record = new MedicDetailInfo();

            if (current["DivisionShortName"] != DBNull.Value)
                record.DivisionShortName = current["DivisionShortName"].ToString();

            else
                record.DivisionShortName = null;

            record.Brand = current["Brand"].ToString();
            record.PharmaForm = current["PharmaForm"].ToString();
            record.Atc = current["Atc"].ToString();
            record.Substances = current["sustancias"].ToString();
            record.Indications = current["indicaciones"].ToString();
            record.NumberEdition =Convert.ToInt32(current["NumberEdition"]);

            return record;
        }
        #endregion
        public static readonly ReportsDALC Instance = new ReportsDALC();
    }
}
