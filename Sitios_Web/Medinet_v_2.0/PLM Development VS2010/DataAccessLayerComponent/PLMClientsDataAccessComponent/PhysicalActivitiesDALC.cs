using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using PLMClientsBusinessEntities;

namespace PLMClientsDataAccessComponent
{
    public class PhysicalActivitiesDALC : PLMClientsDataAccessAdapter<PhysicalActivitiesInfo>
    {

        #region Constructors

        private PhysicalActivitiesDALC() { }

        #endregion

        #region Public Methods

        public List<PLMClientsBusinessEntities.PhysicalActivityDetailInfo> getActivitiesByCalculatorResult(int calculatorId, int resultId)
        {
            DbCommand dbCmd = PhysicalActivitiesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetActivitiesByCalculatorResult");

            List<PLMClientsBusinessEntities.PhysicalActivityDetailInfo> BECollection = new List<PLMClientsBusinessEntities.PhysicalActivityDetailInfo>();

            // Add the parameters:
            PhysicalActivitiesDALC.InstanceDatabase.AddParameter(dbCmd, "@calculatorId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, calculatorId);
            PhysicalActivitiesDALC.InstanceDatabase.AddParameter(dbCmd, "@resultId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, resultId);

            // Get the result set from the stored procedure:  
            using (IDataReader dataReader = PhysicalActivitiesDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                // Iterates through records:
                while (dataReader.Read())
                {
                    PLMClientsBusinessEntities.PhysicalActivityDetailInfo record = new PLMClientsBusinessEntities.PhysicalActivityDetailInfo();

                    record.ActivityId = Convert.ToByte(dataReader["ActivityId"]);
                    record.ActivityName = dataReader["ActivityName"].ToString();

                    BECollection.Add(record);
                }
            }
            return BECollection;
        }

        #endregion

        public static readonly PhysicalActivitiesDALC Instance = new PhysicalActivitiesDALC();

    }
}
