using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using PLMClientsBusinessEntities;

namespace PLMClientsDataAccessComponent
{
    public class PromotionsDALC : PLMClientsDataAccessAdapter<PromotionsInfo>
    {

        #region Constructors

        private PromotionsDALC() { }

        #endregion

        #region Public Methods

        public List<PLMClientsBusinessEntities.PromotionsInfo> getByCompanyClient(int companyClientId)
        {
            List<PLMClientsBusinessEntities.PromotionsInfo> BECollection = new List<PromotionsInfo>();

            DbCommand dbCmd = PromotionsDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetCompanyPromotions");

            // Add the parameters:
            PromotionsDALC.InstanceDatabase.AddParameter(dbCmd, "@companyClientId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, companyClientId);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = PromotionsDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                // Iterates through records:
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }
            return BECollection;
        }

        #endregion

        #region Protected Methods

        protected override PromotionsInfo getFromDataReader(IDataReader current)
        {
            PromotionsInfo record = new PromotionsInfo();

            record.PromotionId = Convert.ToInt32(current["PromotionId"]);
            record.PromotionName = current["PromotionName"].ToString();
            record.Active = Convert.ToBoolean(current["Active"]);

            return record;
        }

        #endregion


        public static readonly PromotionsDALC Instance = new PromotionsDALC();
    }
}
