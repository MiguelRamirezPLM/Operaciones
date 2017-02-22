using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using MedinetBusinessEntries;

namespace MedinetDataAccessComponent
{
    public class ProductContentsDALC : MedinetDataAccessAdapter<ProductContentsInfo>
    {

        #region Constructors

        private ProductContentsDALC() { }

        #endregion

        #region Public Methods

        public List<MedinetBusinessEntries.ProductContentsInfo> getProductContents(int editionId, int divisionId, int categoryId, int productId, int pharmaFormId)
        {
            List<MedinetBusinessEntries.ProductContentsInfo> BECollection = new List<ProductContentsInfo>();

            DbCommand dbCmd = ProductContentsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDProductContents");

            ProductContentsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Read);
            ProductContentsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            ProductContentsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, divisionId);
            ProductContentsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, categoryId);
            ProductContentsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
            ProductContentsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pharmaFormId);

            // Get the resultset from the stored procedure:
            using (IDataReader dataReader = ProductContentsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                // Iterates through records:
                while (dataReader.Read())
                {
                    BECollection.Add(getFromDataReader(dataReader));
                }
            }
            return BECollection;
        }

        #endregion

        #region Protected Methods

        protected override ProductContentsInfo getFromDataReader(IDataReader current)
        {
            ProductContentsInfo record = new ProductContentsInfo();

            record.ProductContentId = Convert.ToInt32(current["ProductContentId"]);
            record.EditionId = Convert.ToInt32(current["EditionId"]);
            record.DivisionId = Convert.ToInt32(current["DivisionId"]);
            record.CategoryId = Convert.ToInt32(current["CategoryId"]);
            record.ProductId = Convert.ToInt32(current["ProductId"]);
            record.PharmaFormId = Convert.ToInt32(current["PharmaFormId"]);
            record.AttributeId = Convert.ToInt32(current["AttributeId"]);

            if (current["Content"] != System.DBNull.Value)
                record.Content = current["Content"].ToString();

            if (current["PlainContent"] != System.DBNull.Value)
                record.PlainContent = current["PlainContent"].ToString();

            if (current["HTMLContent"] != System.DBNull.Value)
                record.HTMLContent = current["HTMLContent"].ToString();

            return record;
        }

        #endregion

        public static readonly ProductContentsDALC Instance = new ProductContentsDALC();

    }
}
