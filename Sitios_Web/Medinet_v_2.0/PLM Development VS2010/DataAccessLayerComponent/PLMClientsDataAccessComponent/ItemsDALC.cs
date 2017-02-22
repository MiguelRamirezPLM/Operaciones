using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using PLMClientsBusinessEntities;

namespace PLMClientsDataAccessComponent
{
    public class ItemsDALC : PLMClientsDataAccessAdapter<ItemsInfo>
    {

        #region Constructors

        private ItemsDALC() { }

        #endregion

        #region Public Methods

        public List<PLMClientsBusinessEntities.ItemDetailInfo> getActivityItemsByCalculatorResult(int calculatorId, int resultId, byte activityId)
        {
            DbCommand dbCmd = ItemsDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetActivityItemsByCalculatorResult");

            List<PLMClientsBusinessEntities.ItemDetailInfo> BECollection = new List<PLMClientsBusinessEntities.ItemDetailInfo>();

            // Add the parameters:
            ItemsDALC.InstanceDatabase.AddParameter(dbCmd, "@calculatorId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, calculatorId);
            ItemsDALC.InstanceDatabase.AddParameter(dbCmd, "@resultId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, resultId);
            ItemsDALC.InstanceDatabase.AddParameter(dbCmd, "@activityId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, activityId);

            // Get the result set from the stored procedure:  
            using (IDataReader dataReader = ItemsDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                // Iterates through records:
                while (dataReader.Read())
                {
                    PLMClientsBusinessEntities.ItemDetailInfo record = new PLMClientsBusinessEntities.ItemDetailInfo();

                    record.ItemId = Convert.ToByte(dataReader["ItemId"]);
                    record.ItemName = dataReader["ItemName"].ToString();
                    record.Content = dataReader["Content"].ToString();
                    record.Active = Convert.ToBoolean(dataReader["Active"]);

                    BECollection.Add(record);
                }
            }
            return BECollection;
        }

        #endregion

        public static readonly ItemsDALC Instance = new ItemsDALC();

    }
}
