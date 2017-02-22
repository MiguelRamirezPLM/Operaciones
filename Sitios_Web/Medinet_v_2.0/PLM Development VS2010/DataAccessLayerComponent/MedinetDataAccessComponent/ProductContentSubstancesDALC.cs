using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using MedinetBusinessEntries;

namespace MedinetDataAccessComponent
{
    public class ProductContentSubstancesDALC : MedinetDataAccessAdapter<ProductContentSubstancesInfo>
    {

        #region Constructors

        private ProductContentSubstancesDALC() { }

        #endregion

        #region Public Methods

        public override int insert(ProductContentSubstancesInfo businessEntity)
        {
            DbCommand dbCmd = ProductContentSubstancesDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDProductContentSubstances");

            ProductContentSubstancesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            ProductContentSubstancesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Create);
            ProductContentSubstancesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productContentId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ProductContentId);
            ProductContentSubstancesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ProductId);
            ProductContentSubstancesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@activeSubstanceId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ActiveSubstanceId);

            ProductContentSubstancesDALC.MedInstanceDatabase.ExecuteNonQuery(dbCmd);

            return Convert.ToInt32(dbCmd.Parameters["@Return"].Value);
        }

        public override void delete(ProductContentSubstancesInfo businessEntity)
        {
            DbCommand dbCmd = ProductContentSubstancesDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDProductContentSubstances");

            ProductContentSubstancesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            ProductContentSubstancesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Delete);
            ProductContentSubstancesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productContentId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ProductContentId);
            ProductContentSubstancesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ProductId);
            ProductContentSubstancesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@activeSubstanceId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ActiveSubstanceId);

            ProductContentSubstancesDALC.MedInstanceDatabase.ExecuteNonQuery(dbCmd);
        }

        public List<ProductContentSubstancesInfo> getProductContentSubstances(int activeSubstanceId)
        {
            DbCommand dbCmd = ProductContentSubstancesDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetProductContentSubstances");

            ProductContentSubstancesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@activeSubstanceId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, activeSubstanceId);

            List<ProductContentSubstancesInfo> BECollection = new List<ProductContentSubstancesInfo>();

            // Get the resultset from the stored procedure:
            using (IDataReader dataReader = ProductContentSubstancesDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                // Iterates through records:
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }
            return BECollection;
        }

        public List<ProductContentSubstancesInfo> getProductContentSubstances(int productId, int activeSubstanceId)
        {
            DbCommand dbCmd = ProductContentSubstancesDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetProductContentSubstances");

            ProductContentSubstancesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
            ProductContentSubstancesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@activeSubstanceId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, activeSubstanceId);

            List<ProductContentSubstancesInfo> BECollection = new List<ProductContentSubstancesInfo>();

            // Get the resultset from the stored procedure:
            using (IDataReader dataReader = ProductContentSubstancesDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                // Iterates through records:
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }
            return BECollection;
        }

        #endregion

        #region Protected methods

        protected override ProductContentSubstancesInfo getFromDataReader(IDataReader current)
        {
            ProductContentSubstancesInfo record = new ProductContentSubstancesInfo();

            record.ProductContentId = Convert.ToInt32(current["ProductContentId"]);
            record.ProductId = Convert.ToInt32(current["ProductId"]);
            record.ActiveSubstanceId = Convert.ToInt32(current["ActiveSubstanceId"]);

            return record;
        }

        #endregion

        public static readonly ProductContentSubstancesDALC Instance = new ProductContentSubstancesDALC();

    }
}
