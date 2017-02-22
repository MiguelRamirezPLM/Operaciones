using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using MedinetBusinessEntries;
using PharmaSearchEngineBusinessEntries;

namespace PharmaSearchEngineDataAccessComponent
{
    public class OtherElementsDALC : PharmaSearchEngineDataAccessAdapter<OtherElementsInfo>
    {

        #region Constructors

        private OtherElementsDALC() { }

        #endregion

        #region Public Methods

        public List<OtherElementsInfo> getByProduct(int editionId, int divisionId, int categoryId, int productId, int pharmaFormId)
        {
            DbCommand dbCmd = OtherElementsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetPSEOtherInteractions");

            // Add the parameters:
            OtherElementsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            OtherElementsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, divisionId);
            OtherElementsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, categoryId);
            OtherElementsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
            OtherElementsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pharmaFormId);

            List<OtherElementsInfo> BECollection = new List<OtherElementsInfo>();

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = OtherElementsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            return BECollection;
        }

        #endregion

        #region Protected Methods

        protected override OtherElementsInfo getFromDataReader(IDataReader current)
        {
            OtherElementsInfo record = new OtherElementsInfo();

            record.ElementId = Convert.ToInt32(current["ElementId"]);
            record.ElementName = current["ElementName"].ToString();
            record.Active = Convert.ToBoolean(current["Active"]);

            return record;
        }

        #endregion

        public static readonly OtherElementsDALC Instance = new OtherElementsDALC();

    }
}
