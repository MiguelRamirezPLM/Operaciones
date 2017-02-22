using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using MedinetBusinessEntries;

namespace MedinetDataAccessComponent
{
    public class ProductInteractionsDALC : MedinetDataAccessAdapter<ProductInteractionsInfo>
    {

        #region Constructors

        private ProductInteractionsDALC() { }

        #endregion

        #region Public Methods

        public override int insert(MedinetBusinessEntries.ProductInteractionsInfo businessEntity)
        {
            DbCommand dbCmd = ProductInteractionsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDProductInteractions");

            ProductInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            ProductInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Create);
            ProductInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productContentId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ProductContentId);
            ProductInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ProductId);
            ProductInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@activeSubstanceId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ActiveSubstanceId);
            ProductInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@substanceInteractId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.SubstanceInteractId);

            ProductInteractionsDALC.MedInstanceDatabase.ExecuteNonQuery(dbCmd);

            return Convert.ToInt32(dbCmd.Parameters["@Return"].Value);
        }

        public override void delete(ProductInteractionsInfo businessEntity)
        {
            DbCommand dbCmd = ProductInteractionsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDProductInteractions");

            ProductInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            ProductInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Delete);
            ProductInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productContentId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ProductContentId);
            ProductInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ProductId);
            ProductInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@activeSubstanceId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ActiveSubstanceId);
            ProductInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@substanceInteractId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.SubstanceInteractId);

            ProductInteractionsDALC.MedInstanceDatabase.ExecuteNonQuery(dbCmd);
        }

        public List<ProductInteractionsInfo> getProductInteractions(int productId, int activeSubstanceId)
        {
            DbCommand dbCmd = ProductInteractionsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetProductInteractions");

            ProductInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
            ProductInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@activeSubstanceId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, activeSubstanceId);

            List<ProductInteractionsInfo> BECollection = new List<ProductInteractionsInfo>();

            // Get the resultset from the stored procedure:
            using (IDataReader dataReader = ProductInteractionsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                // Iterates through records:
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }
            return BECollection;
        }

        public List<ProductInteractionsInfo> getProductInteractions(int activeSubstanceId)
        {
            DbCommand dbCmd = ProductInteractionsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetProductInteractions");

            ProductInteractionsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@activeSubstanceId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, activeSubstanceId);

            List<ProductInteractionsInfo> BECollection = new List<ProductInteractionsInfo>();

            // Get the resultset from the stored procedure:
            using (IDataReader dataReader = ProductInteractionsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                // Iterates through records:
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }
            return BECollection;
        }

        #endregion

        #region Protected methods

        protected override ProductInteractionsInfo getFromDataReader(IDataReader current)
        {
            ProductInteractionsInfo record = new ProductInteractionsInfo();

            record.ProductContentId = Convert.ToInt32(current["ProductContentId"]);
            record.ProductId = Convert.ToInt32(current["ProductId"]);
            record.ActiveSubstanceId = Convert.ToInt32(current["ActiveSubstanceId"]);
            record.SubstanceInteractId = Convert.ToInt32(current["SubstanceInteractId"]);

            return record;
        }

        #endregion

        public static readonly ProductInteractionsDALC Instance = new ProductInteractionsDALC();

    }
}
