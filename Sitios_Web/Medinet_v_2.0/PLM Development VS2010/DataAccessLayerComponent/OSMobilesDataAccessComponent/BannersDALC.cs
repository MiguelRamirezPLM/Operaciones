using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using PLMClientsBusinessEntities;


namespace OSMobilesDataAccessComponent
{
    public sealed class BannersDALC : OSMobilesDataAccessAdapter<BannerInfo>
    {
        #region Constructors

        private BannersDALC() { }

        #endregion

        #region Public Methods

        public List<BannerInfo> getBannerByDevice(string isbn, Catalogs.OSMobileDevices osMobileId)
        {
            DbCommand dbCmd = BannersDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetBannerInfo");

            List<BannerInfo> BECollection = new List<BannerInfo>();

            // Add the parameters:
            BannersDALC.InstanceDatabase.AddParameter(dbCmd, "@isbn", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, isbn);

            BannersDALC.InstanceDatabase.AddParameter(dbCmd, "@osMobileId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, (byte)osMobileId);

            // Get the result set from the stored procedure:  
            using (IDataReader dataReader = BannersDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                // Iterates through records:
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            return BECollection;

        }

        

        #endregion

        #region Protected Methods

        protected override BannerInfo getFromDataReader(IDataReader current)
        {
            BannerInfo record = new BannerInfo();

            record.BannerId = Convert.ToInt32(current["BannerId"]);
            record.BannerTypeId = Convert.ToByte(current["BannerTypeId"]);
            record.BannerDescription = current["BannerDescription"].ToString();
            record.FileName = current["FileName"].ToString();
            record.Active = Convert.ToBoolean(current["Active"]);
            record.BaseUrl = current["BaseUrl"].ToString();

            return record;

        }

        #endregion

        public static readonly BannersDALC Instance = new BannersDALC();
    }
}
