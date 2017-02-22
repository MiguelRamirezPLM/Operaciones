using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using AgroBusinessEntries;

namespace AgroDataAccessComponent
{
    public sealed class EditionDivisionProductsDALC : AgroDataAccessAdapter<EditionDivisionProductInfo>
    {

        #region Constructors

        private EditionDivisionProductsDALC() { }

        #endregion

        #region Public Methods

        public override int insert(EditionDivisionProductInfo businessEntity)
        {
            DbCommand dbCmd = EditionDivisionProductsDALC.AgroInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDEditionDivisionProducts");

            EditionDivisionProductsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            EditionDivisionProductsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Create);
            EditionDivisionProductsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.EditionId);
            EditionDivisionProductsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ProductId);
            EditionDivisionProductsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.PharmaFormId);
            EditionDivisionProductsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.DivisionId);
            EditionDivisionProductsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.CategoryId);

            EditionDivisionProductsDALC.AgroInstanceDatabase.ExecuteNonQuery(dbCmd);

            return Convert.ToInt32(dbCmd.Parameters["@Return"].Value);
        }

        public override void delete(EditionDivisionProductInfo businessEntity)
        {
            DbCommand dbCmd = EditionDivisionProductsDALC.AgroInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDEditionDivisionProducts");

            EditionDivisionProductsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Delete);
            EditionDivisionProductsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.EditionId);
            EditionDivisionProductsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ProductId);
            EditionDivisionProductsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.PharmaFormId);
            EditionDivisionProductsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.DivisionId);
            EditionDivisionProductsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.CategoryId);

            EditionDivisionProductsDALC.AgroInstanceDatabase.ExecuteNonQuery(dbCmd);
        }

        public EditionDivisionProductInfo getOne(int editionId, int divisionId, int categoryId, int productId, int pharmaFormId)
        {
            DbCommand dbCmd = EditionDivisionProductsDALC.AgroInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDEditionDivisionProducts");

            EditionDivisionProductsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Read);
            EditionDivisionProductsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            EditionDivisionProductsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
            EditionDivisionProductsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pharmaFormId);
            EditionDivisionProductsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, divisionId);
            EditionDivisionProductsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, categoryId);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = EditionDivisionProductsDALC.AgroInstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);
                else
                    return null;
            }
        }

        public bool checkEdiDivProd(int editionId, int divisionId, int categoryId, int productId, int pharmaFormId)
        {
            DbCommand dbCmd = EditionDivisionProductsDALC.AgroInstanceDatabase.GetStoredProcCommand("dbo.plm_spCheckEditionDivisionProducts");

            EditionDivisionProductsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            EditionDivisionProductsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            EditionDivisionProductsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
            EditionDivisionProductsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pharmaFormId);
            EditionDivisionProductsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, divisionId);
            EditionDivisionProductsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, categoryId);

            EditionDivisionProductsDALC.AgroInstanceDatabase.ExecuteNonQuery(dbCmd);
            
            return Convert.ToInt32(dbCmd.Parameters["@Return"].Value) > 0;
        }

        #endregion

        #region Protected Methods
            
        protected override EditionDivisionProductInfo getFromDataReader(IDataReader current)
        {
            EditionDivisionProductInfo record = new EditionDivisionProductInfo();

            record.EditionId = Convert.ToInt32(current["EditionId"]);
            record.DivisionId = Convert.ToInt32(current["DivisionId"]);
            record.CategoryId = Convert.ToInt32(current["CategoryId"]);
            record.ProductId = Convert.ToInt32(current["ProductId"]);
            record.PharmaFormId = Convert.ToInt32(current["PharmaFormId"]);

            return record;
        }

        #endregion

        public static readonly EditionDivisionProductsDALC Instance = new EditionDivisionProductsDALC();
    }
}
