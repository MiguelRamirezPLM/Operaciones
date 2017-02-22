using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;

using MedinetBusinessEntries;

namespace MedinetDataAccessComponent
{
    public sealed class ProductIndicationsDALC : MedinetDataAccessAdapter<ProductIndicationInfo>
    {
        #region Constructors

        private ProductIndicationsDALC() { }

        #endregion

        #region Public methods

        public ProductIndicationInfo check(ProductIndicationInfo businessEntity)
        {
            DbCommand dbCmd = ProductIndicationsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDProductIndications");

            // Add the parameters:
            ProductIndicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Read);
            ProductIndicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ProductId);
            ProductIndicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@indicationId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.IndicationId);


            // Get the result set from the stored procedure:
            using (IDataReader dataReader = ProductIndicationsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);
                else
                    return null;
            }
        }
        
        public override int insert(ProductIndicationInfo businessEntity)
        {
            DbCommand dbCmd = ProductIndicationsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDProductIndications");

            ProductIndicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            ProductIndicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, ProductIndicationsDALC.CRUD.Create);
            ProductIndicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ProductId);
            ProductIndicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@indicationId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.IndicationId);

            ProductIndicationsDALC.MedInstanceDatabase.ExecuteNonQuery(dbCmd);

            return Convert.ToInt32(dbCmd.Parameters["@Return"].Value); 
        }
      
        public override void delete(ProductIndicationInfo businessEntity)
        {
            DbCommand dbCmd = ProductIndicationsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDProductIndications");

            ProductIndicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            ProductIndicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, ProductIndicationsDALC.CRUD.Delete);
            ProductIndicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ProductId);
            ProductIndicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@indicationId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.IndicationId);

            ProductIndicationsDALC.MedInstanceDatabase.ExecuteNonQuery(dbCmd);
        }

        public List<ProductIndicationInfo> getProductIndications(int indicationId)
        {
            DbCommand dbCmd = ProductIndicationsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetProductIndications");

            ProductIndicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@indicationId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, indicationId);

            List<ProductIndicationInfo> BECollection = new List<ProductIndicationInfo>();

            // Get the resultset from the stored procedure:
            using (IDataReader dataReader = ProductIndicationsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                // Iterates through records:
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }
            return BECollection;
        }

        public ProductIndicationInfo getProductIndication(int productId, int indicationId)
        {
            DbCommand dbCmd = ProductIndicationsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetProductIndications");

            ProductIndicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
            ProductIndicationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@indicationId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, indicationId);

            // Get the resultset from the stored procedure:
            using (IDataReader dataReader = ProductIndicationsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);
                else
                    return null;
            }
        }

        #endregion

        #region Protected methods

        protected override ProductIndicationInfo getFromDataReader(IDataReader current)
        {
            ProductIndicationInfo record = new ProductIndicationInfo();

            record.ProductId = Convert.ToInt32(current["productId"]);
            record.IndicationId = Convert.ToInt32(current["IndicationId"]);

            return record;
        }

        #endregion

        public static readonly ProductIndicationsDALC Instance = new ProductIndicationsDALC();
    }
}
