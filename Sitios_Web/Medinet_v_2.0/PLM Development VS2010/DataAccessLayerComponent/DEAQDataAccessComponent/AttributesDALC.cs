using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data;

namespace DEAQDataAccessComponent
{
    public sealed class AttributesDALC : DEAQEngineDataAccessAdapter<MedinetBusinessEntries.AttributesInfo>
    {
         #region Constructor

        private AttributesDALC() { }

        #endregion

        #region Methods

        public List<PharmaSearchEngineBusinessEntries.AttributeDetailInfo> getCompleteAttributes(string isbn, int divisionId, int categoryId, int productId, int pharmaFormId)
        {
            DbCommand dbCmd = AttributesDALC.DEAQInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetCompleteAttributes");

            // Add the parameters:
            AttributesDALC.DEAQInstanceDatabase.AddParameter(dbCmd, "@isbn", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, isbn);
            AttributesDALC.DEAQInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, divisionId);
            AttributesDALC.DEAQInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, categoryId);
            AttributesDALC.DEAQInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
            AttributesDALC.DEAQInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pharmaFormId);
            
            List<PharmaSearchEngineBusinessEntries.AttributeDetailInfo> BECollection = new List<PharmaSearchEngineBusinessEntries.AttributeDetailInfo>();

            // Get the resultset from the stored procedure:
            using (IDataReader dataReader = AttributesDALC.DEAQInstanceDatabase.ExecuteReader(dbCmd))
            {
                PharmaSearchEngineBusinessEntries.AttributeDetailInfo record;

                // Iterates through records:
                while (dataReader.Read())
                {
                    record = new PharmaSearchEngineBusinessEntries.AttributeDetailInfo();

                    record.EditionId = Convert.ToInt32(dataReader["EditionId"]);
                    record.DivisionId = Convert.ToInt32(dataReader["DivisionId"]);
                    record.CategoryId = Convert.ToInt32(dataReader["CategoryId"]);
                    record.ProductId = Convert.ToInt32(dataReader["ProductId"]);
                    record.PharmaFormId = Convert.ToInt32(dataReader["PharmaFormId"]);
                    record.AttributeId = Convert.ToInt32(dataReader["AttributeGroupId"]);
                    record.AttributeName = dataReader["AttributeGroupName"].ToString();
                    record.AttributeContent = dataReader["AttributePlainContent"].ToString();
                    record.HTMLContent = dataReader["AttributeHTMLContent"].ToString();
                    record.AttributeGroupKey = dataReader["AttributeGroupKey"].ToString();

                    BECollection.Add(record);
                }
            }
            return BECollection;
        }

        #endregion
        public static readonly AttributesDALC Instance = new AttributesDALC();
    }
}
