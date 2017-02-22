using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using MedinetBusinessEntries;
using PharmaSearchEngineBusinessEntries;

namespace PharmaSearchEngineDataAccessComponent
{
    public class PharmacologicalGroupsDALC : PharmaSearchEngineDataAccessAdapter<PharmacologicalGroupsInfo>
    {

        #region Constructors

        private PharmacologicalGroupsDALC() { }

        #endregion

        #region Public Methods

        public List<PharmacologicalGroupsInfo> getByProduct(int editionId, int divisionId, int categoryId, int productId, int pharmaFormId)
        {
            DbCommand dbCmd = PharmacologicalGroupsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetPSEPharmacologicalGroups");

            // Add the parameters:
            PharmacologicalGroupsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@editionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, editionId);
            PharmacologicalGroupsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, divisionId);
            PharmacologicalGroupsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, categoryId);
            PharmacologicalGroupsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
            PharmacologicalGroupsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pharmaFormId);

            List<PharmacologicalGroupsInfo> BECollection = new List<PharmacologicalGroupsInfo>();

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = PharmacologicalGroupsDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            return BECollection;
        }

        #endregion

        #region Protected Methods

        protected override PharmacologicalGroupsInfo getFromDataReader(IDataReader current)
        {
            PharmacologicalGroupsInfo record = new PharmacologicalGroupsInfo();

            record.PharmaGroupId = Convert.ToInt32(current["PharmaGroupId"]);
            record.GroupName = current["GroupName"].ToString();
            record.Active = Convert.ToBoolean(current["Active"]);
            
            return record;
        }

        #endregion

        public static readonly PharmacologicalGroupsDALC Instance = new PharmacologicalGroupsDALC();

    }
}
