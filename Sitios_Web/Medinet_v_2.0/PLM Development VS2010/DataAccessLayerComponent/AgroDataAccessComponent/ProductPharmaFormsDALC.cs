using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;

using AgroBusinessEntries;

namespace AgroDataAccessComponent
{
    public sealed class ProductPharmaFormsDALC : AgroDataAccessAdapter<ProductPharmaFormInfo>
    {
        #region Constructors

        private ProductPharmaFormsDALC() { }

        #endregion

        #region Public methods

        public override int insert(ProductPharmaFormInfo businessEntity)
        {
            DbCommand dbCmd = ProductPharmaFormsDALC.AgroInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDProductPharmaForms");

            ProductPharmaFormsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            ProductPharmaFormsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, ProductPharmaFormsDALC.CRUD.Create);
            ProductPharmaFormsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.PharmaFormId);
            ProductPharmaFormsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ProductId);
            ProductPharmaFormsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@active", DbType.Boolean,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Active);

            ProductPharmaFormsDALC.AgroInstanceDatabase.ExecuteNonQuery(dbCmd);

            return Convert.ToInt32(dbCmd.Parameters["@Return"].Value); 
        }

        public List<ProductPharmaFormInfo> getAllByProduct(int productId)
        {
            List<ProductPharmaFormInfo> BECollection = new List<ProductPharmaFormInfo>();

            // Get the resultset from the stored procedure:
            using (IDataReader dataReader = ProductPharmaFormsDALC.AgroInstanceDatabase.ExecuteReader(
                ProductPharmaFormsDALC.AgroInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetPharmaFormByProduct", productId)))
            {
                // Iterates through records:
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            return BECollection;   
        }

        public ProductPharmaFormInfo getOne(int productId, int pharmaFormId)
        {
            DbCommand dbCmd = ProductPharmaFormsDALC.AgroInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDProductPharmaForms");

            // Add the parameters:
            ProductPharmaFormsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Read);
            ProductPharmaFormsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pharmaFormId);
            ProductPharmaFormsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = ProductsDALC.AgroInstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);
                else
                    return null;
            }

        }

        public override void delete(ProductPharmaFormInfo businessEntity)
        {
            DbCommand dbCmd = ProductPharmaFormsDALC.AgroInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDProductPharmaForms");

            ProductPharmaFormsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            ProductPharmaFormsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, ProductPharmaFormsDALC.CRUD.Delete);
            ProductPharmaFormsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.PharmaFormId);
            ProductPharmaFormsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ProductId);

            ProductPharmaFormsDALC.AgroInstanceDatabase.ExecuteNonQuery(dbCmd);
        }

        public override void update(ProductPharmaFormInfo businessEntity)
        {
            ProductsDALC.AgroInstanceDatabase.ExecuteNonQuery("dbo.plm_spCRUDProductPharmaForms",
                    CRUD.Update,
                    businessEntity.PharmaFormId,
                    businessEntity.ProductId,
                    businessEntity.Active); 
        }
        
        #endregion

        #region Protected methods

        protected override ProductPharmaFormInfo getFromDataReader(System.Data.IDataReader current)
        {
            ProductPharmaFormInfo record = new ProductPharmaFormInfo();

            record.PharmaFormId = Convert.ToInt32(current["PharmaFormId"]);
            record.ProductId = Convert.ToInt32(current["ProductId"]);
            record.Active = Convert.ToBoolean(current["Active"]);
            
            return record;
        }

        #endregion

        public static readonly ProductPharmaFormsDALC Instance = new ProductPharmaFormsDALC();
    }
}
