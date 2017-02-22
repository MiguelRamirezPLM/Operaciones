using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using AgroBusinessEntries;

namespace AgroDataAccessComponent
{
    public sealed class ProductCategoriesDALC : AgroDataAccessAdapter<ProductCategoriesInfo>
    {
        #region Constructors

        private ProductCategoriesDALC() { }

        #endregion

        #region Public Methods

        public bool assigned(int productId, int pharmaFormId, int divisionId, int categoryId)
        {
            DbCommand dbCmd = ProductCategoriesDALC.AgroInstanceDatabase.GetStoredProcCommand("dbo.plm_spProductCategories");

            ProductCategoriesDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            ProductCategoriesDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
            ProductCategoriesDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pharmaFormId);
            ProductCategoriesDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, divisionId);
            ProductCategoriesDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, categoryId);

            ProductCategoriesDALC.AgroInstanceDatabase.ExecuteNonQuery(dbCmd);

            return Convert.ToInt32(dbCmd.Parameters["@Return"].Value) > 0;
        }

        public override int insert(ProductCategoriesInfo businessEntity)
        {
            DbCommand dbCmd = ProductCategoriesDALC.AgroInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDProductCategories");

            ProductCategoriesDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            ProductCategoriesDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Create);
            ProductCategoriesDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.DivisionId);
            ProductCategoriesDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.CategoryId);
            ProductCategoriesDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.PharmaFormId);
            ProductCategoriesDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ProductId);
            ProductCategoriesDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@productDescription", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ProductDescription);
            ProductCategoriesDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@technicalSheet", DbType.Boolean,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.TechnicalSheet);
            
            ProductCategoriesDALC.AgroInstanceDatabase.ExecuteNonQuery(dbCmd);

            return Convert.ToInt32(dbCmd.Parameters["@Return"].Value);
        }

        public override void delete(ProductCategoriesInfo businessEntity)
        {
            DbCommand dbCmd = ProductCategoriesDALC.AgroInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDProductCategories");

            // Add the parameters:
            ProductCategoriesDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Delete);
            ProductCategoriesDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.DivisionId);
            ProductCategoriesDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.CategoryId);
            ProductCategoriesDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.PharmaFormId);
            ProductCategoriesDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ProductId);

            //Delete record:
            ProductCategoriesDALC.AgroInstanceDatabase.ExecuteNonQuery(dbCmd);

        }

        public override void update(ProductCategoriesInfo businessEntity)
        {
            DbCommand dbCmd = ProductCategoriesDALC.AgroInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDProductCategories");

            // Add the parameters:
            ProductCategoriesDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Update);
            ProductCategoriesDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.DivisionId);
            ProductCategoriesDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.CategoryId);
            ProductCategoriesDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.PharmaFormId);
            ProductCategoriesDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ProductId);
            ProductCategoriesDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@productDescription", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ProductDescription);
            ProductCategoriesDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@technicalSheet", DbType.Boolean,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.TechnicalSheet);

            //Update record:
            ProductCategoriesDALC.AgroInstanceDatabase.ExecuteNonQuery(dbCmd);
        }

        public ProductCategoriesInfo getOne(int productId, int pharmaFormId, int divisionId, int categoryId)
        {
            DbCommand dbCmd = ProductCategoriesDALC.AgroInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDProductCategories");

            // Add the parameters:
            ProductCategoriesDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Read);
            ProductCategoriesDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
            ProductCategoriesDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pharmaFormId);
            ProductCategoriesDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, divisionId);
            ProductCategoriesDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, categoryId);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = ProductCategoriesDALC.AgroInstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);
                else
                    return null;
            }
        }

        #endregion

        #region Protected Methods

        protected override ProductCategoriesInfo getFromDataReader(IDataReader current)
        {
            ProductCategoriesInfo record = new ProductCategoriesInfo();

            record.DivisionId = Convert.ToInt32(current["DivisionId"]);
            record.CategoryId = Convert.ToInt32(current["CategoryId"]);
            record.PharmaFormId = Convert.ToInt32(current["PharmaFormId"]);
            record.ProductId = Convert.ToInt32(current["ProductId"]);
         //   record.TechnicalSheet = Convert.ToBoolean(current["TechnicalSheet"]);

            return record;
        }

        #endregion

        public static readonly ProductCategoriesDALC Instance = new ProductCategoriesDALC();

    }
}
