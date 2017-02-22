using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedinetBusinessEntries;
using System.Data;
using System.Data.Common;
namespace MedinetDataAccessComponent
{
  public  class OthersElementsDALC:MedinetDataAccessAdapter<MedinetBusinessEntries.OtherElementsInfo>
    {
        #region Construtors

        private OthersElementsDALC() { }

        #endregion

        #region Propierties
        public List<OtherElementsInfo> getAllOthersElementswithOutProductInteractions(int categoryId, int pharmaFormId, int productId, int divisionId, string elementName)
        {
            List<OtherElementsInfo> interactions = new List<OtherElementsInfo>();

            DbCommand dbCmd = OthersElementsDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetOtherElemensWithoutInteractions");
            OthersElementsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@divisionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, divisionId);
            OthersElementsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@categoryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, categoryId);
            OthersElementsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@productId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, productId);
            OthersElementsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@pharmaFormId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pharmaFormId);
            OthersElementsDALC.MedInstanceDatabase.AddParameter(dbCmd, "@elementName", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, elementName);
            using (IDataReader dataReader = OthersElementsDALC.MedInstanceDatabase.ExecuteReader(dbCmd)) 
            {
                while (dataReader.Read())
                {
                    OtherElementsInfo record = new OtherElementsInfo();
                    record.ElementId = Convert.ToInt32(dataReader["ElementId"]);
                    record.ElementName = dataReader["ElementName"].ToString();
                    interactions.Add(record);
                }

            }
            return interactions;
        }


        #endregion
        public static readonly OthersElementsDALC Instance = new OthersElementsDALC();
    }
}
