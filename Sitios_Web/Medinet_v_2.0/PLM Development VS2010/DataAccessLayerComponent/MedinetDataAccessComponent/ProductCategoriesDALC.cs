using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using MedinetBusinessEntries;

namespace MedinetDataAccessComponent
{
    public sealed class ProductCategoriesDALC : MedinetDataAccessAdapter<ProductCategoriesInfo>
    {
        #region Constructors

        private ProductCategoriesDALC() { }

        #endregion

        #region Public Methods

        public bool assigned(int productId, int pharmaFormId, int divisionId, int categoryId)
        {
            DbCommand dbCmd = ProductCategoriesDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spProductCategories");

            ProductCategoriesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            ProductCategoriesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
            ProductCategoriesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pharmaFormId);
            ProductCategoriesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, divisionId);
            ProductCategoriesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, categoryId);

            ProductCategoriesDALC.MedInstanceDatabase.ExecuteNonQuery(dbCmd);

            return Convert.ToInt32(dbCmd.Parameters["@Return"].Value) > 0;
        }

        public override int insert(ProductCategoriesInfo businessEntity)
        {
            DbCommand dbCmd = ProductCategoriesDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDProductCategories");

            ProductCategoriesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            ProductCategoriesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Create);
            ProductCategoriesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.DivisionId);
            ProductCategoriesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.CategoryId);
            ProductCategoriesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.PharmaFormId);
            ProductCategoriesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ProductId);
            ProductCategoriesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productDescription", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ProductDescription);
            ProductCategoriesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@technicalSheet", DbType.Boolean,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.TechnicalSheet);
            
            ProductCategoriesDALC.MedInstanceDatabase.ExecuteNonQuery(dbCmd);

            return Convert.ToInt32(dbCmd.Parameters["@Return"].Value);
        }

        public override void delete(ProductCategoriesInfo businessEntity)
        {
            DbCommand dbCmd = ProductCategoriesDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDProductCategories");

            // Add the parameters:
            ProductCategoriesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Delete);
            ProductCategoriesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.DivisionId);
            ProductCategoriesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.CategoryId);
            ProductCategoriesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.PharmaFormId);
            ProductCategoriesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ProductId);

            //Delete record:
            ProductCategoriesDALC.MedInstanceDatabase.ExecuteNonQuery(dbCmd);

        }

        public override void update(ProductCategoriesInfo businessEntity)
        {
            DbCommand dbCmd = ProductCategoriesDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDProductCategories");

            // Add the parameters:
            ProductCategoriesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Update);
            ProductCategoriesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.DivisionId);
            ProductCategoriesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.CategoryId);
            ProductCategoriesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.PharmaFormId);
            ProductCategoriesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ProductId);
            ProductCategoriesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productDescription", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ProductDescription);
            ProductCategoriesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@technicalSheet", DbType.Boolean,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.TechnicalSheet);

            //Update record:
            ProductCategoriesDALC.MedInstanceDatabase.ExecuteNonQuery(dbCmd);
        }

        public ProductCategoriesInfo getOne(int productId, int pharmaFormId, int divisionId, int categoryId)
        {
            DbCommand dbCmd = ProductCategoriesDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDProductCategories");

            // Add the parameters:
            ProductCategoriesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Read);
            ProductCategoriesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
            ProductCategoriesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pharmaFormId);
            ProductCategoriesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, divisionId);
            ProductCategoriesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, categoryId);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = ProductCategoriesDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
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
            record.TechnicalSheet = Convert.ToBoolean(current["TechnicalSheet"]);

            return record;
        }

        #endregion

        public static readonly ProductCategoriesDALC Instance = new ProductCategoriesDALC();

    }
}
