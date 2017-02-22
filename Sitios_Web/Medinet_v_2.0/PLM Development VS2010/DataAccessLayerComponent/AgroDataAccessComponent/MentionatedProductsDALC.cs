using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using AgroBusinessEntries;

namespace AgroDataAccessComponent
{
    public sealed class MentionatedProductsDALC : AgroDataAccessAdapter<MentionatedProductInfo>
    {
        #region Constructors

        private MentionatedProductsDALC() { }

        #endregion

        #region Public Methods

        public bool participate(int productId, int editionId, int pharmaFormId, int divisionId, int categoryId)
        {
            DbCommand dbCmd = MentionatedProductsDALC.AgroInstanceDatabase.GetStoredProcCommand("dbo.plm_spMentionatedProduct");

            MentionatedProductsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            MentionatedProductsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
            MentionatedProductsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            MentionatedProductsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pharmaFormId);
            MentionatedProductsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, divisionId);
            MentionatedProductsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, categoryId);

            MentionatedProductsDALC.AgroInstanceDatabase.ExecuteNonQuery(dbCmd);

            return Convert.ToInt32(dbCmd.Parameters["@Return"].Value) > 0;
        }

        public override int insert(MentionatedProductInfo businessEntity)
        {

            DbCommand dbCmd = MentionatedProductsDALC.AgroInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDMentionatedProducts");

            MentionatedProductsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            MentionatedProductsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Create);
            MentionatedProductsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ProductId);
            MentionatedProductsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.EditionId);
            MentionatedProductsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.PharmaFormId);
            MentionatedProductsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.DivisionId);
            MentionatedProductsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.CategoryId);

            MentionatedProductsDALC.AgroInstanceDatabase.ExecuteNonQuery(dbCmd);

            return Convert.ToInt32(dbCmd.Parameters["@Return"].Value);
        }

        public MentionatedProductInfo getOne(int productId, int editionId, int pharmaFormId, int divisionId, int categoryId)
        {
            DbCommand dbCmd = MentionatedProductsDALC.AgroInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDMentionatedProducts");

            // Add the parameters:
            MentionatedProductsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Read);
            MentionatedProductsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
            MentionatedProductsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            MentionatedProductsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pharmaFormId);
            MentionatedProductsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, divisionId);
            MentionatedProductsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, categoryId);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = MentionatedProductsDALC.AgroInstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);
                else
                    return null;
            }

        }

        public override void delete(MentionatedProductInfo businessEntity)
        {
            DbCommand dbCmd = MentionatedProductsDALC.AgroInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDMentionatedProducts");

            // Add the parameters:
            MentionatedProductsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Delete);
            MentionatedProductsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ProductId);
            MentionatedProductsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.EditionId);
            MentionatedProductsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.PharmaFormId);
            MentionatedProductsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.DivisionId);
            MentionatedProductsDALC.AgroInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.CategoryId);

            //Delete record:
            MentionatedProductsDALC.AgroInstanceDatabase.ExecuteNonQuery(dbCmd);
        }


        #endregion

        #region Protected Methods

        protected override MentionatedProductInfo getFromDataReader(IDataReader current)
        {
            MentionatedProductInfo record = new MentionatedProductInfo();

            record.ProductId = Convert.ToInt32(current["ProductId"]);
            record.EditionId = Convert.ToInt32(current["EditionId"]);
            record.PharmaFormId = Convert.ToInt32(current["PharmaFormId"]);
            record.DivisionId = Convert.ToInt32(current["DivisionId"]);
            record.CategoryId = Convert.ToInt32(current["CategoryId"]);

            return record;
        }

        #endregion


        public static readonly MentionatedProductsDALC Instance = new MentionatedProductsDALC();
    }
}
