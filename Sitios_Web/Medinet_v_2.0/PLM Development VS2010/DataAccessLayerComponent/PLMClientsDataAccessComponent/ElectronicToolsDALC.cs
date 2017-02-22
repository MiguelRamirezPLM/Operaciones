using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using PLMClientsBusinessEntities;

namespace PLMClientsDataAccessComponent
{
    public sealed class ElectronicToolsDALC : PLMClientsDataAccessAdapter<ToolsByTargetInfo>
    {
        #region Constructors

        private ElectronicToolsDALC() { }

        #endregion

        #region Public Methods

        public List<ToolsByTargetInfo> getByTargetByPrefix(byte countryId, byte targetId, byte toolTypeId, int prefixId)
        {
            DbCommand dbCmd = ElectronicToolsDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetToolsByPrefix");

            // Add the parameters:
            ElectronicToolsDALC.InstanceDatabase.AddParameter(dbCmd, "@countryId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, countryId);
            ElectronicToolsDALC.InstanceDatabase.AddParameter(dbCmd, "@targetId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, targetId);
            ElectronicToolsDALC.InstanceDatabase.AddParameter(dbCmd, "@toolTypeId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, toolTypeId);
            ElectronicToolsDALC.InstanceDatabase.AddParameter(dbCmd, "@prefixId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, prefixId);

            List<ToolsByTargetInfo> BECollection = new List<ToolsByTargetInfo>();

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = ElectronicToolsDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                // Iterates through records:
                while (dataReader.Read())
                {
                    BECollection.Add(this.getFromDataReader(dataReader));
                }
            }

            return BECollection;
        }

        #endregion

        #region ProtectedMethods

        protected override ToolsByTargetInfo getFromDataReader(System.Data.IDataReader current)
        {
            ToolsByTargetInfo record = new ToolsByTargetInfo();

            record.CountryId = Convert.ToByte(current["CountryId"]);
            record.CompanyClientId = Convert.ToInt32(current["CompanyClientId"]);
            record.ToolId = Convert.ToInt32(current["ToolId"]);
            record.TargetId = Convert.ToByte(current["TargetId"]);
            record.ToolTitle = current["ToolTitle"].ToString();
            record.ToolDescription = current["ToolDescription"].ToString();
            record.ToolOrder = Convert.ToInt32(current["ToolOrder"]);

            if (current["FileName"] != System.DBNull.Value)
                record.FileName = current["FileName"].ToString();

            if (current["BaseUrl"] != System.DBNull.Value)
                record.BaseUrl = current["BaseUrl"].ToString();

            if (current["PublishedDate"] != System.DBNull.Value)
                record.PublishedDate = Convert.ToDateTime(current["PublishedDate"]);

            if (current["Url"] != System.DBNull.Value)
                record.Url = current["Url"].ToString();

            if (current["BannerId"] != System.DBNull.Value)
                record.BannerId = Convert.ToInt32(current["BannerId"]);

            if (current["BannerName"] != System.DBNull.Value)
                record.BannerName = current["BannerName"].ToString();

            if (current["BannerUrl"] != System.DBNull.Value)
                record.BannerUrl = current["BannerUrl"].ToString();

            return record;
        }

        #endregion

        public static readonly ElectronicToolsDALC Instance = new ElectronicToolsDALC();
    }
}
