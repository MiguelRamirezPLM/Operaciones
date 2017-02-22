using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PharmaSearchEngineDataAccessComponent
{
    class PrsentationsDALC
    {
        #region Constructors

        private PrsentationsDALC() { }

        #endregion

        #region Public Methods

        public List<PharmaSearchEngineBusinessEntries.PresentationsByProductInfo> getByProduct(int editionId, int divisionId, int categoryId, int productId, int pharmaFormId)
        {
            DbCommand dbCmd = PrsentationsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetPSEPresentationsProduct");

            // Add the parameters:
            PrsentationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            PrsentationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, divisionId);
            PrsentationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, categoryId);
            PrsentationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
            PrsentationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pharmaFormId);

            List<PharmaSearchEngineBusinessEntries.PresentationsByProductInfo> BECollection = new List<PharmaSearchEngineBusinessEntries.PresentationsByProductInfo>();

            // Get the resultset from the stored procedure:
            using (IDataReader dataReader = PrsentationsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                PharmaSearchEngineBusinessEntries.PresentationsByProductInfo record;

                // Iterates through records:
                while (dataReader.Read())
                {
                    record = new PharmaSearchEngineBusinessEntries.PresentationsByProductInfo();

                    record.EditionId = Convert.ToInt32(dataReader["PresentationId"]);
                    record.EditionId = Convert.ToInt32(dataReader["ExternalPackId"]);
                    record.AttributeId = Convert.ToInt32(dataReader["QtyExternalPack"]);
                    record.AttributeName = dataReader["ExternalPackName"].ToString();
                    record.EditionId = Convert.ToInt32(dataReader["InternalPackId"]);
                    record.AttributeId = Convert.ToInt32(dataReader["QtyInternalPack"]);
                    record.AttributeName = dataReader["InternalPackName"].ToString();
                    record.EditionId = Convert.ToInt32(dataReader["ContentUnitId"]);
                    record.AttributeName = dataReader["QtyContentUnit"].ToString();
                    record.AttributeName = dataReader["UnitName"].ToString();
                    record.EditionId = Convert.ToInt32(dataReader["WeightUnitId"]);
                    record.AttributeName = dataReader["QtyWeightUnit"].ToString();
                    record.AttributeName = dataReader["ShortName"].ToString();

                    BECollection.Add(record);

                }
            }

            return BECollection;
        }

        #endregion
    }
}
