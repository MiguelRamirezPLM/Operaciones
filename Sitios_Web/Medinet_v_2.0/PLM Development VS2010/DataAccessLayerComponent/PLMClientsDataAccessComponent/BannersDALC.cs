using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using PLMClientsBusinessEntities;

namespace PLMClientsDataAccessComponent
{
    public class BannersDALC : PLMClientsDataAccessAdapter<BannersByTargetInfo>
    {

        #region Constructors

        private BannersDALC() { }

        #endregion

        #region Public Methods

        public List<BannersByTargetInfo> getByTargetByPrefix(byte countryId, byte targetId, int prefixId)
        {
            DbCommand dbCmd = BannersDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetBannersByPrefix");

            // Add the parameters:
            BannersDALC.InstanceDatabase.AddParameter(dbCmd, "@countryId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, countryId);
            BannersDALC.InstanceDatabase.AddParameter(dbCmd, "@targetId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, targetId);
            BannersDALC.InstanceDatabase.AddParameter(dbCmd, "@prefixId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, prefixId);

            List<BannersByTargetInfo> BECollection = new List<BannersByTargetInfo>();

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = BannersDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                // Iterates through records:
                while (dataReader.Read())
                {
                    BECollection.Add(this.getFromDataReader(dataReader));
                }
            }

            return BECollection;
        }

        public List<BannerInfo> getBannerByTarget(string isbn, PLMClientsBusinessEntities.Catalogs.TargetOutputs targetId)
        {
            DbCommand dbCmd = BannersDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetBannerInfo");

            List<BannerInfo> BECollection = new List<BannerInfo>();

            // Add the parameters:
            BannersDALC.InstanceDatabase.AddParameter(dbCmd, "@isbn", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, isbn);
            BannersDALC.InstanceDatabase.AddParameter(dbCmd, "@targetId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, Convert.ToByte(targetId));

            // Get the result set from the stored procedure:  
            using (IDataReader dataReader = BannersDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                // Iterates through records:
                while (dataReader.Read())
                {
                    BannerInfo record = new BannerInfo();

                    record.BannerId = Convert.ToInt32(dataReader["ElectronicId"]);
                    record.BannerTypeId = Convert.ToByte(dataReader["InfoTypeId"]);
                    record.BannerDescription = dataReader["ElectronicDescription"].ToString();
                    record.FileName = dataReader["FileName"].ToString();
                    record.Active = Convert.ToBoolean(dataReader["Active"]);
                    record.BaseUrl = dataReader["BaseUrl"].ToString();

                    BECollection.Add(record);
                }
            }
            return BECollection;
        }

        #endregion

        #region Protected Methods

        protected override BannersByTargetInfo getFromDataReader(IDataReader current)
        {
            BannersByTargetInfo record = new BannersByTargetInfo();

            record.CountryId = Convert.ToByte(current["CountryId"]);
            record.CompanyClientId = Convert.ToInt32(current["CompanyClientId"]);
            record.BannerId = Convert.ToInt32(current["BannerId"]);
            record.TargetId = Convert.ToByte(current["TargetId"]);
            record.BannerDescription = current["BannerDescription"].ToString();
            record.FileName = current["FileName"].ToString();
            record.BaseUrl = current["BaseUrl"].ToString();
            record.BannerOrder = Convert.ToInt32(current["BannerOrder"]);
            record.Url = current["Url"].ToString();

            return record;
        }

        #endregion

        public static readonly BannersDALC Instance = new BannersDALC();

    }
}
