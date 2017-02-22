using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using PLMClientsBusinessEntities;

namespace OSMobilesDataAccessComponent
{
    public class MenuesDALC : OSMobilesDataAccessAdapter<MobileMenuesInfo>
    {

        #region Constructors

        private MenuesDALC() { }

        #endregion

        #region Public Methods

        public List<MobileMenuesInfo> getByOSMobileByEdition(string isbn, byte osMobileId)
        {

            DbCommand dbCmd = MenuesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetMobileMenues");

            List<MobileMenuesInfo> BECollection = new List<MobileMenuesInfo>();

            // Add the parameters:
            MenuesDALC.InstanceDatabase.AddParameter(dbCmd, "@isbn", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, isbn);

            MenuesDALC.InstanceDatabase.AddParameter(dbCmd, "@osMobileId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, (byte)osMobileId);

            // Get the result set from the stored procedure:  
            using (IDataReader dataReader = MenuesDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                // Iterates through records:
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }
            return BECollection;
        }

        public List<CountryInfo> getMobileCountries(byte osMobileId)
        {
            DbCommand dbCmd = MenuesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetMobileCountries");

            List<CountryInfo> BECollection = new List<CountryInfo>();

            // Add the parameters:
            MenuesDALC.InstanceDatabase.AddParameter(dbCmd, "@osMobileId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, (byte)osMobileId);

            // Get the result set from the stored procedure:  
            using (IDataReader dataReader = MenuesDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                // Iterates through records:
                while (dataReader.Read())
                {
                    CountryInfo record = new CountryInfo();

                    record.CountryId = Convert.ToByte(dataReader["CountryId"]);
                    record.CountryName = dataReader["CountryName"].ToString();

                    if (dataReader["CountryCode"] != System.DBNull.Value)
                        record.CountryCode = Convert.ToByte(dataReader["CountryCode"]);

                    record.ID = dataReader["ID"].ToString();
                    record.Active = Convert.ToBoolean(dataReader["Active"]);

                    BECollection.Add(record);

                }
            }
            return BECollection;
        }

        #endregion

        #region Protected Methods

        protected override MobileMenuesInfo getFromDataReader(IDataReader current)
        {
            MobileMenuesInfo record = new MobileMenuesInfo();

            record.EditionId = Convert.ToInt32(current["EditionId"]);
            record.OSMobileId = Convert.ToByte(current["OSMobileId"]);
            record.MenuId = Convert.ToByte(current["MenuId"]);
            record.MenuName = current["MenuName"].ToString();
            record.ShortName = current["ShortName"].ToString();
            record.ImageName = current["ImageName"].ToString();
            record.BaseUrl = current["BaseUrl"].ToString();
            record.Order = Convert.ToByte(current["Order"]);

            return record;
        }


        #endregion

        public static readonly MenuesDALC Instance = new MenuesDALC();

    }
}
