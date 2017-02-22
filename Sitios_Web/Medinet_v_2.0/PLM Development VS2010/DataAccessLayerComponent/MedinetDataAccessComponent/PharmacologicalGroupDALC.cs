using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedinetBusinessEntries;
using System.Data;
using System.Data.Common;
namespace MedinetDataAccessComponent
{
  public  class PharmacologicalGroupDALC : MedinetDataAccessAdapter<MedinetBusinessEntries.PharmacologicalGroupsInfo>
    {
        #region Construtors

        private PharmacologicalGroupDALC() { }

        #endregion

        #region Propierties
        public List<PharmacologicalGroupsInfo> getAllGroupswithOutProductInteractions(int categoryId, int pharmaFormId, int productId, int divisionId,string pharmaGroup)
        {
            List<PharmacologicalGroupsInfo> interactions = new List<PharmacologicalGroupsInfo>();

            DbCommand dbCmd = PharmacologicalGroupDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetPharmacologicalGroupsWithoutInteraction");
            PharmacologicalGroupDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, divisionId);
            PharmacologicalGroupDALC.MedInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, categoryId);
            PharmacologicalGroupDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
            PharmacologicalGroupDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pharmaFormId);
            PharmacologicalGroupDALC.MedInstanceDatabase.AddParameter(dbCmd, "@groupName", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pharmaGroup);
            using (IDataReader dataReader = PharmacologicalGroupDALC.MedInstanceDatabase.ExecuteReader(dbCmd)) 
            {
                while (dataReader.Read())
                {
                    PharmacologicalGroupsInfo record = new PharmacologicalGroupsInfo();
                    record.PharmaGroupId = Convert.ToInt32(dataReader["PharmaGroupId"]);
                    record.GroupName = dataReader["GroupName"].ToString();
                    interactions.Add(record);
                }

            }
            return interactions;
        }


        #endregion
        public static readonly PharmacologicalGroupDALC Instance = new PharmacologicalGroupDALC();
    }
}
