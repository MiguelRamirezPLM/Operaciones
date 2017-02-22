using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using MedinetBusinessEntries;
using PharmaSearchEngineBusinessEntries;

namespace PharmaSearchEngineDataAccessComponent
{
    public sealed class PresentationsDALC : PharmaSearchEngineDataAccessAdapter<PharmaSearchEngineBusinessEntries.PresentationByProductInfo>
    {
        #region Constructors

        private PresentationsDALC() { }

        #endregion

        #region Public Methods

        public List<PharmaSearchEngineBusinessEntries.PresentationByProductInfo> getPresentationByProduct(int editionId, int divisionId, int categoryId, int productId, int pharmaFormId)
        {
            DbCommand dbCmd = PresentationsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetPSEPresentationsProduct");

            // Add the parameters:
            PresentationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            PresentationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, divisionId);
            PresentationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, categoryId);
            PresentationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
            PresentationsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pharmaFormId);

            List<PharmaSearchEngineBusinessEntries.PresentationByProductInfo> BECollection = new List<PharmaSearchEngineBusinessEntries.PresentationByProductInfo>();

            // Get the resultset from the stored procedure:
            using (IDataReader dataReader = PresentationsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                PharmaSearchEngineBusinessEntries.PresentationByProductInfo record;

                // Iterates through records:
                while (dataReader.Read())
                {
                    record = new PharmaSearchEngineBusinessEntries.PresentationByProductInfo();

                    record.PresentationId = Convert.ToInt32(dataReader["PresentationId"]);
                    if (dataReader["ExternalPackId"] != DBNull.Value)
                        record.ExternalPackId = Convert.ToInt32(dataReader["ExternalPackId"]);
                    if (dataReader["QtyExternalPack"] != DBNull.Value)
                        record.QtyExternalPack = Convert.ToInt32(dataReader["QtyExternalPack"]);
                    record.ExternalPackName = dataReader["ExternalPackName"].ToString();
                    if (dataReader["InternalPackId"] != DBNull.Value)
                        record.InternalPackId = Convert.ToInt32(dataReader["InternalPackId"]);
                    if (dataReader["QtyInternalPack"] != DBNull.Value)
                        record.QtyInternalPack = Convert.ToInt32(dataReader["QtyInternalPack"]);
                    record.InternalPackName = dataReader["InternalPackName"].ToString();
                    if (dataReader["ContentUnitId"] != DBNull.Value)
                        record.ContentUnitId = Convert.ToInt32(dataReader["ContentUnitId"]);
                    record.QtyContentUnit = dataReader["QtyContentUnit"].ToString();
                    record.ContentUnitName = dataReader["UnitName"].ToString();
                    if (dataReader["WeightUnitId"] != DBNull.Value)
                        record.WeightUnitId = Convert.ToInt32(dataReader["WeightUnitId"]);
                    record.QtyWeightUnit = dataReader["QtyWeightUnit"].ToString();
                    record.WeightShortName = dataReader["ShortName"].ToString();

                    BECollection.Add(record);

                }
            }

            return BECollection;
        }

        #endregion
       
        public static readonly PresentationsDALC Instance = new PresentationsDALC();
    }
}
