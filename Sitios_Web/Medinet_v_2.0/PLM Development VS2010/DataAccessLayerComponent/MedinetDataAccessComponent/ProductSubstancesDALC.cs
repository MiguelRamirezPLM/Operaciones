using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;

using MedinetBusinessEntries;

namespace MedinetDataAccessComponent
{
    public sealed class ProductSubstancesDALC : MedinetDataAccessAdapter<ProductSubstanceInfo>
    {
        #region Constructors

        private ProductSubstancesDALC() { }

        #endregion

        #region Public methods

        public override int insert(ProductSubstanceInfo businessEntity)
        {
            DbCommand dbCmd = ProductSubstancesDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDProductSubstance");

            ProductSubstancesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            ProductSubstancesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, ProductSubstancesDALC.CRUD.Create);
            ProductSubstancesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ProductId);
            ProductSubstancesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@activeSubstanceId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ActiveSubstanceId);

            ProductSubstancesDALC.MedInstanceDatabase.ExecuteNonQuery(dbCmd);

            return Convert.ToInt32(dbCmd.Parameters["@Return"].Value);
        }

        public override void delete(ProductSubstanceInfo businessEntity)
        {
            DbCommand dbCmd = ProductSubstancesDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDProductSubstance");

            ProductSubstancesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            ProductSubstancesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, ProductSubstancesDALC.CRUD.Delete);
            ProductSubstancesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ProductId);
            ProductSubstancesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@activeSubstanceId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ActiveSubstanceId);

            ProductSubstancesDALC.MedInstanceDatabase.ExecuteNonQuery(dbCmd);
        }

        public ProductSubstanceInfo getProductSubstance(int productId, int activeSubstanceId)
        {
            DbCommand dbCmd = ProductSubstancesDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetProductSubstances");

            ProductSubstancesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
            ProductSubstancesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@activeSubstanceId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, activeSubstanceId);

            // Get the resultset from the stored procedure:
            using (IDataReader dataReader = ProductSubstancesDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);
                else
                    return null;
            }
        }

        public List<ProductSubstanceInfo> getProductSubstances(int activeSubstanceId)
        {
            DbCommand dbCmd = ProductSubstancesDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetProductSubstances");

            ProductSubstancesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@activeSubstanceId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, activeSubstanceId);

            List<ProductSubstanceInfo> BECollection = new List<ProductSubstanceInfo>();

            // Get the resultset from the stored procedure:
            using (IDataReader dataReader = ProductSubstancesDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                // Iterates through records:
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }
            return BECollection;
        }

        public bool checkSubstance(int activeSubstanceId)
        {
            DbCommand dbCmd = ProductSubstancesDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCheckSubstance");

            ProductSubstancesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            ProductSubstancesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@activeSubstanceId", DbType.Int32,
                            ParameterDirection.Input, string.Empty, DataRowVersion.Current, activeSubstanceId);

            ProductSubstancesDALC.MedInstanceDatabase.ExecuteNonQuery(dbCmd);

            return Convert.ToInt32(dbCmd.Parameters["@Return"].Value) > 0;

        }

        public bool checkSubstance(int productId, int activeSubstanceId)
        {
            DbCommand dbCmd = ProductSubstancesDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCheckSubstance");

            ProductSubstancesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            ProductSubstancesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
            ProductSubstancesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@activeSubstanceId", DbType.Int32,
                            ParameterDirection.Input, string.Empty, DataRowVersion.Current, activeSubstanceId);

            ProductSubstancesDALC.MedInstanceDatabase.ExecuteNonQuery(dbCmd);

            return Convert.ToInt32(dbCmd.Parameters["@Return"].Value) > 0;

        }

        #endregion

        #region Protected methods

        protected override ProductSubstanceInfo getFromDataReader(IDataReader current)
        {
            ProductSubstanceInfo record = new ProductSubstanceInfo();

            record.ProductId = Convert.ToInt32(current["ProductId"]);
            record.ActiveSubstanceId = Convert.ToInt32(current["ActiveSubstanceId"]);

            return record;
        }

        #endregion

        public static readonly ProductSubstancesDALC Instance = new ProductSubstancesDALC();
    }
}
